using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading.Tasks;
using System.Threading;

namespace DanmakuPlugin_NewOnlineViewer
{
    internal static partial class Conf
    {
        #region 初始化插件信息
        internal static readonly string PluginName = "新·在线人数视窗";
        internal static readonly string PluginAuthor = "Const";
        internal static readonly string PluginVersion = "1.0.1";
        internal static readonly string PluginDescription = "在桌面部署一个置顶窗口浏览当前在线观众人数";
        internal static readonly string PluginContact = "xzso3@outlook.com";
        #endregion

        #region 初始化插件部分窗口属性参数
        /// <summary>
        /// 初始化变量是否可以窗口穿透
        /// 默认可穿透
        /// </summary>
        public static bool MouseBypass = true;

        /// <summary>
        /// 初始化变量是否可以拖曳窗口
        /// 默认不可拖曳
        /// </summary>
        public static bool WindowDragEnable = false;

        /// <summary>
        /// 初始化窗口缩放比例
        /// </summary>
        public static double ZoomRatio = 1;
        /// <summary>
        /// 初始化窗口位置 X轴
        /// </summary>
        public static int WindowResX = 0;
        /// <summary>
        /// 初始化窗口位置 X轴
        /// </summary>
        public static int WindowResY = 0;
        /// <summary>
        /// 初始化窗口透明度
        /// </summary>
        public static int WindowOpacity = 100;

        public static int ZoomRaw = 50;

        #endregion

        #region 初始化Windows API
        internal const uint WS_EX_TRANSPARENT = 0x20;
        internal const int GWL_EXSTYLE = -20;
        internal const int _maxCapacity = 100;
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        internal static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        internal static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        #endregion

        internal static void Init()
        {
            ///<summary>
            ///初始化插件信息
            ///</summary>
            InitPluginConfigs();
            AsyncInitalizationEventBind();


            /// <summary>
            /// 设置鼠标穿透
            /// </summary>
            Main.that.mainWindow.SourceInitialized += delegate
            {
                var hwnd = new WindowInteropHelper(Main.that.mainWindow).Handle;
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            };

            IOEvent.ReadSettingsEvent();

            double resW = 140;
            double resH = 45;
            double fontSizeTitle = 9;
            double fontSizeValue = 30;
            Console.WriteLine("[DEBUG] ZoomRatio: " + ZoomRatio.ToString());
            resW = resW * Conf.ZoomRatio;
            resH = resH * Conf.ZoomRatio;
            fontSizeTitle = fontSizeTitle * Conf.ZoomRatio;
            fontSizeValue = fontSizeValue * Conf.ZoomRatio;
            Main.that.mainWindow.Width = resW;
            Main.that.mainWindow.Height = resH;
            Main.that.mainWindow.labelViewer.FontSize = fontSizeTitle;
            Main.that.mainWindow.textCount.FontSize = fontSizeValue;
            Main.that.mainWindow.Alert.FontSize = fontSizeTitle * 0.8;
            Main.that.mainWindow.Left = WindowResX;
            Main.that.mainWindow.Top = WindowResY;
            Main.that.mainWindow.Opacity = WindowOpacity / 100;
            Main.that.controlWindow.opac_slider.Value = WindowOpacity;
            Main.that.controlWindow.size_slider.Value = ZoomRaw;
        }
            




        internal static void InitPluginConfigs()
        {
            Main transferPluginConf = Main.that;
            transferPluginConf.PluginAuth = PluginAuthor;
            transferPluginConf.PluginCont = PluginContact;
            transferPluginConf.PluginDesc = PluginDescription;
            transferPluginConf.PluginName = PluginName;
            transferPluginConf.PluginVer = PluginVersion;
            ///<summary>
            ///传递初始化插件信息
            ///</summary>
        }





        internal static void AsyncInitalizationEventBind()
        {
            Main transferPluginConf = Main.that;
            transferPluginConf.ReceivedDanmaku += DanmukuEvents.ReceivedDanmaku;
            transferPluginConf.Connected += DanmukuEvents.ConnectedEvent;
            transferPluginConf.Disconnected += DanmukuEvents.DisconnectedEvent;
            transferPluginConf.ReceivedRoomCount += DanmukuEvents.ReceivedRoomCount;
            ///<summary>
            ///传递插件对应响应函数
            ///</summary>
        }





    }
}