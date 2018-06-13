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
using System.IO;

namespace TreeviewExTest
{
    using IOPath = System.IO.Path;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            contextMenu = new ContextMenu();
            MenuItem item = new MenuItem();
            item.Header = "打开";
            item.Click += item_Click;
            contextMenu.Items.Add(item);
            
        }
       
        void item_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private TreeModel GetTreeModel(string path)
        {
            if (!Directory.Exists(path))
                return null;
            TreeModel topModel = new TreeModel();
            topModel.DisplayName = IOPath.GetFileName(path);
            topModel.AbsolutePath = path;
            topModel.Parent = null;
            topModel.ContextMenu = contextMenu;
            GetChildModel(topModel);
            return topModel;
        }
        private void GetChildModel(TreeModel topModel)
        {
            if (File.Exists(topModel.AbsolutePath))
            {
                //topModel.ContextMenu = contextMenu;
                return;
            }
            else
            {
                //获取文件和文件夹
                string[] paths = Directory.GetFileSystemEntries(topModel.AbsolutePath, "*", SearchOption.TopDirectoryOnly);
                if (paths != null && paths.Length > 0)
                {
                    for (int i = 0; i < paths.Length; i++)
                    {
                        TreeModel childModel = new TreeModel();
                        childModel.AbsolutePath = paths[i];
                        childModel.DisplayName = IOPath.GetFileName(childModel.AbsolutePath);
                        childModel.Parent = topModel;
                        topModel.Children.Add(childModel);
                        GetChildModel(childModel);
                    }
                }
            }
        }
        private ContextMenu contextMenu = null;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fb = new System.Windows.Forms.FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var itemSource = new List<TreeModel>() { GetTreeModel(fb.SelectedPath) };
                this.treeview.ItemsSource = itemSource;
                this.treeviewEx.ItemsSource = itemSource;
            }
        }

        private void Grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        public void TreeViewExItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //tainicom.TreeViewEx.TreeViewExItem item = sender as tainicom.TreeViewEx.TreeViewExItem;
            //e.Handled = true;
            //TreeModel model = item.Header as TreeModel;
            //MessageBox.Show(model.DisplayName);

        }
        public void TreeViewExItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            tainicom.TreeViewEx.TreeViewExItem item = sender as tainicom.TreeViewEx.TreeViewExItem;
            e.Handled = true;
            TreeModel model = item.Header as TreeModel;
            this.treeviewEx.SelectedItems.Clear();
            //MessageBox.Show(model.DisplayName);
            model.IsSelected = true;
            //item.ContextMenu = model.ContextMenu;
            

        }

        private void treeviewEx_OnSelecting(object sender, tainicom.TreeViewEx.SelectionChangedCancelEventArgs e)
        {
            
        }
    }
}
