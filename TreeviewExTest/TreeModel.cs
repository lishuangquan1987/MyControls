using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TreeviewExTest
{
   public class TreeModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; OnPropertyChanged("DisplayName"); }
        }
        private string absolutePath;

        public string AbsolutePath
        {
            get { return absolutePath; }
            set { absolutePath = value; OnPropertyChanged("AbsolutePath"); }
        }
        private ObservableCollection<TreeModel> children = new ObservableCollection<TreeModel>();
        public ObservableCollection<TreeModel> Children
        {
            get { return children; }
            set { children = value; OnPropertyChanged("Children"); }
        }
        private TreeModel parent;
        public TreeModel Parent
        {
            get { return parent; }
            set { parent = value; OnPropertyChanged("Parent"); }
        }
        private System.Windows.Controls.ContextMenu contextMenu = null;

        public System.Windows.Controls.ContextMenu ContextMenu
        {
            get { return contextMenu; }
            set { contextMenu = value; OnPropertyChanged("ContextMenu"); }
        }
        private bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged("IsSelected"); }
        }

    }
}
