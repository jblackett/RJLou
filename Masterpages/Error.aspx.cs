using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou.Masterpages
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Exception LastOneError = Server.GetLastError();

                if (LastOneError != null)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/logs/Log_" + DateTime.Now.Year + ".txt");

                    if (File.Exists(filePath))
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("-------------------START-------------" + DateTime.Now);
                            writer.WriteLine(LastOneError.ToString());
                            writer.WriteLine("-------------------END-------------" + DateTime.Now);
                        }
                    }
                    else
                    {
                        StreamWriter writer = File.CreateText(filePath);
                        writer.WriteLine("-------------------START-------------" + DateTime.Now);
                        writer.WriteLine(LastOneError.ToString());
                        writer.WriteLine("-------------------END-------------" + DateTime.Now);
                    }
                }
            }
            catch { }
        }
    }
}