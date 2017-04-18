using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanmakuPlugin_NewOnlineViewer
{
    internal static partial class Conf
    {
        internal static readonly string PluginName          = "新·在线人数视窗";
        internal static readonly string PluginAuthor        = "Const";
        internal static readonly string PluginVersion       = "alpha-p1";
        internal static readonly string PluginDescription   = "在桌面部署一个置顶窗口浏览当前在线观众人数";
        internal static readonly string PluginContact       = "xzso3@outlook.com";
        ///<summary>
        ///插件基本信息
        ///</summary> 




        internal static void Init()
        {
            InitPluginConfigs();
            AsyncInitalizationEventBind();
            System.Windows.MessageBox.Show("2333-1");
            ///<summary>
            ///初始化插件信息
            ///</summary>

        }

        private static void InitPluginConfigs()
        {
            ///<summary>
            ///传递初始化插件信息
            ///</summary>
            Main transferPluginConf         = Main.that;
            transferPluginConf.PluginAuth   = PluginAuthor;
            transferPluginConf.PluginCont   = PluginContact;
            transferPluginConf.PluginDesc   = PluginDescription;
            transferPluginConf.PluginName   = PluginName;
            transferPluginConf.PluginVer    = PluginVersion;
           
        }

        private static void AsyncInitalizationEventBind()
        {
            Main transferPluginConf = Main.that;
            transferPluginConf.ReceivedDanmaku += DanmukuEvents.ReceivedDanmaku;
            transferPluginConf.Connected += DanmukuEvents.ConnectedEvent;
            transferPluginConf.Disconnected += DanmukuEvents.DisconnectedEvent;
            transferPluginConf.ReceivedRoomCount += DanmukuEvents.ReceivedRoomCount;

        }
    }
}
