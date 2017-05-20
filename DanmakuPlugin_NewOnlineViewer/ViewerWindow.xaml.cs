using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace DanmakuPlugin_NewOnlineViewer
{
    /// <summary>
    /// ViewerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewerWindow : Window
    {
        public ViewerWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 50;
            this.Top = Screen.PrimaryScreen.Bounds.Height - 90;
            Topmost = true;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


    }
}
