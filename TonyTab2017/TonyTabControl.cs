using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;



namespace TonyTab2017
{
   public class TonyTabControl:Selector
    {
       static TonyTabControl()
       {
           DefaultStyleKeyProperty.OverrideMetadata(typeof(TonyTabControl), new FrameworkPropertyMetadata(typeof(TonyTabControl)));
       }
       public new event Action<object, SelectionChangedCancelEventArgs> SelectionChanged = null;
      
      internal void tonyTabItem_Selected(SelectionChangedCancelEventArgs e)
       {
           if (SelectionChanged != null)
           {
               foreach (var method in SelectionChanged.GetInvocationList())
               {
                   method.Method.Invoke(method.Target, new object[] { this, e });
                   if (e.Cancel)
                       break;
               }
           }
       }

      public new int SelectedIndex
      {
          get 
          {
              int count = -1;
              if (this.Items.Count == 0)
                  return count;
              else
              {
                  int index = 0;
                  foreach (var item in this.Items)
                  {
                      TonyTabItem tonyTabItem = item as TonyTabItem;
                      if (tonyTabItem != null && tonyTabItem.IsSelected)
                      {
                          count = index;
                          break;
                      }
                      else
                      {
                          index++;
                      }
                  }
                  return count;
              }
          }
          set
          {
            if (this.Items.Count == 0||value>=this.Items.Count)
                return;              
            foreach (var item in this.Items)
            {
                TonyTabItem tonyTabItem = item as TonyTabItem;
                if (tonyTabItem != null)
                {
                    tonyTabItem.IsSelected = false;
                }
            }
            if (value != -1)
            {
                (this.Items[value] as TonyTabItem).IsSelected = true;
            }
             
          }
      }
      public new TonyTabItem SelectedItem
      {
          get
          {
              int index = this.SelectedIndex;
              if (index == -1)
                  return null;
              else
                  return this.Items[index] as TonyTabItem;
          }
      }

       protected override System.Windows.DependencyObject GetContainerForItemOverride()
       {
           return new TonyTabItem();
       }
       protected override bool IsItemItsOwnContainerOverride(object item)
       {
           return item is TonyTabItem;
       }
       
    }
}
