using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Contracts
{
    public partial class Main : BasePage
    {
        #region Enumerations

        public enum IdType
        {
            ContractFramework,
            ContractAmendment,
            SpecificContract,
            OrderForm
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems3);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems4);

            gvItems1.RowCommand += gvItems1_RowCommand; // to handle the row selection of gvItems1
            gvItems2.RowCommand += gvItems2_RowCommand; // to handle the row selection of gvItems2
            gvItems3.RowCommand += gvItems3_RowCommand; // to handle the row selection of gvItems3
            gvItems4.RowCommand += gvItems4_RowCommand; // to handle the row selection of gvItems4

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);

                #endregion

                if (!Equals(Session["contractSearchValues"], null))
                {
                    SetSearchValues();                    
                }                
                
                #region capture the enter key -> Search action

                txtFrameworkName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtFrameworkDateBeginFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtFrameworkDateBeginTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtFrameworkDateEndFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtFrameworkDateEndTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                txtAmendmentName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAmendmentDateBeginFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAmendmentDateBeginTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAmendmentDateEndFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtAmendmentDateEndTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                txtSpecificName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSpecificDateBeginFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSpecificDateBeginTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSpecificDateEndFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtSpecificDateEndTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                txtOrderFormName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderFormDateBeginFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderFormDateBeginTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderFormDateEndFrom.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOrderFormDateEndTo.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                
                #endregion
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                ((MasterPage)Master).SetGridviewProperties(gvItems3);
                ((MasterPage)Master).SetGridviewProperties(gvItems4);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                Session["contractSearchValues"] = GetSearchValues();

                if (rblSearch.SelectedValue == "0")
                {
                    LoadGvContractFrameworks();                    
                                        
                    //clear other gridviews
                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblItems2Count.Text = "0";
                    gvItems3.DataSource = null;
                    gvItems3.DataBind();
                    lblItems3Count.Text = "0";
                    gvItems4.DataSource = null;
                    gvItems4.DataBind();
                    lblItems4Count.Text = "0";
                }
                else if(rblSearch.SelectedValue == "1")
                {
                    LoadGvContractAmendments();                    

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblItems1Count.Text = "0";
                    gvItems3.DataSource = null;
                    gvItems3.DataBind();
                    lblItems3Count.Text = "0";
                    gvItems4.DataSource = null;
                    gvItems4.DataBind();
                    lblItems4Count.Text = "0";
                }
                else if (rblSearch.SelectedValue == "2")
                {
                    LoadGvSpecificContracts();                    

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblItems1Count.Text = "0";
                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblItems2Count.Text = "0";                    
                    gvItems4.DataSource = null;
                    gvItems4.DataBind();
                    lblItems4Count.Text = "0";
                }
                else if (rblSearch.SelectedValue == "3")
                {
                    LoadGvOrderForms();

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblItems1Count.Text = "0";
                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblItems2Count.Text = "0";
                    gvItems3.DataSource = null;
                    gvItems3.DataBind();
                    lblItems3Count.Text = "0";                    
                }
                up1.Update();
                up2.Update();
                up3.Update();
                up4.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();
                upSearch.Update();
            }
        }

        protected void rblSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSearch.SelectedValue == "0")
            {
                tblSearchFrameworkContracts.Visible = true;
                tblSearchAmendments.Visible = false;
                tblSearchSpecificContracts.Visible = false;
                tblSearchOrders.Visible = false;
            }
            else if (rblSearch.SelectedValue == "1")
            {
                tblSearchFrameworkContracts.Visible = false;
                tblSearchAmendments.Visible = true;
                tblSearchSpecificContracts.Visible = false;
                tblSearchOrders.Visible = false;
            }
            else if (rblSearch.SelectedValue == "2")
            {
                tblSearchFrameworkContracts.Visible = false;
                tblSearchAmendments.Visible = false;
                tblSearchSpecificContracts.Visible = true;
                tblSearchOrders.Visible = false;
            }
            else if (rblSearch.SelectedValue == "3")
            {
                tblSearchFrameworkContracts.Visible = false;
                tblSearchAmendments.Visible = false;
                tblSearchSpecificContracts.Visible = false;
                tblSearchOrders.Visible = true;
            }

            //clear gridviews
            gvItems1.DataSource = null;
            gvItems1.DataBind();
            lblItems1Count.Text = "0";
            gvItems2.DataSource = null;
            gvItems2.DataBind();
            lblItems2Count.Text = "0";
            gvItems3.DataSource = null;
            gvItems3.DataBind();
            lblItems3Count.Text = "0";
            gvItems4.DataSource = null;
            gvItems4.DataBind();
            lblItems4Count.Text = "0";

            upSearch.Update();
            up1.Update();
            up2.Update();
            up3.Update();
            up4.Update();
        }

        private void gvItems1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

                LoadGvContractAmendments(id, IdType.ContractAmendment);

                LoadGvSpecificContracts(id, IdType.SpecificContract);

                up2.Update();
                up3.Update();
                up4.Update();
            }
        }

        private void gvItems2_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems2.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

                LoadGvContractFrameworks(id, IdType.ContractFramework);

                LoadGvSpecificContracts(id, IdType.SpecificContract);

                LoadGvOrderForms(id, IdType.OrderForm);

                up1.Update();
                up3.Update();
                up4.Update();
            }
        }

        private void gvItems3_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems3.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

                LoadGvContractFrameworks(id, IdType.ContractFramework);
                
                LoadGvContractAmendments(id, IdType.ContractAmendment);

                LoadGvOrderForms(id, IdType.OrderForm);

                up1.Update();
                up2.Update();
                up4.Update();
            }
        }

        private void gvItems4_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems4.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

                LoadGvContractFrameworks(id, IdType.ContractFramework);

                LoadGvContractAmendments(id, IdType.ContractAmendment);

                LoadGvSpecificContracts(id, IdType.SpecificContract);

                up1.Update();
                up2.Update();
                up3.Update();
            }
        }

        protected void ibtnExportItems1_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems1, Response);
        }

        protected void ibtnExportItems2_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems2, Response);
        }

        protected void ibtnExportItems3_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems3, Response);
        }

        protected void ibtnExportItems4_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems4, Response);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Utilities.MakeSelectable(gvItems1, Page);
            Utilities.MakeSelectable(gvItems2, Page);
            Utilities.MakeSelectable(gvItems3, Page);
            Utilities.MakeSelectable(gvItems4, Page);
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
            txtFrameworkName.Text = "";
            txtFrameworkDateBeginFrom.Text = "";
            txtFrameworkDateBeginTo.Text = "";
            txtFrameworkDateEndFrom.Text = "";
            txtFrameworkDateEndTo.Text = "";

            txtAmendmentName.Text = "";
            txtAmendmentDateBeginFrom.Text = "";
            txtAmendmentDateBeginTo.Text = "";
            txtAmendmentDateEndFrom.Text = "";
            txtAmendmentDateEndTo.Text = "";

            txtSpecificName.Text = "";
            txtSpecificDateBeginFrom.Text = "";
            txtSpecificDateBeginTo.Text = "";
            txtSpecificDateEndFrom.Text = "";
            txtSpecificDateEndTo.Text = "";

            txtOrderFormName.Text = "";
            txtOrderFormDateBeginFrom.Text = "";
            txtOrderFormDateBeginTo.Text = "";
            txtOrderFormDateEndFrom.Text = "";
            txtOrderFormDateEndTo.Text = "";

            Session["ContractSearchValues"] = null;
        }

        private void SetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();
            if (!Equals(Session["ContractSearchValues"], null))
            {
                searchValues = (Dictionary<string, string>)Session["ContractSearchValues"];
            }

            txtFrameworkName.Text = searchValues["frameworkName"];
            txtFrameworkDateBeginFrom.Text = searchValues["frameworkDateBeginFrom"];
            txtFrameworkDateBeginTo.Text = searchValues["frameworkDateBeginTo"];
            txtFrameworkDateEndFrom.Text = searchValues["frameworkDateEndFrom"];
            txtFrameworkDateEndTo.Text = searchValues["frameworkDateEndTo"];

            txtAmendmentName.Text = searchValues["amendmentName"];
            txtAmendmentDateBeginFrom.Text = searchValues["amendmentDateBeginFrom"];
            txtAmendmentDateBeginTo.Text = searchValues["amendmentDateBeginTo"];
            txtAmendmentDateEndFrom.Text = searchValues["amendmentDateEndFrom"];
            txtAmendmentDateEndTo.Text = searchValues["amendmentDateEndTo"];

            txtSpecificName.Text = searchValues["specificName"];
            txtSpecificDateBeginFrom.Text = searchValues["specificDateBeginFrom"];
            txtSpecificDateBeginTo.Text = searchValues["specificDateBeginTo"];
            txtSpecificDateEndFrom.Text = searchValues["specificDateEndFrom"];
            txtSpecificDateEndTo.Text = searchValues["specificDateEndTo"];

            txtOrderFormName.Text = searchValues["orderFormName"];
            txtOrderFormDateBeginFrom.Text = searchValues["orderFormDateBeginFrom"];
            txtOrderFormDateBeginTo.Text = searchValues["orderFormDateBeginTo"];
            txtOrderFormDateEndFrom.Text = searchValues["orderFormDateEndFrom"];
            txtOrderFormDateEndTo.Text = searchValues["orderFormDateEndTo"];

        }

        private Dictionary<string, string> GetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();

            searchValues.Add("frameworkName", txtFrameworkName.Text);
            searchValues.Add("frameworkDateBeginFrom", txtFrameworkDateBeginFrom.Text);
            searchValues.Add("frameworkDateBeginTo", txtFrameworkDateBeginTo.Text);
            searchValues.Add("frameworkDateEndFrom", txtFrameworkDateEndFrom.Text);
            searchValues.Add("frameworkDateEndTo", txtFrameworkDateEndTo.Text);

            searchValues.Add("amendmentName", txtAmendmentName.Text);
            searchValues.Add("amendmentDateBeginFrom", txtAmendmentDateBeginFrom.Text);
            searchValues.Add("amendmentDateBeginTo", txtAmendmentDateBeginTo.Text);
            searchValues.Add("amendmentDateEndFrom", txtAmendmentDateEndFrom.Text);
            searchValues.Add("amendmentDateEndTo", txtAmendmentDateEndTo.Text);

            searchValues.Add("specificName", txtSpecificName.Text);
            searchValues.Add("specificDateBeginFrom", txtSpecificDateBeginFrom.Text);
            searchValues.Add("specificDateBeginTo", txtSpecificDateBeginTo.Text);
            searchValues.Add("specificDateEndFrom", txtSpecificDateEndFrom.Text);
            searchValues.Add("specificDateEndTo", txtSpecificDateEndTo.Text);

            searchValues.Add("orderFormName", txtOrderFormName.Text);
            searchValues.Add("orderFormDateBeginFrom", txtOrderFormDateBeginFrom.Text);
            searchValues.Add("orderFormDateBeginTo", txtOrderFormDateBeginTo.Text);
            searchValues.Add("orderFormDateEndFrom", txtOrderFormDateEndFrom.Text);
            searchValues.Add("orderFormDateEndTo", txtOrderFormDateEndTo.Text);

            return searchValues;
        }

        private void LoadGvContractFrameworks()
        {
            gvItems1.DataSource = ContractFrameworkBL.GetByParameters(txtFrameworkName.Text,
                                                                      txtFrameworkDateBeginFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtFrameworkDateBeginFrom.Text),
                                                                      txtFrameworkDateBeginTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtFrameworkDateBeginTo.Text),
                                                                      txtFrameworkDateEndFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtFrameworkDateEndFrom.Text),
                                                                      txtFrameworkDateEndTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtFrameworkDateEndTo.Text)
                                                                      );
            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);

            lblItems1Count.Text = gvItems1.Rows.Count.ToString();                        
        }

        private void LoadGvContractFrameworks(int id, IdType idType)
        {
            if (idType == IdType.ContractAmendment)
                gvItems1.DataSource = ContractFrameworkBL.GetByContractAmendment(id);
            else if (idType == IdType.SpecificContract)
                gvItems1.DataSource = ContractFrameworkBL.GetBySpecificContract(id);

            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);

            lblItems1Count.Text = gvItems1.Rows.Count.ToString();
        }

        private void LoadGvContractAmendments()
        {
            gvItems2.DataSource = ContractAmendmentBL.GetByParameters(txtAmendmentName.Text,
                                                                      txtAmendmentDateBeginFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtAmendmentDateBeginFrom.Text),
                                                                      txtAmendmentDateBeginTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtAmendmentDateBeginTo.Text),
                                                                      txtAmendmentDateEndFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtAmendmentDateEndFrom.Text),
                                                                      txtAmendmentDateEndTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtAmendmentDateEndTo.Text)
                                                                      );
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvContractAmendments(int id, IdType idType)
        {
            if (idType == IdType.ContractFramework)
                gvItems2.DataSource = ContractAmendmentBL.GetByContractFramework(id);
            else if (idType == IdType.SpecificContract)
                gvItems2.DataSource = ContractAmendmentBL.GetBySpecificContract(id);
            else if (idType == IdType.OrderForm)
                gvItems2.DataSource = ContractAmendmentBL.GetByOrderForm(id);

            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvSpecificContracts()
        {
            gvItems3.DataSource = SpecificContractBL.GetByParameters(txtSpecificName.Text,
                                                                      txtSpecificDateBeginFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtSpecificDateBeginFrom.Text),
                                                                      txtSpecificDateBeginTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtSpecificDateBeginTo.Text),
                                                                      txtSpecificDateEndFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtSpecificDateEndFrom.Text),
                                                                      txtSpecificDateEndTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtSpecificDateEndTo.Text)
                                                                      );
            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
            lblItems3Count.Text = gvItems3.Rows.Count.ToString();
        }

        private void LoadGvSpecificContracts(int id, IdType idType)
        {
            if (idType == IdType.ContractFramework)
                gvItems3.DataSource = SpecificContractBL.GetByContractFramework(id);
            else if (idType == IdType.ContractAmendment)
                gvItems3.DataSource = SpecificContractBL.GetByContractAmendment(id);
            else if (idType == IdType.OrderForm)
                gvItems3.DataSource = SpecificContractBL.GetByOrderForm(id);

            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
            lblItems3Count.Text = gvItems3.Rows.Count.ToString();
        }

        private void LoadGvOrderForms()
        {
            gvItems4.DataSource = OrderFormBL.GetByParameters(txtOrderFormName.Text,
                                                                      txtOrderFormDateBeginFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtOrderFormDateBeginFrom.Text),
                                                                      txtOrderFormDateBeginTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtOrderFormDateBeginTo.Text),
                                                                      txtOrderFormDateEndFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtOrderFormDateEndFrom.Text),
                                                                      txtOrderFormDateEndTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtOrderFormDateEndTo.Text)
                                                                      );
            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
            lblItems4Count.Text = gvItems4.Rows.Count.ToString();
        }

        private void LoadGvOrderForms(int id, IdType idType)
        {
            if (idType == IdType.SpecificContract)
                gvItems4.DataSource = OrderFormBL.GetBySpecificContract(id);
            else if (idType == IdType.ContractAmendment)
                gvItems4.DataSource = OrderFormBL.GetByContractAmendment(id);

            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
            lblItems4Count.Text = gvItems4.Rows.Count.ToString();
        }

        #endregion
    }
}