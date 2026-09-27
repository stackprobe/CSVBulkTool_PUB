namespace HLTStudio
{
	partial class MainWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.G001 = new System.Windows.Forms.GroupBox();
			this.Btn入力CSV下へ = new System.Windows.Forms.Button();
			this.Btn入力CSV上へ = new System.Windows.Forms.Button();
			this.Lbl入力CSVCount = new System.Windows.Forms.Label();
			this.L011 = new System.Windows.Forms.Label();
			this.Cmb入力ファイル形式 = new System.Windows.Forms.ComboBox();
			this.L002 = new System.Windows.Forms.Label();
			this.L003 = new System.Windows.Forms.Label();
			this.Cmb入力文字コード = new System.Windows.Forms.ComboBox();
			this.Btn入力CSV追加 = new System.Windows.Forms.Button();
			this.Btn入力CSV削除 = new System.Windows.Forms.Button();
			this.Btn入力CSV全て削除 = new System.Windows.Forms.Button();
			this.L001 = new System.Windows.Forms.Label();
			this.Lst入力CSV = new System.Windows.Forms.ListBox();
			this.G002 = new System.Windows.Forms.GroupBox();
			this.Chkソートする = new System.Windows.Forms.CheckBox();
			this.Grpソート = new System.Windows.Forms.GroupBox();
			this.L006 = new System.Windows.Forms.Label();
			this.L005 = new System.Windows.Forms.Label();
			this.Cmbソート順 = new System.Windows.Forms.ComboBox();
			this.Cmbソート型 = new System.Windows.Forms.ComboBox();
			this.Cmbソート列 = new System.Windows.Forms.ComboBox();
			this.L004 = new System.Windows.Forms.Label();
			this.Chk重複行を削除 = new System.Windows.Forms.CheckBox();
			this.Chk空行削除 = new System.Windows.Forms.CheckBox();
			this.Chk全セルTrim = new System.Windows.Forms.CheckBox();
			this.Chk先頭行をヘッダ行とする = new System.Windows.Forms.CheckBox();
			this.G004 = new System.Windows.Forms.GroupBox();
			this.Lbl出力メッセージ = new System.Windows.Forms.Label();
			this.Btn実行 = new System.Windows.Forms.Button();
			this.L009 = new System.Windows.Forms.Label();
			this.Cmb出力改行コード = new System.Windows.Forms.ComboBox();
			this.L010 = new System.Windows.Forms.Label();
			this.L008 = new System.Windows.Forms.Label();
			this.L007 = new System.Windows.Forms.Label();
			this.Cmb出力文字コード = new System.Windows.Forms.ComboBox();
			this.Cmb出力ファイル形式 = new System.Windows.Forms.ComboBox();
			this.Btn出力先 = new System.Windows.Forms.Button();
			this.Txt出力先 = new System.Windows.Forms.TextBox();
			this.Lbl出力先 = new System.Windows.Forms.Label();
			this.G005 = new System.Windows.Forms.GroupBox();
			this.Rdo出力_別々 = new System.Windows.Forms.RadioButton();
			this.Rdo出力_結合 = new System.Windows.Forms.RadioButton();
			this.G001.SuspendLayout();
			this.G002.SuspendLayout();
			this.Grpソート.SuspendLayout();
			this.G004.SuspendLayout();
			this.G005.SuspendLayout();
			this.SuspendLayout();
			// 
			// G001
			// 
			this.G001.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.G001.Controls.Add(this.Btn入力CSV下へ);
			this.G001.Controls.Add(this.Btn入力CSV上へ);
			this.G001.Controls.Add(this.Lbl入力CSVCount);
			this.G001.Controls.Add(this.L011);
			this.G001.Controls.Add(this.Cmb入力ファイル形式);
			this.G001.Controls.Add(this.L002);
			this.G001.Controls.Add(this.L003);
			this.G001.Controls.Add(this.Cmb入力文字コード);
			this.G001.Controls.Add(this.Btn入力CSV追加);
			this.G001.Controls.Add(this.Btn入力CSV削除);
			this.G001.Controls.Add(this.Btn入力CSV全て削除);
			this.G001.Controls.Add(this.L001);
			this.G001.Controls.Add(this.Lst入力CSV);
			this.G001.Location = new System.Drawing.Point(12, 12);
			this.G001.Name = "G001";
			this.G001.Size = new System.Drawing.Size(651, 224);
			this.G001.TabIndex = 0;
			this.G001.TabStop = false;
			this.G001.Text = "1. 入力";
			// 
			// Btn入力CSV下へ
			// 
			this.Btn入力CSV下へ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn入力CSV下へ.Location = new System.Drawing.Point(585, 101);
			this.Btn入力CSV下へ.Name = "Btn入力CSV下へ";
			this.Btn入力CSV下へ.Size = new System.Drawing.Size(60, 49);
			this.Btn入力CSV下へ.TabIndex = 5;
			this.Btn入力CSV下へ.Text = "下へ";
			this.Btn入力CSV下へ.UseVisualStyleBackColor = true;
			this.Btn入力CSV下へ.Click += new System.EventHandler(this.Btn入力CSV下へ_Click);
			// 
			// Btn入力CSV上へ
			// 
			this.Btn入力CSV上へ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn入力CSV上へ.Location = new System.Drawing.Point(585, 46);
			this.Btn入力CSV上へ.Name = "Btn入力CSV上へ";
			this.Btn入力CSV上へ.Size = new System.Drawing.Size(60, 49);
			this.Btn入力CSV上へ.TabIndex = 4;
			this.Btn入力CSV上へ.Text = "上へ";
			this.Btn入力CSV上へ.UseVisualStyleBackColor = true;
			this.Btn入力CSV上へ.Click += new System.EventHandler(this.Btn入力CSV上へ_Click);
			// 
			// Lbl入力CSVCount
			// 
			this.Lbl入力CSVCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Lbl入力CSVCount.AutoSize = true;
			this.Lbl入力CSVCount.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Lbl入力CSVCount.ForeColor = System.Drawing.Color.Teal;
			this.Lbl入力CSVCount.Location = new System.Drawing.Point(510, 26);
			this.Lbl入力CSVCount.Name = "Lbl入力CSVCount";
			this.Lbl入力CSVCount.Size = new System.Drawing.Size(69, 17);
			this.Lbl入力CSVCount.TabIndex = 2;
			this.Lbl入力CSVCount.Text = "( 9999 件 )";
			// 
			// L011
			// 
			this.L011.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.L011.AutoSize = true;
			this.L011.Location = new System.Drawing.Point(6, 160);
			this.L011.Name = "L011";
			this.L011.Size = new System.Drawing.Size(74, 20);
			this.L011.TabIndex = 6;
			this.L011.Text = "入力形式：";
			// 
			// Cmb入力ファイル形式
			// 
			this.Cmb入力ファイル形式.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Cmb入力ファイル形式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmb入力ファイル形式.FormattingEnabled = true;
			this.Cmb入力ファイル形式.Location = new System.Drawing.Point(99, 156);
			this.Cmb入力ファイル形式.Name = "Cmb入力ファイル形式";
			this.Cmb入力ファイル形式.Size = new System.Drawing.Size(190, 28);
			this.Cmb入力ファイル形式.TabIndex = 7;
			// 
			// L002
			// 
			this.L002.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.L002.AutoSize = true;
			this.L002.ForeColor = System.Drawing.Color.Blue;
			this.L002.Location = new System.Drawing.Point(119, 23);
			this.L002.Name = "L002";
			this.L002.Size = new System.Drawing.Size(373, 20);
			this.L002.TabIndex = 1;
			this.L002.Text = "↓ここへ入力CSVファイルをドラッグ＆ドロップしてください";
			// 
			// L003
			// 
			this.L003.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.L003.AutoSize = true;
			this.L003.Location = new System.Drawing.Point(6, 193);
			this.L003.Name = "L003";
			this.L003.Size = new System.Drawing.Size(87, 20);
			this.L003.TabIndex = 8;
			this.L003.Text = "文字コード：";
			// 
			// Cmb入力文字コード
			// 
			this.Cmb入力文字コード.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Cmb入力文字コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmb入力文字コード.FormattingEnabled = true;
			this.Cmb入力文字コード.Location = new System.Drawing.Point(99, 190);
			this.Cmb入力文字コード.Name = "Cmb入力文字コード";
			this.Cmb入力文字コード.Size = new System.Drawing.Size(190, 28);
			this.Cmb入力文字コード.TabIndex = 9;
			this.Cmb入力文字コード.SelectedIndexChanged += new System.EventHandler(this.Cmb入力文字コード_SelectedIndexChanged);
			// 
			// Btn入力CSV追加
			// 
			this.Btn入力CSV追加.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn入力CSV追加.ForeColor = System.Drawing.Color.Navy;
			this.Btn入力CSV追加.Location = new System.Drawing.Point(327, 156);
			this.Btn入力CSV追加.Name = "Btn入力CSV追加";
			this.Btn入力CSV追加.Size = new System.Drawing.Size(80, 36);
			this.Btn入力CSV追加.TabIndex = 10;
			this.Btn入力CSV追加.Text = "追加";
			this.Btn入力CSV追加.UseVisualStyleBackColor = true;
			this.Btn入力CSV追加.Click += new System.EventHandler(this.Btn入力CSV追加_Click);
			// 
			// Btn入力CSV削除
			// 
			this.Btn入力CSV削除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn入力CSV削除.ForeColor = System.Drawing.Color.Maroon;
			this.Btn入力CSV削除.Location = new System.Drawing.Point(413, 156);
			this.Btn入力CSV削除.Name = "Btn入力CSV削除";
			this.Btn入力CSV削除.Size = new System.Drawing.Size(80, 36);
			this.Btn入力CSV削除.TabIndex = 11;
			this.Btn入力CSV削除.Text = "削除";
			this.Btn入力CSV削除.UseVisualStyleBackColor = true;
			this.Btn入力CSV削除.Click += new System.EventHandler(this.Btn入力CSV削除_Click);
			// 
			// Btn入力CSV全て削除
			// 
			this.Btn入力CSV全て削除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn入力CSV全て削除.ForeColor = System.Drawing.Color.Maroon;
			this.Btn入力CSV全て削除.Location = new System.Drawing.Point(499, 156);
			this.Btn入力CSV全て削除.Name = "Btn入力CSV全て削除";
			this.Btn入力CSV全て削除.Size = new System.Drawing.Size(80, 36);
			this.Btn入力CSV全て削除.TabIndex = 12;
			this.Btn入力CSV全て削除.Text = "全削除";
			this.Btn入力CSV全て削除.UseVisualStyleBackColor = true;
			this.Btn入力CSV全て削除.Click += new System.EventHandler(this.Btn入力CSV全て削除_Click);
			// 
			// L001
			// 
			this.L001.AutoSize = true;
			this.L001.Location = new System.Drawing.Point(6, 23);
			this.L001.Name = "L001";
			this.L001.Size = new System.Drawing.Size(100, 20);
			this.L001.TabIndex = 0;
			this.L001.Text = "入力CSV一覧：";
			// 
			// Lst入力CSV
			// 
			this.Lst入力CSV.AllowDrop = true;
			this.Lst入力CSV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Lst入力CSV.FormattingEnabled = true;
			this.Lst入力CSV.ItemHeight = 20;
			this.Lst入力CSV.Items.AddRange(new object[] {
            "ファイル 1",
            "ファイル 2",
            "ファイル 3",
            "ファイル 4",
            "ファイル 5"});
			this.Lst入力CSV.Location = new System.Drawing.Point(6, 46);
			this.Lst入力CSV.Name = "Lst入力CSV";
			this.Lst入力CSV.Size = new System.Drawing.Size(573, 104);
			this.Lst入力CSV.TabIndex = 3;
			this.Lst入力CSV.SelectedIndexChanged += new System.EventHandler(this.Lst入力CSV_SelectedIndexChanged);
			this.Lst入力CSV.DragDrop += new System.Windows.Forms.DragEventHandler(this.Lst入力CSV_DragDrop);
			this.Lst入力CSV.DragEnter += new System.Windows.Forms.DragEventHandler(this.Lst入力CSV_DragEnter);
			// 
			// G002
			// 
			this.G002.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.G002.Controls.Add(this.Chkソートする);
			this.G002.Controls.Add(this.Grpソート);
			this.G002.Controls.Add(this.Chk重複行を削除);
			this.G002.Controls.Add(this.Chk空行削除);
			this.G002.Controls.Add(this.Chk全セルTrim);
			this.G002.Controls.Add(this.Chk先頭行をヘッダ行とする);
			this.G002.Location = new System.Drawing.Point(12, 242);
			this.G002.Name = "G002";
			this.G002.Size = new System.Drawing.Size(651, 190);
			this.G002.TabIndex = 1;
			this.G002.TabStop = false;
			this.G002.Text = "2. 加工";
			// 
			// Chkソートする
			// 
			this.Chkソートする.AutoSize = true;
			this.Chkソートする.Location = new System.Drawing.Point(20, 152);
			this.Chkソートする.Name = "Chkソートする";
			this.Chkソートする.Size = new System.Drawing.Size(93, 24);
			this.Chkソートする.TabIndex = 4;
			this.Chkソートする.Text = "ソートする";
			this.Chkソートする.UseVisualStyleBackColor = true;
			this.Chkソートする.CheckedChanged += new System.EventHandler(this.Chkソートする_CheckedChanged);
			// 
			// Grpソート
			// 
			this.Grpソート.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Grpソート.Controls.Add(this.L006);
			this.Grpソート.Controls.Add(this.L005);
			this.Grpソート.Controls.Add(this.Cmbソート順);
			this.Grpソート.Controls.Add(this.Cmbソート型);
			this.Grpソート.Controls.Add(this.Cmbソート列);
			this.Grpソート.Controls.Add(this.L004);
			this.Grpソート.Location = new System.Drawing.Point(249, 26);
			this.Grpソート.Name = "Grpソート";
			this.Grpソート.Size = new System.Drawing.Size(396, 153);
			this.Grpソート.TabIndex = 5;
			this.Grpソート.TabStop = false;
			this.Grpソート.Text = "ソート";
			// 
			// L006
			// 
			this.L006.AutoSize = true;
			this.L006.Location = new System.Drawing.Point(26, 106);
			this.L006.Name = "L006";
			this.L006.Size = new System.Drawing.Size(74, 20);
			this.L006.TabIndex = 4;
			this.L006.Text = "ソート順：";
			// 
			// L005
			// 
			this.L005.AutoSize = true;
			this.L005.Location = new System.Drawing.Point(26, 72);
			this.L005.Name = "L005";
			this.L005.Size = new System.Drawing.Size(74, 20);
			this.L005.TabIndex = 2;
			this.L005.Text = "データ型：";
			// 
			// Cmbソート順
			// 
			this.Cmbソート順.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Cmbソート順.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmbソート順.FormattingEnabled = true;
			this.Cmbソート順.Location = new System.Drawing.Point(106, 103);
			this.Cmbソート順.Name = "Cmbソート順";
			this.Cmbソート順.Size = new System.Drawing.Size(284, 28);
			this.Cmbソート順.TabIndex = 5;
			this.Cmbソート順.SelectedIndexChanged += new System.EventHandler(this.Cmbソート順_SelectedIndexChanged);
			// 
			// Cmbソート型
			// 
			this.Cmbソート型.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Cmbソート型.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmbソート型.FormattingEnabled = true;
			this.Cmbソート型.Location = new System.Drawing.Point(106, 69);
			this.Cmbソート型.Name = "Cmbソート型";
			this.Cmbソート型.Size = new System.Drawing.Size(284, 28);
			this.Cmbソート型.TabIndex = 3;
			this.Cmbソート型.SelectedIndexChanged += new System.EventHandler(this.Cmbソート型_SelectedIndexChanged);
			// 
			// Cmbソート列
			// 
			this.Cmbソート列.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Cmbソート列.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmbソート列.FormattingEnabled = true;
			this.Cmbソート列.Location = new System.Drawing.Point(106, 35);
			this.Cmbソート列.Name = "Cmbソート列";
			this.Cmbソート列.Size = new System.Drawing.Size(284, 28);
			this.Cmbソート列.TabIndex = 1;
			this.Cmbソート列.SelectedIndexChanged += new System.EventHandler(this.Cmbソート列_SelectedIndexChanged);
			// 
			// L004
			// 
			this.L004.AutoSize = true;
			this.L004.Location = new System.Drawing.Point(26, 38);
			this.L004.Name = "L004";
			this.L004.Size = new System.Drawing.Size(35, 20);
			this.L004.TabIndex = 0;
			this.L004.Text = "列：";
			// 
			// Chk重複行を削除
			// 
			this.Chk重複行を削除.AutoSize = true;
			this.Chk重複行を削除.Location = new System.Drawing.Point(20, 122);
			this.Chk重複行を削除.Name = "Chk重複行を削除";
			this.Chk重複行を削除.Size = new System.Drawing.Size(106, 24);
			this.Chk重複行を削除.TabIndex = 3;
			this.Chk重複行を削除.Text = "重複行を削除";
			this.Chk重複行を削除.UseVisualStyleBackColor = true;
			this.Chk重複行を削除.CheckedChanged += new System.EventHandler(this.Chk重複行を削除_CheckedChanged);
			// 
			// Chk空行削除
			// 
			this.Chk空行削除.AutoSize = true;
			this.Chk空行削除.Location = new System.Drawing.Point(20, 92);
			this.Chk空行削除.Name = "Chk空行削除";
			this.Chk空行削除.Size = new System.Drawing.Size(80, 24);
			this.Chk空行削除.TabIndex = 2;
			this.Chk空行削除.Text = "空行削除";
			this.Chk空行削除.UseVisualStyleBackColor = true;
			this.Chk空行削除.CheckedChanged += new System.EventHandler(this.Chk空行削除_CheckedChanged);
			// 
			// Chk全セルTrim
			// 
			this.Chk全セルTrim.AutoSize = true;
			this.Chk全セルTrim.Location = new System.Drawing.Point(20, 62);
			this.Chk全セルTrim.Name = "Chk全セルTrim";
			this.Chk全セルTrim.Size = new System.Drawing.Size(223, 24);
			this.Chk全セルTrim.TabIndex = 1;
			this.Chk全セルTrim.Text = "全てのセルの前後空白を削除する";
			this.Chk全セルTrim.UseVisualStyleBackColor = true;
			this.Chk全セルTrim.CheckedChanged += new System.EventHandler(this.Chk全セルTrim_CheckedChanged);
			// 
			// Chk先頭行をヘッダ行とする
			// 
			this.Chk先頭行をヘッダ行とする.AutoSize = true;
			this.Chk先頭行をヘッダ行とする.Checked = true;
			this.Chk先頭行をヘッダ行とする.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Chk先頭行をヘッダ行とする.Location = new System.Drawing.Point(20, 32);
			this.Chk先頭行をヘッダ行とする.Name = "Chk先頭行をヘッダ行とする";
			this.Chk先頭行をヘッダ行とする.Size = new System.Drawing.Size(184, 24);
			this.Chk先頭行をヘッダ行とする.TabIndex = 0;
			this.Chk先頭行をヘッダ行とする.Text = "先頭行をヘッダ行と見なす";
			this.Chk先頭行をヘッダ行とする.UseVisualStyleBackColor = true;
			this.Chk先頭行をヘッダ行とする.CheckedChanged += new System.EventHandler(this.Chk先頭行をヘッダ行とする_CheckedChanged);
			// 
			// G004
			// 
			this.G004.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.G004.Controls.Add(this.Lbl出力メッセージ);
			this.G004.Controls.Add(this.Btn実行);
			this.G004.Controls.Add(this.L009);
			this.G004.Controls.Add(this.Cmb出力改行コード);
			this.G004.Controls.Add(this.L010);
			this.G004.Controls.Add(this.L008);
			this.G004.Controls.Add(this.L007);
			this.G004.Controls.Add(this.Cmb出力文字コード);
			this.G004.Controls.Add(this.Cmb出力ファイル形式);
			this.G004.Controls.Add(this.Btn出力先);
			this.G004.Controls.Add(this.Txt出力先);
			this.G004.Controls.Add(this.Lbl出力先);
			this.G004.Controls.Add(this.G005);
			this.G004.Location = new System.Drawing.Point(12, 438);
			this.G004.Name = "G004";
			this.G004.Size = new System.Drawing.Size(651, 256);
			this.G004.TabIndex = 2;
			this.G004.TabStop = false;
			this.G004.Text = "3. 出力";
			// 
			// Lbl出力メッセージ
			// 
			this.Lbl出力メッセージ.AutoSize = true;
			this.Lbl出力メッセージ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Lbl出力メッセージ.ForeColor = System.Drawing.Color.Blue;
			this.Lbl出力メッセージ.Location = new System.Drawing.Point(6, 222);
			this.Lbl出力メッセージ.Name = "Lbl出力メッセージ";
			this.Lbl出力メッセージ.Size = new System.Drawing.Size(74, 20);
			this.Lbl出力メッセージ.TabIndex = 11;
			this.Lbl出力メッセージ.Text = "メッセージ";
			// 
			// Btn実行
			// 
			this.Btn実行.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn実行.Location = new System.Drawing.Point(515, 214);
			this.Btn実行.Name = "Btn実行";
			this.Btn実行.Size = new System.Drawing.Size(130, 36);
			this.Btn実行.TabIndex = 12;
			this.Btn実行.Text = "実行";
			this.Btn実行.UseVisualStyleBackColor = true;
			this.Btn実行.Click += new System.EventHandler(this.Btn実行_Click);
			// 
			// L009
			// 
			this.L009.AutoSize = true;
			this.L009.Location = new System.Drawing.Point(262, 97);
			this.L009.Name = "L009";
			this.L009.Size = new System.Drawing.Size(87, 20);
			this.L009.TabIndex = 5;
			this.L009.Text = "改行コード：";
			// 
			// Cmb出力改行コード
			// 
			this.Cmb出力改行コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmb出力改行コード.FormattingEnabled = true;
			this.Cmb出力改行コード.Location = new System.Drawing.Point(355, 94);
			this.Cmb出力改行コード.Name = "Cmb出力改行コード";
			this.Cmb出力改行コード.Size = new System.Drawing.Size(227, 28);
			this.Cmb出力改行コード.TabIndex = 6;
			this.Cmb出力改行コード.SelectedIndexChanged += new System.EventHandler(this.Cmb出力_改行コード_SelectedIndexChanged);
			// 
			// L010
			// 
			this.L010.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.L010.AutoSize = true;
			this.L010.ForeColor = System.Drawing.Color.Blue;
			this.L010.Location = new System.Drawing.Point(184, 141);
			this.L010.Name = "L010";
			this.L010.Size = new System.Drawing.Size(308, 20);
			this.L010.TabIndex = 8;
			this.L010.Text = "↓ここへ出力先をドラッグ＆ドロップしてください";
			// 
			// L008
			// 
			this.L008.AutoSize = true;
			this.L008.Location = new System.Drawing.Point(262, 63);
			this.L008.Name = "L008";
			this.L008.Size = new System.Drawing.Size(87, 20);
			this.L008.TabIndex = 3;
			this.L008.Text = "文字コード：";
			// 
			// L007
			// 
			this.L007.AutoSize = true;
			this.L007.Location = new System.Drawing.Point(262, 29);
			this.L007.Name = "L007";
			this.L007.Size = new System.Drawing.Size(74, 20);
			this.L007.TabIndex = 1;
			this.L007.Text = "出力形式：";
			// 
			// Cmb出力文字コード
			// 
			this.Cmb出力文字コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmb出力文字コード.FormattingEnabled = true;
			this.Cmb出力文字コード.Location = new System.Drawing.Point(355, 60);
			this.Cmb出力文字コード.Name = "Cmb出力文字コード";
			this.Cmb出力文字コード.Size = new System.Drawing.Size(227, 28);
			this.Cmb出力文字コード.TabIndex = 4;
			this.Cmb出力文字コード.SelectedIndexChanged += new System.EventHandler(this.Cmb出力_文字コード_SelectedIndexChanged);
			// 
			// Cmb出力ファイル形式
			// 
			this.Cmb出力ファイル形式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmb出力ファイル形式.FormattingEnabled = true;
			this.Cmb出力ファイル形式.Location = new System.Drawing.Point(355, 26);
			this.Cmb出力ファイル形式.Name = "Cmb出力ファイル形式";
			this.Cmb出力ファイル形式.Size = new System.Drawing.Size(227, 28);
			this.Cmb出力ファイル形式.TabIndex = 2;
			this.Cmb出力ファイル形式.SelectedIndexChanged += new System.EventHandler(this.Cmb出力_ファイル形式_SelectedIndexChanged);
			// 
			// Btn出力先
			// 
			this.Btn出力先.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn出力先.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn出力先.Location = new System.Drawing.Point(579, 163);
			this.Btn出力先.Name = "Btn出力先";
			this.Btn出力先.Size = new System.Drawing.Size(66, 28);
			this.Btn出力先.TabIndex = 10;
			this.Btn出力先.Text = "参照...";
			this.Btn出力先.UseVisualStyleBackColor = true;
			this.Btn出力先.Click += new System.EventHandler(this.Btn出力先_Click);
			// 
			// Txt出力先
			// 
			this.Txt出力先.AllowDrop = true;
			this.Txt出力先.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Txt出力先.Location = new System.Drawing.Point(6, 164);
			this.Txt出力先.Name = "Txt出力先";
			this.Txt出力先.ReadOnly = true;
			this.Txt出力先.Size = new System.Drawing.Size(571, 27);
			this.Txt出力先.TabIndex = 9;
			this.Txt出力先.Text = "出力ファイル";
			this.Txt出力先.TextChanged += new System.EventHandler(this.Txt出力先_TextChanged);
			this.Txt出力先.DragDrop += new System.Windows.Forms.DragEventHandler(this.Txt出力先_DragDrop);
			this.Txt出力先.DragEnter += new System.Windows.Forms.DragEventHandler(this.Txt出力先_DragEnter);
			this.Txt出力先.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt出力先_KeyPress);
			// 
			// Lbl出力先
			// 
			this.Lbl出力先.AutoSize = true;
			this.Lbl出力先.Location = new System.Drawing.Point(6, 141);
			this.Lbl出力先.Name = "Lbl出力先";
			this.Lbl出力先.Size = new System.Drawing.Size(100, 20);
			this.Lbl出力先.TabIndex = 7;
			this.Lbl出力先.Text = "出力ファイル：";
			// 
			// G005
			// 
			this.G005.Controls.Add(this.Rdo出力_別々);
			this.G005.Controls.Add(this.Rdo出力_結合);
			this.G005.Location = new System.Drawing.Point(6, 26);
			this.G005.Name = "G005";
			this.G005.Size = new System.Drawing.Size(212, 99);
			this.G005.TabIndex = 0;
			this.G005.TabStop = false;
			this.G005.Text = "出力方法";
			// 
			// Rdo出力_別々
			// 
			this.Rdo出力_別々.AutoSize = true;
			this.Rdo出力_別々.Location = new System.Drawing.Point(14, 58);
			this.Rdo出力_別々.Name = "Rdo出力_別々";
			this.Rdo出力_別々.Size = new System.Drawing.Size(144, 24);
			this.Rdo出力_別々.TabIndex = 1;
			this.Rdo出力_別々.Text = "ファイルごとに出力";
			this.Rdo出力_別々.UseVisualStyleBackColor = true;
			this.Rdo出力_別々.CheckedChanged += new System.EventHandler(this.Rdo出力_別々_CheckedChanged);
			// 
			// Rdo出力_結合
			// 
			this.Rdo出力_結合.AutoSize = true;
			this.Rdo出力_結合.Checked = true;
			this.Rdo出力_結合.Location = new System.Drawing.Point(14, 28);
			this.Rdo出力_結合.Name = "Rdo出力_結合";
			this.Rdo出力_結合.Size = new System.Drawing.Size(126, 24);
			this.Rdo出力_結合.TabIndex = 0;
			this.Rdo出力_結合.TabStop = true;
			this.Rdo出力_結合.Text = "1ファイルに結合";
			this.Rdo出力_結合.UseVisualStyleBackColor = true;
			this.Rdo出力_結合.CheckedChanged += new System.EventHandler(this.Rdo出力_結合_CheckedChanged);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(672, 706);
			this.Controls.Add(this.G004);
			this.Controls.Add(this.G002);
			this.Controls.Add(this.G001);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CSV加工ツール";
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.G001.ResumeLayout(false);
			this.G001.PerformLayout();
			this.G002.ResumeLayout(false);
			this.G002.PerformLayout();
			this.Grpソート.ResumeLayout(false);
			this.Grpソート.PerformLayout();
			this.G004.ResumeLayout(false);
			this.G004.PerformLayout();
			this.G005.ResumeLayout(false);
			this.G005.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox G001;
		private System.Windows.Forms.Button Btn入力CSV追加;
		private System.Windows.Forms.Button Btn入力CSV削除;
		private System.Windows.Forms.Button Btn入力CSV全て削除;
		private System.Windows.Forms.Label L001;
		private System.Windows.Forms.ListBox Lst入力CSV;
		private System.Windows.Forms.GroupBox G002;
		private System.Windows.Forms.CheckBox Chk先頭行をヘッダ行とする;
		private System.Windows.Forms.GroupBox Grpソート;
		private System.Windows.Forms.CheckBox Chk重複行を削除;
		private System.Windows.Forms.CheckBox Chk空行削除;
		private System.Windows.Forms.CheckBox Chk全セルTrim;
		private System.Windows.Forms.CheckBox Chkソートする;
		private System.Windows.Forms.ComboBox Cmbソート型;
		private System.Windows.Forms.ComboBox Cmbソート列;
		private System.Windows.Forms.Label L004;
		private System.Windows.Forms.GroupBox G004;
		private System.Windows.Forms.Label Lbl出力先;
		private System.Windows.Forms.GroupBox G005;
		private System.Windows.Forms.RadioButton Rdo出力_別々;
		private System.Windows.Forms.RadioButton Rdo出力_結合;
		private System.Windows.Forms.Button Btn出力先;
		private System.Windows.Forms.TextBox Txt出力先;
		private System.Windows.Forms.Label L006;
		private System.Windows.Forms.Label L005;
		private System.Windows.Forms.ComboBox Cmbソート順;
		private System.Windows.Forms.ComboBox Cmb出力ファイル形式;
		private System.Windows.Forms.Label L007;
		private System.Windows.Forms.ComboBox Cmb出力文字コード;
		private System.Windows.Forms.Label L003;
		private System.Windows.Forms.ComboBox Cmb入力文字コード;
		private System.Windows.Forms.Label L008;
		private System.Windows.Forms.Label L002;
		private System.Windows.Forms.Label L010;
		private System.Windows.Forms.Label L009;
		private System.Windows.Forms.ComboBox Cmb出力改行コード;
		private System.Windows.Forms.Button Btn実行;
		private System.Windows.Forms.Label Lbl出力メッセージ;
		private System.Windows.Forms.Label L011;
		private System.Windows.Forms.ComboBox Cmb入力ファイル形式;
		private System.Windows.Forms.Label Lbl入力CSVCount;
		private System.Windows.Forms.Button Btn入力CSV下へ;
		private System.Windows.Forms.Button Btn入力CSV上へ;
	}
}

