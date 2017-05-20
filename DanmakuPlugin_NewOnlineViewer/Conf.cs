using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace DanmakuPlugin_NewOnlineViewer
{
    internal static partial class Conf
    {
        #region 初始化插件信息
        internal static readonly string PluginName = "新·在线人数视窗";
        internal static readonly string PluginAuthor = "Const";
        internal static readonly string PluginVersion = "alpha-p1";
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
            InitPluginConfigs();
            AsyncInitalizationEventBind();
            ///<summary>
            ///初始化插件信息
            ///</summary>


            Main.that.mainWindow.SourceInitialized += delegate
            {
                var hwnd = new WindowInteropHelper(Main.that.mainWindow).Handle;
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            };
            /// <summary>
            /// 设置鼠标穿透
            /// </summary>
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