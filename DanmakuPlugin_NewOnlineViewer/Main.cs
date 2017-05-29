using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;

namespace DanmakuPlugin_NewOnlineViewer
{
    public class Main : BilibiliDM_PluginFramework.DMPlugin
    {
        internal static Main that;
        /// <summary>
        /// 主插件本体，方便插件其他部分代码调用
        /// </summary>


        private const uint WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;
        private const int _maxCapacity = 100;
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);



        public Main()
        {
            that = this;

            Conf.Init();
            mainWindow.Show();
            mainWindow.Hide();

            new Thread(() =>
            {
                try
                {
                    var plugin = this;
                    var versionChecker = new VersionChecker("NewOnlineViewer");
                    if (versionChecker.FetchInfo())
                    {
                        if (versionChecker.hasNewVersion(plugin.PluginVer))
                        {
                            string info = "插件有新版本了！最新版本:" + versionChecker.Version + "  当前版本:" + plugin.PluginVer;
                            info += "  更新时间:" + versionChecker.UpdateDateTime.ToString("yyyy.MM.dd HH:mm");
                            info += "\r\n" + "下载地址：" + versionChecker.WebPageUrl;
                            info += "\r\n" + versionChecker.UpdateDescription;
                            plugin.Log(info);
                        }
                    }
                    else
                    {
                        plugin.Log("版本检查失败：" + versionChecker.lastException.Message);
                    }

                }
                catch (Exception)
                { }
            });
        ///<summary>
        ///初始化插件    
        /// </summary>

        }

        public ViewerWindow mainWindow = new ViewerWindow();
        public ControlWindow controlWindow = new ControlWindow();

        public override void Start()
        {
            base.Start();
            DanmukuEvents.UpdateRoomCount(DanmukuEvents._roomCount);
            mainWindow.Show();
            
        }


        public override void Stop()
        {
            base.Stop();
            mainWindow.Hide();

        }


        public override void Admin()
        {
            this.controlWindow.Show();            
        }

        public void MouseBypassEnable ()
        {
            if(Conf.MouseBypass == true)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    var hwnd = new WindowInteropHelper(that.mainWindow).Handle;
                    var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                    SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
                }));
            }
            else
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    var hwnd = new WindowInteropHelper(that.mainWindow).Handle;
                    var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                    SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
                }));
            }
        }


    }
}
