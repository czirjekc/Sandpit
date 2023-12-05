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
    public partial class EditOrderItem : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Back);
                ((MasterPage)Master).EnableAction(Action.Save);

                #endregion

                SetFormValues();
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                ArrayList validationErrors = new ArrayList();

                GetFormValues(ref validationErrors);

                ((MasterPage)Master).ShowValidationMessages(validationErrors);
                if (validationErrors.Count == 0)
                {
                    if (SelectedSoftwareOrderItem.Id == 0)
                    {
                        SelectedSoftwareOrderItem.CreationDate = DateTime.Now;
                        SelectedSoftwareOrderItem.CreatorLogin = ((MasterPage)Master).UserLogin;
                    }

                    SelectedSoftwareOrderItem.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                    ((MasterPage)Master).ShowValidationMessages(validationErrors);
                    if (validationErrors.Count == 0)
                    {
                        SelectedSoftwareOrderItem.Select(SelectedSoftwareOrderItem.Id);
                        FromPage = GetCurrentPage();
                        Response.Redirect(PreviousPageStack.Pop().Url);
                    }
                }
            }
            else if (((MasterPage)Master).CurrentAction == Action.Back)
            {
                FromPage = GetCurrentPage();
                Response.Redirect(PreviousPageStack.Pop().Url);
            }
        }

        protected void ibtnDownloadLicenseFile_Click(object sender, EventArgs e)
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
            ArrayList validationErrors = new ArrayList();
            GetFormValues(ref validationErrors);

            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            #region Grant access to additional pages

            if (SelectedSoftwareLicense.SoftwareOrderItemTypeId == 1) // License
            {
                ((MasterPage)Master).AdditionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicense.aspx", "► Software ► Entries ► Entry Details ► License Details"));
            }
            else if (SelectedSoftwareLicense.SoftwareOrderItemTypeId == 2) // Upgrade License
            {
                ((MasterPage)Master).AdditionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicense.aspx", "► Software ► Entries ► Entry Details ► Upgrade License Details"));
            }

            #endregion

            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditLicense.aspx");
        }

        protected void ibtnDeleteLicense_Click(object sender, EventArgs e)
        {
            SelectedSoftwareLicense = GetSelectedLicense((ImageButton)sender);

            ArrayList validationErrors = new ArrayList();
            SelectedSoftwareLicense.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                LoadGvLicense(SelectedSoftwareOrderItem.Id); // reload gridview

                #region if necessary disable the add license button

                if ((SelectedSoftwareOrderItem.SoftwareOrderItemTypeId.Value == 1 || SelectedSoftwareOrderItem.SoftwareOrderItemTypeId.Value == 2 || SelectedSoftwareOrderItem.SoftwareOrderItemTypeId.Value == 3) && gvItems1.Rows.Count == 0) // License, License Upgrade, License Lifetime Extension
                {
                    ibtnAddLicense.Enabled = true;
                    ibtnAddLicense.ImageUrl = "~/Images/Add.png";
                }

                #endregion
            }
        }

        protected void ibtnEditMediaItem_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();
            GetFormValues(ref validationErrors);

            SelectedMediaItem = GetSelectedMediaItem((ImageButton)sender);
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditMediaItem.aspx");
        }

        protected void ibtnDeleteMediaItem_Click(object sender, EventArgs e)
        {
            SelectedMediaItem = GetSelectedMediaItem((ImageButton)sender);

            ArrayList validationErrors = new ArrayList();
            SelectedMediaItem.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                LoadGvMediaItem(SelectedSoftwareOrderItem.Id); // reload gridview                
            }
        }

        protected void ibtnAddOrderForm_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();
            GetFormValues(ref validationErrors);

            SelectedOrderForm = null;
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditOrderForm.aspx");
        }

        protected void ibtnAddProduct_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();
            GetFormValues(ref validationErrors);

            SelectedSoftwareProduct = null;
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditProduct.aspx");
        }

        protected void ibtnAddLicense_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();
            GetFormValues(ref validationErrors);

            if (SelectedSoftwareOrderItem.Id == 0)
            {
                SelectedSoftwareOrderItem.CreationDate = DateTime.Now;
                SelectedSoftwareOrderItem.CreatorLogin = ((MasterPage)Master).UserLogin;
                SelectedSoftwareOrderItem.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
            }

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                SelectedSoftwareLicense = null;
                SelectedSoftwareLicense.SoftwareLicenseTypeId = 1;
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditLicense.aspx");
            }
        }

        protected void ibtnAddMediaItem_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();
            GetFormValues(ref validationErrors);

            if (SelectedSoftwareOrderItem.Id == 0)
            {
                SelectedSoftwareOrderItem.CreationDate = DateTime.Now;
                SelectedSoftwareOrderItem.CreatorLogin = ((MasterPage)Master).UserLogin;
                SelectedSoftwareOrderItem.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
            }

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                SelectedMediaItem = null;
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditMediaItem.aspx");
            }
        }

        #endregion

        #region Public Methods

        [System.Web.Services.WebMethod]
        public static string[] GetLicensePackCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<SoftwareOrderItemBL> itemList = SoftwareOrderItemBL.GetPackByInfix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        [System.Web.Services.WebMethod]
        public static string[] GetProductCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<SoftwareProductBL> itemList = SoftwareProductBL.GetByInfix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        [System.Web.Services.WebMethod]
        public static string[] GetOrderFormCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<OrderFormBL> itemList = OrderFormBL.GetByInfix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        #endregion

        #region Private Methods

        private SoftwareLicenseBL GetSelectedLicense(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            SoftwareLicenseBL item = new SoftwareLicenseBL();
            item.Select(id);

            return item;
        }

        private MediaItemBL GetSelectedMediaItem(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems2.DataKeys[row.RowIndex]["Id"].ToString());

            MediaItemBL item = new MediaItemBL();
            item.Select(id);

            return item;
        }

        private void SetFormValues()
        {
            SoftwareOrderItemTypeBL orderItemType = new SoftwareOrderItemTypeBL();
            orderItemType.Select(SelectedSoftwareOrderItem.SoftwareOrderItemTypeId.Value);
            lblTypeValue.Text = orderItemType.Name;

            lblIdValue.Text = SelectedSoftwareOrderItem.Id.ToString();
            txtComment.Text = SelectedSoftwareOrderItem.Comment;
            txtOlafRef.Text = SelectedSoftwareOrderItem.OlafRef;

            #region Fill textboxes with autocompletion

            txtOrderForm.Text = "";
            if (SelectedSoftwareOrderItem.OrderFormId.HasValue)
            {
                int id = SelectedSoftwareOrderItem.OrderFormId.Value;
                OrderFormBL orderForm = new OrderFormBL();
                orderForm.Select(id);

                txtOrderForm.Text = orderForm.Concatenation;
            }

            txtPrevious.Text = "";
            if (SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId.HasValue)
            {
                int id = SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId.Value;
                SoftwareOrderItemBL orderItem = new SoftwareOrderItemBL();
                orderItem.Select(id);

                txtPrevious.Text = orderItem.Concatenation;
            }

            txtProduct.Text = "";
            if (SelectedSoftwareOrderItem.SoftwareProductId.HasValue)
            {
                int id = SelectedSoftwareOrderItem.SoftwareProductId.Value;
                SoftwareProductBL product = new SoftwareProductBL();
                product.Select(id);

                txtProduct.Text = product.Concatenation;
            }

            #endregion

            LoadGvLicense(SelectedSoftwareOrderItem.Id);

            LoadGvMediaItem(SelectedSoftwareOrderItem.Id);

            if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 1) // License (Pack)
            {
                trPrevious.Visible = false;
                trProduct.Visible = true;
                trLicenses.Visible = true;
                trMaintenance.Visible = false;
            }
            else if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 2) // License Upgrade (Pack)
            {
                trPrevious.Visible = true;
                lblPrevious.Text = "Upgrade from (Entry):";
                trProduct.Visible = true;
                lblProduct.Text = "Upgrade to (Product):";
                trLicenses.Visible = true;
                lblLicense.Text = "Upgrade Licenses:";
                lblItems1CountPrefix.Text = "Upgrade Licenses:";
                trMaintenance.Visible = false;
            }
            else if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 3) // Maintenance
            {
                trPrevious.Visible = true;
                lblPrevious.Text = "Maintenance for (Entry):";
                trProduct.Visible = false;
                trLicenses.Visible = false;
                trMaintenance.Visible = true;
                txtFrom_CalendarExtender.SelectedDate = SelectedSoftwareOrderItem.MaintenanceStartDate;
                txtTo_CalendarExtender.SelectedDate = SelectedSoftwareOrderItem.MaintenanceEndDate;
            }
        }

        private void GetFormValues(ref ArrayList validationErrorList)
        {
            SelectedSoftwareOrderItem.Comment = txtComment.Text;
            SelectedSoftwareOrderItem.OlafRef = txtOlafRef.Text;

            SelectedSoftwareOrderItem.MaintenanceStartDate = txtFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtFrom.Text);
            SelectedSoftwareOrderItem.MaintenanceEndDate = txtTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtTo.Text);

            SelectedSoftwareOrderItem.OrderFormId = txtOrderForm.Text == "" ? (int?)null : Utilities.GetIdFromConcatenation(txtOrderForm.Text);
            if (SelectedSoftwareOrderItem.OrderFormId == -1)
            {
                validationErrorList.Add("The order is not valid. You have to select one from the suggestions.");
                txtOrderForm.Text = "";
                SelectedSoftwareOrderItem.OrderFormId = null;
            }

            SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId = txtPrevious.Text == "" ? (int?)null : Utilities.GetIdFromConcatenation(txtPrevious.Text);
            if (SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId == -1)
            {
                if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 2) // License Upgrade (Pack)
                {
                    validationErrorList.Add("The 'upgrade from' is not valid. You have to select one from the suggestions.");
                }
                else if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 3) // Maintenance
                {
                    validationErrorList.Add("The 'maintenance for' is not valid. You have to select one from the suggestions.");
                }

                txtPrevious.Text = "";
                SelectedSoftwareOrderItem.PreviousSoftwareOrderItemId = null;
            }

            SelectedSoftwareOrderItem.SoftwareProductId = txtProduct.Text == "" ? (int?)null : Utilities.GetIdFromConcatenation(txtProduct.Text);
            if (SelectedSoftwareOrderItem.SoftwareProductId == -1)
            {
                if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 1) // License (Pack)
                {
                    validationErrorList.Add("The product is not valid. You have to select one from the suggestions.");
                }
                else if (SelectedSoftwareOrderItem.SoftwareOrderItemTypeId == 2) // License Upgrade (Pack)
                {
                    validationErrorList.Add("The 'upgrade to' is not valid. You have to select one from the suggestions.");
                }

                txtProduct.Text = "";
                SelectedSoftwareOrderItem.SoftwareProductId = null;
            }
        }

        private void LoadGvLicense(int softwareOrderItemId)
        {
            gvItems1.DataSource = SoftwareLicenseBL.GetBySoftwareOrder(softwareOrderItemId);
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvMediaItem(int softwareOrderItemId)
        {
            gvItems2.DataSource = MediaItemBL.GetBySoftwareOrder(softwareOrderItemId);
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString();
        }

        #endregion
    }
}