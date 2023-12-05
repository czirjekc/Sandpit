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
    public partial class GroupAccess : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            if (!IsPostBack)
            {
                LoadCbGroup();

                LoadLbDenied(0);
                LoadLbGranted(0);
            }
            
            UpdateActionButtons();
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                ArrayList validationErrors = new ArrayList();

                int groupId = int.Parse(cbGroup.SelectedValue);

                MenuElementGroupBL menuElementGroup = new MenuElementGroupBL();

                // remove the previously granted items
                List<MenuElementGroupBL> previousItemList = MenuElementGroupBL.GetByGroup(groupId);
                previousItemList.ForEach(item =>
                {
                    item.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);
                });

                // add the current granted items
                foreach (ListItem item in lbGranted.Items)
                {
                    menuElementGroup.Id = 0;
                    menuElementGroup.GroupId = groupId;
                    menuElementGroup.MenuElementId = int.Parse(item.Value);

                    menuElementGroup.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                }

                ShowValidationMessages(validationErrors);

                if (validationErrors.Count == 0)
                {
                    LoadCbGroup();

                    LoadLbDenied(0);
                    LoadLbGranted(0);

                    ibtnGrant.Enabled = false;
                    ibtnGrant.ImageUrl = "~/Images/ArrowRightGray.png";
                    ibtnDeny.Enabled = false;
                    ibtnDeny.ImageUrl = "~/Images/ArrowLeftGray.png";

                    UpdateActionButtons();
                }
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                LoadCbGroup();

                LoadLbDenied(0);
                LoadLbGranted(0);

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

        protected void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroup.SelectedValue == "")
            {
                ibtnGrant.Enabled = false;
                ibtnGrant.ImageUrl = "~/Images/ArrowRightGray.png";
                ibtnDeny.Enabled = false;
                ibtnDeny.ImageUrl = "~/Images/ArrowLeftGray.png";

                LoadLbGranted(0);
                LoadLbDenied(0);
            }
            else if (cbGroup.SelectedValue == "1") //developer group has always access to all menu elements, so no change possible
            {
                ibtnGrant.Enabled = false;
                ibtnGrant.ImageUrl = "~/Images/ArrowRightGray.png";
                ibtnDeny.Enabled = false;
                ibtnDeny.ImageUrl = "~/Images/ArrowLeftGray.png";

                LoadLbGranted(int.Parse(cbGroup.SelectedValue));
                LoadLbDenied(int.Parse(cbGroup.SelectedValue));
            }
            else
            {
                ibtnGrant.Enabled = true;
                ibtnGrant.ImageUrl = "~/Images/ArrowRight.png";
                ibtnDeny.Enabled = true;
                ibtnDeny.ImageUrl = "~/Images/ArrowLeft.png";

                LoadLbGranted(int.Parse(cbGroup.SelectedValue));
                LoadLbDenied(int.Parse(cbGroup.SelectedValue));
            }

            MasterPage masterPage = Master as MasterPage;
            masterPage.ClearErrorMessages();
        }

        #endregion

        #region Private Methods

        private void UpdateActionButtons()
        {
            if (cbGroup.SelectedValue == "" || cbGroup.SelectedValue == "1")
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

        private void LoadCbGroup()
        {
            cbGroup.DataSource = GroupBL.GetAll();

            cbGroup.DataTextField = "Name";
            cbGroup.DataValueField = "Id";

            cbGroup.DataBind();

            cbGroup.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadLbDenied(int groupId)
        {
            if (groupId == 0)
            {
                lbDenied.DataSource = new List<MenuElementBL>();
            }
            else
            {
                lbDenied.DataSource = MenuElementBL.GetNotAccessiblePageListByGroup(groupId);
            }

            lbDenied.DataTextField = "Path";
            lbDenied.DataValueField = "Id";

            lbDenied.DataBind();
        }

        private void LoadLbGranted(int groupId)
        {
            lbGranted.DataSource = MenuElementBL.GetPageListByGroup(groupId);

            lbGranted.DataTextField = "Path";
            lbGranted.DataValueField = "Id";

            lbGranted.DataBind();
        }

        #endregion
    }
}