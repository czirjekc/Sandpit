using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;
using System.Transactions;

namespace Eu.Europa.Ec.Olaf.Gmsr.Software
{
    public partial class EditProduct : BasePage
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

                LoadDdlStatus();

                SetFormValues();
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();
                ArrayList validationErrors = new ArrayList();
                ValidateDetectedProduct(ref validationErrors);
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                ArrayList validationErrors = new ArrayList();

                GetFormValues();

                SelectedSoftwareProduct.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                #region Save the Detected Product

                // if there was already a match -> delete it
                // Todo: the following is not correct when you allow multiple matches for one product!
                if (SelectedSoftwareProduct.HasMatchWithDetectedSoftwareProduct)
                {
                    DetectedSoftwareProductMatchBL match = (DetectedSoftwareProductMatchBL.GetBySoftwareProduct(SelectedSoftwareProduct.Id)).First();
                    match.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);
                }

                if (txtDetectedProduct.Text != "")
                {
                    DetectedSoftwareProductMatchBL match = new DetectedSoftwareProductMatchBL();

                    match.SoftwareProductId = SelectedSoftwareProduct.Id;
                    match.DetectedSoftwareProductId = Utilities.GetIdFromConcatenation(txtDetectedProduct.Text);

                    match.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                }

                #endregion

                ((MasterPage)Master).ShowValidationMessages(validationErrors);

                if (validationErrors.Count == 0)
                {
                    // select the added product in the editorder page                    
                    SelectedSoftwareOrderItem.SoftwareProductId = SelectedSoftwareProduct.Id;

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

        #endregion

        #region Public Methods

        [System.Web.Services.WebMethod]
        public static string[] GetDetectedProductCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<DetectedSoftwareProductBL> itemList = DetectedSoftwareProductBL.GetByInfix(prefixText);
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
            txtComment.Text = SelectedSoftwareProduct.Comment;
            txtCompany.Text = SelectedSoftwareProduct.CompanyName;
            txtName.Text = SelectedSoftwareProduct.Name;
            txtOther.Text = SelectedSoftwareProduct.Other;
            txtVersion.Text = SelectedSoftwareProduct.Version;

            ddlStatus.SelectedValue = SelectedSoftwareProduct.SoftwareProductStatusId.HasValue ? SelectedSoftwareProduct.SoftwareProductStatusId.Value.ToString() : "";

            // set the detected software product
            if (SelectedSoftwareProduct.HasMatchWithDetectedSoftwareProduct)
            {
                DetectedSoftwareProductMatchBL match = (DetectedSoftwareProductMatchBL.GetBySoftwareProduct(SelectedSoftwareProduct.Id)).First();
                DetectedSoftwareProductBL detectedItem = new DetectedSoftwareProductBL();
                detectedItem.Select(match.DetectedSoftwareProductId);
                txtDetectedProduct.Text = detectedItem.Concatenation;
            }
        }

        private void GetFormValues()
        {
            SelectedSoftwareProduct.Comment = txtComment.Text;
            SelectedSoftwareProduct.CompanyName = txtCompany.Text;
            SelectedSoftwareProduct.Name = txtName.Text;
            SelectedSoftwareProduct.Other = txtOther.Text;
            SelectedSoftwareProduct.Version = txtVersion.Text;
            SelectedSoftwareProduct.Source = Constants.GMSR;

            SelectedSoftwareProduct.SoftwareProductStatusId = ddlStatus.SelectedValue == "" ? null : (int?)int.Parse(ddlStatus.SelectedValue);            
        }

        private void ValidateDetectedProduct(ref ArrayList validationErrorList)
        {
            if (txtDetectedProduct.Text != "" && Utilities.GetIdFromConcatenation(txtDetectedProduct.Text) == -1)
            {
                validationErrorList.Add("The Detected Product is not valid. You have to select one from the suggestions.");
                txtDetectedProduct.Text = "";
            }
        }

        private void LoadDdlStatus()
        {
            ddlStatus.DataSource = SoftwareProductStatusBL.GetAll();

            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "Id";

            ddlStatus.DataBind();

            ddlStatus.Items.Insert(0, new ListItem("", ""));
        }

        #endregion
    }
}