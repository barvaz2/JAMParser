using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertJAMApp
{
    public class ThreadSafeFacade
    {
        public static void AppendTextToRichTextbox(RichTextBox rtb, string s)
        {
            if (rtb.InvokeRequired)
            {
                rtb.BeginInvoke(new Action(() =>
                {
                    AppendTextToRichTextbox(rtb, s);
                }));
                return;
            }
            rtb.AppendText(s);
        }

        public static void UpdateTextboxText(TextBox tb, string s)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke(new Action(() =>
                {
                    UpdateTextboxText(tb, s);
                }));
                return;
            }
            tb.Text = s;
        }

        public static void UpdateRichTextboxText(RichTextBox rtb, string s)
        {
            if (rtb.InvokeRequired)
            {
                rtb.BeginInvoke(new Action(() =>
                {
                    UpdateRichTextboxText(rtb, s);
                }));
                return;
            }
            rtb.Text = s;
        }

        public static void UpdateBrowserDocumentText(WebBrowser wb, string s)
        {
            if (wb.InvokeRequired)
            {
                wb.BeginInvoke(new Action(() =>
                {
                    UpdateBrowserDocumentText(wb, s);
                }));
                return;
            }
            if (wb.Document == null)
            {
                wb.DocumentText = s;
            }
            else
            {
                wb.Document.OpenNew(true);
                wb.Document.Write(s);
            }
        }

        public static void UpdateLabelText(Label l, string s)
        {
            if (l.InvokeRequired)
            {
                l.BeginInvoke(new Action(() =>
                {
                    UpdateLabelText(l, s);
                }));
                return;
            }
            l.Text = s;
        }

        public static void AddRecordToDataGridView(DataGridView dgv, object[] obj)
        {
            if (dgv.InvokeRequired)
            {
                dgv.BeginInvoke(new Action(() =>
                {
                    AddRecordToDataGridView(dgv, obj);
                }));
                return;
            }

            dgv.Rows.Add(obj);
        }

        internal static void ClearDataGridView(DataGridView dgv)
        {
            if (dgv.InvokeRequired)
            {
                dgv.BeginInvoke(new Action(() =>
                {
                    ClearDataGridView(dgv);
                }));
                return;
            }

            dgv.Rows.Clear();
        }
    }
}
