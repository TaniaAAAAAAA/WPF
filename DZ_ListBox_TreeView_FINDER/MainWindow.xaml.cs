using System;
using System.Collections.Generic;
using System.IO;
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


namespace DZ_ListBox_TreeView_FINDER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            TreeViewItem root = new TreeViewItem() {Header= "Desktop" };
        public MainWindow()
        {
            InitializeComponent();
            treeView.Items.Add(root);
            LoadDirectories(root, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void LoadDirectories( TreeViewItem item, string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);

            foreach (var d in dir.GetDirectories())
            {
                var subItem = new TreeViewItem() { Header = d.Name, Tag = d.FullName };
                // subItem.Selected+=Su
                item.Items.Add(subItem);

                try
                {
                    if(d.GetDirectories().Length>0)
                    {
                        LoadDirectories(subItem, d.FullName);
                    }
                }
                catch { }
            }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listBox.Items.Clear();
            foreach (TreeViewItem el in (treeView.SelectedItem as TreeViewItem).Items)
            {
                listBox.Items.Add(el.Header);
            }

            try
            {

                FileInfo f = new FileInfo((treeView.SelectedItem as TreeViewItem).Tag.ToString());


                lblName.Content = f.Name;
                lblDateCreate.Content = f.CreationTime.ToString();
                lblSize.Content = f.FullName.Length.ToString();
                lblDateChange.Content = f.LastWriteTime.ToString();

            }
            catch { }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            
            Directory.CreateDirectory(@"C:\Users\Tanuha\Desktop\newfolder");
            
        }
    }
}
