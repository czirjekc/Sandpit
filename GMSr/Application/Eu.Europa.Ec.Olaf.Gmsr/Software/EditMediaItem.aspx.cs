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
    public partial class EditMediaItem : BasePage
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

                LoadDdlType();
                LoadDdlLocation();

                SetFormValues();
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                GetFormValues();

                ArrayList validationErrors = new ArrayList();
                SelectedMediaItem.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

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
            txtComment.Text = SelectedMediaItem.Comment;
            txtName.Text = SelectedMediaItem.Name;

            ddlLocation.SelectedValue = SelectedMediaItem.MediaItemLocationId.HasValue ? SelectedMediaItem.MediaItemLocationId.Value.ToString() : "";
            ddlType.SelectedValue = SelectedMediaItem.MediaItemTypeId.HasValue ? SelectedMediaItem.MediaItemTypeId.Value.ToString() : "";
        }

        private void GetFormValues()
        {
            //if this is a newly added mediaItem, then the softwareOrderItemId must be set
            if (SelectedMediaItem.SoftwareOrderItemId == null)
            {
                SelectedMediaItem.SoftwareOrderItemId = SelectedSoftwareOrderItem.Id;
            }

            SelectedMediaItem.Comment = txtComment.Text;
            SelectedMediaItem.Name = txtName.Text;

            SelectedMediaItem.MediaItemLocationId = ddlLocation.SelectedValue == "" ? null : (int?)int.Parse(ddlLocation.SelectedValue);
            SelectedMediaItem.MediaItemTypeId = ddlType.SelectedValue == "" ? null : (int?)int.Parse(ddlType.SelectedValue);
        }

        private void LoadDdlType()
        {
            ddlType.DataSource = MediaItemTypeBL.GetAll();

            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "Id";

            ddlType.DataBind();

            ddlType.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadDdlLocation()
        {
            ddlLocation.DataSource = MediaItemLocationBL.GetAll();

            ddlLocation.DataTextField = "Name";
            ddlLocation.DataValueField = "Id";

            ddlLocation.DataBind();

            ddlLocation.Items.Insert(0, new ListItem("", ""));
        }

        #endregion
    }
}