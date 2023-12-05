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
    public partial class UserAccess : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            if (!IsPostBack)
            {
                LoadCbUser();

                LoadLbDenied(0);
                LoadLbGranted(0);
                LoadLbInherited(0);
            }

            UpdateActionButtons();
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                ArrayList validationErrors = new ArrayList();

                int userId = int.Parse(cbUser.SelectedValue);

                MenuElementUserBL menuElementUser = new MenuElementUserBL();

                // remove the previously granted items
                List<MenuElementUserBL> previousItemList = MenuElementUserBL.GetByUser(userId);
                previousItemList.ForEach(item =>
                {
                    item.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);
                });

                // add the current granted items
                foreach (ListItem item in lbGranted.Items)
                {
                    menuElementUser.Id = 0;
                    menuElementUser.UserId = userId;
                    menuElementUser.MenuElementId = int.Parse(item.Value);

                    menuElementUser.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                }

                ShowValidationMessages(validationErrors);

                if (validationErrors.Count == 0)
                {
                    LoadCbUser();

                    LoadLbDenied(0);
                    LoadLbGranted(0);
                    LoadLbInherited(0);

                    ibtnGrant.Enabled = false;
                    ibtnGrant.ImageUrl = "~/Images/ArrowRightGray.png";
                    ibtnDeny.Enabled = false;
                    ibtnDeny.ImageUrl = "~/Images/ArrowLeftGray.png";

                    UpdateActionButtons();
                }
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                LoadCbUser();

                LoadLbDenied(0);
                LoadLbGranted(0);
                LoadLbInherited(0);

                ibtnGrant.Enabled = false;
                ibtnGrant.ImageUrl = "~/Images/ArrowRightGray.png";
                ibtnDeny.Enabled = false;
                ibtnDeny.ImageUrl = "~/Images/ArrowLeftGray.png";                

                MasterPage masterPage = Master as MasterPage;
                masterPage.ClearErrorMessages();

                UpdateActionButtons();
            }
        }

        protected void lbDenied_PreRender(object sender, EventArgs e)
        {
            Utilities.AttachBackColors(lbDenied);
        }

        protected void lbGranted_PreRender(object sender, EventArgs e)
        {
            Utilities.AttachBackColors(lbGranted);
        }

        protected void lbInherited_PreRender(object sender, EventArgs e)
        {
            Utilities.AttachBackColorsGray(lbInherited);
        }

        protected void ibtnGrant_Click(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem item in lbDenied.Items)
            {
                if (item.Selected)
                {
                    lbGranted.Items.Add(new ListItem(item.Text, item.Value));
                    itemsToRemove.Add(item);
                }
            }

            itemsToRemove.ForEach(delegate(ListItem itemToRemove)
            {
                lbDenied.Items.Remove(itemToRemove);
            });

            MasterPage masterPage = Master as MasterPage;
            masterPage.ClearErrorMessages();
        }

        protected void ibtnDeny_Click(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem item in lbGranted.Items)
            {
                if (item.Selected)
                {
                    lbDenied.Items.Add(new ListItem(item.Text, item.Value));
                    itemsToRemove.Add(item);
                }
            }
            itemsToRemove.ForEach(delegate(ListItem itemToRemove)
            {
                lbGranted.Items.Remove(itemToRemove);
            });

            MasterPage masterPage = Master as MasterPage;
            masterPage.ClearErrorMessages();
        }  

        protected void cbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUser.SelectedItem.Value == "")
            {
                ibtnGrant.Enabled = false;
                ibtnGrant.ImageUrl = "~/Images/ArrowRightGray.png";
                ibtnDeny.Enabled = false;
                ibtnDeny.ImageUrl = "~/Images/ArrowLeftGray.png";                

                LoadLbDenied(0);
                LoadLbGranted(0);
                LoadLbInherited(0);
            }
            else
            {
                ibtnGrant.Enabled = true;
                ibtnGrant.ImageUrl = "~/Images/ArrowRight.png";
                ibtnDeny.Enabled = true;
                ibtnDeny.ImageUrl = "~/Images/ArrowLeft.png";
               
                LoadLbDenied(int.Parse(cbUser.SelectedValue));
                LoadLbGranted(int.Parse(cbUser.SelectedValue));
                LoadLbInherited(int.Parse(cbUser.SelectedValue));
            }

            MasterPage masterPage = Master as MasterPage;
            masterPage.ClearErrorMessages();
        }

        #endregion

        #region Private Methods

        private void UpdateActionButtons()
        {
            if (cbUser.SelectedValue == "")
            {
                ((MasterPage)Master).DisableAction(Action.Save);
                ((MasterPage)Master).DisableAction(Action.Clear);
            }
            else
            {
                ((MasterPage)Master).EnableAction(Action.Save);
                ((MasterPage)Master).EnableAction(Action.Clear);
            }
        }

        private void ShowValidationMessages(ArrayList validationErrors)
        {
            MasterPage masterPage = Master as MasterPage;
            if (validationErrors.Count > 0)
            {
                string errorMessages = "<br />";
                foreach (string message in validationErrors)
                {
                    errorMessages += message + "<br />";
                }

                masterPage.SetErrorMessages(errorMessages);
            }
            else
            {
                masterPage.ClearErrorMessages();
            }
        }

        private void LoadCbUser()
        {
            cbUser.DataSource = UserBL.GetAll();

            cbUser.DataTextField = "FullName";
            cbUser.DataValueField = "Id";

            cbUser.DataBind();

            cbUser.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadLbDenied(int userId)
        {
            if (userId == 0)
            {
                lbDenied.DataSource = new List<MenuElementBL>();
            }
            else
            {
                lbDenied.DataSource = MenuElementBL.GetNotAccessiblePageListByUser(userId);
            }

            lbDenied.DataTextField = "Path";
            lbDenied.DataValueField = "Id";

            lbDenied.DataBind();
        }

        private void LoadLbGranted(int userId)
        {
            lbGranted.DataSource = MenuElementBL.GetNotInheritedPageListByUser(userId);

            lbGranted.DataTextField = "Path";
            lbGranted.DataValueField = "Id";

            lbGranted.DataBind();
        }

        private void LoadLbInherited(int userId)
        {
            lbInherited.DataSource = MenuElementBL.GetInheritedPageListByUser(userId);

            lbInherited.DataTextField = "Path";
            lbInherited.DataValueField = "Id";

            lbInherited.DataBind();
        }

        #endregion
    }
}