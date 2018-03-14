using System;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)

        {
            string gameSettingFilesLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
            if (Directory.Exists(gameSettingFilesLocation))
            {
                MessageBox.Show("Rua,存在的");

            }
            else
            {
                MessageBox.Show("你连彩虹都没安装，真鸡儿丢人，删软件吧兄弟");
                //TODO:丢人的没安装彩虹 强制播放一次HOP和魔王医生来进行谴责 同时背景循环机枪哥鬼畜
            }

            string[] emo = Directory.GetDirectories(gameSettingFilesLocation);
            MessageBox.Show("发现" + emo.Length + "个垃圾玩家");
            object[] array = Directory.GetDirectories(gameSettingFilesLocation);

            //Listbbox1.Items.Add(array[0]);
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


        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string IniReadValue(string Section, string Key,string path)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }

    }
}
