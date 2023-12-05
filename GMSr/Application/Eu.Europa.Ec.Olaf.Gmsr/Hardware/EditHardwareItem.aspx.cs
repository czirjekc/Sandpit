using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Hardware
{
    public partial class EditHardwareItem : BasePage
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
                SelectedHardwareItem.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

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
            //check whether the extra fields have to be editable
            if (SelectedHardwareItem.Local == true)
            {
                tblExtra.Visible = true;
            }

            txtOlafName.Text = SelectedHardwareItem.OlafName;
            if (tblExtra.Visible)
            {
                txtInventoryNo.Text = SelectedHardwareItem.InventoryNo;
                txtModel.Text = SelectedHardwareItem.Model;
                txtDescription.Text = SelectedHardwareItem.Description;
                txtSerial.Text = SelectedHardwareItem.Serial;
                txtOffice.Text = SelectedHardwareItem.Office;
                txtLogin.Text = SelectedHardwareItem.Login;
            }
        }

        private void GetFormValues()
        {                                  
            SelectedHardwareItem.OlafName = txtOlafName.Text;            
            if (tblExtra.Visible)
            {
                SelectedHardwareItem.InventoryNo = txtInventoryNo.Text;
                SelectedHardwareItem.Model = txtModel.Text;
                SelectedHardwareItem.Description = txtDescription.Text;
                SelectedHardwareItem.Serial = txtSerial.Text;
                SelectedHardwareItem.Office = txtOffice.Text;
                SelectedHardwareItem.Login = txtLogin.Text;
            }            
        }

        #endregion
    }
}