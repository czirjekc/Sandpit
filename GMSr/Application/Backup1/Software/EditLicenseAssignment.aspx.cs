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
    public partial class EditLicenseAssignment : BasePage
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

            ((MasterPage)Master).ClearErrorMessages();

            ArrayList validationErrors = new ArrayList();

            GetFormValues(ref validationErrors);

            if (SelectedSoftwareLicenseAssignment.OlafUserId != null && SelectedSoftwareLicenseAssignment.HardwareItemId == null)
            {
                //change hardware item textbox to dropdown
                txtHardwareItem.Visible = false;
                cbHardwareItem.Visible = true;

                //fill the dropdown with the hardware items for the selected user
                OlafUserBL olafUser = new OlafUserBL();
                olafUser.Select(SelectedSoftwareLicenseAssignment.OlafUserId.Value);
                LoadDdlHardwareItem(olafUser.Login);
            }
            else if (SelectedSoftwareLicenseAssignment.OlafUserId == null && cbHardwareItem.Visible == true)
            {
                //change hardware item dropdown to textbox
                cbHardwareItem.Visible = false;
                txtHardwareItem.Visible = true;
            }
            else if (SelectedSoftwareLicenseAssignment.OlafUserId == null && SelectedSoftwareLicenseAssignment.HardwareItemId != null)
            {
                //select the owner of the hardware item
                HardwareItemBL hardwareItem = new HardwareItemBL();
                hardwareItem.Select(SelectedSoftwareLicenseAssignment.HardwareItemId.Value);
                OlafUserBL olafUser = OlafUserBL.GetByKeyword(hardwareItem.Login).First();
                txtUser.Text = olafUser.Concatenation;
                txtComment.Focus();
            }

            ((MasterPage)Master).ShowValidationMessages(validationErrors);

        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                ArrayList validationErrors = new ArrayList();

                GetFormValues(ref validationErrors);
                if (SelectedSoftwareLicenseAssignment.SoftwareLicenseId == null)
                {
                    validationErrors.Add("You have to select a license.");
                }

                ((MasterPage)Master).ShowValidationMessages(validationErrors);

                if (validationErrors.Count == 0)
                {
                    SelectedSoftwareLicenseAssignment.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                    ((MasterPage)Master).ShowValidationMessages(validationErrors);
                    if (validationErrors.Count == 0)
                    {
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

            List<SoftwareLicenseBL> itemList = SoftwareLicenseBL.GetFreeByInfix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        [System.Web.Services.WebMethod]
        public static string[] GetUserCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<OlafUserBL> itemList = OlafUserBL.GetByInfix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        [System.Web.Services.WebMethod]
        public static string[] GetHardwareItemCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<HardwareItemBL> itemList = HardwareItemBL.GetByInfix(prefixText);
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
            if (SelectedSoftwareLicenseAssignment.Id != 0) // editing a license assignment
            {
                SoftwareLicenseBL license = new SoftwareLicenseBL();
                license.Select(SelectedSoftwareLicenseAssignment.SoftwareLicenseId.Value);
                txtLicense.Text = license.Concatenation;
                txtUser.Text = SelectedSoftwareLicenseAssignment.OlafUserConcatenation;
                txtHardwareItem.Text = SelectedSoftwareLicenseAssignment.HardwareItemConcatenation;                
                txtAssigned_CalendarExtender.SelectedDate = SelectedSoftwareLicenseAssignment.DateAssigned;                
                txtInstalled_CalendarExtender.SelectedDate = SelectedSoftwareLicenseAssignment.DateInstalled;                

                if (SelectedSoftwareLicenseAssignment.DateInstalled.HasValue)
                {
                    trUnassigned.Visible = true;
                    txtUnassigned_CalendarExtender.SelectedDate = SelectedSoftwareLicenseAssignment.DateUnassigned;
                }

                txtComment.Text = SelectedSoftwareLicenseAssignment.Comment;
            }
            else // adding of a new license assignment
            {
                if (PreviousPageStack.Peek().Url.Contains("Software"))
                {
                    txtLicense.Text = SelectedSoftwareLicense.Concatenation;
                }
                else if (PreviousPageStack.Peek().Url.Contains("Users"))
                {
                    txtUser.Text = SelectedOlafUser.Concatenation;
                }
                else if (PreviousPageStack.Peek().Url.Contains("Hardware"))
                {
                    txtHardwareItem.Text = SelectedHardwareItem.Concatenation;
                }

                txtAssigned_CalendarExtender.SelectedDate = DateTime.Today;                                
                txtInstalled_CalendarExtender.SelectedDate = SelectedSoftwareLicenseAssignment.DateInstalled;
            }
        }

        private void GetFormValues(ref ArrayList validationErrorList)
        {
            // check the license value
            if (txtLicense.Text == "")
            {
                SelectedSoftwareLicenseAssignment.SoftwareLicenseId = null;
            }
            else
            {
                SelectedSoftwareLicenseAssignment.SoftwareLicenseId = Utilities.GetIdFromConcatenation(txtLicense.Text);
            }

            if (SelectedSoftwareLicenseAssignment.SoftwareLicenseId == -1)
            {
                validationErrorList.Add("The license is not valid. You have to select one from the suggestions.");
                txtLicense.Text = "";
                SelectedSoftwareLicenseAssignment.SoftwareLicenseId = null;
            }

            // check the user value
            if (txtUser.Text == "")
            {
                SelectedSoftwareLicenseAssignment.OlafUserId = null;
            }
            else
            {
                SelectedSoftwareLicenseAssignment.OlafUserId = Utilities.GetIdFromConcatenation(txtUser.Text);
            }

            if (SelectedSoftwareLicenseAssignment.OlafUserId == -1)
            {
                validationErrorList.Add("The user is not valid. You have to select one from the suggestions.");
                txtUser.Text = "";
                SelectedSoftwareLicenseAssignment.OlafUserId = null;
            }

            // check the hardware item value            
            if (txtHardwareItem.Visible == true)
            {
                if (txtHardwareItem.Text == "")
                {
                    SelectedSoftwareLicenseAssignment.HardwareItemId = null;
                }
                else
                {
                    SelectedSoftwareLicenseAssignment.HardwareItemId = Utilities.GetIdFromConcatenation(txtHardwareItem.Text);
                }

                if (SelectedSoftwareLicenseAssignment.HardwareItemId == -1)
                {
                    validationErrorList.Add("The Hardware Item is not valid. You have to select one from the suggestions.");
                    txtHardwareItem.Text = "";
                    SelectedSoftwareLicenseAssignment.HardwareItemId = null;
                }
            }
            else
            {
                SelectedSoftwareLicenseAssignment.HardwareItemId = cbHardwareItem.SelectedValue == "" ? null : (int?)int.Parse(cbHardwareItem.SelectedValue);
            }

            SelectedSoftwareLicenseAssignment.Comment = txtComment.Text;
            SelectedSoftwareLicenseAssignment.DateAssigned = txtAssigned.Text == "" ? null : (DateTime?)DateTime.Parse(txtAssigned.Text);
            SelectedSoftwareLicenseAssignment.DateInstalled = txtInstalled.Text == "" ? null : (DateTime?)DateTime.Parse(txtInstalled.Text);
            SelectedSoftwareLicenseAssignment.DateUnassigned = txtUnassigned.Text == "" ? null : (DateTime?)DateTime.Parse(txtUnassigned.Text);
        }

        private void LoadDdlHardwareItem(string userLogin)
        {
            cbHardwareItem.DataSource = HardwareItemBL.GetActiveByUserLogin(userLogin);

            cbHardwareItem.DataTextField = "Concatenation";
            cbHardwareItem.DataValueField = "Id";

            cbHardwareItem.DataBind();

            cbHardwareItem.Items.Insert(0, new ListItem("", ""));
        }

        #endregion
    }
}