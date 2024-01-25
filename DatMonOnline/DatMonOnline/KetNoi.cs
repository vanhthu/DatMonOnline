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
}