using System;
using System.Collections.Generic;
using System.Text;

namespace Problem_01
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                return @"Server=DESKTOP-533LOVH\SQLEXPRESS;Integrated Security=true";
            }
        }
    }
}
