using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;
using System.IO;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace Eu.Europa.Ec.Olaf.Gmsr.Software
{
    public partial class Main : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems3);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems4);

            // to handle the row selection
            gvItems1.RowCommand += gvItems1_RowCommand;
            gvItems2.RowCommand += gvItems2_RowCommand;

            if (!IsPostBack)
            {                                
                LoadDdlProductStatus();
                LoadDdlProductSource();
                LoadDdlOrderType();
                LoadDdlType();

                SetSearchValues();
                SetSearchType();

                bool autoSearch = false;

                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    if ((FromPage.Url.Contains("EditProduct.aspx") || FromPage.Url.Contains("EditLicense.aspx") || FromPage.Url.Contains("EditOrderItem.aspx")) && SelectedSoftwareLicense.Id != 0)
                    {
                        LoadGvItems(SelectedSoftwareLicense);
                    }
                    else if (FromPage.Url.Contains("EditLicenseAssignment.aspx") && SelectedSoftwareLicenseAssignment.Id != 0)
                    {
                        LoadGvSoftwareLicenseAssignments(SelectedSoftwareLicenseAssignment);
                    }
                    else if (FromPage.Url.Contains("Products.aspx") && SelectedSoftwareProduct.Id != 0)
                    {
                        ClearSearchValues();
                        rblSearchType.SelectedValue = "0";
                        txtProductName.Text = SelectedSoftwareProduct.Name;
                        txtProductVersion.Text = SelectedSoftwareProduct.Version;
                        txtProductCompany.Text = SelectedSoftwareProduct.CompanyName;
                        ddlProductSource.SelectedValue = SelectedSoftwareProduct.Source;

                        autoSearch = true;
                    }
                    else if (FromPage.Url.Contains("OrderItems.aspx") && SelectedSoftwareOrderItem.Id != 0)
                    {
                        ClearSearchValues();
                        rblSearchType.SelectedValue = "0";
                        txtOrderIdFrom.Text = SelectedSoftwareOrderItem.Id.ToString();
                        txtOrderIdTo.Text = SelectedSoftwareOrderItem.Id.ToString();
                        ddlProductSource.SelectedValue = SelectedSoftwareOrderItem.SoftwareProductSource;

                        autoSearch = true;
                    }

                    FromPage = null;
                }

                #endregion

                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                if (PreviousPageStack.Count > 0)
                {
                    ((MasterPage)Master).EnableAction(Action.Back);
                }

                #endregion

                #region capture the enter key -> Search action

                txtContractFramework.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateEndFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateEndTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateStartFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateStartTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtIdFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtIdTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOlafRef.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderForm.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderIdFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderIdTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");                
                txtProductCompany.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductVersion.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSerial.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSpecificContract.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlProductSource.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlAvailability.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlOrderType.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlProductStatus.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlType.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                cbWithoutUpgrade.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                cbOneLicensePerProduct.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                txtProductNameA.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductVersionA.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlStatus.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtUser.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtHardwareItem.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAssignmentDateFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAssignmentDateTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInstallationDateFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInstallationDateTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtUnassignmentDateFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtUnassignmentDateTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtComment.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                txtUserI.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInventoryI.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductNameI.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductVersionI.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlSourceI.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDetectionDateIFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDetectionDateITo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAdditionalInfoI.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                txtUserU.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInventoryU.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductNameU.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductVersionU.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlSourceU.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDetectionDateUFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDetectionDateUTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAdditionalInfoU.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInstallationDateUFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInstallationDateUTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion

                if (autoSearch)
                {
                    GetSearchValues();
                    LoadGvItems();                    
                }                
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                ((MasterPage)Master).SetGridviewProperties(gvItems3);
                ((MasterPage)Master).SetGridviewProperties(gvItems4);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                GetSearchValues();

                if (rblSearchType.SelectedValue == "0")
                {
                    LoadGvItems();
                    up1.Update();

                    //clear other gridviews
                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblItems2Count.Text = "0";
                    ibtnAddLicenseAssignment.Enabled = false;
                    ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
                    up2.Update();

                    gvItems3.DataSource = null;
                    gvItems3.DataBind();
                    lblItems3Count.Text = "0";
                    up3.Update();

                    gvItems4.DataSource = null;
                    gvItems4.DataBind();
                    lblItems4Count.Text = "0";
                    up4.Update();
                }
                else if (rblSearchType.SelectedValue == "1")
                {
                    LoadGvSoftwareLicenseAssignments();
                    up2.Update();
                    ibtnAddLicenseAssignment.Enabled = false;
                    ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblResultsCount.Text = "0";
                    up1.Update();

                    gvItems3.DataSource = null;
                    gvItems3.DataBind();
                    lblItems3Count.Text = "0";
                    up3.Update();

                    gvItems4.DataSource = null;
                    gvItems4.DataBind();
                    lblItems4Count.Text = "0";
                    up4.Update();
                }
                else if (rblSearchType.SelectedValue == "2")
                {
                    LoadGvDetectedSoftwareInstallations();
                    up3.Update();

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblResultsCount.Text = "0";
                    up1.Update();

                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblItems2Count.Text = "0";
                    ibtnAddLicenseAssignment.Enabled = false;
                    ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
                    up2.Update();

                    gvItems4.DataSource = null;
                    gvItems4.DataBind();
                    lblItems4Count.Text = "0";
                    up4.Update();
                }
                else if (rblSearchType.SelectedValue == "3")
                {
                    LoadGvDetectedSoftwareUninstallations();
                    up4.Update();

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblResultsCount.Text = "0";
                    up1.Update();

                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblItems2Count.Text = "0";
                    ibtnAddLicenseAssignment.Enabled = false;
                    ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
                    up2.Update();

                    gvItems3.DataSource = null;
                    gvItems3.DataBind();
                    lblItems3Count.Text = "0";
                    up3.Update();
                }
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();
                upSearch.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Back)
            {
                FromPage = GetCurrentPage();
                Response.Redirect(PreviousPageStack.Pop().Url);
            }
        }

        protected void gvItems1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Control downloadButton = e.Row.FindControl("ibtnDownload");
            if(downloadButton != null)
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(downloadButton);
        }
        
        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SoftwareLicenseBL item = (SoftwareLicenseBL)e.Row.DataItem;
                if (item.SoftwareOrderItemHasUpgrade.HasValue && item.SoftwareOrderItemHasUpgrade.Value)
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#778db7");

                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#6dbd6d");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductVersion")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#6dbd6d");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductCompanyName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#6dbd6d");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductStatusName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#6dbd6d");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductSource")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#6dbd6d");                   

                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemOlafRef")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemTypeName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemMaintenanceStartDate")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemMaintenanceEndDate")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "OrderFormName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SpecificContractName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "ContractFrameworkName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#b28ac5");
                }
                else if (item.SoftwareProductSource == "ABAC")
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");

                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductId")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductVersion")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductCompanyName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductStatusName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareProductSource")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "Id")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemId")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemOlafRef")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemTypeName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemPreviousId")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemMaintenanceStartDate")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SoftwareOrderItemMaintenanceEndDate")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "OrderFormName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "SpecificContractName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "ContractFrameworkName")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                }

                // check whether the limited version is requested
                if (Request.QueryString["limited"] == "true")
                {                                       
                    e.Row.Cells[0].Controls[1].Visible = false;
                    e.Row.Cells[1].Controls[1].Visible = false;
                    e.Row.Cells[24].Controls[1].Visible = false;
                    e.Row.Cells[25].Controls[1].Visible = false;
                }
            }
        }

        protected void rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            upSearchType.Update();

            SetSearchType();
            upSearch.Update();

            //clear gridviews
            gvItems1.DataSource = null;
            gvItems1.DataBind();
            lblResultsCount.Text = "0";
            up1.Update();

            gvItems2.DataSource = null;
            gvItems2.DataBind();
            lblItems2Count.Text = "0";
            ibtnAddLicenseAssignment.Enabled = false;
            ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
            up2.Update();

            gvItems3.DataSource = null;
            gvItems3.DataBind();
            lblItems3Count.Text = "0";
            up3.Update();

            gvItems4.DataSource = null;
            gvItems4.DataBind();
            lblItems4Count.Text = "0";
            up4.Update();
        }

        private void gvItems1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));
                SelectedSoftwareLicense.Select(id);

                LoadGvSoftwareLicenseAssignments(SelectedSoftwareLicense);
                lblItems2Count.Text = gvItems2.Rows.Count.ToString() + ComposeLicenseAssignmentsCountSuffix(SelectedSoftwareLicense);
                ibtnAddLicenseAssignment.Enabled = true;
                ibtnAddLicenseAssignment.ImageUrl = "~/Images/Add.png";
                up2.Update();

                LoadGvDetectedSoftwareInstallations(SelectedSoftwareLicense.SoftwareProductId.Value);
                up3.Update();

                LoadGvDetectedSoftwareUninstallations(SelectedSoftwareLicense.SoftwareProductId.Value);
                up4.Update();
            }
        }

        private void gvItems2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems2.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));
                SelectedSoftwareLicenseAssignment.Select(id);

                SoftwareLicenseBL license = new SoftwareLicenseBL();
                license.Select(SelectedSoftwareLicenseAssignment.SoftwareLicenseId.Value);
                LoadGvItems(license);
                up1.Update();

                LoadGvDetectedSoftwareInstallations(SelectedSoftwareLicenseAssignment.SoftwareProductId.Value);
                up3.Update();

                LoadGvDetectedSoftwareUninstallations(SelectedSoftwareLicenseAssignment.SoftwareProductId.Value);
                up4.Update();
            }
        }

        protected void ibtnGoToProduct_Click(object sender, EventArgs e)
        {
            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            SelectedSoftwareProduct = null;

            FromPage = GetCurrentPage();
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/Products.aspx");
        }
        
        protected void ibtnGoToEntry_Click(object sender, EventArgs e)
        {
            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            SelectedSoftwareOrderItem = null;

            FromPage = GetCurrentPage();
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/OrderItems.aspx");
        }

        protected void ibtnDownload_Click(object sender, EventArgs e)
        {
            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            if (SelectedSoftwareLicense.File != null)
            {
                Utilities.DownloadFile(SelectedSoftwareLicense.Filename, SelectedSoftwareLicense.File, SelectedSoftwareLicense.FileSize, Response);
            }
            else
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("There is no file.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void ibtnEditLicense_Click(object sender, EventArgs e)
        {            
            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            if (SelectedSoftwareLicense.SoftwareProductSource == Constants.ABAC)
            {                                
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not edit a license with product source='" + SelectedSoftwareOrderItem.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else
            {
                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();

                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicense.aspx", "► Software ► Licenses ► License Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditLicense.aspx");
            }
        } 

        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            if (SelectedSoftwareLicense.SoftwareProductSource == Constants.GMSR || SelectedSoftwareLicense.SoftwareProductSource == Constants.OLD_GMS)
            {
                ArrayList validationErrors = new ArrayList();
                SelectedSoftwareLicense.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

                ((MasterPage)Master).ShowValidationMessages(validationErrors);
                if (validationErrors.Count == 0)
                {
                    //clear gridview
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblResultsCount.Text = "0";
                    up1.Update();
                }
            }
            else
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not delete a license with product source='" + SelectedSoftwareOrderItem.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void ibtnAddSoftwareLicenseAssignment_Click(object sender, EventArgs e)
        {
            if (SelectedSoftwareLicense.SoftwareProductSource != Constants.GMSR && SelectedSoftwareLicense.SoftwareProductSource != Constants.OLD_GMS)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not assign licenses with product source='" + SelectedSoftwareLicense.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }            
            else
            {
                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();

                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicenseAssignment.aspx", "► Software ► Licenses ► License Assignment Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                SelectedSoftwareLicenseAssignment = null;
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditLicenseAssignment.aspx");
            }           
        }

        protected void ibtnEditSoftwareLicenseAssignment_Click(object sender, EventArgs e)
        {
            #region Grant access to additional pages

            List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();

            additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicenseAssignment.aspx", "► Software ► Licenses ► License Assignment Details"));

            ((MasterPage)Master).AdditionalPageList = additionalPageList;

            #endregion

            SelectedSoftwareLicenseAssignment = GetSelectedLicenseAssignment((ImageButton)sender);
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditLicenseAssignment.aspx");
        }

        protected void ibtnDeleteSoftwareLicenseAssignment_Click(object sender, EventArgs e)
        {
            SelectedSoftwareLicenseAssignment = GetSelectedLicenseAssignment((ImageButton)sender);

            ArrayList validationErrors = new ArrayList();
            SelectedSoftwareLicenseAssignment.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                //clear gridview
                gvItems2.DataSource = null;
                gvItems2.DataBind();
                lblItems2Count.Text = "0";
                up2.Update();
            }
        }

        protected void ibtnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);            
        }

        protected void ibtnExportItems2_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems2, Response);
        }

        protected void ibtnExportItems3_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems3, Response);
        }

        protected void ibtnExportItems4_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems4, Response);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Utilities.MakeSelectable(gvItems1, Page);
            Utilities.MakeSelectable(gvItems2, Page);
            base.Render(writer);
        }

        #endregion

        #region Public Methods

        public override void VerifyRenderingInServerForm(Control control)
        {
            // This override is needed to make the export to excel work
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }

        #endregion

        #region Private Methods

        private void SetSearchType()
        {
            if (rblSearchType.SelectedValue == "0")
            {
                tblSearchLicenses.Visible = true;
                tblSearchLicenseAssignments.Visible = false;
                tblSearchDetectedProductInstallations.Visible = false;
                tblSearchDetectedProductUninstallations.Visible = false;
            }
            else if (rblSearchType.SelectedValue == "1")
            {
                tblSearchLicenses.Visible = false;
                tblSearchLicenseAssignments.Visible = true;
                tblSearchDetectedProductInstallations.Visible = false;
                tblSearchDetectedProductUninstallations.Visible = false;
            }
            else if (rblSearchType.SelectedValue == "2")
            {
                tblSearchLicenses.Visible = false;
                tblSearchLicenseAssignments.Visible = false;
                tblSearchDetectedProductInstallations.Visible = true;
                tblSearchDetectedProductUninstallations.Visible = false;
            }
            else if (rblSearchType.SelectedValue == "3")
            {
                tblSearchLicenses.Visible = false;
                tblSearchLicenseAssignments.Visible = false;
                tblSearchDetectedProductInstallations.Visible = false;
                tblSearchDetectedProductUninstallations.Visible = true;
            }
        }

        private void ClearSearchValues()
        {
            txtContractFramework.Text = "";
            txtDateStartFrom.Text = "";
            txtDateStartTo.Text = "";
            txtDateEndFrom.Text = "";
            txtDateEndTo.Text = "";
            txtIdFrom.Text = "";
            txtIdTo.Text = "";
            txtOlafRef.Text = "";
            txtOrderForm.Text = "";
            txtOrderIdFrom.Text = "";
            txtOrderIdTo.Text = "";
            txtProductCompany.Text = "";
            txtProductName.Text = "";
            txtProductVersion.Text = "";
            txtSerial.Text = "";
            txtSpecificContract.Text = "";

            cbWithoutUpgrade.Checked = false;
            cbOneLicensePerProduct.Checked = false;

            ddlProductSource.SelectedValue = "GMSr";
            ddlAvailability.SelectedValue = "[Free]";
            ddlOrderType.SelectedValue = "";
            ddlProductStatus.SelectedValue = "6";
            ddlType.SelectedValue = "";

            ddlStatus.SelectedValue = "";
            txtUser.Text = "";
            txtHardwareItem.Text = "";
            txtComment.Text = "";
            txtAssignmentDateFrom.Text = "";
            txtAssignmentDateTo.Text = "";
            txtInstallationDateFrom.Text = "";
            txtInstallationDateTo.Text = "";
            txtUnassignmentDateFrom.Text = "";
            txtUnassignmentDateTo.Text = "";

            txtUserI.Text = "";
            txtInventoryI.Text = "";
            txtProductNameI.Text = "";
            txtProductVersionI.Text = "";
            ddlSourceI.SelectedValue = "";
            txtDetectionDateIFrom.Text = "";
            txtDetectionDateITo.Text = "";
            txtAdditionalInfoI.Text = "";

            txtUserU.Text = "";
            txtInventoryU.Text = "";
            txtProductNameU.Text = "";
            txtProductVersionU.Text = "";
            ddlSourceU.SelectedValue = "";
            txtDetectionDateUFrom.Text = "";
            txtDetectionDateUTo.Text = "";
            txtAdditionalInfoU.Text = "";
            txtInstallationDateUFrom.Text = "";
            txtInstallationDateUTo.Text = "";
        }

        private void SetSearchValues()
        {
            if (!Equals(Session["softwareSearchType"], null))
            {
                rblSearchType.SelectedValue = (string)Session["softwareSearchType"];
            }

            if (!Equals(Session["softwareSearchValues"], null))
            {
                Dictionary<string, string> searchValues = (Dictionary<string, string>)Session["softwareSearchValues"];
                txtContractFramework.Text = searchValues["ContractFramework"];
                txtDateEndFrom.Text = searchValues["DateEndFrom"];
                txtDateEndTo.Text = searchValues["DateEndTo"];
                txtDateStartFrom.Text = searchValues["DateStartFrom"];
                txtDateStartTo.Text = searchValues["DateStartTo"];
                txtIdFrom.Text = searchValues["IdFrom"];
                txtIdTo.Text = searchValues["IdTo"];
                txtOlafRef.Text = searchValues["OlafRef"];
                txtOrderForm.Text = searchValues["OrderForm"];
                txtOrderIdFrom.Text = searchValues["OrderIdFrom"];
                txtOrderIdTo.Text = searchValues["OrderIdTo"];               
                txtProductCompany.Text = searchValues["ProductCompany"];
                txtProductName.Text = searchValues["ProductName"];
                txtProductVersion.Text = searchValues["ProductVersion"];
                txtSerial.Text = searchValues["Serial"];
                txtSpecificContract.Text = searchValues["SpecificContract"];
                ddlProductSource.SelectedValue = searchValues["ProductSource"];
                ddlAvailability.SelectedValue = searchValues["Availability"];
                ddlOrderType.SelectedValue = searchValues["OrderType"];
                ddlProductStatus.SelectedValue = searchValues["ProductStatus"];
                ddlType.SelectedValue = searchValues["Type"];
                cbWithoutUpgrade.Checked = Boolean.Parse(searchValues["WithoutUpgrade"]);
                cbOneLicensePerProduct.Checked = Boolean.Parse(searchValues["OneLicensePerProduct"]);
            }

            if (!Equals(Session["softwareSearchValues1"], null))
            {
                Dictionary<string, string> searchValues1 = (Dictionary<string, string>)Session["softwareSearchValues1"];
                ddlStatus.SelectedValue = searchValues1["status"];
                txtUser.Text = searchValues1["user"];
                txtHardwareItem.Text = searchValues1["hardwareItem"];
                txtComment.Text = searchValues1["comment"];
                txtAssignmentDateFrom.Text = searchValues1["assignmentDateFrom"];
                txtAssignmentDateTo.Text = searchValues1["assignmentDateTo"];
                txtInstallationDateFrom.Text = searchValues1["installationDateFrom"];
                txtInstallationDateTo.Text = searchValues1["installationDateTo"];
                txtUnassignmentDateFrom.Text = searchValues1["unassignmentDateFrom"];
                txtUnassignmentDateTo.Text = searchValues1["unassignmentDateTo"];
            }

            if (!Equals(Session["softwareSearchValues2"], null))
            {
                Dictionary<string, string> searchValues2 = (Dictionary<string, string>)Session["softwareSearchValues2"];
                txtUserI.Text = searchValues2["userI"];
                txtInventoryI.Text = searchValues2["inventoryI"];
                txtProductNameI.Text = searchValues2["productNameI"];
                txtProductVersionI.Text = searchValues2["productVersionI"];
                ddlSourceI.SelectedValue = searchValues2["sourceI"];
                txtDetectionDateIFrom.Text = searchValues2["detectionDateIFrom"];
                txtDetectionDateITo.Text = searchValues2["detectionDateITo"];
                txtAdditionalInfoI.Text = searchValues2["additionalInfoI"];
            }

            if (!Equals(Session["softwareSearchValues3"], null))
            {
                Dictionary<string, string> searchValues3 = (Dictionary<string, string>)Session["softwareSearchValues3"];
                txtUserU.Text = searchValues3["userU"];
                txtInventoryU.Text = searchValues3["inventoryU"];
                txtProductNameU.Text = searchValues3["productNameU"];
                txtProductVersionU.Text = searchValues3["productVersionU"];
                ddlSourceU.SelectedValue = searchValues3["sourceU"];
                txtDetectionDateUFrom.Text = searchValues3["detectionDateUFrom"];
                txtDetectionDateUTo.Text = searchValues3["detectionDateUTo"];
                txtAdditionalInfoU.Text = searchValues3["additionalInfoU"];
                txtInstallationDateUFrom.Text = searchValues3["installationDateUFrom"];
                txtInstallationDateUTo.Text = searchValues3["installationDateUTo"];
            }
        }

        private void GetSearchValues()
        {
            Session["softwareSearchType"] = rblSearchType.SelectedValue;

            Dictionary<string, string> searchValues = new Dictionary<string, string>();
            searchValues.Add("IdFrom", txtIdFrom.Text);
            searchValues.Add("IdTo", txtIdTo.Text);
            searchValues.Add("OrderIdFrom", txtOrderIdFrom.Text);
            searchValues.Add("OrderIdTo", txtOrderIdTo.Text);       
            searchValues.Add("DateStartFrom", txtDateStartFrom.Text);
            searchValues.Add("DateStartTo", txtDateStartTo.Text);
            searchValues.Add("DateEndFrom", txtDateEndFrom.Text);
            searchValues.Add("DateEndTo", txtDateEndTo.Text);
            searchValues.Add("Serial", txtSerial.Text);
            searchValues.Add("Type", ddlType.SelectedItem.Value);
            searchValues.Add("OlafRef", txtOlafRef.Text);
            searchValues.Add("OrderType", ddlOrderType.SelectedItem.Value);
            searchValues.Add("ProductName", txtProductName.Text);
            searchValues.Add("ProductVersion", txtProductVersion.Text);
            searchValues.Add("ProductCompany", txtProductCompany.Text);
            searchValues.Add("ProductStatus", ddlProductStatus.SelectedItem.Value);
            searchValues.Add("OrderForm", txtOrderForm.Text);
            searchValues.Add("SpecificContract", txtSpecificContract.Text);
            searchValues.Add("ContractFramework", txtContractFramework.Text);
            searchValues.Add("ProductSource", ddlProductSource.SelectedItem.Value);
            searchValues.Add("Availability", ddlAvailability.SelectedItem.Value);
            searchValues.Add("WithoutUpgrade", cbWithoutUpgrade.Checked.ToString());
            searchValues.Add("OneLicensePerProduct", cbOneLicensePerProduct.Checked.ToString());
            Session["softwareSearchValues"] = searchValues;

            Dictionary<string, string> searchValues1 = new Dictionary<string, string>();
            searchValues1.Add("status", ddlStatus.SelectedItem.Value);
            searchValues1.Add("user", txtUser.Text);
            searchValues1.Add("hardwareItem", txtHardwareItem.Text);
            searchValues1.Add("comment", txtComment.Text);
            searchValues1.Add("assignmentDateFrom", txtAssignmentDateFrom.Text);
            searchValues1.Add("assignmentDateTo", txtAssignmentDateTo.Text);
            searchValues1.Add("installationDateFrom", txtInstallationDateFrom.Text);
            searchValues1.Add("installationDateTo", txtInstallationDateTo.Text);
            searchValues1.Add("unassignmentDateFrom", txtUnassignmentDateFrom.Text);
            searchValues1.Add("unassignmentDateTo", txtUnassignmentDateTo.Text);
            Session["softwareSearchValues1"] = searchValues1;

            Dictionary<string, string> searchValues2 = new Dictionary<string, string>();
            searchValues2.Add("userI", txtUserI.Text);
            searchValues2.Add("inventoryI", txtInventoryI.Text);
            searchValues2.Add("productNameI", txtProductNameI.Text);
            searchValues2.Add("productVersionI", txtProductVersionI.Text);
            searchValues2.Add("sourceI", ddlSourceI.SelectedItem.Value);
            searchValues2.Add("detectionDateIFrom", txtDetectionDateIFrom.Text);
            searchValues2.Add("detectionDateITo", txtDetectionDateITo.Text);
            searchValues2.Add("additionalInfoI", txtAdditionalInfoI.Text);
            Session["softwareSearchValues2"] = searchValues2;

            Dictionary<string, string> searchValues3 = new Dictionary<string, string>();
            searchValues3.Add("userU", txtUserU.Text);
            searchValues3.Add("inventoryU", txtInventoryU.Text);
            searchValues3.Add("productNameU", txtProductNameU.Text);
            searchValues3.Add("productVersionU", txtProductVersionU.Text);
            searchValues3.Add("sourceU", ddlSourceU.SelectedItem.Value);
            searchValues3.Add("detectionDateUFrom", txtDetectionDateUFrom.Text);
            searchValues3.Add("detectionDateUTo", txtDetectionDateUTo.Text);
            searchValues3.Add("additionalInfoU", txtAdditionalInfoU.Text);
            searchValues3.Add("installationDateUFrom", txtInstallationDateUFrom.Text);
            searchValues3.Add("installationDateUTo", txtInstallationDateUTo.Text);
            Session["softwareSearchValues3"] = searchValues3;
        }

        private void LoadDdlProductStatus()
        {
            ddlProductStatus.DataSource = SoftwareProductStatusBL.GetAll();

            ddlProductStatus.DataTextField = "Name";
            ddlProductStatus.DataValueField = "Id";

            ddlProductStatus.DataBind();

            ddlProductStatus.Items.Insert(0, new ListItem("", ""));


            ddlProductStatus.SelectedValue = "6"; // 'Operational' as default
        }

        private void LoadDdlOrderType()
        {
            ddlOrderType.DataSource = SoftwareOrderItemTypeBL.GetAll();

            ddlOrderType.DataTextField = "Name";
            ddlOrderType.DataValueField = "Id";

            ddlOrderType.DataBind();

            ddlOrderType.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadDdlType()
        {
            ddlType.DataSource = SoftwareLicenseTypeBL.GetAll();

            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "Id";

            ddlType.DataBind();

            ddlType.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadDdlProductSource()
        {
            ddlProductSource.Items.Add("");
            ddlProductSource.Items.Add(Constants.GMSR);
            ddlProductSource.Items.Add(Constants.OLD_GMS);
            ddlProductSource.Items.Add(Constants.ABAC);

            ddlProductSource.SelectedValue = Constants.GMSR; // as default
        }

        private SoftwareProductBL GetSelectedProduct(ImageButton button)
        {
            // get product id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int productId = int.Parse(Utilities.GetGridViewRowText(row, "SoftwareProductId"));

            SoftwareProductBL item = new SoftwareProductBL();
            item.Select(productId);

            return item;
        }

        private SoftwareOrderItemBL GetSelectedOrderItem(ImageButton button)
        {
            // get order id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int orderId = int.Parse(Utilities.GetGridViewRowText(row, "SoftwareOrderItemId"));

            SoftwareOrderItemBL item = new SoftwareOrderItemBL();
            item.Select(orderId);

            return item;
        }

        private SoftwareOrderItemBL GetSelectedPreviousOrderItem(ImageButton button)
        {
            SoftwareOrderItemBL item = new SoftwareOrderItemBL();

            // get order id of row
            GridViewRow row = button.Parent.Parent as GridViewRow;

            int orderId = 0;
            if (int.TryParse(Utilities.GetGridViewRowText(row, "SoftwareOrderPreviousId"), out orderId))
            {
                item.Select(orderId);
            }
            else
            {
                item = null;
            }

            return item;
        }

        private SoftwareLicenseBL GetSelectedLicense(ImageButton button)
        {
            // get license id of row
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int licenseId = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

            SoftwareLicenseBL item = new SoftwareLicenseBL();
            item.Select(licenseId);

            return item;
        }

        private SoftwareLicenseBL GetSelectedPreviousLicense(ImageButton button)
        {
            SoftwareLicenseBL item = new SoftwareLicenseBL();

            // get license id of row
            GridViewRow row = button.Parent.Parent as GridViewRow;

            int licenseId = 0;
            if (int.TryParse(Utilities.GetGridViewRowText(row, "PreviousSoftwareLicenseId"), out licenseId))
            {
                item.Select(licenseId);
            }
            else
            {
                item = null;
            }

            return item;
        }

        private SoftwareLicenseAssignmentBL GetSelectedLicenseAssignment(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems2.DataKeys[row.RowIndex]["Id"].ToString());

            SoftwareLicenseAssignmentBL item = new SoftwareLicenseAssignmentBL();
            item.Select(id);

            return item;
        }

        private string ComposeLicenseAssignmentsCountSuffix(SoftwareLicenseBL license)
        {
            string suffix;

            if (license.SoftwareLicenseTypeId == 1)
                suffix = " / 1";
            else if (license.SoftwareLicenseTypeId == 2)
                suffix = " / " + license.MultiUserQuantity;
            else
                suffix = "";

            return suffix;
        }

        private void LoadGvItems()
        {
            List<SoftwareLicenseBL> itemList = SoftwareLicenseBL.GetByParameters(txtIdFrom.Text == "" ? null : (int?)int.Parse(txtIdFrom.Text),
                                                                   txtIdTo.Text == "" ? null : (int?)int.Parse(txtIdTo.Text),
                                                                   null,
                                                                   null,
                                                                   txtOrderIdFrom.Text == "" ? null : (int?)int.Parse(txtOrderIdFrom.Text),
                                                                   txtOrderIdTo.Text == "" ? null : (int?)int.Parse(txtOrderIdTo.Text),
                                                                   null,
                                                                   null,
                                                                   null,
                                                                   null,
                                                                   ddlType.SelectedValue == "" ? null : (int?)int.Parse(ddlType.SelectedValue),
                                                                   ddlOrderType.SelectedValue == "" ? null : (int?)int.Parse(ddlOrderType.SelectedValue),
                                                                   ddlProductStatus.SelectedValue == "" ? null : (int?)int.Parse(ddlProductStatus.SelectedValue),
                                                                   txtDateStartFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateStartFrom.Text),
                                                                   txtDateStartTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateStartTo.Text),
                                                                   txtDateEndFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateEndFrom.Text),
                                                                   txtDateEndTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateEndTo.Text),
                                                                   txtSerial.Text,
                                                                   txtOlafRef.Text,
                                                                   txtProductName.Text,
                                                                   txtProductVersion.Text,
                                                                   txtProductCompany.Text,
                                                                   ddlProductSource.SelectedValue,
                                                                   ddlAvailability.SelectedValue,
                                                                   txtOrderForm.Text,
                                                                   txtSpecificContract.Text,
                                                                   txtContractFramework.Text,
                                                                   cbWithoutUpgrade.Checked,
                                                                   cbOneLicensePerProduct.Checked);

            gvItems1.DataSource = itemList;

            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblResultsCount.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvItems(SoftwareLicenseBL softwareLicense)
        {
            List<SoftwareLicenseBL> itemList = new List<SoftwareLicenseBL>();
            itemList.Add(softwareLicense);

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblResultsCount.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvSoftwareLicenseAssignments()
        {
            List<SoftwareLicenseAssignmentBL> itemList = SoftwareLicenseAssignmentBL.GetByParameters(
                                                                                                txtProductNameA.Text,
                                                                                                txtProductVersionA.Text,
                                                                                                ddlStatus.SelectedValue,
                                                                                                txtUser.Text,
                                                                                                txtHardwareItem.Text,
                                                                                                txtAssignmentDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtAssignmentDateFrom.Text),
                                                                                                txtAssignmentDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtAssignmentDateTo.Text),
                                                                                                txtInstallationDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtInstallationDateFrom.Text),
                                                                                                txtInstallationDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtInstallationDateTo.Text),
                                                                                                txtUnassignmentDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtUnassignmentDateFrom.Text),
                                                                                                txtUnassignmentDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtUnassignmentDateTo.Text),
                                                                                                txtComment.Text
                                                                                                );

            gvItems2.DataSource = itemList;
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvSoftwareLicenseAssignments(SoftwareLicenseBL softwareLicense)
        {
            gvItems2.DataSource = SoftwareLicenseAssignmentBL.GetBySoftwareLicense(softwareLicense.Id);
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString() + ComposeLicenseAssignmentsCountSuffix(softwareLicense);
        }

        private void LoadGvSoftwareLicenseAssignments(SoftwareLicenseAssignmentBL softwareLicenseAssignment)
        {
            List<SoftwareLicenseAssignmentBL> itemList = new List<SoftwareLicenseAssignmentBL>();
            itemList.Add(softwareLicenseAssignment);

            gvItems2.DataSource = itemList;
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvDetectedSoftwareInstallations()
        {
            List<DetectedSoftwareInstallationBL> itemList = DetectedSoftwareInstallationBL.GetByParameters(txtUserI.Text,
                                                                                                txtInventoryI.Text,
                                                                                                txtProductNameI.Text,
                                                                                                txtProductVersionI.Text,
                                                                                                ddlSourceI.SelectedValue,
                                                                                                txtAdditionalInfoI.Text,
                                                                                                txtDetectionDateIFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDetectionDateIFrom.Text),
                                                                                                txtDetectionDateITo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDetectionDateITo.Text)
                                                                                                );

            gvItems3.DataSource = itemList;
            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
            lblItems3Count.Text = gvItems3.Rows.Count.ToString();
        }

        private void LoadGvDetectedSoftwareInstallations(int softwareProductId)
        {
            List<DetectedSoftwareInstallationBL> itemList = DetectedSoftwareInstallationBL.GetBySoftwareProduct(softwareProductId);

            gvItems3.DataSource = itemList;
            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
            lblItems3Count.Text = gvItems3.Rows.Count.ToString();
        }

        private void LoadGvDetectedSoftwareUninstallations()
        {
            List<DetectedSoftwareUninstallationBL> itemList = DetectedSoftwareUninstallationBL.GetByParameters(txtUserU.Text,
                                                                                                txtInventoryU.Text,
                                                                                                txtProductNameU.Text,
                                                                                                txtProductVersionU.Text,
                                                                                                ddlSourceU.SelectedValue,
                                                                                                txtAdditionalInfoU.Text,
                                                                                                txtDetectionDateIFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDetectionDateIFrom.Text),
                                                                                                txtDetectionDateITo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDetectionDateITo.Text),
                                                                                                txtInstallationDateUFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtInstallationDateUFrom.Text),
                                                                                                txtInstallationDateUTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtInstallationDateUTo.Text)
                                                                                                );
            gvItems4.DataSource = itemList;
            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
            lblItems4Count.Text = gvItems4.Rows.Count.ToString();
        }

        private void LoadGvDetectedSoftwareUninstallations(int softwareProductId)
        {
            List<DetectedSoftwareUninstallationBL> itemList = DetectedSoftwareUninstallationBL.GetBySoftwareProduct(softwareProductId);

            gvItems4.DataSource = itemList;
            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
            lblItems4Count.Text = gvItems4.Rows.Count.ToString();
        }

        #endregion
    }
}