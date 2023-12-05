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
    public partial class MediaItems : BasePage
    {
        #region Properties

        public string Keyword
        {
            get
            {
                if (Equals(Session[Constants.MEDIA_ITEMS_KEYWORD], null))
                {
                    Session[Constants.MEDIA_ITEMS_KEYWORD] = "";
                }
                return (string)Session[Constants.MEDIA_ITEMS_KEYWORD];
            }
            set { Session[Constants.MEDIA_ITEMS_KEYWORD] = value; }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            if (!IsPostBack)
            {
                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    if (SelectedMediaItem.Id != 0)
                        LoadGvMediaItem(SelectedMediaItem);
                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditMediaItem.aspx", "► Software ► Media Items ► Media Item Details"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);

                #endregion                

                #region capture the enter key

                txtKeyword.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion

                txtKeyword.Text = Keyword;
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                Keyword = txtKeyword.Text;
                LoadGvMediaItem(txtKeyword.Text);
                up1.Update();
            }
        }

        protected void ibtnEdit_Click(object sender, EventArgs e)
        {
            SelectedMediaItem = GetSelectedItem((ImageButton)sender);            
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditMediaItem.aspx");
        }

        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            SelectedMediaItem = GetSelectedItem((ImageButton)sender);

            ArrayList validationErrors = new ArrayList();
            SelectedMediaItem.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                //clear gridview
                gvItems1.DataSource = null;
                gvItems1.DataBind();
                lblItems1Count.Text = "0";
                up1.Update();
            }
        }

        protected void ibtnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
        }

        #endregion

        #region Public Methods

        public override void VerifyRenderingInServerForm(Control control)
        {
            // This override is needed to make the export to excel work
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }

        #endregion

        #region Private Methods

        private MediaItemBL GetSelectedItem(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            MediaItemBL item = new MediaItemBL();
            item.Select(id);

            return item;
        }

        private void LoadGvMediaItem(string keyword)
        {
            gvItems1.DataSource = MediaItemBL.GetByKeyword(keyword);
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvMediaItem(MediaItemBL mediaItem)
        {
            List<MediaItemBL> itemList = new List<MediaItemBL>();
            itemList.Add(mediaItem);

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        #endregion
    }
}