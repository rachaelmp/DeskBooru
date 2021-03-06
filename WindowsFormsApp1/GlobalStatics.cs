using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeskBooruApp
{
    internal static class GlobalStatics
    {
        //keeps the array of file locations for use by multiple buttons.
        internal static List<string> ImageLocations = new List<string>();
        internal static int imageArrayindex;
        //this List will keep track of the current images tags, yet to be commited to the database.
        internal static List<string> currentTagsSingle = new List<string>();
        //this one is for the addFolder tab of the application, so they do not interfere with one another
        internal static List<string> currentTagsFolder = new List<string>(); 
    }
}
