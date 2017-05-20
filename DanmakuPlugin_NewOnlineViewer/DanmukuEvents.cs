using System;
using BilibiliDM_PluginFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;


namespace DanmakuPlugin_NewOnlineViewer
{
    internal class DanmukuEvents
    {
        internal static string _roomCount = "LOAD...";


        internal static void ReceivedDanmaku(object _sender, ReceivedDanmakuArgs _args)
        {
            
        }

        internal static void DisconnectedEvent(object _sender, DisconnectEvtArgs _args)
        {
            
        }

        internal static void ConnectedEvent(object _sender, ConnectedEvtArgs _args)
        {
            UpdateRoomCount(_roomCount.ToString());
        }

        internal static void ReceivedRoomCount(object _sender, ReceivedRoomCountArgs _args)
        {
            _roomCount = _args.UserCount.ToString();
            UpdateRoomCount(_roomCount.ToString());           
        }

        internal static void UpdateRoomCount(string _count)
        {
            Main.that.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                Main.that.mainWindow.textCount.Text = _count.ToString();

            }));
            /// <summary>
            /// 与WPF窗口进行交互
            /// </summary>
        }
   

        
       internal static void ControlEvent()
        {

        }
    }
    
}
