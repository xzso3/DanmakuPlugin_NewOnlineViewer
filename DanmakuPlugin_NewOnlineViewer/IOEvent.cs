using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DanmakuPlugin_NewOnlineViewer
{
    class IOEvent
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string initSettingsContent = "0/0/50/100";

        public static void SaveSettingsEvent(int windowPosX, int windowPosY, int windowZoomRatio, int windowOpacity)
        {
            int preload = new IOEvent().FileIsExist();
            string savedSettingsContent = windowPosX.ToString() + "/" + windowPosY.ToString() + "/" + windowZoomRatio.ToString() + "/" + windowOpacity.ToString();
            File.WriteAllText(new IOEvent().path + "/Settings.novs", savedSettingsContent, Encoding.UTF8);
        }

        public static void ReadSettingsEvent()
        {
            int preload = new IOEvent().FileIsExist();
            if(preload == 0)
            {

            }
            else
            {
                var Settings = File.ReadAllText(new IOEvent().path + "/Settings.novs");
                string[] SettingsArray = Settings.Split('/');
                Conf.WindowResX = int.Parse(SettingsArray[0]);
                Conf.WindowResY = int.Parse(SettingsArray[1]);
                Conf.ZoomRatio = int.Parse(SettingsArray[2]) / 50;
                Conf.WindowOpacity = int.Parse(SettingsArray[3]);
            }
        }

        private int FileIsExist()
        {
            if(File.Exists(path + "/Settings.novs"))
            {
                return 1;
            }
            else
            {
                var FileAction = File.Create(path+"/Settings.novs");
                FileAction.Close();
                File.WriteAllText(path + "/Settings.novs", initSettingsContent, Encoding.UTF8);
                return 0;
            }
        }
    }
}
