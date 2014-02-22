using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RJLou
{
    public class Constants
    {
        public static readonly string DSN = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
    }
}