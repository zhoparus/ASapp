using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    public class ConnectionDB
    {
        public static string connectionDB()
        {
            return @"Data Source=DESKTOP-R71RRJI\EVA;Initial Catalog=ArealSourceDB;Integrated Security=True;TrustServerCertificate=True";
        }
    }
}