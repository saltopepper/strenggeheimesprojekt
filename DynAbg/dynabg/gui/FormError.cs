using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynAbg.gui
{
	internal partial class FormError : Form
	{
		private FormError()
		{
			InitializeComponent();
		}

		private static FormError formError = null;

		public static void showError(List<string> fehler)
		{
			if (formError == null)
				formError = new FormError();

			formError.listBox1.DataSource = fehler;
			formError.BringToFront();

			if (!formError.Visible)
				formError.Show();
		}

		private void FormError_FormClosed(object sender, FormClosedEventArgs e)
		{
			formError = null;
		}

		internal static void dispose()
		{
			if (formError != null)
				formError.Dispose();
		}
	}
}
