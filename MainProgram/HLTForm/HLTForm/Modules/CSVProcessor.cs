using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLTStudio.Dialogs;
using HLTStudio.Tools;

namespace HLTStudio.Modules
{
	public class CSVProcessor
	{
		public enum Encoding_e
		{
			AUTO = 1,
			SJIS,
			UTF8,
			UTF8_NoBOM
		}

		public enum Format_e
		{
			AUTO = 1,
			CSV,
			TSV
		}

		public class SortOption_t
		{
			public enum Mode_e
			{
				STRING = 1,
				NUMERIC
			}

			public int KeyColumnIndex;
			public string KeyColumnName;
			public Mode_e Mode;
			public bool Ascending;
		}

		public bool PrecheckMode;
		public bool UseFirstRowAsHeader;
		public bool TrimCellWhitespace;
		public bool RemoveEmptyRows;
		public bool RemoveDuplicateRows;
		public bool SortRows;

		public SortOption_t SortOption;

		public Format_e InputFormat = Format_e.AUTO;
		public Format_e OutputFormat = Format_e.AUTO;
		public Encoding_e InputEncoding = Encoding_e.AUTO;
		public Encoding_e OutputEncoding = Encoding_e.SJIS;
		public string OutputNewLine = Consts.NEW_LINE_CRLF;
		public bool MergeFiles = true;

		private class Input_t
		{
			public string File;
			public char Delimiter;
			public string[] Header;
			public int Columns;
			public long Length;
			public DateTime LastWriteTime;
		}

		private class Output_t
		{
			public string File;
			public char Delimiter;
			public Input_t[] Inputs;
			public string[] Header;
			public string TemporaryFile;
			public string BackupFile;
			public bool Committed;
		}

		public class Result_t
		{
			public bool Success;
			public bool PrecheckFailed;
			public bool Cancelled;
			public string ErrorMessage;
		}

		private Encoding GetOutputEncoding()
		{
			switch (this.OutputEncoding)
			{
				case Encoding_e.SJIS:
					return Encoding.GetEncoding(932, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

				case Encoding_e.UTF8:
					return new UTF8Encoding(true, true);

				case Encoding_e.UTF8_NoBOM:
					return new UTF8Encoding(false, true);

				default:
					throw new InvalidDataException("出力文字コードを指定してください。");
			}
		}

		private char GetDelimiter(string file)
		{
			if (this.InputFormat == Format_e.CSV)
				return ',';

			if (this.InputFormat == Format_e.TSV)
				return '\t';

			if (this.InputFormat != Format_e.AUTO)
				throw new InvalidDataException("入力ファイル形式を指定してください。");

			string extension = Path.GetExtension(file);

			if (string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase))
				return ',';

			if (string.Equals(extension, ".tsv", StringComparison.OrdinalIgnoreCase))
				return '\t';

			throw new InvalidDataException("拡張子から CSV / TSV を判定できません。");
		}

		private CsvFileReader OpenReader(string file, char delimiter)
		{
			switch (this.InputEncoding)
			{
				// 既存の自動判定に合わせ、UTF-8 BOM がなければ CP932 とする。
				case Encoding_e.AUTO:
					return new CsvFileReader(file, delimiter);

				case Encoding_e.SJIS:
					return new CsvFileReader(file, Encoding.GetEncoding(932), delimiter);

				case Encoding_e.UTF8:
				case Encoding_e.UTF8_NoBOM:
					return new CsvFileReader(file, new UTF8Encoding(false, true), delimiter);

				default:
					throw new InvalidDataException("入力文字コードを指定してください。");
			}
		}

		private string[] PrepareRow(string[] row)
		{
			return this.TrimCellWhitespace ? row.Select(cell => cell.Trim()).ToArray() : row;
		}

		private string[] ReadHeader(CsvFileReader reader)
		{
			string[] header = reader.ReadRow();

			if (header == null)
				throw new InvalidDataException("ヘッダー行がありません。");

			if (header.Any(cell => cell.Any(char.IsControl)))
				throw new InvalidDataException("ヘッダーに制御文字が含まれています。");

			header = this.PrepareRow(header);

			if (header.Any(string.IsNullOrWhiteSpace))
				throw new InvalidDataException("空のヘッダー名があります。");

			if (header.Distinct(StringComparer.Ordinal).Count() != header.Length)
				throw new InvalidDataException("ヘッダー名が重複しています。");

			return header;
		}

		private int GetSortIndex(string[] header)
		{
			if (!this.SortRows)
				return -1;

			if (
				this.SortOption == null ||
				this.SortOption.KeyColumnIndex < 0 ||
				(this.SortOption.Mode != SortOption_t.Mode_e.STRING && this.SortOption.Mode != SortOption_t.Mode_e.NUMERIC)
				)
				throw new InvalidDataException("ソート列とデータ型を指定してください。");

			if (header == null)
				return this.SortOption.KeyColumnIndex;

			string name = this.SortOption.KeyColumnName;

			if (this.TrimCellWhitespace && name != null)
				name = name.Trim();

			int index = name == null ? this.SortOption.KeyColumnIndex : Array.IndexOf(header, name);
			return index;
		}

		private static string GetCell(string[] row, int index)
		{
			return 0 <= index && index < row.Length ? row[index] : string.Empty;
		}

		private static decimal? GetNumber(string value)
		{
			// 欠けた列と空セルは同じ空値として扱い、昇順では数値より前に並べる。
			if (value.Length == 0)
				return null;

			decimal number;

			if (!decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
				throw new InvalidDataException("ソート列に数値として扱えない値があります。小数点は . を使用してください。");

			return number;
		}

		private bool SkipRow(string[] row)
		{
			return this.RemoveEmptyRows && row.All(cell => cell.Length == 0);
		}

		private Input_t Inspect(string file)
		{
			Input_t input = new Input_t
			{
				File = Path.GetFullPath(file),
				Delimiter = this.GetDelimiter(file)
			};

			FileInfo stamp = new FileInfo(input.File);

			input.Length = stamp.Length;
			input.LastWriteTime = stamp.LastWriteTimeUtc;

			Encoding outputEncoding = this.GetOutputEncoding();
			long record = 0;

			try
			{
				using (CsvFileReader reader = this.OpenReader(input.File, input.Delimiter))
				{
					if (this.UseFirstRowAsHeader)
					{
						record++;

						input.Header = this.ReadHeader(reader);
						input.Columns = input.Header.Length;

						foreach (string cell in input.Header)
							outputEncoding.GetByteCount(cell);
					}

					int sortIndex = this.GetSortIndex(input.Header);

					for (; ; )
					{
						record++;

						string[] row = reader.ReadRow();

						if (row == null)
							break;

						if (input.Header == null)
							input.Columns = Math.Max(input.Columns, row.Length);

						row = this.PrepareRow(row);

						if (this.SkipRow(row))
							continue;

						if (input.Header != null && row.Length > input.Header.Length)
							throw new InvalidDataException("ヘッダーにない列のデータがあります。");

						if (this.SortRows && this.SortOption.Mode == SortOption_t.Mode_e.NUMERIC)
							GetNumber(GetCell(row, sortIndex));

						foreach (string cell in row)
							outputEncoding.GetByteCount(cell);
					}
				}
			}
			catch (Exception ex)
			{
				throw new InvalidDataException($"レコード {Math.Max(1, record)}: {DescribeError(ex)}", ex);
			}

			return input;
		}

		public void Check(string inputFile)
		{
			this.Inspect(inputFile);
		}

		public void RefreshSortColumns(string[] files, ComboBox columns, ComboBox inputFormat, CheckBox trim, EventHandler refresh)
		{
			// 入力形式・空白削除の変更にも追従する。イベントの重複登録を防ぐ。
			inputFormat.SelectedIndexChanged -= refresh;
			inputFormat.SelectedIndexChanged += refresh;

			trim.CheckedChanged -= refresh;
			trim.CheckedChanged += refresh;

			string selected = columns.SelectedItem as string;
			int previousIndex = columns.SelectedIndex;
			List<string> names = new List<string>();
			HashSet<string> seen = new HashSet<string>(StringComparer.Ordinal);
			int maxColumns = 0;
			List<string> errors = new List<string>();

			foreach (string file in files)
			{
				try
				{
					using (CsvFileReader reader = this.OpenReader(file, this.GetDelimiter(file)))
					{
						if (this.UseFirstRowAsHeader)
						{
							foreach (string name in this.ReadHeader(reader))
								if (seen.Add(name))
									names.Add(name);
						}
						else
						{
							int fileColumns = 0;

							string[] row;

							while ((row = reader.ReadRow()) != null)
								fileColumns = Math.Max(fileColumns, row.Length);

							maxColumns = Math.Max(maxColumns, fileColumns);
						}
					}
				}
				catch (Exception ex)
				{
					errors.Add(file + " : " + DescribeError(ex));
				}
			}

			if (!this.UseFirstRowAsHeader)
				for (int index = 0; index < maxColumns; index++)
					names.Add($"{index + 1} 列目");

			bool available = names.Count != 0;

			if (!available)
				names.Add(MainWin.Cmbソート列_Unused);

			columns.BeginUpdate();

			try
			{
				columns.Items.Clear();
				columns.Items.AddRange(names.ToArray());

				int index = selected == null ? -1 : names.IndexOf(selected);

				if (index < 0 && this.TrimCellWhitespace && selected != null)
					index = names.IndexOf(selected.Trim());

				columns.SelectedIndex = index >= 0 ? index : Math.Max(0, Math.Min(previousIndex, names.Count - 1));
				columns.Enabled = available;
			}
			finally
			{
				columns.EndUpdate();
			}

			if (errors.Count != 0)
				throw new InvalidDataException(string.Join(Consts.NEW_LINE_CRLF, errors));
		}

		private static string DescribeError(Exception ex)
		{
			if (ex is DecoderFallbackException)
				return "指定した入力文字コードで読み込めません。入力文字コードを確認してください。";

			if (ex is EncoderFallbackException)
				return "出力文字コードで表現できない文字があります。";

			string message = ex.Message
				.Replace('\r', ' ')
				.Replace('\n', ' ');

			const int MESSAGE_LIMIT = 300;

			return message.Length > MESSAGE_LIMIT ?
				message.Substring(0, MESSAGE_LIMIT) + "..." :
				message;
		}

		private List<Input_t> Precheck(string[] files, List<string> errors)
		{
			List<Input_t> inputs = new List<Input_t>();
			HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			if (files == null || files.Length == 0 || files.Length > Consts.INPUT_CSV_FILE_COUNT_MAX)
			{
				errors.Add($"入力ファイルは 1 ～ {Consts.INPUT_CSV_FILE_COUNT_MAX} 件で指定してください。");

				return inputs;
			}

			foreach (string file in files)
			{
				try
				{
					if (!seen.Add(Path.GetFullPath(file)))
						throw new InvalidDataException("入力ファイルが重複しています。");

					Input_t input = this.Inspect(file);

					inputs.Add(input);
				}
				catch (Exception ex)
				{
					errors.Add(file + " : " + DescribeError(ex));
				}
			}
			return inputs;
		}

		private List<Output_t> PlanOutputs(List<Input_t> inputs, string destination)
		{
			if (string.IsNullOrWhiteSpace(destination))
				throw new InvalidDataException("出力先が指定されていません。");

			if (
				this.OutputNewLine != Consts.NEW_LINE_CRLF &&
				this.OutputNewLine != Consts.NEW_LINE_LF
				)
				throw new InvalidDataException("出力改行コードが不正です。");

			if (!Enum.IsDefined(typeof(Format_e), this.OutputFormat))
				throw new InvalidDataException("出力形式が不正です。");

			List<Output_t> outputs = new List<Output_t>();

			foreach (Input_t input in inputs)
			{
				char delimiter =
					this.OutputFormat == Format_e.AUTO ?
					input.Delimiter :
					this.OutputFormat == Format_e.CSV ? ',' : '\t';

				string file =
					this.MergeFiles ?
					destination :
					Path.Combine(destination, Path.GetFileNameWithoutExtension(input.File) + (delimiter == ',' ? ".csv" : ".tsv"));

				outputs.Add(new Output_t
				{
					File = Path.GetFullPath(file),
					Delimiter = delimiter,
					Inputs = this.MergeFiles ? inputs.ToArray() : new[] { input }
				});

				// 結合時の自動出力形式は、一覧の最初の入力に合わせる。
				if (this.MergeFiles)
					break;
			}

			HashSet<string> names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (Output_t output in outputs)
			{
				if (this.UseFirstRowAsHeader)
				{
					List<string> header = new List<string>();
					HashSet<string> seen = new HashSet<string>(StringComparer.Ordinal);

					// 入力一覧・各ヘッダーの順に、新しく登場した列を末尾へ追加する。
					foreach (Input_t input in output.Inputs)
						foreach (string name in input.Header)
							if (seen.Add(name))
								header.Add(name);

					output.Header = header.ToArray();
				}
				if (!names.Add(output.File))
					throw new InvalidDataException("個別出力のファイル名が重複します: " + output.File);

				if (!Directory.Exists(Path.GetDirectoryName(output.File)))
					throw new DirectoryNotFoundException("出力フォルダが存在しません: " + output.File);

				if (Directory.Exists(output.File))
					throw new InvalidDataException("出力ファイルと同名のフォルダがあります: " + output.File);

				if (File.Exists(output.File))
				{
					using (new FileStream(output.File, FileMode.Open, FileAccess.Write, FileShare.None))
					{ }
				}
			}
			return outputs;
		}

		private IEnumerable<string[]> ReadRows(Output_t output)
		{
			string[] targetHeader = output.Header;

			foreach (Input_t input in output.Inputs)
			{
				FileInfo stamp = new FileInfo(input.File);

				if (stamp.Length != input.Length || stamp.LastWriteTimeUtc != input.LastWriteTime)
					throw new IOException("事前検査後に入力ファイルが変更されました: " + input.File);

				using (CsvFileReader reader = this.OpenReader(input.File, input.Delimiter))
				{
					int[] map = null;

					if (input.Header != null)
					{
						string[] header = this.ReadHeader(reader);

						if (!header.SequenceEqual(input.Header))
							throw new IOException("事前検査後にヘッダーが変更されました: " + input.File);

						map = targetHeader.Select(name => Array.IndexOf(header, name)).ToArray();
					}

					string[] row;

					while ((row = reader.ReadRow()) != null)
					{
						row = this.PrepareRow(row);

						if (this.SkipRow(row))
							continue;

						if (map != null)
						{
							if (row.Length > input.Header.Length)
								throw new InvalidDataException("データの列数が変わりました: " + input.File);

							string[] original = row;

							row = map.Select(index => GetCell(original, index)).ToArray();
						}

						yield return row;
					}
				}
			}
		}

		private class RowComparer : IEqualityComparer<string[]>
		{
			public bool Equals(string[] x, string[] y)
			{
				return x.SequenceEqual(y, StringComparer.Ordinal);
			}

			public int GetHashCode(string[] row)
			{
				unchecked
				{
					int hash = 17;

					foreach (string cell in row)
						hash = hash * 31 + StringComparer.Ordinal.GetHashCode(cell);

					return hash;
				}
			}
		}

		private long WriteOutput(Output_t output)
		{
			IEnumerable<string[]> rows = this.ReadRows(output);

			// ソートなしは逐次処理。重複削除を選択した場合のみ既出の行を保持する。
			if (this.RemoveDuplicateRows)
				rows = rows.Distinct(new RowComparer());

			if (this.SortRows)
			{
				int index = this.GetSortIndex(output.Header);

				if (this.SortOption.Mode == SortOption_t.Mode_e.NUMERIC)
					rows = this.SortOption.Ascending ?
						rows.OrderBy(row => GetNumber(GetCell(row, index))) :
						rows.OrderByDescending(row => GetNumber(GetCell(row, index)));
				else
					rows = this.SortOption.Ascending ?
						rows.OrderBy(row => GetCell(row, index), StringComparer.Ordinal) :
						rows.OrderByDescending(row => GetCell(row, index), StringComparer.Ordinal);
			}

			long count = 0;

			using (CsvFileWriter writer = new CsvFileWriter(output.TemporaryFile, false, this.GetOutputEncoding(), output.Delimiter))
			{
				writer.NewLine = this.OutputNewLine;

				if (output.Header != null)
					writer.WriteRow(output.Header);

				foreach (string[] row in rows)
				{
					writer.WriteRow(row);
					count++;
				}
			}

			return count;
		}

		private void Publish(List<Output_t> outputs, Action<string> log)
		{
			try
			{
				foreach (Output_t output in outputs)
				{
					output.TemporaryFile = Path.Combine(Path.GetDirectoryName(output.File), ".csvbulk-" + Guid.NewGuid().ToString("N") + ".tmp");

					using (new FileStream(output.TemporaryFile, FileMode.CreateNew, FileAccess.Write, FileShare.None))
					{ }

					long count = this.WriteOutput(output);

					log($"出力準備: {output.File} ({count} 行)");
				}

				foreach (Output_t output in outputs)
				{
					if (File.Exists(output.File))
					{
						output.BackupFile = output.TemporaryFile + ".bak";

						File.Replace(output.TemporaryFile, output.File, output.BackupFile);
					}
					else
						File.Move(output.TemporaryFile, output.File);

					output.Committed = true;
				}
			}
			catch (Exception ex)
			{
				List<string> rollbackErrors = new List<string>();

				foreach (Output_t output in outputs.AsEnumerable().Reverse().Where(item => item.Committed))
				{
					try
					{
						if (output.BackupFile != null)
							File.Replace(output.BackupFile, output.File, null);
						else
							File.Delete(output.File);
					}
					catch (Exception restoreError)
					{
						rollbackErrors.Add($"復元失敗: {output.File} / バックアップ: {output.BackupFile} / {DescribeError(restoreError)}");
					}
				}

				if (rollbackErrors.Count != 0)
					throw new IOException(DescribeError(ex) + Consts.NEW_LINE_CRLF + string.Join(Consts.NEW_LINE_CRLF, rollbackErrors), ex);

				throw;
			}
			finally
			{
				foreach (Output_t output in outputs)
				{
					if (output.TemporaryFile != null && File.Exists(output.TemporaryFile))
					{
						try
						{
							File.Delete(output.TemporaryFile);
						}
						catch (Exception ex)
						{
							log("一時ファイル削除失敗: " + output.TemporaryFile + " / " + DescribeError(ex));
						}
					}
				}
			}

			foreach (Output_t output in outputs)
			{
				if (output.BackupFile != null)
				{
					try
					{
						File.Delete(output.BackupFile);
					}
					catch (Exception ex)
					{
						log("バックアップ削除失敗: " + output.BackupFile + " / " + DescribeError(ex));
					}
				}
				log("出力完了: " + output.File);
			}
		}

		private Result_t RunCore(string[] files, string destination, Action<string> log, string[] reservedPaths, Func<int, bool> confirmOverwrite = null)
		{
			List<string> errors = new List<string>();
			List<Input_t> inputs = this.Precheck(files, errors);
			List<Output_t> outputs = null;

			try
			{
				outputs = this.PlanOutputs(inputs, destination);

				if (outputs.Any(output => reservedPaths.Any(path => string.Equals(output.File, Path.GetFullPath(path), StringComparison.OrdinalIgnoreCase))))
					throw new InvalidDataException("出力先にログファイルは指定できません。");
			}
			catch (Exception ex)
			{
				errors.Add("出力先 : " + DescribeError(ex));
			}

			if (errors.Count != 0)
				return new Result_t
				{
					PrecheckFailed = true,
					ErrorMessage = string.Join(Consts.NEW_LINE_CRLF, errors)
				};

			log($"事前検査完了: {inputs.Count} ファイル");

			if (!this.PrecheckMode)
			{
				int overwriteCount = outputs.Count(output => File.Exists(output.File));

				if (overwriteCount > 0 && confirmOverwrite != null && !confirmOverwrite(overwriteCount))
					return new Result_t { Cancelled = true };

				this.Publish(outputs, log);
			}
			return new Result_t { Success = true };
		}

		public void RunOne(string inputFile, string outputFile)
		{
			bool merge = this.MergeFiles;
			try
			{
				this.MergeFiles = true;

				Result_t result = this.RunCore(new[] { inputFile }, outputFile, message => { }, new string[0]);

				if (!result.Success)
					throw new InvalidDataException(result.ErrorMessage);
			}
			finally
			{
				this.MergeFiles = merge;
			}
		}

		public Result_t Run(string[] files, string destination, string logFile, string errorLogFile, Func<int, bool> confirmOverwrite = null)
		{
			using (StreamWriter log = new StreamWriter(logFile, false, new UTF8Encoding(true)))
			using (StreamWriter errorLog = new StreamWriter(errorLogFile, false, new UTF8Encoding(true)))
			{
				log.AutoFlush = true;
				errorLog.AutoFlush = true;

				Result_t result;
				try
				{
					log.WriteLine("処理開始: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

					result = this.RunCore(files, destination, log.WriteLine, new[] { logFile, errorLogFile }, confirmOverwrite);
				}
				catch (Exception ex)
				{
					result = new Result_t { ErrorMessage = ex.Message };
				}

				if (!result.Success && !result.Cancelled)
					errorLog.WriteLine(result.ErrorMessage);

				log.WriteLine(result.Success ? "処理が完了しました。" : "処理を中止しました。");

				return result;
			}
		}

		public void RunWithDialog(string[] files, string destination, string logFile, string errorLogFile, MainWin mainWin)
		{
			Result_t result = null;
			System.Threading.SynchronizationContext context = System.Threading.SynchronizationContext.Current;

			bool wasVisible = mainWin.Visible;

			try
			{
				mainWin.Visible = false;

				ProcessingDlg.Run(
					"入力ファイルを検査・加工しています。",
					() => result = this.Run(
						files,
						destination,
						logFile,
						errorLogFile,
						count =>
						{
							bool overwrite = false;

							context.Send(
								state =>
								{
									overwrite = MessageDlg.Run(
										MessageDlg.Kind_e.Warning,
										Consts.APP_TITLE,
										$"既存の {count} ファイルを上書きします。続行しますか？",
										null,
										new[] { "はい", "いいえ" }
										) == 1;
								},
								null
								);

							return overwrite;
						}
						)
					);

				if (result.PrecheckFailed)
				{
					ErrorLogDlg.Run(result.ErrorMessage.Split(new[] { Consts.NEW_LINE_CRLF }, StringSplitOptions.None));
					return;
				}
			}
			finally
			{
				mainWin.Visible = wasVisible;
			}

			if (result.Cancelled)
			{
				MessageDlg.Run(MessageDlg.Kind_e.Information, Consts.APP_TITLE, "処理を中止しました。", null, new[] { "OK" });
				return;
			}

			MessageDlg.Run(
				result.Success ? MessageDlg.Kind_e.Complete : MessageDlg.Kind_e.Error,
				Consts.APP_TITLE,
				result.Success ? "処理が完了しました。" : "処理に失敗しました。",
				result.Success ? logFile : result.ErrorMessage + Consts.NEW_LINE_CRLF + errorLogFile,
				new[] { "OK" }
				);
		}
	}
}
