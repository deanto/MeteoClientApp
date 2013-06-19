using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MeteoServer.Components;
using MeteoServer.Components.UserManagement;
using MeteoServer.Objects;

using MeteoServer.Components.WeatherCalculating;

using MeteoServer.Components.FileManagement;


namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IUserID current;

        FileManager fm;

        FileTree files;

        List<string> tree;


        public MainWindow()
        {
            InitializeComponent();

            IUserManagement userManagement = Fabric.GetUserManagement();

            current = userManagement.GetProfile();

            ShowFileList();

        }

        private void ShowFileList()
        {
            fm = Fabric.GetFileManager();
            files = fm.GetFiles(current);

            tree = new List<string>();
            FillTreeList(files.FileList);

            string show = "";
            for (int i = 0; i < tree.Count; i++)
                if (tree[i].Contains(".txt"))show += tree[i] + "\n";

            System.IO.MemoryStream stream = new System.IO.MemoryStream(ASCIIEncoding.Default.GetBytes(show));
            richTextBox1.Selection.Load(stream, DataFormats.Text);
            
        }

        private void FillTreeList(Directorys listtree)
        {
            // заполнить List<string> tree из  FileTree files;

            for (int i = 0; i < listtree.subdirectorys.Count; i++)
                tree.Add(listtree.subdirectorys[i].path);


            for (int i = 0; i < listtree.files.Count; i++)
                tree.Add(listtree.files[i]);


            for (int i = 0; i < listtree.subdirectorys.Count; i++)
                FillTreeList(listtree.subdirectorys[i]);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            WeatherDisplay.wpfDisplay wpfd = new WeatherDisplay.wpfDisplay();
            wpfd.ShowWeather(current, textBox1.Text, textBox2.Text);
            
        }

    }
}
