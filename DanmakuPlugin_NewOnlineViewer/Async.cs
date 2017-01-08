using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DanmakuPlugin_NewOnlineViewer
{
    internal static class Async
    {
    ///<summary>
    ///弹幕姬事件异步处理
    /// </summary>
    
        private static void AsyncInitalizationEventBind()
        {
            Main MainInitalizationSetup                  = Main.that;
            MainInitalizationSetup.ReceivedDanmaku      += DanmukuEvents.ReceivedDanmaku;
            MainInitalizationSetup.Connected            += DanmukuEvents.ConnectedEvent;
            MainInitalizationSetup.Disconnected         += DanmukuEvents.DisconnectedEvent;
            MainInitalizationSetup.ReceivedRoomCount    += DanmukuEvents.ReceivedRoomCount;

        }

        
    }

}
