using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Software
{
    public partial class EditDocument : BasePage
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
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                GetFormValues();

                ArrayList validationErrors = new ArrayList();
                SelectedOrderFormDocument.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                ((MasterPage)Master).ShowValidationMessages(validationErrors);
                if (validationErrors.Count == 0)
                {
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();

            if (fuDocument.HasFile)
            {
                try
                {
                    SelectedOrderFormDocument.File = new byte[fuDocument.PostedFile.InputStream.Length + 1];
                    SelectedOrderFormDocument.Filename = fuDocument.FileName;
                    fuDocument.PostedFile.InputStream.Read(SelectedOrderFormDocument.File, 0, SelectedOrderFormDocument.File.Length);
                }
                catch(Exception ex)
                {
                    validationErrors.Add("Error: " + ex.Message);                    
                }
            }
            else
            {                
                validationErrors.Add("There's no file selected to upload.");
            }
            ((MasterPage)Master).ShowValidationMessages(validationErrors);

            if (validationErrors.Count == 0)
            {
                lblUploadedInfo.Text = SelectedOrderFormDocument.FileInfo;
            }
        }

        #endregion

        #region Private Methods

        private void SetFormValues()
        {
            txtDescription.Text = SelectedOrderFormDocument.Description;            
            lblUploadedInfo.Text = SelectedOrderFormDocument.FileInfo;
            txtPath.Text = SelectedOrderFormDocument.Path;
        }

        private void GetFormValues()
        {            
            //if this is a newly added document, then the orderFormId must be set
            if (SelectedOrderFormDocument.OrderFormId == null)
            {
                SelectedOrderFormDocument.OrderFormId = SelectedOrderForm.Id;
            }

            SelectedOrderFormDocument.Description = txtDescription.Text;            
                                 
            SelectedOrderFormDocument.Path = txtPath.Text;
        }

        #endregion
    }
}