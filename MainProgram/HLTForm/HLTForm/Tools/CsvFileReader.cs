using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HLTStudio.Commons;

namespace HLTStudio.Tools
{
	public class CsvFileReader : IDisposable
	{
		public const char DELIMITER_COMMA = ','; // for .csv
		public const char DELIMITER_SPACE = ' '; // for .ssv
		public const char DELIMITER_TAB = '\t';  // for .tsv

		private char Delimiter;
		private TextReader Reader;

		public CsvFileReader(string file)
			: this(file, GetFileEncoding(file))
		{ }

		public CsvFileReader(string file, char delimiter)
			: this(file, GetFileEncoding(file), delimiter)
		{ }

		public CsvFileReader(string file, Encoding encoding)
			: this(file, encoding, DELIMITER_COMMA)
		{ }

		public CsvFileReader(string file, Encoding encoding, char delimiter)
		{
			this.Delimiter = delimiter;

			Encoding strictEncoding = (Encoding)encoding.Clone();
			strictEncoding.DecoderFallback = DecoderFallback.ExceptionFallback;

			FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				byte[] prefix = new byte[4];
				int count = stream.Read(prefix, 0, prefix.Length);
				bool utf8Bom = count >= 3 &&
					prefix[0] == 0xEF &&
					prefix[1] == 0xBB &&
					prefix[2] == 0xBF;

				if (
					(utf8Bom && encoding.CodePage != 65001) ||
					(count >= 2 && ((prefix[0] == 0xFF && prefix[1] == 0xFE) || (prefix[0] == 0xFE && prefix[1] == 0xFF))) ||
					(count >= 4 && prefix[0] == 0 && prefix[1] == 0 && prefix[2] == 0xFE && prefix[3] == 0xFF)
					)
					throw new InvalidDataException("BOM と入力文字コードが一致しないか、未対応の文字コードです。");

				stream.Position = utf8Bom ? 3 : 0;
				this.Reader = new StrictTextReader(stream, strictEncoding);
			}
			catch
			{
				stream.Dispose();
				throw;
			}
		}

		// .NET Framework の StreamReader が末尾の未完結 CP932 バイトを
		// 見逃す場合があるため、EOF でも Decoder を明示的に flush する。
		private class StrictTextReader : TextReader
		{
			private readonly Stream Stream;
			private readonly Decoder Decoder;
			private readonly byte[] Bytes = new byte[4096];
			private readonly char[] Characters;
			private int Position;
			private int Count;
			private bool EndOfStream;

			public StrictTextReader(Stream stream, Encoding encoding)
			{
				this.Stream = stream;
				this.Decoder = encoding.GetDecoder();
				this.Characters = new char[encoding.GetMaxCharCount(this.Bytes.Length)];
			}

			public override int Peek()
			{
				while (this.Position == this.Count)
				{
					if (this.EndOfStream) return -1;
					int count = this.Stream.Read(this.Bytes, 0, this.Bytes.Length);
					this.EndOfStream = count == 0;
					this.Count = this.Decoder.GetChars(this.Bytes, 0, count, this.Characters, 0, this.EndOfStream);
					this.Position = 0;
				}
				return this.Characters[this.Position];
			}

			public override int Read()
			{
				int value = this.Peek();
				if (value >= 0) this.Position++;
				return value;
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing) this.Stream.Dispose();
				base.Dispose(disposing);
			}
		}

		private static Encoding GetFileEncoding(string file)
		{
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				// ? UTF-8 with BOM
				if (
					reader.ReadByte() == 0xEF &&
					reader.ReadByte() == 0xBB &&
					reader.ReadByte() == 0xBF
					)
					return Encoding.UTF8;
			}
			return SCommon.ENCODING_SJIS;
		}

		private int ReadChar()
		{
			int value = this.Reader.Read();

			if (value >= 0 && char.IsControl((char)value) && value != '\r' && value != '\n' && value != '\t')
				throw new InvalidDataException($"使用できない制御文字 U+{value:X4} が含まれています。");

			return value;
		}

		public string[] ReadRow()
		{
			if (this.Reader.Peek() == -1)
				return null;

			List<string> row = new List<string>();
			for (; ; )
			{
				StringBuilder cell = new StringBuilder();

				int value = this.ReadChar();
				if (value == '"')
				{
					for (; ; )
					{
						value = this.ReadChar();

						if (value == -1)
							throw new InvalidDataException("引用符が閉じられていません。");

						if (value == '"')
						{
							if (this.Reader.Peek() != '"')
								break;
							this.ReadChar();
						}
						cell.Append((char)value);
					}
					value = this.ReadChar();

					if (value != -1 && value != this.Delimiter && value != '\r' && value != '\n')
						throw new InvalidDataException("閉じ引用符の後に区切り文字以外の文字があります。");
				}
				else
				{
					while (value != -1 && value != this.Delimiter && value != '\r' && value != '\n')
					{
						if (value == '"')
							throw new InvalidDataException("引用符はセルの先頭から囲んでください。");

						cell.Append((char)value);
						value = this.ReadChar();
					}
				}
				row.Add(cell.ToString());

				if (value == this.Delimiter)
					continue;

				if (value == '\r' && this.Reader.Peek() == '\n')
					this.ReadChar();

				return row.ToArray();
			}
		}

		public string[][] ReadToEnd()
		{
			List<string[]> rows = new List<string[]>();

			for (; ; )
			{
				string[] row = this.ReadRow();

				if (row == null)
					break;

				rows.Add(row);
			}
			return rows.ToArray();
		}

		public void Dispose()
		{
			if (this.Reader != null)
			{
				this.Reader.Dispose();
				this.Reader = null;
			}
		}

		public static string[][] ReadToEnd(string file)
		{
			return ReadToEnd(file, GetFileEncoding(file));
		}

		public static string[][] ReadToEnd(string file, char delimiter)
		{
			return ReadToEnd(file, GetFileEncoding(file), delimiter);
		}

		public static string[][] ReadToEnd(string file, Encoding encoding)
		{
			return ReadToEnd(file, encoding, DELIMITER_COMMA);
		}

		public static string[][] ReadToEnd(string file, Encoding encoding, char delimiter)
		{
			using (CsvFileReader reader = new CsvFileReader(file, encoding, delimiter))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
