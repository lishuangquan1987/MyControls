﻿namespace BindingSample
{
    #region

    using BindingSample.Model;
    using System.Windows;

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        #region Constructors and Destructors

        Node firstNode;
        Node first1;
        Node first2;
        Node first11;

        public MainWindow()
        {
            firstNode = new Node { Name = "element" };
            first1 = new Node { Name = "element1" };
            first2 = new Node { Name = "element2" };
            first11 = new Node { Name = "element11" };
            var first12 = new Node { Name = "element12" };
            var first13 = new Node { Name = "element13" };
            var first14 = new Node { Name = "element14" };
            var first15 = new Node { Name = "element15" };
            var first131 = new Node { Name = "element131" };
            var first132 = new Node { Name = "element132" };

            firstNode.Children.Add(first1);
            firstNode.Children.Add(first2);
            first1.Children.Add(first11);
            first1.Children.Add(first12);
            first1.Children.Add(first13);
            first1.Children.Add(first14);
            first1.Children.Add(first15);
            first13.Children.Add(first131);
            first13.Children.Add(first132);

            // for (int i = 0; i < 1000; i++)
            // {
            // first.Children.Add(new Node { Name = "element" + (i + 2) });
            // }
            DataContext = firstNode;

            InitializeComponent();

            this.KeyDown += MainWindow_KeyDown;
        }

        void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key==System.Windows.Input.Key.Space)
            {
                var newNode = new Node { Name = "elementNew" };
                firstNode.Children.Add(newNode);                
                leftTree.BringIntoView(newNode);
            }
        }

        #endregion

        private void leftTree_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }
    }
}