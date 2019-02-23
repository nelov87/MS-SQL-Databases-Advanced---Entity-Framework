using System;
using System.Collections.Generic;
using System.Text;

namespace Problem_02
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                return @"Server=DESKTOP-533LOVH\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";
            }
        }
    }
}
