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
    }
}
