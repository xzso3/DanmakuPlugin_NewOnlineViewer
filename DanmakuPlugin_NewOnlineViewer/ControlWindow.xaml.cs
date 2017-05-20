using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Threading.Tasks;


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
            size_slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sizeValue_Changed);
            saveSettingsTimer.Interval = TimeSpan.FromSeconds(1);
            saveSettingsTimer.Tick += new EventHandler(SavingPreparationEvent);
            saveSettingsTimer.IsEnabled = true;
            saveSettingsTimer.Stop();
            savingTitle.Opacity = 0;

        }

        private static DispatcherTimer saveSettingsTimer = new DispatcherTimer();
        

        /// <summary>
        /// 保存事件计时器
        /// </summary>
        
        /// <summary>
        /// 保存时间倒计时时间
        /// </summary>
        private static int saveCountDown;
        private int saveWindowPosX;
        private int saveWindowPosY;
        private int saveWindowZoomRatio;
        private int saveWindowOpac;
        protected override void OnClosing(CancelEventArgs e)
        {
            /// <summary>
            /// Override窗口关闭时只会隐藏，并不会被销毁
            /// </summary>
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
                button_windowDragEnable.IsEnabled = true;
                button_windowDragEnable.Content = "切换";
                Main.that.MouseBypassEnable();
                StartTimerTicking();
            }
            else
            {
                Conf.MouseBypass = true;
                label_mouseBypassStatus.Foreground = new SolidColorBrush(Colors.Green);
                label_mouseBypassStatus.Content = "开启";
                //Warning!!! 这里需要加一个取消窗口随意拖曳事件!!!!
                button_windowDragEnable.IsEnabled = false;
                button_windowDragEnable.Content = "请先关闭鼠标穿透";
                Conf.WindowDragEnable = false;
                label_windowDragEnableStatus.Foreground = new SolidColorBrush(Colors.Red);
                label_windowDragEnableStatus.Content = "关闭";
                Main.that.mainWindow.IsEnabled = false;
                Main.that.MouseBypassEnable();
                Main.that.mainWindow.Alert.Visibility = Visibility.Hidden;
                StartTimerTicking();


            }
            

        }

        

        private void sizeValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          
            Conf.ZoomRatio = size_slider.Value / 50;
            double resW = 140;
            double resH = 45;
            double fontSizeTitle = 9;
            double fontSizeValue = 30;

            resW = resW * Conf.ZoomRatio;
            resH = resH * Conf.ZoomRatio;
            fontSizeTitle = fontSizeTitle * Conf.ZoomRatio;
            fontSizeValue = fontSizeValue * Conf.ZoomRatio;
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                Main.that.mainWindow.Width = resW;
                Main.that.mainWindow.Height = resH;
                Main.that.mainWindow.labelViewer.FontSize = fontSizeTitle;
                Main.that.mainWindow.textCount.FontSize = fontSizeValue;
                Main.that.mainWindow.Alert.FontSize = fontSizeTitle * 0.8;

            }));
            StartTimerTicking();
        }

        private void ButtonWindowDragEnable_Click(object sender, RoutedEventArgs e)
        {
            if(Conf.WindowDragEnable == true)
            {
                Conf.WindowDragEnable = false;
                label_windowDragEnableStatus.Foreground = new SolidColorBrush(Colors.Red);
                label_windowDragEnableStatus.Content = "关闭";
                Main.that.mainWindow.Alert.Visibility = Visibility.Hidden;
                Main.that.mainWindow.IsEnabled = false;
                StartTimerTicking();

            }
            else
            {
                Conf.WindowDragEnable = true;
                label_windowDragEnableStatus.Foreground = new SolidColorBrush(Colors.Green);
                label_windowDragEnableStatus.Content = "开启";
                Main.that.mainWindow.Alert.Visibility = Visibility.Visible;
                Main.that.mainWindow.IsEnabled = true;
                
            }
        }

        /// <summary>
        /// 透明度调整事件
        /// </summary>
        private void OpacRatioValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                Main.that.mainWindow.Opacity = opac_slider.Value / 100;
            }));
            StartTimerTicking();
        }

        private void SavingPreparationEvent(object sender, EventArgs e)
        {
            if(saveCountDown == 0)
            {
                saveSettingsTimer.Stop();
                saveWindowPosX = (int)Main.that.mainWindow.Left;
                saveWindowPosY = (int)Main.that.mainWindow.Top;
                saveWindowZoomRatio = (int)size_slider.Value;
                saveWindowOpac = (int)opac_slider.Value;
                IOEvent.SaveSettingsEvent(saveWindowPosX, saveWindowPosY, saveWindowZoomRatio, saveWindowOpac);
                SettingBoxFadeOut();

            }
            else
            {
                saveCountDown --;
            }
        }

        public static void StartTimerTicking()
        {
            saveCountDown = 2;
            saveSettingsTimer.Start();
            
        }

        public async void SettingBoxFadeOut()
        {
            if(saveCountDown == 0)
            {
                DoubleAnimation animationFadeIn = new DoubleAnimation();
                DoubleAnimation animationFadeOut = new DoubleAnimation();

                animationFadeOut.From = 1;
                animationFadeOut.To = 0;
                animationFadeIn.From = 0;
                animationFadeIn.To = 1;

                animationFadeOut.Duration = TimeSpan.FromMilliseconds(500);
                animationFadeIn.Duration = TimeSpan.FromMilliseconds(500);

                savingTitle.BeginAnimation(GroupBox.OpacityProperty, animationFadeIn);
                
                await Task.Delay(2500);
                savingTitle.BeginAnimation(GroupBox.OpacityProperty, animationFadeOut);
            }

        }
    }
}
