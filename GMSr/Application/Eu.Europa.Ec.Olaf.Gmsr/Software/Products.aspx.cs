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
    public partial class Products : BasePage
    {   
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            if (!IsPostBack)
            {
                LoadDdlStatus();
                LoadDdlSource();
                SetSearchValues();

                bool autoSearch = false;

                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    if (FromPage.Url.Contains("Main.aspx") && SelectedSoftwareLicense.Id != 0)
                    {
                        ClearSearchValues();                        
                        txtName.Text = SelectedSoftwareLicense.SoftwareProductName;
                        txtVersion.Text = SelectedSoftwareLicense.SoftwareProductVersion;
                        txtCompany.Text = SelectedSoftwareLicense.SoftwareProductCompanyName;
                        ddlSource.SelectedValue = SelectedSoftwareLicense.SoftwareProductSource;
                                                
                        autoSearch = true;
                    }
                    else if (FromPage.Url.Contains("OrderItems.aspx") && SelectedSoftwareOrderItem.Id != 0)
                    {
                        ClearSearchValues();
                        txtName.Text = SelectedSoftwareOrderItem.SoftwareProductName;
                        txtVersion.Text = SelectedSoftwareOrderItem.SoftwareProductVersion;
                        txtCompany.Text = SelectedSoftwareOrderItem.SoftwareProductCompany;
                        ddlSource.SelectedValue = SelectedSoftwareOrderItem.SoftwareProductSource;

                        autoSearch = true;
                    }
                    else if (SelectedSoftwareProduct.Id != 0)
                    {
                        LoadGvProduct(SelectedSoftwareProduct);
                    }
                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditProduct.aspx", "► Software ► Products ► Product Details"));

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

                txtName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtVersion.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtCompany.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOther.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtComment.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");                
                ddlStatus.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlSource.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion

                if (autoSearch)
                {
                    GetSearchValues();
                    LoadGvProduct();                    
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

                LoadGvProduct();
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
                SoftwareProductBL item = (SoftwareProductBL)e.Row.DataItem;
                if (item.Source == "ABAC")
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#777777");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "Id")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
                }
                else if (item.HasMatchWithDetectedSoftwareProduct)
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#009900");
                    e.Row.Cells[Utilities.GetGridViewIndex(gvItems1, "Id")].ForeColor = System.Drawing.ColorTranslator.FromHtml("#99FF99");
                }
            }
        }

        protected void ibtnGoToLicense_Click(object sender, EventArgs e)
        {
            SelectedSoftwareProduct = GetSelectedItem((ImageButton)sender);

            SelectedSoftwareLicense = null;

            FromPage = GetCurrentPage();
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/Main.aspx");
        }

        protected void ibtnGoToEntries_Click(object sender, EventArgs e)
        {
            SelectedSoftwareProduct = GetSelectedItem((ImageButton)sender);

            SelectedSoftwareOrderItem = null;

            FromPage = GetCurrentPage();
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/OrderItems.aspx");
        }        

        protected void ibtnAddLicenses_Click(object sender, EventArgs e)
        {
            SelectedSoftwareProduct = GetSelectedItem((ImageButton)sender);

            if (SelectedSoftwareProduct.Source == Constants.GMSR || SelectedSoftwareProduct.Source == Constants.OLD_GMS)
            {
                SelectedSoftwareOrderItem = null;
                SelectedSoftwareOrderItem.SoftwareOrderItemTypeId = 1;
                SelectedSoftwareOrderItem.SoftwareProductId = SelectedSoftwareProduct.Id;

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditOrderItem.aspx", "► Software ► Products ► Entry Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicense.aspx", "► Software ► Products ► Entry Details ► License Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditProduct.aspx", "► Software ► Products ► Entry Details ► Product Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditMediaItem.aspx", "► Software ► Products ► Entry Details ► Media Item Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditOrderForm.aspx", "► Software ► Products ► Entry Details ► Order Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditDocument.aspx", "► Software ► Products ► Entry Details ► Order Details ► Document Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditOrderItem.aspx");
            }
            else
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not add licenses to a product with source='" + SelectedSoftwareProduct.Source + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }
        
        protected void ibtnEdit_Click(object sender, EventArgs e)
        {
            SelectedSoftwareProduct = GetSelectedItem((ImageButton)sender);

            if (SelectedSoftwareProduct.Source == Constants.ABAC)
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("You can not edit a product with source='" + SelectedSoftwareProduct.Source + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
            else
            {                
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditProduct.aspx");
            }
        }        

        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            SelectedSoftwareProduct = GetSelectedItem((ImageButton)sender);
            if (SelectedSoftwareProduct.Source == Constants.GMSR || SelectedSoftwareProduct.Source == Constants.OLD_GMS)
            {
                ArrayList validationErrors = new ArrayList();
                SelectedSoftwareProduct.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

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
                validationErrors.Add("You can not delete a product with source='" + SelectedSoftwareProduct.Source + "'.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void ibtnAdd_Click(object sender, EventArgs e)
        {
            SelectedSoftwareProduct = null;
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditProduct.aspx");
        }

        protected void ibtnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
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
            txtName.Text = "";
            txtVersion.Text = "";
            txtCompany.Text = "";
            txtOther.Text = "";
            txtComment.Text = "";                                   
            ddlStatus.SelectedValue = "";
            ddlSource.SelectedValue = "GMSr";
        }

        private void SetSearchValues()
        {            
            if (!Equals(Session["softwareProductSearchValues"], null))
            {
                Dictionary<string, string> searchValues = (Dictionary<string, string>)Session["softwareProductSearchValues"];
                txtName.Text = searchValues["Name"];
                txtVersion.Text = searchValues["Version"];
                txtCompany.Text = searchValues["Company"];
                txtOther.Text = searchValues["Other"];
                txtComment.Text = searchValues["Comment"];                
                ddlStatus.SelectedValue = searchValues["Status"];
                ddlSource.SelectedValue = searchValues["Source"];              
            }          
        }

        private void GetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();
            searchValues.Add("Name", txtName.Text);
            searchValues.Add("Version", txtVersion.Text);
            searchValues.Add("Company", txtCompany.Text);
            searchValues.Add("Other", txtOther.Text);
            searchValues.Add("Comment", txtComment.Text);
            searchValues.Add("Status", ddlStatus.SelectedItem.Value);
            searchValues.Add("Source", ddlSource.SelectedItem.Value);            
            Session["softwareProductSearchValues"] = searchValues;    
        }
        
        private void LoadDdlStatus()
        {
            ddlStatus.DataSource = SoftwareProductStatusBL.GetAll();

            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "Id";

            ddlStatus.DataBind();

            ddlStatus.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadDdlSource()
        {
            ddlSource.Items.Add("");
            ddlSource.Items.Add(Constants.GMSR);
            ddlSource.Items.Add(Constants.OLD_GMS);
            ddlSource.Items.Add(Constants.ABAC);

            ddlSource.SelectedValue = Constants.GMSR;
        }
        
        private SoftwareProductBL GetSelectedItem(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            SoftwareProductBL item = new SoftwareProductBL();
            item.Select(id);

            return item;
        }

        private void LoadGvProduct()
        {
            List<SoftwareProductBL> itemList = SoftwareProductBL.GetByParameters(
                                                                    txtName.Text,
                                                                    txtVersion.Text,
                                                                    txtCompany.Text,
                                                                    txtOther.Text,                                                                    
                                                                    ddlStatus.SelectedValue == "" ? null : (int?)int.Parse(ddlStatus.SelectedValue),
                                                                    txtComment.Text,
                                                                    ddlSource.SelectedValue
                                                                 );

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvProduct(SoftwareProductBL product)
        {
            List<SoftwareProductBL> itemList = new List<SoftwareProductBL>();
            itemList.Add(product);

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        #endregion
    }
}