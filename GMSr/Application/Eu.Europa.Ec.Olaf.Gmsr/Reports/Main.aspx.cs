using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;
using Eu.Europa.Ec.Olaf.GmsrBLL.ToBeReviewed;


namespace Eu.Europa.Ec.Olaf.Gmsr.Reports
{
    public partial class Main : BasePage 
    {

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems3);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems4);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportItems5);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportMPR);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportOits);

           

            
        }
        
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

        protected void lbShowLUNSpace_Click(object sender, EventArgs e)
        {
            Panel2.Visible = !Panel2.Visible;
            btnExportItems2.Visible = !btnExportItems2.Visible;
            lbShowLUNSpace.Text = "Show PRTG's LUN space";
            if (Panel2.Visible)
            {
                gvItems2.DataSource = GmsrBLL.PrtgLunSpaceItemBL.GetAll();
                gvItems2.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                lblItems2Count.Text = gvItems2.Rows.Count.ToString();
                lbShowLUNSpace.Text = "Hide Show PRTG's LUN space";
            }
            up2.Update();
        }

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
        
        protected void btnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
        }
        protected void btnExportItems2_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems2, Response);
        }
        protected void btnExportItems3_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems3, Response);
        }
        protected void btnExportItems4_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems4, Response);
        }
        protected void btnExportItems5_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems5, Response);
        }

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

        protected void lbShowPrtgVolumeAvailabilityGraphs_Click(object sender, EventArgs e)
        {
            Panelnogrid2.Visible = !Panelnogrid2.Visible;
            lbShowPrtgVolumeAvailabilityGraphs.Text = "Show PRTG volume availability graphs";
            if (Panelnogrid2.Visible) 
            {
                foreach (Control control in Panelnogrid2.Controls) 
                {

                    if (control is Image) 
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
                lbShowPrtgVolumeAvailabilityGraphs.Text = "Hide PRTG volume availability graphs";
            }
            upnogrid2.Update();
            
        }

        protected void lbShowSmartphoneOwnerships_Click(object sender, EventArgs e)
        {
            Panel5.Visible = !Panel5.Visible;
            btnExportItems5.Visible = !btnExportItems5.Visible;
            lbShowSmartphoneOwnerships.Text = "Show smartphone ownerships";
            if (Panel5.Visible)
            {
                gvItems5.DataSource = GmsrBLL.ToBeReviewed.SmartphoneOwnership.GetAll();
                gvItems5.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems5);
                lblItems5Count.Text = gvItems5.Rows.Count.ToString();
                lbShowSmartphoneOwnerships.Text = "Hide smartphone ownerships";
            }
            up5.Update();
        }

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

        protected void btnExportOits_Click(object sender, EventArgs e)
        {
            if ((calOitsFrom.SelectedDate.ToBinary() > 0) && (calOitsTo.SelectedDate.ToBinary() > 0) && (calOitsFrom.SelectedDate <= calOitsTo.SelectedDate))
            {
                OitsStatsReportBL.OpenXMLExport(calOitsFrom.SelectedDate, calOitsTo.SelectedDate, Response);
            }
        }

        protected void lkbtnShowOITS_Click(object sender, EventArgs e)
        {
            panelnogrid4.Visible = !panelnogrid4.Visible;
            lkbtnShowOITS.Text = "Show OITS stts export options";
            if (panelnogrid4.Visible)
            {
                lkbtnShowOITS.Text = "Hide OITS stts export options";
            }
            upnogrid4.Update();

        }

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