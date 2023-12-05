using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;


namespace Eu.Europa.Ec.Olaf.Gmsr.Reports
{
    public partial class Calls : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportMPR);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportOits);
        }

        #region Open groups report

        protected void ddlOpenGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOpenGroups.SelectedIndex != 0)
            {
                gvItems1.DataSource = GmsrBLL.SmtOpenGroupBL.GetByGroupId(int.Parse(ddlOpenGroups.SelectedValue));
                gvItems1.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                lblItems1Count.Text = gvItems1.Rows.Count.ToString();

            }
            else
            {
                gvItems1.DataSource = null;
                gvItems1.DataBind();
                lblItems1Count.Text = "-";
            }
            up1.Update();
        }
        protected void lbShowOpenGroups_Click(object sender, EventArgs e)
        {
            Panel1.Visible = !Panel1.Visible;
            btnExportItems1.Visible = !btnExportItems1.Visible;
            lbShowOpenGroups.Text = "Show open action groups";
            if (Panel1.Visible)
            {
                ddlOpenGroups.Items.Clear();
                ddlOpenGroups.Items.Add("Select group");
                ddlOpenGroups.DataSource = TicketFunctionalService.GetValid();
                ddlOpenGroups.AppendDataBoundItems = true;
                ddlOpenGroups.DataTextField = "Code";
                ddlOpenGroups.DataValueField = "Id";
                ddlOpenGroups.DataBind();


                lbShowOpenGroups.Text = "Hide open action groups";
            }
            up1.Update();
        }
        protected void btnExportItems1_Click(object sender, ImageClickEventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
        }

        #endregion

        #region MPR report

        protected void lkbtnShowMPR_Click(object sender, EventArgs e)
        {
            panelnogrid3.Visible = !panelnogrid3.Visible;
            lkbtnShowMPR.Text = "Show MPR export options";
            if (panelnogrid3.Visible)
            {
                lkbtnShowMPR.Text = "Hide MPR export options";
            }
            upnogrid3.Update();
        }

        protected void calMPR_SelectionChanged(object sender, EventArgs e)
        {
            upnogrid3.Update();
        }

        protected void calMPR_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            upnogrid3.Update();
        }

        protected void ibtnExportMPR_Click(object sender, ImageClickEventArgs e)
        {
            if (calMPR.SelectedDate.ToBinary() > 0)
            {
                MonthlyProgressReportBL.OpenXMLExport(calMPR.SelectedDate, Response);
            }

        }

        #endregion

        #region OITS stats report

        protected void calOitsFrom_SelectionChanged(object sender, EventArgs e)
        {
            btnExportOits.Enabled = ((calOitsFrom.SelectedDate.ToBinary() > 0) && (calOitsTo.SelectedDate.ToBinary() > 0) && (calOitsFrom.SelectedDate <= calOitsTo.SelectedDate));
            upnogrid4.Update();
        }

        protected void calOitsFrom_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            upnogrid4.Update();
        }

        protected void calOitsTo_SelectionChanged(object sender, EventArgs e)
        {
            btnExportOits.Enabled = ((calOitsFrom.SelectedDate.ToBinary() > 0) && (calOitsTo.SelectedDate.ToBinary() > 0) && (calOitsFrom.SelectedDate <= calOitsTo.SelectedDate));
            upnogrid4.Update();
        }

        protected void calOitsTo_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            upnogrid4.Update();
        }

        protected void btnExportOits_Click(object sender, ImageClickEventArgs e)
        {
            if ((calOitsFrom.SelectedDate.ToBinary() > 0) && (calOitsTo.SelectedDate.ToBinary() > 0) && (calOitsFrom.SelectedDate <= calOitsTo.SelectedDate))
            {
                OitsStatsReportBL.OpenXMLExport(calOitsFrom.SelectedDate, calOitsTo.SelectedDate, Response);
            }
        }

        protected void lkbtnShowOITS_Click(object sender, EventArgs e)
        {
            panelnogrid4.Visible = !panelnogrid4.Visible;
            lkbtnShowOITS.Text = "Show OITS stats export options";
            if (panelnogrid4.Visible)
            {
                lkbtnShowOITS.Text = "Hide OITS stats export options";
            }
            upnogrid4.Update();

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