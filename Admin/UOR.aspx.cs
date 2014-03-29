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
            UOR_Code.Text = thisCharge.UORCode.ToString();
            Description.Text = thisCharge.Description.ToString();

            MainContainer.Visible = true;
        }

        protected internal void SaveCharge(object sender, EventArgs e)
        {
            int chargeID = int.Parse(ChargeID.Text);
            thisCharge = Charge.Get(chargeID);
            thisCharge.UORCode = UOR_Code.Text;
            thisCharge.Description = Description.Text;


            thisCharge.UpdateUOR();
            ChargeUpdatedPanel.CssClass += " visible";
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
        protected internal void CloseUpdatePanel()
        {
            ChargeUpdatedPanel.Visible = false;
        }

        protected internal void OpenNewChargeModalPanel(object Sender, EventArgs e)
        {
            NewChargeModalPanel.CssClass += " visible";
        }

        protected internal void CloseNewChargeModalPanel(object Sender, EventArgs e)
        {
            LinkButton CloseButton = Sender as LinkButton;
            Label statusLabel = (Label)CloseButton.Parent.FindControl("statusLabel");
            statusLabel.Text = "";
            NewChargeModalPanel.CssClass = "modal-background";
        }

        protected internal void NewCharge(object Sender, EventArgs e)
        {
            LinkButton AddButton = Sender as LinkButton;

            if (AddButton != null)
            {
                TextBox uorBox = (TextBox)AddButton.Parent.FindControl("uorTextbox");
                TextBox descriptionBox = (TextBox)AddButton.Parent.FindControl("descriptionTextbox");
                Label statusLabel = (Label)AddButton.Parent.FindControl("statusLabel");

                if (uorBox != null && descriptionBox != null)
                {
                    if(!(string.IsNullOrWhiteSpace(uorBox.Text) || string.IsNullOrWhiteSpace(descriptionBox.Text)))
                    {
                        Charge.Add(uorBox.Text, descriptionBox.Text);
                        ChargeUpdatedPanel.CssClass+= " visible";
                        NewChargeModalPanel.CssClass="modal-background";
                    }
                    else
                    {
                        statusLabel.Text="All fields must be filled out!";
                    }
                }
            }
        }
    }
}