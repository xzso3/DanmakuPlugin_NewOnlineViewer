﻿using System;
using BilibiliDM_PluginFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanmakuPlugin_NewOnlineViewer
{
    internal class DanmukuEvents
    {
        internal static void ReceivedDanmaku(object _sender, ReceivedDanmakuArgs _args)
        {
            Console.Write("Bullshit!");
        }

        internal static void DisconnectedEvent(object _sender, DisconnectEvtArgs _args)
        {
            
        }

        internal static void ConnectedEvent(object _sender, ConnectedEvtArgs _args)
        {

        }

        internal static void ReceivedRoomCount(object _sender, ReceivedRoomCountArgs _args)
        {

        }
    }
}
