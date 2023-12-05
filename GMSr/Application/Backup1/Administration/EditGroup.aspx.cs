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
    public partial class EditGroup : BasePage
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
                SelectedGroup.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

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
            txtName.Text = SelectedGroup.Name;
            cbActive.Checked = SelectedGroup.IsActive;
        }

        private void GetFormValues()
        {
            SelectedGroup.Name = txtName.Text;
            SelectedGroup.IsActive = cbActive.Checked;
        }

        #endregion
    }
}