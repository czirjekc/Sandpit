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
    public partial class Backlog : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons            

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                #endregion              

                if (!Equals(Session["backlogSearchValues"], null))
                {
                    SetSearchValues();
                    LoadGvItems();
                }

                #region capture the enter key -> Search action
        
                txtName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlStatus.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion
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
                Session["backlogSearchValues"] = GetSearchValues();
                LoadGvItems();                
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();                
            }
        }  

        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BacklogItemBL item = (BacklogItemBL)e.Row.DataItem;
                if (item.StatusId == 1)
                    e.Row.ForeColor = System.Drawing.Color.Gray;
                else if (item.StatusId == 2)
                    e.Row.ForeColor = System.Drawing.Color.Blue;
                else if (item.StatusId == 3)
                    e.Row.ForeColor = System.Drawing.Color.Green;
                else if (item.StatusId == 4)
                    e.Row.ForeColor = System.Drawing.Color.Black;
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

        private void ClearSearchValues()
        {
            txtName.Text = "";
            ddlStatus.SelectedValue = "";

            Session["backlogSearchValues"] = null;
        }

        private void SetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();
            if (!Equals(Session["backlogSearchValues"], null))
            {
                searchValues = (Dictionary<string, string>)Session["backlogSearchValues"];
            }

            txtName.Text = searchValues["name"];
            ddlStatus.SelectedValue = searchValues["status"];
        }

        private Dictionary<string, string> GetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();
        
            searchValues.Add("name", txtName.Text);
            searchValues.Add("status", ddlStatus.SelectedItem.Value);

            return searchValues;
        }

        private void LoadGvItems()
        {
            List<BacklogItemBL> itemList = BacklogItemBL.GetByParameters(txtName.Text, ddlStatus.SelectedValue);
                                                             
            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
        }
   
        #endregion
    }
}