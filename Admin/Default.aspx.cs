using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou
{
    public partial class Default : System.Web.UI.Page
    {
        Case thisCase;
        List<Case> cases;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        protected internal void BindData()
        {
            cases = Case.GetCases();
            CasesRepeater.DataSource = cases;
            CasesRepeater.DataBind();
        }

        protected internal void CasesRepeater_Databind(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}