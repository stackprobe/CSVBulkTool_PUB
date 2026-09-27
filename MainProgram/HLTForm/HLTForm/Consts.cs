using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLTStudio
{
	public static class Consts
	{
		/// <summary>
		/// このアプリケーションの名称・タイトル
		/// </summary>
		public const string APP_TITLE = "CSV加工ツール";

		/// <summary>
		/// 最大入力CSVファイル数
		/// </summary>
		public const int INPUT_CSV_FILE_COUNT_MAX = 1000;

		/// <summary>
		/// CSVファイル拡張子
		/// </summary>
		public const string FILE_EXT_CSV = ".csv";

		/// <summary>
		/// TSVファイル拡張子
		/// </summary>
		public const string FILE_EXT_TSV = ".tsv";

		/// <summary>
		/// 改行コード：CR-LF
		/// </summary>
		public const string NEW_LINE_CRLF = "\r\n";

		/// <summary>
		/// 改行コード：LF
		/// </summary>
		public const string NEW_LINE_LF = "\n";
	}
}
