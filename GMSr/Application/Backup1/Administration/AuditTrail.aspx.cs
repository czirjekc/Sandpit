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
    public partial class AuditTrail : BasePage
    {
        #region Properties

        public Dictionary<string, string> AuditSearchValues
        {
            get
            {
                if (Equals(Session[Constants.AUDIT_SEARCH_VALUES], null))
                {
                    Session[Constants.AUDIT_SEARCH_VALUES] = new Dictionary<string, string>();
                }
                return (Dictionary<string, string>)Session[Constants.AUDIT_SEARCH_VALUES];
            }
            set { Session[Constants.AUDIT_SEARCH_VALUES] = value; }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);

            gvItems1.RowCommand += gvItems1_RowCommand; // to handle the row selection of gvItems1

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                #endregion

                SetSearchValues();
                SelectedAudit = null;

                if (AuditSearchValues.Count > 0)
                {
                    
                    SetSearchValues();
                }
                else
                {
                    ClearSearchValues();
                }

                #region capture the enter key -> Search action

                ddlAction.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtEntitySet.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtTimestampFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtTimestampTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtUserLogin.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                ((MasterPage)Master).SetGridviewProperties(gvItems3);
                ((MasterPage)Master).SetGridviewProperties(gvItems4);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                GetSearchValues();

                LoadGvItems();
                lblItems1Count.Text = gvItems1.Rows.Count.ToString();
                up1.Update();

                gvItems3.DataSource = null;
                gvItems3.DataBind();
                up3.Update();

                gvItems4.DataSource = null;
                gvItems4.DataBind();
                up4.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();
                upSearch.Update();
            }
        }

        private void gvItems1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                string guid = gvItems1.DataKeys[row.RowIndex]["Guid"].ToString();

                AuditBL item = AuditBL.GetByGuid(guid);
                SelectedAudit = item;

                LoadGvOldData(item.OldValues);
                up3.Update();

                LoadGvNewData(item.NewValues);
                up4.Update();
            }
        }

        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AuditBL item = (AuditBL)e.Row.DataItem;
                if (item.Action == "Insert")
                    e.Row.ForeColor = System.Drawing.Color.Green;
                else if (item.Action == "Delete")
                    e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvItems3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NameValuePairBL property = (NameValuePairBL)e.Row.DataItem;

                if (SelectedAudit.ChangedProperties != null)
                {
                    SelectedAudit.ChangedProperties.ForEach(item =>
                    {
                        if (property.Name == item.Value)
                        {
                            e.Row.Font.Bold = true;
                        }
                    });
                }
            }
        }

        protected void gvItems4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NameValuePairBL property = (NameValuePairBL)e.Row.DataItem;

                if (SelectedAudit.ChangedProperties != null)
                {
                    SelectedAudit.ChangedProperties.ForEach(item =>
                    {
                        if (property.Name == item.Value)
                        {
                            e.Row.Font.Bold = true;
                        }
                    });
                }
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
            ddlAction.SelectedValue = "";
            txtEntitySet.Text = "";
            txtTimestampFrom.Text = DateTime.Today.AddDays(-7).ToShortDateString();
            txtTimestampTo.Text = "";
            txtUserLogin.Text = "";

            AuditSearchValues = null;
        }

        private void SetSearchValues()
        {
            if (AuditSearchValues.Count > 0)
            {
                txtTimestampFrom.Text = AuditSearchValues["timestampFrom"];
                txtTimestampTo.Text = AuditSearchValues["timestampTo"];
                txtEntitySet.Text = AuditSearchValues["entitySet"];
                txtUserLogin.Text = AuditSearchValues["userLogin"];
                ddlAction.SelectedValue = AuditSearchValues["action"];
            }
        }

        private void GetSearchValues()
        {
            AuditSearchValues["timestampFrom"] = txtTimestampFrom.Text;
            AuditSearchValues["timestampTo"] = txtTimestampTo.Text;
            AuditSearchValues["entitySet"] = txtEntitySet.Text;
            AuditSearchValues["userLogin"] = txtUserLogin.Text;
            AuditSearchValues["action"] = ddlAction.SelectedItem.Value;
        }

        private void LoadGvItems()
        {
            List<AuditBL> itemList = AuditBL.GetByParameters(txtTimestampFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtTimestampFrom.Text),
                                                             txtTimestampTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtTimestampTo.Text),
                                                             txtEntitySet.Text,
                                                             txtUserLogin.Text,
                                                             ddlAction.SelectedValue
                                                             );
            gvItems1.DataSource = itemList;
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
        }

        private void LoadGvOldData(List<NameValuePairBL> oldValues)
        {
            List<NameValuePairBL> itemList = oldValues;

            gvItems3.DataSource = itemList;
            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
        }

        private void LoadGvNewData(List<NameValuePairBL> newValues)
        {
            List<NameValuePairBL> itemList = newValues;

            gvItems4.DataSource = itemList;
            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
        }

        #endregion
    }
}