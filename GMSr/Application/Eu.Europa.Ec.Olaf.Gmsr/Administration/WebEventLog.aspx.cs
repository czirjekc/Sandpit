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
    public partial class WebEventLog : BasePage
    {
        #region Properties

        public Dictionary<string, string> WebEventSearchValues
        {
            get
            {
                if (Equals(Session[Constants.WEB_EVENT_SEARCH_VALUES], null))
                {
                    Session[Constants.WEB_EVENT_SEARCH_VALUES] = new Dictionary<string, string>();
                }
                return (Dictionary<string, string>)Session[Constants.WEB_EVENT_SEARCH_VALUES];
            }
            set { Session[Constants.WEB_EVENT_SEARCH_VALUES] = value; }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons            

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            // to view the stacktrace
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            // to handle the row selection
            gvItems1.RowCommand += gvItems1_RowCommand;

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                #endregion

                if (WebEventSearchValues.Count > 0)
                {
                    SetSearchValues();
                }
                else
                {
                    ClearSearchValues();
                }

                #region capture the enter key -> Search action

                txtDateFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

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
                GetSearchValues();

                LoadGvItems();
                up1.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();
                upSearch.Update();
            }
        }

        protected void gvItems1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(e.Row);
        }

        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes.Add("onclick", "showLoadingAnimation();");
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                WebEventBL item = (WebEventBL)e.Row.DataItem;
                if (item.isError)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8888");
                }
            }
        }

        private void gvItems1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                string guid = gvItems1.DataKeys[row.RowIndex]["Guid"].ToString();
                WebEventBL item = WebEventBL.GetByGuid(guid);

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + item.Time.ToShortDateString() + "_" + item.Time.ToShortTimeString() + ".txt");
                Response.Charset = "";
                Response.ContentType = "text/plain";
                Response.Write(item.Details);
                Response.End();
            }
        }

        protected void ibtnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Utilities.MakeSelectable(gvItems1, Page);
            base.Render(writer);
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
            txtDateFrom.Text = DateTime.Today.AddDays(-7).ToShortDateString();
            txtDateTo.Text = "";

            WebEventSearchValues = null;
        }

        private void SetSearchValues()
        {
            if (WebEventSearchValues.Count > 0)
            {
                txtDateFrom.Text = WebEventSearchValues["dateFrom"];
                txtDateTo.Text = WebEventSearchValues["dateTo"];
            }
        }

        private void GetSearchValues()
        {
            WebEventSearchValues["dateFrom"] = txtDateFrom.Text;
            WebEventSearchValues["dateTo"] = txtDateTo.Text;
        }

        private void LoadGvItems()
        {
            List<WebEventBL> itemList = WebEventBL.GetByParameters(txtDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateFrom.Text),
                                                                 txtDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateTo.Text)
                                                                 );
            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        #endregion
    }
}