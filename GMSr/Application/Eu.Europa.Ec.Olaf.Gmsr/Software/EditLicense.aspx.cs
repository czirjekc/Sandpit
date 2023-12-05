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
    public partial class EditLicense : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use upload inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnUpload);

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Back);
                ((MasterPage)Master).EnableAction(Action.Save);

                #endregion

                LoadDdlType();
                SetFormValues();
            }

            if (ddlType.SelectedValue == "2")
            {
                trQuantity.Visible = true;
            }
            else
            {
                trQuantity.Visible = false;
            }

            if (SelectedSoftwareLicense.Id == 0) // add new one
            {
                tMultiple.Visible = true;
            }
            
            
            if (cbMultiple.Checked == true)
            {
                trAmount.Visible = true;
            }
            else
            {
                trAmount.Visible = false;
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                ArrayList validationErrors = new ArrayList();

                GetFormValues(ref validationErrors);

                if (cbMultiple.Checked == true)
                {
                    int amount = int.Parse(txtAmount.Text);
                    for (int i = 0; i < amount; i++)
                    {
                        SelectedSoftwareLicense.Id = 0; // so that a new item will be added
                        SelectedSoftwareLicense.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                    }
                }
                else
                {
                    SelectedSoftwareLicense.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                }

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
                    SelectedSoftwareLicense.File = new byte[fuDocument.PostedFile.InputStream.Length + 1];
                    SelectedSoftwareLicense.Filename = fuDocument.FileName;
                    fuDocument.PostedFile.InputStream.Read(SelectedSoftwareLicense.File, 0, SelectedSoftwareLicense.File.Length);
                }
                catch (Exception ex)
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
                lblUploadedInfo.Text = SelectedSoftwareLicense.FileInfo;
            }
        }

        #endregion

        #region Public Methods

        [System.Web.Services.WebMethod]
        public static string[] GetLicenseCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<SoftwareLicenseBL> itemList = SoftwareLicenseBL.GetByInfix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        #endregion

        #region Private Methods

        private void SetFormValues()
        {
            if (SelectedSoftwareLicense.SoftwareOrderItemTypeId == 2)
            {
                lblTitle.Text = "Upgrade License Details";
            }

            lblIdValue.Text = SelectedSoftwareLicense.Id.ToString();

            txtComment.Text = SelectedSoftwareLicense.Comment;
            txtQuantity.Text = SelectedSoftwareLicense.MultiUserQuantity.HasValue ? SelectedSoftwareLicense.MultiUserQuantity.Value.ToString() : "";
            txtSerial.Text = SelectedSoftwareLicense.SerialKey;            
            lblUploadedInfo.Text = SelectedSoftwareLicense.FileInfo;

            ddlType.SelectedValue = SelectedSoftwareLicense.SoftwareLicenseTypeId.HasValue ? SelectedSoftwareLicense.SoftwareLicenseTypeId.Value.ToString() : "";
        }

        private void GetFormValues(ref ArrayList validationErrorList)
        {
            //if this is a newly added softwareLicense, then the softwareOrderItemId must be set
            if (SelectedSoftwareLicense.SoftwareOrderItemId == null)
            {
                SelectedSoftwareLicense.SoftwareOrderItemId = SelectedSoftwareOrderItem.Id;
            }

            SelectedSoftwareLicense.Comment = txtComment.Text;

            SelectedSoftwareLicense.MultiUserQuantity = txtQuantity.Text == "" ? null : (int?)int.Parse(txtQuantity.Text);
            SelectedSoftwareLicense.SerialKey = txtSerial.Text;            

            SelectedSoftwareLicense.SoftwareLicenseTypeId = ddlType.SelectedValue == "" ? null : (int?)int.Parse(ddlType.SelectedValue);
        }

        private void LoadDdlType()
        {
            ddlType.DataSource = SoftwareLicenseTypeBL.GetAll();

            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "Id";

            ddlType.DataBind();

            ddlType.Items.Insert(0, new ListItem("", ""));            
        }

        #endregion
    }
}