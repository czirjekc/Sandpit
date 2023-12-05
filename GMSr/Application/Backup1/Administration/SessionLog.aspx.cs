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
    public partial class SessionLog : BasePage
    {
        #region Properties

        public Dictionary<string, string> SessionSearchValues
        {
            get
            {
                if (Equals(Session[Constants.SESSION_SEARCH_VALUES], null))
                {
                    Session[Constants.SESSION_SEARCH_VALUES] = new Dictionary<string, string>();
                }
                return (Dictionary<string, string>)Session[Constants.SESSION_SEARCH_VALUES];
            }
            set { Session[Constants.SESSION_SEARCH_VALUES] = value; }
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
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                #endregion

                if (SessionSearchValues.Count > 0)
                {
                    SetSearchValues();                    
                }
                else
                {
                    ClearSearchValues();
                }

                #region capture the enter key -> Search action

                txtDateStartFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtDateStartTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtUserName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

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
            txtDateStartFrom.Text = DateTime.Today.AddDays(-7).ToShortDateString();
            txtDateStartTo.Text = "";
            txtUserName.Text = "";

            SessionSearchValues = null;
        }

        private void SetSearchValues()
        {
            if (SessionSearchValues.Count > 0)
            {
                txtDateStartFrom.Text = SessionSearchValues["dateStartFrom"];
                txtDateStartTo.Text = SessionSearchValues["dateStartTo"];
                txtUserName.Text = SessionSearchValues["userName"];
            }
        }

        private void GetSearchValues()
        {
            SessionSearchValues["dateStartFrom"] = txtDateStartFrom.Text;
            SessionSearchValues["dateStartTo"] = txtDateStartTo.Text;
            SessionSearchValues["userName"] = txtUserName.Text;            
        }

        private void LoadGvItems()
        {
            List<SessionBL> itemList = SessionBL.GetByParameters(txtDateStartFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateStartFrom.Text),
                                                                 txtDateStartTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateStartTo.Text),
                                                                 txtUserName.Text
                                                                 );
            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        #endregion
    }
}