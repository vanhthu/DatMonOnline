using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DatMonOnline
{
    public class KetNoi
    {
        public static string LayChuoiKetNoi()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

    }

    public class Utils
    {
        public static bool IsValidExtention(string fileName)
        {
            bool isValid = false;
            string[] fileExtention = { ".jpg", ".png", ".jpeg"};

            for(int i=0; i <= fileExtention.Length - 1; i++)
            {
                if (fileName.Contains(fileExtention[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
    }
}