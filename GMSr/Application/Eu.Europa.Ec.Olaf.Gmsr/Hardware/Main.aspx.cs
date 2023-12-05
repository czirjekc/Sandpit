using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Hardware
{
    public partial class Main : BasePage
    {
        #region Properties

        public Dictionary<string, string> SearchValues
        {
            get
            {
                if (Equals(Session[Constants.HARDWARE_SEARCH_VALUES], null))
                {
                    Dictionary<string, string> searchValues = new Dictionary<string, string>();

                    searchValues.Add("status", "");
                    searchValues.Add("local", "");
                    searchValues.Add("olafName", "");
                    searchValues.Add("inventoryNo", "");
                    searchValues.Add("lastName", "");
                    searchValues.Add("firstName", "");
                    searchValues.Add("phone", "");
                    searchValues.Add("building", "");
                    searchValues.Add("office", "");
                    searchValues.Add("model", "");
                    searchValues.Add("description", "");
                    searchValues.Add("serial", "");
                    searchValues.Add("unit", "");
                    searchValues.Add("rmtCtgNm", "");
                    searchValues.Add("rmtParentCtgNm", "");
                    searchValues.Add("MaintenanceEndDateFrom", "");
                    searchValues.Add("MaintenanceEndDateTo", "");

                    Session[Constants.HARDWARE_SEARCH_VALUES] = searchValues;

                }
                return (Dictionary<string, string>)Session[Constants.HARDWARE_SEARCH_VALUES];
            }
            set { Session[Constants.HARDWARE_SEARCH_VALUES] = value; }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems3);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems4);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems5);

            gvItems1.RowCommand += gvItems1_RowCommand; // to handle the row selection of gvItems1

            if (!IsPostBack)
            {
                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    if ((FromPage.Url.Contains("EditHardwareItem") || FromPage.Url.Contains("Users/Main")) && SelectedHardwareItem.Id != 0)
                        LoadGvItems(SelectedHardwareItem);

                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Hardware/MainHelp.aspx", "► Hardware ► Main ► Help"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Hardware/EditHardwareItem.aspx", "► Hardware ► Main ► Hardware Item Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);
                ((MasterPage)Master).EnableAction(Action.Help);

                if (PreviousPageStack.Count > 0)
                {
                    ((MasterPage)Master).EnableAction(Action.Back);
                }

                #endregion

                SetSearchValues();
                SetSearchType();

                #region capture the enter key -> Search action

                ddlStatus.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlLocal.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOlafName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtInventoryNo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtLastName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtFirstName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtPhone.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtBuilding.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOffice.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtModel.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDescription.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSerial.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                ddlType.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                ((MasterPage)Master).SetGridviewProperties(gvItems3);
                ((MasterPage)Master).SetGridviewProperties(gvItems4);
                ((MasterPage)Master).SetGridviewProperties(gvItems5);
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

                    //clear other gridview
                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblHistoryResultsCount.Text = "0";
                    up2.Update();
                }
                else
                {
                    LoadGvHistoryItems();
                    up2.Update();

                    //clear other gridview
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblResultsCount.Text = "0";
                    up1.Update();
                }

                //clear other gridviews
                gvItems3.DataSource = null;
                gvItems3.DataBind();
                lblItems3Count.Text = "0";
                ibtnAddLicenseAssignment.Enabled = false;
                ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
                up3.Update();

                gvItems4.DataSource = null;
                gvItems4.DataBind();
                lblItems4Count.Text = "0";
                up4.Update();

                gvItems5.DataSource = null;
                gvItems5.DataBind();
                lblItems5Count.Text = "0";
                up5.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();
                upSearch.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Help)
            {
                string url = "";
                if (rblSearchType.SelectedValue == "0")                                   
                    url = "MainHelp1.htm";                                    
                else if (rblSearchType.SelectedValue == "1")                                
                    url = "MainHelp2.htm";
                                    
                //Display in a new window (pop-up)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript", "<script language='javascript'>window.open('" + url + "', 'CustomPopUp','width=1000, height=800, resizable=yes, scrollbars=yes');</script>", false);
            }
            else if (((MasterPage)Master).CurrentAction == Action.Back)
            {
                FromPage = GetCurrentPage();
                Response.Redirect(PreviousPageStack.Pop().Url);
            }
        }

        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "showLoadingAnimation();");
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
            lblHistoryResultsCount.Text = "0";
            up2.Update();

            gvItems3.DataSource = null;
            gvItems3.DataBind();
            lblItems3Count.Text = "0";
            ibtnAddLicenseAssignment.Enabled = false;
            ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
            up3.Update();

            gvItems4.DataSource = null;
            gvItems4.DataBind();
            lblItems4Count.Text = "0";
            up4.Update();

            gvItems5.DataSource = null;
            gvItems5.DataBind();
            lblItems5Count.Text = "0";
            up5.Update();
        }

        private void gvItems1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));
                SelectedHardwareItem.Select(id);

                LoadGvHistoryItems(SelectedHardwareItem.RmtMatId);
                up2.Update();

                LoadGvSoftwareLicenseAssignments(SelectedHardwareItem.Id);
                ibtnAddLicenseAssignment.Enabled = true;
                ibtnAddLicenseAssignment.ImageUrl = "~/Images/Add.png";
                up3.Update();

                LoadGvDetectedSoftwareInstallations(SelectedHardwareItem.Id);
                up4.Update();

                LoadGvDetectedSoftwareUninstallations(SelectedHardwareItem.Id);
                up5.Update();
            }
        }

        protected void ibtnEditHardwareItem_Click(object sender, EventArgs e)
        {
            SelectedHardwareItem = GetSelectedHardwareItem((ImageButton)sender);

            if (SelectedHardwareItem.Local == true || (SelectedHardwareItem.Local == false && SelectedHardwareItem.Status == "A"))
            {
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Hardware/EditHardwareItem.aspx");
            }
            else
            {
                ((MasterPage)Master).SetErrorMessages("That item is not editable.");
            }
        }

        protected void ibtnAddSoftwareLicenseAssignment_Click(object sender, EventArgs e)
        {
            #region Grant access to additional pages

            List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();

            additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicenseAssignment.aspx", "► Hardware ► Main ► License Assignment Details"));

            ((MasterPage)Master).AdditionalPageList = additionalPageList;

            #endregion

            SelectedSoftwareLicenseAssignment = null;
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
                gvItems3.DataSource = null;
                gvItems3.DataBind();
                lblItems3Count.Text = "0";

                up3.Update();
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

        protected void ibtnExportItems5_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems5, Response);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Utilities.MakeSelectable(gvItems1, Page);
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
                tblSearchHardwareItems.Visible = true;
                tblSearchHardwareItemModifications.Visible = false;
            }
            else if (rblSearchType.SelectedValue == "1")
            {
                tblSearchHardwareItems.Visible = false;
                tblSearchHardwareItemModifications.Visible = true;
            }
        }

        private void ClearSearchValues()
        {
            ddlStatus.SelectedValue = "";
            ddlLocal.SelectedValue = "";
            txtOlafName.Text = "";
            txtInventoryNo.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtPhone.Text = "";
            txtBuilding.Text = "";
            txtOffice.Text = "";
            txtModel.Text = "";
            txtDescription.Text = "";
            txtSerial.Text = "";
            txtUnit.Text = "";
            txtRmtCtgNm.Text = "";
            txtRmtParentCtgNm.Text = "";
            txtMaintenanceEndDateFrom.Text = "";
            txtMaintenanceEndDateTo.Text = "";

            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            ddlType.SelectedValue = "";
        }

        private void SetSearchValues()
        {
            if (!Equals(Session["hardwareSearchType"], null))
            {
                rblSearchType.SelectedValue = (string)Session["hardwareSearchType"];
            }

            txtOlafName.Text = SearchValues["olafName"];
            txtInventoryNo.Text = SearchValues["inventoryNo"];
            txtLastName.Text = SearchValues["lastName"];
            txtFirstName.Text = SearchValues["firstName"];
            txtPhone.Text = SearchValues["phone"];
            txtBuilding.Text = SearchValues["building"];
            txtOffice.Text = SearchValues["office"];
            txtModel.Text = SearchValues["model"];
            txtDescription.Text = SearchValues["description"];
            txtSerial.Text = SearchValues["serial"];
            txtUnit.Text = SearchValues["unit"];
            txtRmtCtgNm.Text = SearchValues["rmtCtgNm"];
            txtRmtParentCtgNm.Text = SearchValues["rmtParentCtgNm"];
            txtMaintenanceEndDateFrom.Text = SearchValues["MaintenanceEndDateFrom"];
            txtMaintenanceEndDateTo.Text = SearchValues["MaintenanceEndDateTo"];
            ddlStatus.SelectedValue = SearchValues["status"];
            ddlLocal.SelectedValue = SearchValues["local"];

            if (!Equals(Session["hardwareSearchValues1"], null))
            {
                Dictionary<string, string> searchValues1 = (Dictionary<string, string>)Session["hardwareSearchValues1"];
                txtDateFrom.Text = searchValues1["dateFrom"];
                txtDateTo.Text = searchValues1["dateTo"];
                ddlType.SelectedValue = searchValues1["type"];
            }
        }

        private void GetSearchValues()
        {
            Session["hardwareSearchType"] = rblSearchType.SelectedValue;

            SearchValues["status"] = ddlStatus.SelectedItem.Value;
            SearchValues["local"] = ddlLocal.SelectedItem.Value;
            SearchValues["olafName"] = txtOlafName.Text;
            SearchValues["inventoryNo"] = txtInventoryNo.Text;
            SearchValues["lastName"] = txtLastName.Text;
            SearchValues["firstName"] = txtFirstName.Text;
            SearchValues["phone"] = txtPhone.Text;
            SearchValues["building"] = txtBuilding.Text;
            SearchValues["office"] = txtOffice.Text;
            SearchValues["model"] = txtModel.Text;
            SearchValues["description"] = txtDescription.Text;
            SearchValues["serial"] = txtSerial.Text;
            SearchValues["unit"] = txtUnit.Text;
            SearchValues["rmtCtgNm"] = txtRmtCtgNm.Text;
            SearchValues["rmtParentCtgNm"] = txtRmtParentCtgNm.Text;
            SearchValues["MaintenanceEndDateFrom"] = txtMaintenanceEndDateFrom.Text;
            SearchValues["MaintenanceEndDateTo"] = txtMaintenanceEndDateTo.Text;

            Dictionary<string, string> searchValues1 = new Dictionary<string, string>();
            searchValues1.Add("dateFrom", txtDateFrom.Text);
            searchValues1.Add("dateTo", txtDateTo.Text);
            searchValues1.Add("type", ddlType.SelectedItem.Value);
            Session["hardwareSearchValues1"] = searchValues1;
        }

        private HardwareItemBL GetSelectedHardwareItem(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            HardwareItemBL item = new HardwareItemBL();
            item.Select(id);

            return item;
        }

        private SoftwareLicenseAssignmentBL GetSelectedLicenseAssignment(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems3.DataKeys[row.RowIndex]["Id"].ToString());

            SoftwareLicenseAssignmentBL item = new SoftwareLicenseAssignmentBL();
            item.Select(id);

            return item;
        }

        private void LoadGvItems()
        {
            ArrayList validationErrors = new ArrayList();

            List<HardwareItemBL> itemList = HardwareItemBL.GetByParameters(ref validationErrors, ddlStatus.SelectedValue,
                                                                ddlLocal.SelectedValue == "" ? null : (bool?)bool.Parse(ddlLocal.SelectedValue),
                                                                txtOlafName.Text,
                                                                txtInventoryNo.Text,
                                                                txtLastName.Text,
                                                                txtFirstName.Text,
                                                                txtPhone.Text,
                                                                txtBuilding.Text,
                                                                txtOffice.Text,
                                                                txtModel.Text,
                                                                txtDescription.Text,
                                                                txtSerial.Text,
                                                                txtUnit.Text,
                                                                txtRmtCtgNm.Text,
                                                                txtRmtParentCtgNm.Text,
                                                                txtMaintenanceEndDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtMaintenanceEndDateFrom.Text),
                                                                txtMaintenanceEndDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtMaintenanceEndDateTo.Text)
                                                                );

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblResultsCount.Text = gvItems1.Rows.Count.ToString();

            ((MasterPage)Master).ShowValidationMessages(validationErrors);

        }

        private void LoadGvItems(HardwareItemBL hardwareItem)
        {
            List<HardwareItemBL> itemList = new List<HardwareItemBL>();
            itemList.Add(hardwareItem);

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblResultsCount.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvHistoryItems(decimal? rmtMatId)
        {
            List<HardwareItemModificationBL> itemList = HardwareItemModificationBL.GetByRmtMatId(rmtMatId);

            gvItems2.DataSource = itemList;
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvHistoryItems()
        {
            List<HardwareItemModificationBL> itemList = HardwareItemModificationBL.GetByParameters(txtDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateFrom.Text),
                                                                   txtDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateTo.Text),
                                                                   ddlType.SelectedValue);

            gvItems2.DataSource = itemList;
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvSoftwareLicenseAssignments(int hardwareItemId)
        {
            List<SoftwareLicenseAssignmentBL> itemList = SoftwareLicenseAssignmentBL.GetByHardwareItem(hardwareItemId);

            gvItems3.DataSource = itemList;
            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
            lblItems3Count.Text = gvItems3.Rows.Count.ToString();
        }

        private void LoadGvDetectedSoftwareInstallations(int hardwareItemId)
        {
            List<DetectedSoftwareInstallationBL> itemList = DetectedSoftwareInstallationBL.GetByHardwareItem(hardwareItemId);

            gvItems4.DataSource = itemList;
            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
            lblItems4Count.Text = gvItems4.Rows.Count.ToString();
        }

        private void LoadGvDetectedSoftwareUninstallations(int hardwareItemId)
        {
            List<DetectedSoftwareUninstallationBL> itemList = DetectedSoftwareUninstallationBL.GetByHardwareItem(hardwareItemId);

            gvItems5.DataSource = itemList;
            gvItems5.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems5);
            lblItems5Count.Text = gvItems5.Rows.Count.ToString();
        }

        #endregion
    }
}