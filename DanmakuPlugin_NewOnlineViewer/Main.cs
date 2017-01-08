using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanmakuPlugin_NewOnlineViewer
{
    public class Main : BilibiliDM_PluginFramework.DMPlugin
    {
        internal static Main that;
        /// <summary>
        /// 主插件本体，方便插件其他部分代码调用
        /// </summary>
        
        public Main()
        {
            that = this;

            Conf.Init();
            ///<summary>
            ///初始化插件    
            /// </summary>
        }



    }
}
