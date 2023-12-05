using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Software
{
    public partial class OrderItems : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            if (!IsPostBack)
            {
                LoadDdlOrderType();
                LoadDdlProductSource();
                LoadDdlAddOrderType();
                SetSearchValues();

                bool autoSearch = false;

                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    if (FromPage.Url.Contains("Products.aspx") && SelectedSoftwareProduct.Id != 0)
                    {
                        ClearSearchValues();
                        txtProductName.Text = SelectedSoftwareProduct.Name;
                        txtProductVersion.Text = SelectedSoftwareProduct.Version;
                        ddlProductSource.SelectedValue = SelectedSoftwareProduct.Source;

                        autoSearch = true;
                    }
                    else if (FromPage.Url.Contains("Main.aspx") && SelectedSoftwareLicense.Id != 0)
                    {
                        ClearSearchValues();
                        SelectedSoftwareOrderItem.Select(SelectedSoftwareLicense.SoftwareOrderItemId.Value);
                        LoadGvOrderItem(SelectedSoftwareOrderItem);
                    }
                    else if (SelectedSoftwareOrderItem.Id != 0)
                    {
                        LoadGvOrderItem(SelectedSoftwareOrderItem);
                    }

                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditOrderItem.aspx", "► Software ► Entries ► Entry Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicense.aspx", "► Software ► Entries ► Entry Details ► License Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditOrderForm.aspx", "► Software ► Entries ► Entry Details ► Order Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditDocument.aspx", "► Software ► Entries ► Entry Details ► Order Details ► Document Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditProduct.aspx", "► Software ► Entries ► Entry Details ► Product Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditMediaItem.aspx", "► Software ► Entries ► Entry Details ► Media Item Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                if (PreviousPageStack.Count > 0)
                {
                    ((MasterPage)Master).EnableAction(Action.Back);
                }

                #endregion

                #region capture the enter key

                txtOrder.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOlafRef.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlType.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlProductSource.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtProductVersion.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateStartFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateStartTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateEndFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateEndTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtPreviousConcatenation.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtComment.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtCreator.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtCreationDateFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtCreationDateTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                cbWithoutUpgrade.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion

                if (autoSearch)
                {
                    GetSearchValues();
                    LoadGvOrderItem();
                }
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                GetSearchValues();

                LoadGvOrderItem();
                up1.Update();
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

        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SoftwareOrderItemBL item = (SoftwareOrderItemBL)e.Row.DataItem;
                if (item.SoftwareProductSource == "ABAC")
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "Id")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
                }
                else if (item.SoftwareOrderItemTypeId == 3)
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#b66d19");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "Id")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#e2bc8f");
                }
                else if (item.HasUpgrade.HasValue && item.HasUpgrade.Value)
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#778db7");
                }
            }
        }

        protected void ibtnGoToProduct_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = GetSelectedItem((ImageButton)sender);

            SelectedSoftwareProduct = null;

            FromPage = GetCurrentPage();
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/Products.aspx");
        }

        protected void ibtnGoToLicenses_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = GetSelectedItem((ImageButton)sender);

            SelectedSoftwareLicense = null;

            FromPage = GetCurrentPage();
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/Main.aspx");
        }

        protected void ibtnGoToPredecessor_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = GetSelectedItem((ImageButton)sender);
            if (SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId.HasValue)
            {
                SoftwareOrderItemBL orderItem = new SoftwareOrderItemBL();
                orderItem.Select(SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId.Value);
                LoadGvOrderItem(orderItem);
                up1.Update();
            }
            else
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("There is no predecessor.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void ibtnGoToSuccessors_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = GetSelectedItem((ImageButton)sender);

            ClearSearchValues();
            ddlProductSource.SelectedValue = "";
            txtPreviousConcatenation.Text = SelectedSoftwareOrderItem.Id.ToString();
            GetSearchValues();
            LoadGvOrderItem();

            up1.Update();
            upSearch.Update();
        }

        protected void ibtnAddUpgrade_Click(object sender, EventArgs e)
        {
            SoftwareOrderItemBL orderItem = GetSelectedItem((ImageButton)sender);

            if (orderItem.SoftwareProductSource != Constants.GMSR && orderItem.SoftwareProductSource != Constants.OLD_GMS)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not add upgrade license(s) to an entry with product source='" + orderItem.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else if (orderItem.SoftwareOrderItemTypeId == 3)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not add upgrade license(s) to a maintenance.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else
            {
                SelectedSoftwareOrderItem = null;
                SelectedSoftwareOrderItem.SoftwareOrderItemTypeId = 2;
                SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId = orderItem.Id;

                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditOrderItem.aspx");
            }
        }

        protected void ibtnAddMaintenance_Click(object sender, EventArgs e)
        {
            SoftwareOrderItemBL orderItem = GetSelectedItem((ImageButton)sender);

            if (orderItem.SoftwareProductSource != Constants.GMSR && orderItem.SoftwareProductSource != Constants.OLD_GMS)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not add a maintenance to an entry with product source='" + orderItem.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else if (orderItem.SoftwareOrderItemTypeId == 3)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not add a maintenance to a maintenance. In case of a renewal maintenance, add it to the entry where to the previous maintenance is attached.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else
            {
                SelectedSoftwareOrderItem = null;
                SelectedSoftwareOrderItem.SoftwareOrderItemTypeId = 3;
                SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId = orderItem.Id;

                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditOrderItem.aspx");
            }
        }

        protected void ibtnEdit_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = GetSelectedItem((ImageButton)sender);

            if (SelectedSoftwareOrderItem.SoftwareProductSource == Constants.ABAC)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not edit an entry with product source='" + SelectedSoftwareOrderItem.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else
            {                
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditOrderItem.aspx");
            }

        }

        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = GetSelectedItem((ImageButton)sender);

            if (SelectedSoftwareOrderItem.SoftwareProductSource == Constants.GMSR || SelectedSoftwareOrderItem.SoftwareProductSource == Constants.OLD_GMS)
            {
                ArrayList validationErrors = new ArrayList();
                SelectedSoftwareOrderItem.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

                ((MasterPage)Master).ShowValidationMessages(validationErrors);
                if (validationErrors.Count == 0)
                {
                    //clear gridview
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblItems1Count.Text = "0";
                    up1.Update();
                }
            }
            else
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not delete an entry with product source='" + SelectedSoftwareOrderItem.SoftwareProductSource + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void ibtnAdd_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = null;
            SelectedSoftwareOrderItem.SoftwareOrderItemTypeId = int.Parse(ddlAddOrderType.SelectedValue);

            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditOrderItem.aspx");
        }

        protected void ibtnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
        }

        protected void ddlAddOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAddOrderType.SelectedValue == "")
            {
                ibtnAdd.Enabled = false;
                ibtnAdd.ImageUrl = "~/Images/AddBigGray.png";
            }
            else
            {
                ibtnAdd.Enabled = true;
                ibtnAdd.ImageUrl = "~/Images/AddBig.png";
            }
            upAdd.Update();
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

        private void ClearSearchValues()
        {
            txtOrder.Text = "";
            txtOlafRef.Text = "";
            txtProductName.Text = "";
            txtProductVersion.Text = "";
            txtDateStartFrom.Text = "";
            txtDateStartTo.Text = "";
            txtDateEndFrom.Text = "";
            txtDateEndTo.Text = "";
            txtPreviousConcatenation.Text = "";
            txtComment.Text = "";
            txtCreator.Text = "";
            txtCreationDateFrom.Text = "";
            txtCreationDateTo.Text = "";
            ddlType.SelectedValue = "";
            ddlProductSource.SelectedValue = "GMSr";
            cbWithoutUpgrade.Checked = false;
        }

        private void SetSearchValues()
        {
            if (!Equals(Session["softwareOrderItemSearchValues"], null))
            {
                Dictionary<string, string> searchValues = (Dictionary<string, string>)Session["softwareOrderItemSearchValues"];
                txtOrder.Text = searchValues["Order"];
                txtOlafRef.Text = searchValues["OlafRef"];
                txtProductName.Text = searchValues["ProductName"];
                txtProductVersion.Text = searchValues["ProductVersion"];
                txtDateStartFrom.Text = searchValues["DateStartFrom"];
                txtDateStartTo.Text = searchValues["DateStartTo"];
                txtDateEndFrom.Text = searchValues["DateEndFrom"];
                txtDateEndTo.Text = searchValues["DateEndTo"];
                txtPreviousConcatenation.Text = searchValues["PreviousConcatenation"];
                txtComment.Text = searchValues["Comment"];
                txtCreator.Text = searchValues["Creator"];
                txtCreationDateFrom.Text = searchValues["CreationDateFrom"];
                txtCreationDateTo.Text = searchValues["CreationDateTo"];
                ddlType.SelectedValue = searchValues["Type"];
                ddlProductSource.SelectedValue = searchValues["ProductSource"];
                cbWithoutUpgrade.Checked = Boolean.Parse(searchValues["WithoutUpgrade"]);
            }
        }

        private void GetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();
            searchValues.Add("Order", txtOrder.Text);
            searchValues.Add("OlafRef", txtOlafRef.Text);
            searchValues.Add("ProductName", txtProductName.Text);
            searchValues.Add("ProductVersion", txtProductVersion.Text);
            searchValues.Add("DateStartFrom", txtDateStartFrom.Text);
            searchValues.Add("DateStartTo", txtDateStartTo.Text);
            searchValues.Add("DateEndFrom", txtDateEndFrom.Text);
            searchValues.Add("DateEndTo", txtDateEndTo.Text);
            searchValues.Add("PreviousConcatenation", txtPreviousConcatenation.Text);
            searchValues.Add("Comment", txtComment.Text);
            searchValues.Add("Creator", txtCreator.Text);
            searchValues.Add("CreationDateFrom", txtCreationDateFrom.Text);
            searchValues.Add("CreationDateTo", txtCreationDateTo.Text);
            searchValues.Add("Type", ddlType.SelectedItem.Value);
            searchValues.Add("ProductSource", ddlProductSource.SelectedItem.Value);
            searchValues.Add("WithoutUpgrade", cbWithoutUpgrade.Checked.ToString());
            Session["softwareOrderItemSearchValues"] = searchValues;
        }

        private void LoadDdlOrderType()
        {
            ddlType.DataSource = SoftwareOrderItemTypeBL.GetAll();

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

            ddlProductSource.SelectedValue = Constants.GMSR;
        }

        private SoftwareOrderItemBL GetSelectedItem(ImageButton button)
        {
            // get item id of row
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            SoftwareOrderItemBL item = new SoftwareOrderItemBL();
            item.Select(id);

            return item;
        }

        private void LoadGvOrderItem()
        {
            List<SoftwareOrderItemBL> itemList = SoftwareOrderItemBL.GetByParameters(
                                                                        txtOrder.Text,
                                                                        txtOlafRef.Text,
                                                                        ddlType.SelectedValue == "" ? null : (int?)int.Parse(ddlType.SelectedValue),
                                                                        ddlProductSource.SelectedValue,
                                                                        txtProductName.Text,
                                                                        txtProductVersion.Text,
                                                                        txtDateStartFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateStartFrom.Text),
                                                                        txtDateStartTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateStartTo.Text),
                                                                        txtDateEndFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateEndFrom.Text),
                                                                        txtDateEndTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateEndTo.Text),
                                                                        txtPreviousConcatenation.Text,
                                                                        txtComment.Text,
                                                                        txtCreator.Text,
                                                                        txtCreationDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtCreationDateFrom.Text),
                                                                        txtCreationDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtCreationDateTo.Text),
                                                                        cbWithoutUpgrade.Checked
                                                                        );

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvOrderItem(SoftwareOrderItemBL orderItem)
        {
            List<SoftwareOrderItemBL> itemList = new List<SoftwareOrderItemBL>();
            itemList.Add(orderItem);

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadDdlAddOrderType()
        {
            ddlAddOrderType.DataSource = SoftwareOrderItemTypeBL.GetAll();

            ddlAddOrderType.DataTextField = "Name";
            ddlAddOrderType.DataValueField = "Id";

            ddlAddOrderType.DataBind();

            ddlAddOrderType.Items.Insert(0, new ListItem("", ""));
        }

        #endregion
    }
}