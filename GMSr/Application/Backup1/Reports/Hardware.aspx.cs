using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;


namespace Eu.Europa.Ec.Olaf.Gmsr.Reports
{
    public partial class Hardware : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems3);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems5);
        }

        #region Unknown hardware report

        protected void lbShowUnknownHardware_Click(object sender, EventArgs e)
        {
            Panel3.Visible = !Panel3.Visible;
            btnExportItems3.Visible = !btnExportItems3.Visible;
            lbShowUnknownHardware.Text = "Show unknown hardware";
            if (Panel3.Visible)
            {
                gvItems3.DataSource = GmsrBLL.UnknownHardwareItemBL.GetAll();
                gvItems3.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems3);
                lblItems3Count.Text = gvItems3.Rows.Count.ToString();
                lbShowUnknownHardware.Text = "Hide unknown hardware";
            }
            up3.Update();
        }
        protected void btnExportItems3_Click(object sender, ImageClickEventArgs e)
        {
            Utilities.ExportToExcel(gvItems3, Response);
        }

        #endregion

        #region Smartphones report

        protected void lbShowSmartphoneOwnerships_Click(object sender, EventArgs e)
        {
            Panel5.Visible = !Panel5.Visible;
            btnExportItems5.Visible = !btnExportItems5.Visible;
            lbShowSmartphoneOwnerships.Text = "Show smartphone ownerships";
            if (Panel5.Visible)
            {
                gvItems5.DataSource = GmsrBLL.SmartphoneOwnership.GetAll();
                gvItems5.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems5);
                lblItems5Count.Text = gvItems5.Rows.Count.ToString();
                lbShowSmartphoneOwnerships.Text = "Hide smartphone ownerships";
            }
            up5.Update();
        }

        protected void btnExportItems5_Click(object sender, ImageClickEventArgs e)
        {
            Utilities.ExportToExcel(gvItems5, Response);
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