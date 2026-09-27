using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLTStudio.Dialogs
{
	public partial class ErrorLogDlg : Form
	{
		public static void Run(string[] errorLog)
		{
			using (var f = new ErrorLogDlg())
			{
				f.ErrorLog.Text = string.Join(Consts.NEW_LINE_CRLF, errorLog);
				f.ShowDialog();
			}
		}

		public ErrorLogDlg()
		{
			InitializeComponent();

			this.ErrorLog.ForeColor = Color.Black;
			this.ErrorLog.BackColor = Color.White;
		}

		private void ErrorLogDlg_Load(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
