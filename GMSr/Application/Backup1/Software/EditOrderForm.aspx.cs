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
    public partial class EditOrderForm : BasePage
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
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                GetFormValues();

                ArrayList validationErrors = new ArrayList();
                SelectedOrderForm.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                ((MasterPage)Master).ShowValidationMessages(validationErrors);
                if (validationErrors.Count == 0)
                {
                    // select the added orderform in the editorder page                    
                    SelectedSoftwareOrderItem.OrderFormId = SelectedOrderForm.Id;

                    FromPage = GetCurrentPage();
                    Response.Redirect(PreviousPageStack.Pop().Url);
                }
            }
            else if (((MasterPage)Master).CurrentAction == Action.Back)
            {
                FromPage = GetCurrentPage();
                Response.Redirect(PreviousPageStack.Pop().Url);
            }
        }

        protected void ibtnDownloadDocumentFile_Click(object sender, EventArgs e)
        {
            SelectedOrderFormDocument = GetSelectedItem((ImageButton)sender);

            if (SelectedOrderFormDocument.File != null)
            {
                Utilities.DownloadFile(SelectedOrderFormDocument.Filename, SelectedOrderFormDocument.File, SelectedOrderFormDocument.FileSize, Response);
            }
            else
            {
                ArrayList validationErrors = new ArrayList();
                validationErrors.Add("There is no file.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }
        
        protected void ibtnEditDocument_Click(object sender, EventArgs e)
        {
            SelectedOrderFormDocument = GetSelectedItem((ImageButton)sender);
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditDocument.aspx");
        }

        protected void ibtnDeleteDocument_Click(object sender, EventArgs e)
        {
            SelectedOrderFormDocument = GetSelectedItem((ImageButton)sender);

            ArrayList validationErrors = new ArrayList();
            SelectedOrderFormDocument.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                LoadGvDocument(SelectedOrderForm.Id);
            }
        }

        protected void ibtnAddDocument_Click(object sender, EventArgs e)
        {
            SelectedOrderFormDocument = null;

            GetFormValues();

            ArrayList validationErrors = new ArrayList();
            SelectedOrderForm.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Software/EditDocument.aspx");
            }
        }

        #endregion

        #region Private Methods

        private OrderFormDocumentBL GetSelectedItem(ImageButton button)
        {
            // get item id of row           
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            OrderFormDocumentBL item = new OrderFormDocumentBL();
            item.Select(id);

            return item;
        }

        private void SetFormValues()
        {
            txtName.Text = SelectedOrderForm.Name;

            LoadGvDocument(SelectedOrderForm.Id);
        }

        private void GetFormValues()
        {
            SelectedOrderForm.Name = txtName.Text;
        }

        private void LoadGvDocument(int orderFormId)
        {
            gvItems1.DataSource = OrderFormDocumentBL.GetByOrderForm(orderFormId);
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }  

        #endregion
    }
}