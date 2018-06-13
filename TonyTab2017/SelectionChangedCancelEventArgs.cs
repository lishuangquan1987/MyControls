using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TonyTab2017
{
   public class SelectionChangedCancelEventArgs:CancelEventArgs
    {
       public TonyTabItem OldSelectedItem { get; private set; }
       public TonyTabItem NewSelectedItem { get; private set; }
       public SelectionChangedCancelEventArgs(TonyTabItem oldItem,TonyTabItem newItem)
       {
           OldSelectedItem = oldItem;
           NewSelectedItem = newItem;
       }
    }
}
