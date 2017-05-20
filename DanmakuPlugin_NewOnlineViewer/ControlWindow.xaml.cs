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
    /// ControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ControlWindow : Window
    {
        public ControlWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Conf.MouseBypass == true)
            {
                Conf.MouseBypass = false;
                label_mouseBypassStatus.Foreground = new SolidColorBrush(Colors.Red);
                label_mouseBypassStatus.Content = "关闭";
            }
            else
            {
                Conf.MouseBypass = true;
                label_mouseBypassStatus.Foreground = new SolidColorBrush(Colors.Green);
                label_mouseBypassStatus.Content = "开启";
            }
            

        }

        private void sizeValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
    }
}
