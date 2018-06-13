using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ucCodeEditor
{
    class KeysWordCMDAuto
    {
        static string[] CommandArrys = { "help","?","csc","clear","min","max","nor","exit","save","code","hide","new","exe",
                                       "winexe","dll","arg:","wd","show"};
        static AutocompleteMenu popupMenu;
        public static void CreatMenu(textEditor tb, ImageList imgList)
        {
            //create autocomplete popup menu
            popupMenu = new AutocompleteMenu(tb);

            popupMenu.Items.ImageList = imgList;
            BuildAutocompleteMenu();
        }

        private static void BuildAutocompleteMenu()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            foreach (var item in CommandArrys)
                items.Add(new AutocompleteItem(item) { ImageIndex = 4 });
            popupMenu.Items.SetAutocompleteItems(items);
        }
    }
}
