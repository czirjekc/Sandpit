using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;
using System.Text;

namespace Eu.Europa.Ec.Olaf.Gmsr.Users
{
    public partial class Main : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            // to use export to excel inside updatePanel
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems3);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems4);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems5);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems6);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems7);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibtnExportItems8);

            // to handle the row selection
            gvItems1.RowCommand += gvItems1_RowCommand;
            gvItems3.RowCommand += gvItems3_RowCommand;
            gvItems4.RowCommand += gvItems4_RowCommand;
            gvItems5.RowCommand += gvItems5_RowCommand;
            gvItems6.RowCommand += gvItems6_RowCommand;

            if (!IsPostBack)
            {
                #region check FromPage

                if (FromPage == null)
                {
                    PreviousPageStack.Clear();
                }
                else
                {
                    if (FromPage.Url.Contains("Hardware/Main") && SelectedHardwareItem.Id != 0)
                        LoadGvHardwareItems(SelectedHardwareItem);
                    FromPage = null;
                }

                #endregion

                #region Grant access to additional pages

                List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();
                additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Users/MainHelp.aspx", "► Users ► Main ► Help"));

                ((MasterPage)Master).AdditionalPageList = additionalPageList;

                #endregion

                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Search);
                ((MasterPage)Master).EnableAction(Action.Clear);
                ((MasterPage)Master).EnableAction(Action.Help);

                #endregion

                Session.Remove("olafUserList");

                SetSearchValues();
                SetSearchType();

                #region capture the enter key -> Search action

                txtLastName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtFirstName.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtTitle.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtLogin.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtEmail.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtUnit.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtBuilding.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtOffice.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlStatus.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlFl.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlAct.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                ddlLocal.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
                txtPhone.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                ddlType.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");

                #endregion
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();

                ((MasterPage)Master).SetGridviewProperties(gvItems1);
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                ((MasterPage)Master).SetGridviewProperties(gvItems3);
                ((MasterPage)Master).SetGridviewProperties(gvItems4);
                ((MasterPage)Master).SetGridviewProperties(gvItems5);
                ((MasterPage)Master).SetGridviewProperties(gvItems6);
                ((MasterPage)Master).SetGridviewProperties(gvItems7);
                ((MasterPage)Master).SetGridviewProperties(gvItems8);
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                GetSearchValues();

                if (rblSearchType.SelectedValue == "0")
                {
                    LoadGvItems();
                    up1.Update();

                    //clear other gridviews
                    gvItems2.DataSource = null;
                    gvItems2.DataBind();
                    lblHistoryResultsCount.Text = "0";
                    up2.Update();
                }
                else
                {
                    LoadGvHistoryItems();
                    up2.Update();

                    //clear other gridviews
                    gvItems1.DataSource = null;
                    gvItems1.DataBind();
                    lblResultsCount.Text = "0";
                    up1.Update();
                }

                //clear other gridviews
                gvItems3.DataSource = null;
                gvItems3.DataBind();
                lblGroupsCount.Text = "0";
                up3.Update();

                gvItems4.DataSource = null;
                gvItems4.DataBind();
                lblItems4Count.Text = "0";
                up4.Update();

                gvItems5.DataSource = null;
                gvItems5.DataBind();
                lblItems5Count.Text = "0";
                up5.Update();

                gvItems6.DataSource = null;
                gvItems6.DataBind();
                lblItems6Count.Text = "0";
                up6.Update();

                gvItems8.DataSource = null;
                gvItems8.DataBind();
                lblItems8Count.Text = "0";
                ibtnAddLicenseAssignment.Enabled = false;
                ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
                up8.Update();

                lbLoans.Enabled = false;
                divLoans.Visible = false;
                up7.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ClearSearchValues();
                upSearch.Update();
            }
            else if (((MasterPage)Master).CurrentAction == Action.Help)
            {
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Users/MainHelp.aspx");
            }
        }

        protected void gvItems1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "showLoadingAnimation();");
            }
        }

        protected void rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            upSearchType.Update();

            SetSearchType();
            upSearch.Update();

            //clear gridviews
            gvItems1.DataSource = null;
            gvItems1.DataBind();
            lblResultsCount.Text = "0";
            up1.Update();

            gvItems2.DataSource = null;
            gvItems2.DataBind();
            lblHistoryResultsCount.Text = "0";
            up2.Update();

            gvItems3.DataSource = null;
            gvItems3.DataBind();
            lblGroupsCount.Text = "0";
            up3.Update();

            gvItems4.DataSource = null;
            gvItems4.DataBind();
            lblItems4Count.Text = "0";
            up4.Update();

            gvItems5.DataSource = null;
            gvItems5.DataBind();
            lblItems5Count.Text = "0";
            up5.Update();

            gvItems6.DataSource = null;
            gvItems6.DataBind();
            lblItems6Count.Text = "0";
            up6.Update();

            gvItems8.DataSource = null;
            gvItems8.DataBind();
            lblItems8Count.Text = "0";
            ibtnAddLicenseAssignment.Enabled = false;
            ibtnAddLicenseAssignment.ImageUrl = "~/Images/AddGray.png";
            up8.Update();

            lbLoans.Enabled = false;
            divLoans.Visible = false;
            up7.Update();
        }

        private void gvItems1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));
                SelectedOlafUser.Select(id);

                if (cbSystemAccount.Checked)
                {
                    LoadGvHistoryItems(0);
                    lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
                    up2.Update();

                    // the system accounts have 'OLAF-SA_' or 'SA_' as prefix in the login
                    List<ActiveDirectoryGroupOlafUserBL> activeDirectoryGroupOlafUserList = ActiveDirectoryGroupOlafUserBL.GetByOlafUser("OLAF-SA_" + SelectedOlafUser.Login);
                    List<TicketBL> openTicketList = TicketBL.GetOpenByUserLogin("OLAF-SA_" + SelectedOlafUser.Login);
                    List<TicketBL> resolvedTicketList = TicketBL.GetResolvedByUserLogin("OLAF-SA_" + SelectedOlafUser.Login);

                    activeDirectoryGroupOlafUserList.AddRange(ActiveDirectoryGroupOlafUserBL.GetByOlafUser("SA_" + SelectedOlafUser.Login));
                    openTicketList.AddRange(TicketBL.GetOpenByUserLogin("SA_" + SelectedOlafUser.Login));
                    resolvedTicketList.AddRange(TicketBL.GetResolvedByUserLogin("SA_" + SelectedOlafUser.Login));

                    gvItems3.DataSource = activeDirectoryGroupOlafUserList;
                    gvItems3.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems3);
                    lblGroupsCount.Text = gvItems3.Rows.Count.ToString();
                    up3.Update();

                    gvItems4.DataSource = openTicketList;
                    gvItems4.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems4);
                    lblItems4Count.Text = gvItems4.Rows.Count.ToString();
                    up4.Update();

                    gvItems5.DataSource = resolvedTicketList;
                    gvItems5.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems5);
                    lblItems5Count.Text = gvItems5.Rows.Count.ToString();
                    up5.Update();

                    LoadGvSoftwareLicenseAssignments(0);
                    lblItems8Count.Text = gvItems8.Rows.Count.ToString();
                    up8.Update();

                    LoadGvHardwareItems(null, null);
                    lblItems6Count.Text = gvItems6.Rows.Count.ToString();
                    up6.Update();
                }
                else
                {
                    LoadGvHistoryItems(SelectedOlafUser.PeoId);
                    lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
                    up2.Update();

                    LoadGvGroups(SelectedOlafUser.Login);
                    lblGroupsCount.Text = gvItems3.Rows.Count.ToString();
                    up3.Update();

                    LoadGvOpenTickets(SelectedOlafUser.Login);
                    lblItems4Count.Text = gvItems4.Rows.Count.ToString();
                    up4.Update();

                    LoadGvResolvedTickets(SelectedOlafUser.Login);
                    lblItems5Count.Text = gvItems5.Rows.Count.ToString();
                    up5.Update();

                    LoadGvSoftwareLicenseAssignments(SelectedOlafUser.Id);
                    lblItems8Count.Text = gvItems8.Rows.Count.ToString();
                    ibtnAddLicenseAssignment.Enabled = true;
                    ibtnAddLicenseAssignment.ImageUrl = "~/Images/Add.png";
                    up8.Update();

                    LoadGvHardwareItems(SelectedOlafUser.FirstName, SelectedOlafUser.LastName);
                    lblItems6Count.Text = gvItems6.Rows.Count.ToString();
                    up6.Update();
                }

                lbLoans.Enabled = true;
                up7.Update();
            }
        }

        protected void cbSelectAll1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll1.Checked)
            {
                ArrayList validationErrors = new ArrayList();

                if (gvItems1.Rows.Count > 0)
                {
                    List<OlafUserBL> olafUserList = (List<OlafUserBL>)Session["olafUserList"];

                    List<OlafUserModificationBL> olafUserModificationList = new List<OlafUserModificationBL>();
                    List<ActiveDirectoryGroupOlafUserBL> activeDirectoryGroupOlafUserList = new List<ActiveDirectoryGroupOlafUserBL>();
                    List<TicketBL> openTicketList = new List<TicketBL>();
                    List<TicketBL> resolvedTicketList = new List<TicketBL>();
                    List<HardwareItemBL> hardwareItemList = new List<HardwareItemBL>();
                    List<SoftwareLicenseAssignmentBL> softwareLicenseAssignmentList = new List<SoftwareLicenseAssignmentBL>();

                    if (!cbSystemAccount.Checked)
                    {
                        olafUserList.ForEach(item =>
                        {
                            olafUserModificationList.AddRange(OlafUserModificationBL.GetByPeoId((int)item.PeoId));
                            activeDirectoryGroupOlafUserList.AddRange(ActiveDirectoryGroupOlafUserBL.GetByOlafUser(item.Login));
                            openTicketList.AddRange(TicketBL.GetOpenByUserLogin(item.Login));
                            resolvedTicketList.AddRange(TicketBL.GetResolvedByUserLogin(item.Login));
                            hardwareItemList.AddRange(HardwareItemBL.GetByParameters(ref validationErrors, "", null, "", "", item.LastName, item.FirstName, "", "", "", "", "", "", "", "", "", null, null));
                            softwareLicenseAssignmentList.AddRange(SoftwareLicenseAssignmentBL.GetByOlafUser(item.Id));
                        });
                    }
                    else
                    {
                        // the system accounts have 'OLAF-SA_' or 'SA_' as prefix in the login
                        olafUserList.ForEach(item =>
                        {
                            activeDirectoryGroupOlafUserList.AddRange(ActiveDirectoryGroupOlafUserBL.GetByOlafUser("OLAF-SA_" + item.Login));
                            activeDirectoryGroupOlafUserList.AddRange(ActiveDirectoryGroupOlafUserBL.GetByOlafUser("SA_" + item.Login));
                            openTicketList.AddRange(TicketBL.GetOpenByUserLogin("OLAF-SA_" + item.Login));
                            openTicketList.AddRange(TicketBL.GetOpenByUserLogin("SA_" + item.Login));
                            resolvedTicketList.AddRange(TicketBL.GetResolvedByUserLogin("OLAF-SA_" + item.Login));
                            resolvedTicketList.AddRange(TicketBL.GetResolvedByUserLogin("SA_" + item.Login));
                        });
                    }

                    gvItems2.DataSource = olafUserModificationList;
                    gvItems2.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems2);
                    lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
                    up2.Update();

                    gvItems3.DataSource = activeDirectoryGroupOlafUserList;
                    gvItems3.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems3);
                    lblGroupsCount.Text = gvItems3.Rows.Count.ToString();
                    up3.Update();

                    gvItems4.DataSource = openTicketList;
                    gvItems4.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems4);
                    lblItems4Count.Text = gvItems4.Rows.Count.ToString();
                    up4.Update();

                    gvItems5.DataSource = resolvedTicketList;
                    gvItems5.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems5);
                    lblItems5Count.Text = gvItems5.Rows.Count.ToString();
                    up5.Update();

                    gvItems6.DataSource = hardwareItemList;
                    gvItems6.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems6);
                    lblItems6Count.Text = gvItems6.Rows.Count.ToString();
                    up6.Update();

                    gvItems8.DataSource = softwareLicenseAssignmentList;
                    gvItems8.DataBind();
                    ((MasterPage)Master).SetGridviewProperties(gvItems8);
                    lblItems8Count.Text = gvItems8.Rows.Count.ToString();
                    up8.Update();

                    lbLoans.Enabled = true;
                    up7.Update();

                    ((MasterPage)Master).ShowValidationMessages(validationErrors);
                }
            }
            else
            {
                //clear other gridviews
                gvItems2.DataSource = null;
                gvItems2.DataBind();
                lblHistoryResultsCount.Text = "0";
                up2.Update();

                gvItems3.DataSource = null;
                gvItems3.DataBind();
                lblGroupsCount.Text = "0";
                up3.Update();

                gvItems4.DataSource = null;
                gvItems4.DataBind();
                lblItems4Count.Text = "0";
                up4.Update();

                gvItems5.DataSource = null;
                gvItems5.DataBind();
                lblItems5Count.Text = "0";
                up5.Update();

                gvItems6.DataSource = null;
                gvItems6.DataBind();
                lblItems6Count.Text = "0";
                up6.Update();

                gvItems8.DataSource = null;
                gvItems8.DataBind();
                lblItems8Count.Text = "0";
                up8.Update();

                lbLoans.Enabled = false;
                divLoans.Visible = false;
                up7.Update();
            }
        }

        private void gvItems3_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems3.Rows[int.Parse(e.CommandArgument.ToString())];
                string group = Utilities.GetGridViewRowText(row, "Group");

                //Compose url and display it in a new window                
                string url = "http://d-olaf/CallTools/sharepointpages-dev/GetGroup/ViewMembers2.aspx?GN=" + group;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript", "<script language='javascript'>window.open('" + url + "', 'CustomPopUp','width=800, height=600, resizable=yes, scrollbars=yes');</script>", false);
            }
        }

        private void gvItems4_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems4.Rows[int.Parse(e.CommandArgument.ToString())];
                string code = Utilities.GetGridViewRowText(row, "Code");

                //Compose url and display it in a new window                
                string url = "http://d-olaf/CallTools/Wimt/Default.aspx?ID=" + code;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript", "<script language='javascript'>window.open('" + url + "', 'CustomPopUp','width=800, height=600, resizable=yes, scrollbars=yes');</script>", false);
            }
        }

        private void gvItems5_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems5.Rows[int.Parse(e.CommandArgument.ToString())];
                string code = Utilities.GetGridViewRowText(row, "Code");

                //Compose url and display it in a new window
                string url = "http://d-olaf/CallTools/Wimt/Default.aspx?ID=" + code;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript", "<script language='javascript'>window.open('" + url + "', 'CustomPopUp','width=800, height=600, resizable=yes, scrollbars=yes');</script>", false);
            }
        }

        private void gvItems6_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems6.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

                SelectedHardwareItem.Select(id);

                FromPage = GetCurrentPage();
                PreviousPageStack.Push(GetCurrentPage());
                Response.Redirect("~/Hardware/Main.aspx");
            }
        }

        protected void ibtnAddSoftwareLicenseAssignment_Click(object sender, EventArgs e)
        {
            #region Grant access to additional pages

            List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL> additionalPageList = new List<Eu.Europa.Ec.Olaf.GmsrBLL.PageBL>();

            additionalPageList.Add(new Eu.Europa.Ec.Olaf.GmsrBLL.PageBL("~/Software/EditLicenseAssignment.aspx", "► Users ► Main ► License Assignment Details"));

            ((MasterPage)Master).AdditionalPageList = additionalPageList;

            #endregion

            SelectedSoftwareLicenseAssignment = null;
            PreviousPageStack.Push(GetCurrentPage());
            Response.Redirect("~/Software/EditLicenseAssignment.aspx");
        }

        protected void ibtnSoftwareLicenseAssignmentDelete_Click(object sender, EventArgs e)
        {
            ArrayList validationErrors = new ArrayList();

            if (!cbSelectAll1.Checked)
            {
                SoftwareLicenseAssignmentBL licenseAssignment = GetSelectedSoftwareLicenseAssignment((ImageButton)sender);

                GridViewRow row = gvItems1.SelectedRow;
                int olafUserId = int.Parse(Utilities.GetGridViewRowText(row, "Id"));

                licenseAssignment.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

                ((MasterPage)Master).ShowValidationMessages(validationErrors);
                if (validationErrors.Count == 0)
                {
                    //clear gridview
                    gvItems8.DataSource = null;
                    gvItems8.DataBind();
                    lblItems8Count.Text = "0";
                    up8.Update();
                }
            }
            else
            {
                validationErrors.Add("This action is not allowed when all users are selected.");
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }
        }

        protected void lbLoans_Click(object sender, EventArgs e)
        {
            if (lbLoans.Text == "Show Loans")
            {
                lbLoans.Text = "Hide Loans";
                divLoans.Visible = true;

                if (cbSelectAll1.Checked)
                {
                    List<OlafUserBL> olafUserList = (List<OlafUserBL>)Session["olafUserList"];

                    List<LoanBL> loanList = new List<LoanBL>();

                    olafUserList.ForEach(item =>
                    {
                        loanList.AddRange(LoanBL.GetByUserLogin(item.Login));
                    });

                    gvItems7.DataSource = loanList;
                    gvItems7.DataBind();
                    lblItems7Count.Text = gvItems7.Rows.Count.ToString();
                    ((MasterPage)Master).SetGridviewProperties(gvItems7);
                    up7.Update();
                }
                else
                {
                    GridViewRow row = gvItems1.SelectedRow;
                    LoadGvLoans(Utilities.GetGridViewRowText(row, "Login"));
                    up7.Update();
                }
            }
            else
            {
                lbLoans.Text = "Show Loans";
                divLoans.Visible = false;
                up7.Update();
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

        protected void ibtnExportItems5_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems5, Response);
        }

        protected void ibtnExportItems6_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems6, Response);
        }

        protected void ibtnExportItems7_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems7, Response);
        }

        protected void ibtnExportItems8_Click(object sender, EventArgs e)
        {
            Utilities.ExportToExcel(gvItems8, Response);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Utilities.MakeSelectable(gvItems1, Page);
            Utilities.MakeSelectable(gvItems3, Page);
            Utilities.MakeSelectable(gvItems4, Page);
            Utilities.MakeSelectable(gvItems5, Page);
            Utilities.MakeSelectable(gvItems6, Page);
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

        private void SetSearchType()
        {
            if (rblSearchType.SelectedValue == "0")
            {
                tblSearchUsers.Visible = true;
                tblSearchUserModifications.Visible = false;
            }
            else if (rblSearchType.SelectedValue == "1")
            {
                tblSearchUsers.Visible = false;
                tblSearchUserModifications.Visible = true;
            }
        }

        private void ClearSearchValues()
        {
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtTitle.Text = "";
            txtLogin.Text = "";
            txtEmail.Text = "";
            txtUnit.Text = "";
            txtBuilding.Text = "";
            txtOffice.Text = "";
            ddlStatus.SelectedValue = "";
            ddlFl.SelectedValue = "";
            ddlAct.SelectedValue = "";
            ddlLocal.SelectedValue = "";
            txtPhone.Text = "";

            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            ddlType.SelectedValue = "";
        }

        private void SetSearchValues()
        {
            if (!Equals(Session["usersSearchType"], null))
            {
                rblSearchType.SelectedValue = (string)Session["usersSearchType"];
            }

            if (!Equals(Session["usersSearchValues"], null))
            {
                Dictionary<string, string> searchValues = (Dictionary<string, string>)Session["usersSearchValues"];
                txtLastName.Text = searchValues["lastName"];
                txtFirstName.Text = searchValues["firstName"];
                txtTitle.Text = searchValues["title"];
                txtLogin.Text = searchValues["login"];
                txtEmail.Text = searchValues["email"];
                txtUnit.Text = searchValues["unit"];
                txtBuilding.Text = searchValues["building"];
                txtOffice.Text = searchValues["office"];
                ddlStatus.SelectedValue = searchValues["status"];
                ddlFl.SelectedValue = searchValues["fl"];
                ddlAct.SelectedValue = searchValues["act"];
                ddlLocal.SelectedValue = searchValues["local"];
                txtPhone.Text = searchValues["phone"];
            }

            if (!Equals(Session["usersSearchValues1"], null))
            {
                Dictionary<string, string> searchValues1 = (Dictionary<string, string>)Session["usersSearchValues1"];
                txtDateFrom.Text = searchValues1["dateFrom"];
                txtDateTo.Text = searchValues1["dateTo"];
                ddlType.SelectedValue = searchValues1["type"];
            }
        }

        private void GetSearchValues()
        {
            Dictionary<string, string> searchValues = new Dictionary<string, string>();
            searchValues.Add("lastName", txtLastName.Text);
            searchValues.Add("firstName", txtFirstName.Text);
            searchValues.Add("title", txtTitle.Text);
            searchValues.Add("login", txtLogin.Text);
            searchValues.Add("email", txtEmail.Text);
            searchValues.Add("unit", txtUnit.Text);
            searchValues.Add("building", txtBuilding.Text);
            searchValues.Add("office", txtOffice.Text);
            searchValues.Add("status", ddlStatus.SelectedItem.Value);
            searchValues.Add("fl", ddlFl.SelectedItem.Value);
            searchValues.Add("act", ddlAct.SelectedItem.Value);
            searchValues.Add("local", ddlLocal.SelectedItem.Value);
            searchValues.Add("phone", txtPhone.Text);
            Session["usersSearchValues"] = searchValues;

            Dictionary<string, string> searchValues1 = new Dictionary<string, string>();
            searchValues1.Add("dateFrom", txtDateFrom.Text);
            searchValues1.Add("dateTo", txtDateTo.Text);
            searchValues1.Add("type", ddlType.SelectedItem.Value);
            Session["usersSearchValues1"] = searchValues1;
        }

        private SoftwareLicenseAssignmentBL GetSelectedSoftwareLicenseAssignment(ImageButton button)
        {
            // get item id of row            
            GridViewRow row = button.Parent.Parent as GridViewRow;
            int id = int.Parse(gvItems8.DataKeys[row.RowIndex]["Id"].ToString());

            SoftwareLicenseAssignmentBL item = new SoftwareLicenseAssignmentBL();
            item.Select(id);

            return item;
        }

        private void LoadGvItems()
        {
            List<OlafUserBL> itemList = OlafUserBL.GetByParameters(txtLastName.Text,
                                                            txtFirstName.Text,
                                                            txtTitle.Text,
                                                            txtLogin.Text,
                                                            txtEmail.Text,
                                                            txtUnit.Text,
                                                            txtBuilding.Text,
                                                            txtOffice.Text,
                                                            ddlStatus.SelectedValue,
                                                            ddlFl.SelectedValue,
                                                            ddlAct.SelectedValue,
                                                            ddlLocal.SelectedValue == "" ? null : (bool?)bool.Parse(ddlLocal.SelectedValue),
                                                            txtPhone.Text
                                                            );

            gvItems1.DataSource = itemList;

            gvItems1.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems1);
            lblResultsCount.Text = gvItems1.Rows.Count.ToString();

            Session["olafUserList"] = itemList;
        }

        private void LoadGvHistoryItems(decimal? peoId)
        {
            List<OlafUserModificationBL> itemList = OlafUserModificationBL.GetByPeoId(peoId);

            gvItems2.DataSource = itemList;
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvHistoryItems()
        {
            List<OlafUserModificationBL> itemList = OlafUserModificationBL.GetByParameters(txtDateFrom.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateFrom.Text),
                                                                   txtDateTo.Text == "" ? null : (DateTime?)DateTime.Parse(txtDateTo.Text),
                                                                   ddlType.SelectedValue);

            gvItems2.DataSource = itemList;
            gvItems2.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblHistoryResultsCount.Text = gvItems2.Rows.Count.ToString();
        }

        private void LoadGvGroups(string userLogin)
        {
            List<ActiveDirectoryGroupOlafUserBL> itemList = ActiveDirectoryGroupOlafUserBL.GetByOlafUser(userLogin);

            gvItems3.DataSource = itemList;
            gvItems3.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems3);
            lblGroupsCount.Text = gvItems3.Rows.Count.ToString();
        }

        private void LoadGvOpenTickets(string userLogin)
        {
            List<TicketBL> itemList = TicketBL.GetOpenByUserLogin(userLogin);

            gvItems4.DataSource = itemList;
            gvItems4.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems4);
            lblItems4Count.Text = gvItems4.Rows.Count.ToString();
        }

        private void LoadGvResolvedTickets(string userLogin)
        {
            List<TicketBL> itemList = TicketBL.GetResolvedByUserLogin(userLogin);

            gvItems5.DataSource = itemList;
            gvItems5.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems5);
            lblItems5Count.Text = gvItems5.Rows.Count.ToString();
        }

        private void LoadGvHardwareItems(string firstName, string lastName)
        {
            ArrayList validationErrors = new ArrayList();

            List<HardwareItemBL> itemList = HardwareItemBL.GetByParameters(ref validationErrors, "", null, "", "", lastName, firstName, "", "", "", "", "", "", "", "", "", null, null);

            gvItems6.DataSource = itemList;

            gvItems6.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems6);
            lblItems6Count.Text = gvItems6.Rows.Count.ToString();

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
        }

        private void LoadGvHardwareItems(HardwareItemBL hardwareItem)
        {
            List<HardwareItemBL> itemList = new List<HardwareItemBL>();
            itemList.Add(hardwareItem);

            gvItems6.DataSource = itemList;
            gvItems6.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems6);
            lblItems6Count.Text = gvItems6.Rows.Count.ToString();
        }

        private void LoadGvLoans(string userLogin)
        {
            List<LoanBL> itemList = LoanBL.GetByUserLogin(userLogin);

            gvItems7.DataSource = itemList;
            gvItems7.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems7);
            lblItems7Count.Text = gvItems7.Rows.Count.ToString();
        }

        private void LoadGvSoftwareLicenseAssignments(int olafUserId)
        {
            List<SoftwareLicenseAssignmentBL> itemList = SoftwareLicenseAssignmentBL.GetByOlafUser(olafUserId);

            gvItems8.DataSource = itemList;
            gvItems8.DataBind();

            ((MasterPage)Master).SetGridviewProperties(gvItems8);
            lblItems8Count.Text = gvItems8.Rows.Count.ToString();
        }

        #endregion
    }
}