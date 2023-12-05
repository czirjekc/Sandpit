using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Administration
{
    public partial class EditUser : BasePage
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
                SelectedUser.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

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

        #endregion

        #region Private Methods

        private void SetFormValues()
        {            
            txtEmail.Text = SelectedUser.Email;
            txtFirstName.Text = SelectedUser.FirstName;
            txtLogin.Text = SelectedUser.Login;
            txtSurname.Text = SelectedUser.Surname;
            cbActive.Checked = SelectedUser.IsActive;
        }

        private void GetFormValues()
        {
            SelectedUser.Email = txtEmail.Text;
            SelectedUser.FirstName = txtFirstName.Text;
            SelectedUser.Login = txtLogin.Text;
            SelectedUser.Surname = txtSurname.Text;
            SelectedUser.IsActive = cbActive.Checked;            
        }

        #endregion
    }
}