using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Themes
{
    public partial class Generic
    {
        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox) sender).ToolTip = "blablabl";
            }
        }
    }
}
