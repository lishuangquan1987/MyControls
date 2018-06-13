﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace tainicom.TreeViewEx
{
    internal static class TreeViewElementFinder
    {
        internal static TreeViewExItem FindNext(TreeViewExItem treeViewItem, bool visibleOnly)
        {
            // find first child
            if (treeViewItem.IsExpanded || !visibleOnly)
            {
                TreeViewExItem item = GetFirstVirtualizedItem(treeViewItem);
                if (item != null)
                {
                    if (item.IsEnabled)
                    {
                        if (visibleOnly && !item.IsVisible)
                            return FindNext(item, visibleOnly);
                        else
                            return item;
                    }
                    else
                    {
                        return FindNext(item, visibleOnly);
                    }
                }
            }

            // find next sibling
            TreeViewExItem sibling = FindNextSiblingRecursive(treeViewItem) as TreeViewExItem;
            if (sibling == null)
                return null;
            if (!visibleOnly || sibling.IsVisible)
                return sibling;
            return null;
        }

        private static TreeViewExItem GetFirstVirtualizedItem(TreeViewExItem treeViewItem)
        {
            for (int i = 0; i < treeViewItem.Items.Count; i++)
            {
                TreeViewExItem item = treeViewItem.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewExItem;
                if (item != null) return item;

            }

            return null;
        }

        internal static ItemsControl FindNextSibling(ItemsControl itemsControl)
        {
            ItemsControl parentIc = ItemsControl.ItemsControlFromItemContainer(itemsControl);
            if (parentIc == null) return null;
            int index = parentIc.ItemContainerGenerator.IndexFromContainer(itemsControl);
            return parentIc.ItemContainerGenerator.ContainerFromIndex(index + 1) as ItemsControl; // returns null if index to large or nothing found
        }

        internal static ItemsControl FindNextSiblingRecursive(ItemsControl itemsControl)
        {
            ItemsControl parentIc = ItemsControl.ItemsControlFromItemContainer(itemsControl);
            if (parentIc == null) return null;
            int index = parentIc.ItemContainerGenerator.IndexFromContainer(itemsControl);
            if (index < parentIc.Items.Count - 1)
            {
                return parentIc.ItemContainerGenerator.ContainerFromIndex(index + 1) as ItemsControl; // returns null if index to large or nothing found
            }

            return FindNextSiblingRecursive(parentIc);
        }

        /// <summary>
        /// Returns the first item. If tree is virtualized, it is the first realized item.
        /// </summary>
        /// <param name="treeView">The tree.</param>
        /// <returns>Returns a TreeViewExItem.</returns>
        internal static TreeViewExItem FindFirst(TreeViewEx treeView, bool visibleOnly)
        {
            for (int i = 0; i < treeView.Items.Count; i++)
            {
                var item = treeView.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewExItem;
                if (item == null) continue;
                if (!visibleOnly || item.IsVisible) return item;
            }

            return null;
        }

        /// <summary>
        /// Returns the last item. If tree is virtualized, it is the last realized item.
        /// </summary>
        /// <param name="treeView">The tree.</param>
        /// <returns>Returns a TreeViewExItem.</returns>
        internal static TreeViewExItem FindLast(TreeViewEx treeView, bool visibleOnly)
        {
            for (int i = treeView.Items.Count - 1; i >= 0; i--)
            {
                var item = treeView.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewExItem;
                if (item == null) continue;
                if (!visibleOnly || item.IsVisible) return item;
            }

            return null;
        }

        /// <summary>
        /// Returns all items in tree recursively. If virtualization is enabled, only realized items are returned.
        /// </summary>
        /// <param name="treeView">The tree.</param>
        /// <param name="visibleOnly">True if only visible items should be returned.</param>
        /// <returns>Returns an enumerable of items.</returns>
        internal static IEnumerable<TreeViewExItem> FindAll(TreeViewEx treeView, bool visibleOnly)
        {
            TreeViewExItem currentItem = FindFirst(treeView, visibleOnly);
            while (currentItem != null)
            {
                if (!visibleOnly || currentItem.IsVisible) yield return currentItem;
                currentItem = FindNext(currentItem, visibleOnly);
            }
        }
    }
}
