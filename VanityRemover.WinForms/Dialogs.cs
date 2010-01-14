using System;
using System.Windows.Forms;
using Geeky.VanityRemover.Core;

namespace Geeky.VanityRemover
{
    public static class Dialogs
    {
        public static DialogResult ConfirmClean(IWin32Window owner, FreshDirectory directory)
        {
            var caption = "Please confirm";
            var message = "All empty folders will be deleted from"
                + Environment.NewLine
                + Environment.NewLine
                + directory.FullName;

            return MessageBox.Show(owner, message, caption,
                                   MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
    }
}
