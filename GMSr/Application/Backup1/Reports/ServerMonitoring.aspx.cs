using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;


namespace Eu.Europa.Ec.Olaf.Gmsr.Reports
{
    public partial class ServerMonitoring : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportPrtgStorage);
        }

        #region PRTG Lun occupation & availability report

        protected void lbShowLUNSpace_Click(object sender, EventArgs e)
        {
            Panel2.Visible = !Panel2.Visible;
            btnExportItems2.Visible = !btnExportItems2.Visible;
            lbShowLUNSpace.Text = "Show PRTG's Lun occupation & availability";
            if (Panel2.Visible)
            {
                gvItems2.DataSource = GmsrBLL.PrtgOccupationAndAvailabilityBL.GetAll();
                gvItems2.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                lblItems2Count.Text = gvItems2.Rows.Count.ToString();
                lbShowLUNSpace.Text = "Hide PRTG's Lun occupation & availability";
            }
            up2.Update();
        }
        protected void btnExportItems2_Click(object sender, ImageClickEventArgs e)
        {
            Utilities.ExportToExcel(gvItems2, Response);
        }

        #endregion

        #region Prtg graphs report

        protected void lbShowPrtgVolumeOccupationGraphs_Click(object sender, EventArgs e)
        {
            Panelnogrid1.Visible = !Panelnogrid1.Visible;
            lbShowPrtgVolumeOccupationGraphs.Text = "Show PRTG volume occupation graphs";
            if (Panelnogrid1.Visible)
            {
                foreach (Control control in Panelnogrid1.Controls)
                {

                    if (control is System.Web.UI.WebControls.Image)
                    {
                        ((Image)control).ImageUrl = ((Image)control).ImageUrl.Replace("&sdate=", "&sdate=" + DateTime.Now.AddMonths(-12).Year.ToString() + "-" + DateTime.Now.AddMonths(-12).Month.ToString() + "-" + DateTime.Now.AddMonths(-12).Day.ToString() + "-00-00-00");
                        ((Image)control).ImageUrl = ((Image)control).ImageUrl.Replace("&edate=", "&edate=" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-23-59-59");
                    }
                    if (control is HyperLink)
                    {
                        ((HyperLink)control).NavigateUrl = ((HyperLink)control).NavigateUrl.Replace("&sdate=", "&sdate=" + DateTime.Now.AddMonths(-12).Year.ToString() + "-" + DateTime.Now.AddMonths(-12).Month.ToString() + "-" + DateTime.Now.AddMonths(-12).Day.ToString() + "-00-00-00");
                        ((HyperLink)control).NavigateUrl = ((HyperLink)control).NavigateUrl.Replace("&edate=", "&edate=" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-23-59-59");
                    }

                }
                lbShowPrtgVolumeOccupationGraphs.Text = "Hide PRTG volume occupation graphs";
            }
            upnogrid1.Update();
        }

        #endregion

        #region PRTG storage report

        protected void lkbtnPrtgStorage_Click(object sender, EventArgs e)
        {

            Panel7.Visible = !Panel7.Visible;
            lkbtnPrtgStorage.Text = "Show PRTG storage report";
            if (Panel7.Visible)
            {
                gvItems7.DataSource = PrtgStorageItemBL.GetAll();
                gvItems7.DataBind();
                lblItems7Count.Text = gvItems7.Rows.Count.ToString();
                ((MasterPage)Master).SetGridviewProperties(gvItems7);
                lkbtnPrtgStorage.Text = "Hide PRTG storage report";
            }


            up7.Update();
        }

        protected void ibtnExportPrtgStorage_Click(object sender, ImageClickEventArgs e)
        {
            PrtgStorageItemBL.OpenXMLExport(Response);
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