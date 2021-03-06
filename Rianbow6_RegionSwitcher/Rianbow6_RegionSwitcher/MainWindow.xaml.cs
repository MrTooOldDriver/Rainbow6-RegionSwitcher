﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Drawing;

namespace Rianbow6_RegionSwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPath();
        }

        public void LoadPath()
        {
            string gameSettingFilesLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
            if (Directory.Exists(gameSettingFilesLocation))
            {
                StageLable.Content = "很好，你安装了彩虹";
                StageLable.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                MessageBox.Show("RUA,你连彩虹都没安装，换毛区?","丢人的");
                //TODO:丢人的没安装彩虹 强制播放一次HOP和魔王医生来进行谴责 同时背景循环机枪哥鬼畜
                StageLable.Content = "你根本不是彩虹玩家！";
                StageLable.Foreground = new SolidColorBrush(Colors.Red);
            }

            string[] emo = Directory.GetDirectories(gameSettingFilesLocation);
            //MessageBox.Show("发现" + emo.Length + "个垃圾玩家");
            FoundedLabel.Content = emo.Length + "玩家";
            object[] array = Directory.GetDirectories(gameSettingFilesLocation);
            Listbbox1.ItemsSource = array;
        }

        private void Listbbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectArray = Listbbox1.SelectedItem;
            string rua = selectArray.ToString();
            //MessageBox.Show(new DirectoryInfo(rua).Name);
            PlayerIDLable.Content = new DirectoryInfo(rua).Name;
            rua = rua + "\\GameSettings.ini";
            RegionLabel.Content = IniReadValue("ONLINE", "DataCenterHint", rua);
            LoadSwiterBox();
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        public string IniReadValue(string Section, string Key,string path)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }

        public void IniWriteValue(string Section, string Key, string Value,string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }



        private void SwiterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectArray = Listbbox1.SelectedItem;
            string rua = selectArray.ToString();
            rua = rua + "\\GameSettings.ini";
            IniWriteValue("ONLINE","DataCenterHint",SwiterBox.SelectedItem.ToString(),rua);
            MessageBox.Show("区域已经修改到"+ SwiterBox.SelectedItem.ToString(),"成功");
            RegionLabel.Content = IniReadValue("ONLINE", "DataCenterHint", rua);
        }

        public void LoadSwiterBox()
        {
            object[] Regions = new object[12];
            Regions[0] = "default";
            Regions[1] = "eus";
            Regions[2] = "cus";
            Regions[3] = "scus";
            Regions[4] = "wus";
            Regions[5] = "sbr";
            Regions[6] = "neu";
            Regions[7] = "weu";
            Regions[8] = "eas";
            Regions[9] = "seas";
            Regions[10] = "eau";
            Regions[11] = "wja";
            SwiterBox.ItemsSource = Regions;
        }
    }
}
