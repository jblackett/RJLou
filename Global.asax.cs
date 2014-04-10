using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace RJLou
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
        }

        /// <summary>
        /// Returns the file extension for a given file name.
        /// </summary>
        /// <param name="FileName">The full file name (e.g. "document.docx")</param>
        /// <param name="IncludeDot">Include the "." prefix in the return.  Default = true.</param>
        /// <returns>The extension for the file (i.e. ".docx")</returns>
        internal static string GetFileExtension(string FileName, bool IncludeDot = true)
        {
            string extension = "";

            if (!FileName.Contains('.'))
                extension = FileName;
            else
            {
                if (IncludeDot)
                    extension = FileName.Remove(0, FileName.LastIndexOf('.'));
                else
                    extension = FileName.Remove(0, FileName.LastIndexOf('.') + 1);
            }

            return extension;
        }
    }
}