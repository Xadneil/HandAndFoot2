using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandAndFoot.Client
{
    static class TextBoxExtensions
    {
        public static void SetPlaceholder(this TextBox t, string placeholderText)
        {
            NativeMethods.SendMessage(t.Handle, 0x1501, 1, placeholderText);
        }
    }
}
