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
    public partial class Add : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditOrderItem.aspx", "► Software ► Add"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditOrderForm.aspx", "► Software ► Add ► Order Form Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditDocument.aspx", "► Software ► Add ► Order Form Details ► Document Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditProduct.aspx", "► Software ► Add ► Product Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicense.aspx", "► Software ► Add ► License Details"));
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditMediaItem.aspx", "► Software ► Add ► Media Item Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                LoadDdlAddOrderType();
            }
        }

        protected void ibtnAdd_Click(object sender, EventArgs e)
        {
            SelectedSoftwareOrderItem = null;
            SelectedSoftwareOrderItem.SoftwareOrderItemTypeId = int.Parse(ddlAddOrderType.SelectedValue);
            SelectedSoftwareOrderItem.SoftwareOrderItemTypeName = ddlAddOrderType.SelectedItem.Text;

            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditOrderItem.aspx");
        }

        protected void ddlAddOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAddOrderType.SelectedValue == "")
            {
                ibtnAdd.Enabled = false;
                ibtnAdd.ImageUrl = "~/Images/AddBigGray.png";
            }
            else
            {
                ibtnAdd.Enabled = true;
                ibtnAdd.ImageUrl = "~/Images/AddBig.png";
            }
        }

        #endregion

        #region Private Methods

        private void LoadDdlAddOrderType()
        {
            ddlAddOrderType.DataSource = SoftwareOrderItemTypeBL.GetAll();

            ddlAddOrderType.DataTextField = "Name";
            ddlAddOrderType.DataValueField = "Id";

            ddlAddOrderType.DataBind();

            ddlAddOrderType.Items.Insert(0, new ListItem("", ""));
        }

        #endregion
    }
}