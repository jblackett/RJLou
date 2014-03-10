using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RJLou.Classes;

namespace RJLou.Admin
{
    public partial class UOR : System.Web.UI.Page
    {
        Charge thisCharge;
        List<Charge> charges;
        int PersonID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try { PersonID = Convert.ToInt32(Session["PersonID"]); }
            catch { }

            if (PersonID <= 0)
            {
                Response.Redirect("Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                charges = Charge.GetCharges();
                ChargesRepeater.DataSource = charges;
                ChargesRepeater.DataBind();
            }
        }

        protected internal void LoadCharge(object sender, EventArgs e)
        {
            int ChargeID = -1;
            int.TryParse(((LinkButton)sender).CommandArgument, out ChargeID);

            thisCharge = Charge.Get(ChargeID);

            //BindData();
            LoadHeader();
        }

        protected internal void LoadHeader()
        {
            ChargeID.Text = thisCharge.ChargeID.ToString();
            ChargeID.ReadOnly = true;
            KRS_Code.Text = thisCharge.KRSCode.ToString();
            UOR_Code.Text = thisCharge.UORCode.ToString();
            Description.Text = thisCharge.Description.ToString();

            MainContainer.Visible = true;
        }

        protected internal void SaveCharge(object sender, EventArgs e)
        {
            int KRSHolder;
            int chargeID = int.Parse(ChargeID.Text);
            thisCharge = Charge.Get(chargeID);
            thisCharge.UORCode = UOR_Code.Text;
            thisCharge.Description = Description.Text;
            if (int.TryParse(KRS_Code.Text, out KRSHolder) && KRSHolder != 0)
            {
                thisCharge.KRSCode = KRSHolder;
            }
            else
                thisCharge.KRSCode = 000000;



            thisCharge.UpdateUOR();
            CaseUpdatedPanel.CssClass += " visible";
        }

        protected internal void ChargesRepeater_Databind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label thisLabel = (Label)e.Item.FindControl("ChargeID");
                Charge currentCharge = (Charge)e.Item.DataItem;

                string ChargeCode;


                ChargeCode = currentCharge.Description;

                thisLabel.Text = ChargeCode;
            }
        }
    }
}