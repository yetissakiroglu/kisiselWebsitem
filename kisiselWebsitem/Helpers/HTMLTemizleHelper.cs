using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace kisiselWebsitem.Helpers
{
    public class HTMLTemizleHelper
    {
        public static string HTMLTemizle(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
    }
}