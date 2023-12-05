using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;


namespace Eu.Europa.Ec.Olaf.Gmsr.Reports
{
    public partial class Users : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems4);
        }

        #region Unknown users report

        protected void lbShowUnknownUsers_Click(object sender, EventArgs e)
        {
            Panel4.Visible = !Panel4.Visible;
            btnExportItems4.Visible = !btnExportItems4.Visible;
            lbShowUnknownUsers.Text = "Show unknown users";
            if (Panel4.Visible)
            {
                gvItems4.DataSource = GmsrBLL.UnknownUserBL.GetAll();
                gvItems4.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems4);
                lblItems4Count.Text = gvItems4.Rows.Count.ToString();
                lbShowUnknownUsers.Text = "Hide unknown users";
            }
            up4.Update();
        }

        protected void btnExportItems4_Click(object sender, ImageClickEventArgs e)
        {
            Utilities.ExportToExcel(gvItems4, Response);
        }

        #endregion

        #region AD/HR Discrepancies report

        protected void lkbtnDiscrepancies_Click(object sender, EventArgs e)
        {
            Panel6.Visible = !Panel6.Visible;
            lkbtnDiscrepancies.Text = "Show AD/HR group discrepancies";
            if (Panel6.Visible)
            {
                gvItems6.DataSource = UnitAndActiveDirectoryAssociationBL.GetNegatives();
                gvItems6.DataBind();
                lblItems6Count.Text = gvItems6.Rows.Count.ToString();
                ((MasterPage)Master).SetGridviewProperties(gvItems6);
                lkbtnDiscrepancies.Text = "Hide AD/HR group discrepancies";
            }


            up6.Update();
        }

        protected void ibtnExportDiscrepancies_Click(object sender, ImageClickEventArgs e)
        {
            Utilities.ExportToExcel(gvItems6, Response);
        }

        #endregion

        #endregion

        #region Public Methods

        public override void VerifyRenderingInServerForm(Control control)
        {
            // This override is needed to make the export to excel work
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }

        #endregion
















































    }
}