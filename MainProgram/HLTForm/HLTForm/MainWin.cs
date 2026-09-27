using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLTStudio.Commons;
using HLTStudio.Dialogs;
using HLTStudio.Modules;

namespace HLTStudio
{
	public partial class MainWin : Form
	{
		#region ComboBox_Items

		public const string Cmb入力ファイル形式_Auto = "拡張子から判定";
		public const string Cmb入力ファイル形式_CSV = "全て CSV と見なす";
		public const string Cmb入力ファイル形式_TSV = "全て TSV と見なす";

		public const string Cmb入力文字コード_Auto = "自動";
		public const string Cmb入力文字コード_SJIS = "Shift_JIS (CP-932)";
		public const string Cmb入力文字コード_UTF8 = "UTF-8";

		public const string Cmbソート列_Unused = "(指定できません)";

		public const string Cmbソート型_文字列 = "文字列";
		public const string Cmbソート型_数値 = "数値";

		public const string Cmbソート順_昇順 = "昇順";
		public const string Cmbソート順_降順 = "降順";

		public const string Cmb出力ファイル形式_Auto = "自動 (入力に合わせる)";
		public const string Cmb出力ファイル形式_CSV = "CSV";
		public const string Cmb出力ファイル形式_TSV = "TSV";

		public const string Cmb出力文字コード_SJIS = "Shift_JIS (CP-932)";
		public const string Cmb出力文字コード_UTF8 = "UTF-8 (BOM あり)";
		public const string Cmb出力文字コード_UTF8_NoBOM = "UTF-8 (BOM なし)";

		public const string Cmb出力改行コード_CRLF = "CR-LF";
		public const string Cmb出力改行コード_LF = "LF";

		#endregion

		private enum OutputMode_e
		{
			UNSELECTED = 1,
			FILE,
			DIR,
		}

		private OutputMode_e OutputMode = OutputMode_e.UNSELECTED;

		public MainWin()
		{
			InitializeComponent();

			this.Lst入力CSV.Items.Clear();
			this.Lbl出力メッセージ.Text = "";

			#region ComboBox_Items

			this.Cmb入力ファイル形式.Items.Clear();
			this.Cmb入力ファイル形式.Items.Add(Cmb入力ファイル形式_Auto);
			this.Cmb入力ファイル形式.Items.Add(Cmb入力ファイル形式_CSV);
			this.Cmb入力ファイル形式.Items.Add(Cmb入力ファイル形式_TSV);
			this.Cmb入力ファイル形式.SelectedIndex = 0;

			this.Cmb入力文字コード.Items.Clear();
			this.Cmb入力文字コード.Items.Add(Cmb入力文字コード_Auto);
			this.Cmb入力文字コード.Items.Add(Cmb入力文字コード_SJIS);
			this.Cmb入力文字コード.Items.Add(Cmb入力文字コード_UTF8);
			this.Cmb入力文字コード.SelectedIndex = 0;

			this.Cmbソート列.Items.Clear();
			this.Cmbソート列.Items.Add(Cmbソート列_Unused);
			this.Cmbソート列.SelectedIndex = 0;

			this.Cmbソート型.Items.Clear();
			this.Cmbソート型.Items.Add(Cmbソート型_文字列);
			this.Cmbソート型.Items.Add(Cmbソート型_数値);
			this.Cmbソート型.SelectedIndex = 0;

			this.Cmbソート順.Items.Clear();
			this.Cmbソート順.Items.Add(Cmbソート順_昇順);
			this.Cmbソート順.Items.Add(Cmbソート順_降順);
			this.Cmbソート順.SelectedIndex = 0;

			this.Cmb出力ファイル形式.Items.Clear();
			this.Cmb出力ファイル形式.Items.Add(Cmb出力ファイル形式_Auto);
			this.Cmb出力ファイル形式.Items.Add(Cmb出力ファイル形式_CSV);
			this.Cmb出力ファイル形式.Items.Add(Cmb出力ファイル形式_TSV);
			this.Cmb出力ファイル形式.SelectedIndex = 0;

			this.Cmb出力文字コード.Items.Clear();
			this.Cmb出力文字コード.Items.Add(Cmb出力文字コード_SJIS);
			this.Cmb出力文字コード.Items.Add(Cmb出力文字コード_UTF8);
			this.Cmb出力文字コード.Items.Add(Cmb出力文字コード_UTF8_NoBOM);
			this.Cmb出力文字コード.SelectedIndex = 0;

			this.Cmb出力改行コード.Items.Clear();
			this.Cmb出力改行コード.Items.Add(Cmb出力改行コード_CRLF);
			this.Cmb出力改行コード.Items.Add(Cmb出力改行コード_LF);
			this.Cmb出力改行コード.SelectedIndex = 0;

			#endregion

			this.RefreshUI();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;
		}

		private void RefreshUI()
		{
			this.Lbl入力CSVCount.Text = $"( {this.Lst入力CSV.Items.Count} 件 )";

			bool canAddInputCSVFile = this.Lst入力CSV.Items.Count < Consts.INPUT_CSV_FILE_COUNT_MAX;
			bool canRemoveInputCSVFile = this.Lst入力CSV.SelectedIndex != -1;

			this.Btn入力CSV追加.Enabled = canAddInputCSVFile;
			this.Btn入力CSV削除.Enabled = canRemoveInputCSVFile;
			this.Btn入力CSV全て削除.Enabled = canRemoveInputCSVFile;

			bool canMoveInputCSVFile_Up =
				0 < this.Lst入力CSV.SelectedIndex;
			bool canMoveInputCSVFile_Down =
				this.Lst入力CSV.SelectedIndex != -1 &&
				this.Lst入力CSV.SelectedIndex < this.Lst入力CSV.Items.Count - 1;

			this.Btn入力CSV上へ.Enabled = canMoveInputCSVFile_Up;
			this.Btn入力CSV下へ.Enabled = canMoveInputCSVFile_Down;

			bool sortEnabled = this.Chkソートする.Checked;

			this.Grpソート.Enabled = sortEnabled;

			OutputMode_e currOutputMode =
				this.Rdo出力_結合.Checked ?
				OutputMode_e.FILE :
				OutputMode_e.DIR;

			if (currOutputMode != this.OutputMode)
			{
				switch (currOutputMode)
				{
					case OutputMode_e.FILE:
						this.Lbl出力先.Text = "出力ファイル：";
						this.Txt出力先.Text = string.Empty;
						break;

					case OutputMode_e.DIR:
						this.Lbl出力先.Text = "出力フォルダ：";
						this.Txt出力先.Text = string.Empty;
						break;

					default:
						throw null; // never
				}
				this.OutputMode = currOutputMode;
			}

			var statusMessage = this.GetStatusMessage();

			if (statusMessage.Item1)
			{
				this.Lbl出力メッセージ.ForeColor = Color.DarkRed;
				this.Lbl出力メッセージ.BackColor = Color.LightYellow;
			}
			else
			{
				this.Lbl出力メッセージ.ForeColor = Color.DarkBlue;
				this.Lbl出力メッセージ.BackColor = Color.LightCyan;
			}
			this.Lbl出力メッセージ.Text = statusMessage.Item2;
		}

		private (bool, string) GetStatusMessage()
		{
			if (this.Lst入力CSV.Items.Count == 0)
				return (true, "入力ファイルが指定されていません。");

			if (this.Txt出力先.Text == "")
				return (true, "出力先が指定されていません。");

			string trailer = string.Empty;

			if (
				this.OutputMode == OutputMode_e.FILE &&
				File.Exists(this.Txt出力先.Text)
				)
				trailer = " (ファイルを上書きします！)";

			return (false, "実行ボタンを押してください。" + trailer);
		}

		private void RefreshUI_ソート列()
		{
			try
			{
				new CSVProcessor()
				{
					InputFormat = (CSVProcessor.Format_e)(this.Cmb入力ファイル形式.SelectedIndex + 1),
					InputEncoding = (CSVProcessor.Encoding_e)(this.Cmb入力文字コード.SelectedIndex + 1),
					UseFirstRowAsHeader = this.Chk先頭行をヘッダ行とする.Checked,
					TrimCellWhitespace = this.Chk全セルTrim.Checked,
				}
				.RefreshSortColumns(
					this.Lst入力CSV.Items.Cast<string>().ToArray(),
					this.Cmbソート列,
					this.Cmb入力ファイル形式,
					this.Chk全セルTrim,
					this.Cmb入力文字コード_SelectedIndexChanged
					);
			}
			catch (Exception ex)
			{
				MessageDlg.Run(
					MessageDlg.Kind_e.Error,
					Consts.APP_TITLE,
					"ソート列の読み込み中にエラーが発生しました。",
					ex,
					new[] { "OK" }
					);
			}
		}

		private void Lst入力CSV_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshUI();
		}

		private void Lst入力CSV_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void Lst入力CSV_DragDrop(object sender, DragEventArgs e)
		{
			var files = e.Data?.GetData(DataFormats.FileDrop) as string[];

			if (files == null)
				return;

			this.AddInputCSVFiles(files);

			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void AddInputCSVFiles(IEnumerable<string> files)
		{
			var autoFileType = this.Cmb入力ファイル形式.Text == Cmb入力ファイル形式_Auto;

			foreach (string file in files)
			{
				if (Consts.INPUT_CSV_FILE_COUNT_MAX <= this.Lst入力CSV.Items.Count)
					break;

				if (!File.Exists(file))
					continue;

				string extension = Path.GetExtension(file);

				if (
					autoFileType &&
					!string.Equals(extension, Consts.FILE_EXT_CSV, StringComparison.OrdinalIgnoreCase) &&
					!string.Equals(extension, Consts.FILE_EXT_TSV, StringComparison.OrdinalIgnoreCase)
					)
					continue;

				this.Lst入力CSV.Items.Add(Path.GetFullPath(file));
			}
		}

		private void Btn入力CSV上へ_Click(object sender, EventArgs e)
		{
			int index = this.Lst入力CSV.SelectedIndex;

			if (index <= 0)
				return;

			var item = this.Lst入力CSV.Items[index];
			this.Lst入力CSV.Items[index] = this.Lst入力CSV.Items[index - 1];
			this.Lst入力CSV.Items[index - 1] = item;
			this.Lst入力CSV.SelectedIndex = index - 1;

			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Btn入力CSV下へ_Click(object sender, EventArgs e)
		{
			int index = this.Lst入力CSV.SelectedIndex;

			if (index < 0 || this.Lst入力CSV.Items.Count - 1 <= index)
				return;

			var item = this.Lst入力CSV.Items[index];
			this.Lst入力CSV.Items[index] = this.Lst入力CSV.Items[index + 1];
			this.Lst入力CSV.Items[index + 1] = item;
			this.Lst入力CSV.SelectedIndex = index + 1;

			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Cmb入力文字コード_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Btn入力CSV追加_Click(object sender, EventArgs e)
		{
			var autoFileType = this.Cmb入力ファイル形式.Text == Cmb入力ファイル形式_Auto;

			using (var dlg = new OpenFileDialog())
			{
				dlg.Multiselect = true;
				dlg.Filter = autoFileType ? $"CSV / TSV ファイル (*{Consts.FILE_EXT_CSV};*{Consts.FILE_EXT_TSV})|*{Consts.FILE_EXT_CSV};*{Consts.FILE_EXT_TSV}" : "全てのファイル (*.*)|*.*";

				if (dlg.ShowDialog(this) == DialogResult.OK)
					this.AddInputCSVFiles(dlg.FileNames);
			}

			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Btn入力CSV削除_Click(object sender, EventArgs e)
		{
			int index = this.Lst入力CSV.SelectedIndex;

			if (index != -1)
			{
				this.Lst入力CSV.Items.RemoveAt(index);
				this.Lst入力CSV.SelectedIndex = Math.Min(index, this.Lst入力CSV.Items.Count - 1);
			}

			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Btn入力CSV全て削除_Click(object sender, EventArgs e)
		{
			MessageDlg.Run(
				MessageDlg.Kind_e.Information,
				$"{Consts.APP_TITLE} / 確認",
				"入力CSV一覧をクリアします。",
				null,
				new string[] { "OK", "キャンセル" }
				);

			this.Lst入力CSV.Items.Clear();

			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Chk先頭行をヘッダ行とする_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUI();
			this.RefreshUI_ソート列();
		}

		private void Chk全セルTrim_CheckedChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Chk空行削除_CheckedChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Chk重複行を削除_CheckedChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Chkソートする_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUI();
		}

		private void Cmbソート列_SelectedIndexChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Cmbソート型_SelectedIndexChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Cmbソート順_SelectedIndexChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Rdo出力_結合_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUI();
		}

		private void Rdo出力_別々_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUI();
		}

		private void Cmb出力_ファイル形式_SelectedIndexChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Cmb出力_文字コード_SelectedIndexChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Cmb出力_改行コード_SelectedIndexChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Txt出力先_TextChanged(object sender, EventArgs e)
		{
			// none
		}

		private void Txt出力先_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // Ctrl_A
			{
				this.Txt出力先.SelectAll();
			}
		}

		private void Txt出力先_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void Txt出力先_DragDrop(object sender, DragEventArgs e)
		{
			var paths = e.Data?.GetData(DataFormats.FileDrop) as string[];

			if (paths == null)
				return;

			foreach (string path in paths)
			{
				if (Directory.Exists(path))
					this.Rdo出力_別々.Checked = true;
				else if (File.Exists(path))
					this.Rdo出力_結合.Checked = true;
				else
					continue;

				this.RefreshUI();

				this.Txt出力先.Text = Path.GetFullPath(path);
				break;
			}
			this.RefreshUI();
		}

		private void Btn出力先_Click(object sender, EventArgs e)
		{
			switch (this.OutputMode)
			{
				case OutputMode_e.DIR:
					using (var dlg = new FolderBrowserDialog())
					{
						dlg.Description = "出力フォルダを選択してください。";

						if (dlg.ShowDialog(this) == DialogResult.OK)
							this.Txt出力先.Text = SCommon.ToFullPath(dlg.SelectedPath);
					}
					break;

				case OutputMode_e.FILE:
					using (var dlg = new SaveFileDialog())
					{
						bool tsv = this.Cmb出力ファイル形式.SelectedItem as string == Cmb出力ファイル形式_TSV;

						dlg.Filter = tsv ?
							$"TSV ファイル (*{Consts.FILE_EXT_TSV})|*{Consts.FILE_EXT_TSV}|全てのファイル (*.*)|*.*" :
							$"CSV ファイル (*{Consts.FILE_EXT_CSV})|*{Consts.FILE_EXT_CSV}|全てのファイル (*.*)|*.*";

						dlg.DefaultExt = (tsv ? Consts.FILE_EXT_TSV : Consts.FILE_EXT_CSV).TrimStart('.');

						if (dlg.ShowDialog(this) == DialogResult.OK)
							this.Txt出力先.Text = SCommon.ToFullPath(dlg.FileName);
					}
					break;

				default:
					throw null; // never
			}
			this.RefreshUI();
		}

		private void Btn実行_Click(object sender, EventArgs e)
		{
			try
			{
				string logFile = ProcMain.SelfFile + ".log";
				string errorLogFile = ProcMain.SelfFile + ".error.log";

				SCommon.DeletePath(logFile);
				SCommon.DeletePath(errorLogFile);

				new CSVProcessor()
				{
					InputFormat = (CSVProcessor.Format_e)(this.Cmb入力ファイル形式.SelectedIndex + 1),
					InputEncoding = (CSVProcessor.Encoding_e)(this.Cmb入力文字コード.SelectedIndex + 1),
					OutputFormat = (CSVProcessor.Format_e)(this.Cmb出力ファイル形式.SelectedIndex + 1),
					OutputEncoding = (CSVProcessor.Encoding_e)(this.Cmb出力文字コード.SelectedIndex + 2),
					OutputNewLine = this.Cmb出力改行コード.Text == Cmb出力改行コード_LF ? Consts.NEW_LINE_LF : Consts.NEW_LINE_CRLF,
					MergeFiles = this.OutputMode == OutputMode_e.FILE,
					UseFirstRowAsHeader = this.Chk先頭行をヘッダ行とする.Checked,
					TrimCellWhitespace = this.Chk全セルTrim.Checked,
					RemoveEmptyRows = this.Chk空行削除.Checked,
					RemoveDuplicateRows = this.Chk重複行を削除.Checked,
					SortRows = this.Chkソートする.Checked,
					SortOption = new CSVProcessor.SortOption_t
					{
						KeyColumnIndex = this.Cmbソート列.Enabled ? this.Cmbソート列.SelectedIndex : -1,
						KeyColumnName = this.Cmbソート列.SelectedItem as string,
						Mode = (CSVProcessor.SortOption_t.Mode_e)(this.Cmbソート型.SelectedIndex + 1),
						Ascending = this.Cmbソート順.Text == Cmbソート順_昇順,
					},
				}
				.RunWithDialog(
					this.Lst入力CSV.Items.Cast<string>().ToArray(),
					this.Txt出力先.Text,
					logFile,
					errorLogFile,
					this
					);
			}
			catch (Exception ex)
			{
				MessageDlg.Run(MessageDlg.Kind_e.Error, Consts.APP_TITLE, "処理に失敗しました。", ex.Message, new[] { "OK" });
			}
		}
	}
}
