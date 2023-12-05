using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Administration
{
    public partial class Groups : BasePage
    {
        #region Properties

        public string Keyword
        {
            get
            {
                if (Equals(Session[Constants.GROUPS_KEYWORD], null))
                {
                    Session[Constants.GROUPS_KEYWORD] = "";
                }
                return (string)Session[Constants.GROUPS_KEYWORD];
            }
            set { Session[Constants.GROUPS_KEYWORD] = value; }
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
                    if (SelectedGroup.Id != 0)
                        LoadGvGroup(SelectedGroup);                    
                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new PageBL("~/Administration/EditGroup.aspx", "► GMSr Administration ► Groups ► Group Details"));

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
                LoadGvGroup(txtKeyword.Text);
            }
        }

        protected void ibtnEdit_Click(object sender, EventArgs e)
        {
            SelectedGroup = GetSelectedItem((ImageButton)sender);
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Administration/EditGroup.aspx");
        }

        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            SelectedGroup = GetSelectedItem((ImageButton)sender);

            ArrayList validationErrors = new ArrayList();
            SelectedGroup.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            if (validationErrors.Count == 0)
            {
                //clear gridview
                gvItems1.DataSource = null;
                gvItems1.DataBind();
                lblItems1Count.Text = "0";
            }
        }

        protected void ibtnAdd_Click(object sender, EventArgs e)
        {
            SelectedGroup = null;
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Administration/EditGroup.aspx");
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

        private GroupBL GetSelectedItem(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems1.DataKeys[row.RowIndex]["Id"].ToString());

            GroupBL item = new GroupBL();
            item.Select(id);

            return item;
        }

        private void LoadGvGroup(string keyword)
        {
            gvItems1.DataSource = GroupBL.GetByKeyword(keyword);
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvGroup(GroupBL group)
        {
            List<GroupBL> itemList = new List<GroupBL>();
            itemList.Add(group);

            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        #endregion
    }
}