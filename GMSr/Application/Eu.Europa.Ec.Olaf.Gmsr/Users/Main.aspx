<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Users.Main" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Main
    </div>
    <asp:UpdatePanel ID="upSearchType" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:RadioButtonList ID="rblSearchType" runat="server" RepeatDirection="Horizontal"
                OnSelectedIndexChanged="rblSearchType_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0" Selected="True">Search in Users</asp:ListItem>
                <asp:ListItem Value="1">Search in User Modifications</asp:ListItem>
            </asp:RadioButtonList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="tblSearchUsers" runat="server">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLogin" runat="server" Text="Login:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUnit" runat="server" Text="Unit:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblBuilding" runat="server" Text="Building:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBuilding" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOffice" runat="server" Text="Office:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOffice" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:" ToolTip="'EXT': External. 'COM': Commission."></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>EXT</asp:ListItem>
                                        <asp:ListItem>COM</asp:ListItem>
                                        <asp:ListItem>CEE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFl" runat="server" Text="Active (FL):" ToolTip="Active in Human Resources."></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlFl" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Selected="True">Y</asp:ListItem>
                                        <asp:ListItem>N</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAct" runat="server" Text="Act:" ToolTip="'A': in Active Directory and not managed locally. 'D' and Active (FL) is not empty: deleted in Human Resources and managed locally. 'D' and Active (FL) is empty: never been in Human Resources and managed locally."></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlAct" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>D</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLocal" runat="server" Text="Local:" ToolTip="Locally managed."></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlLocal" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblSearchUserModifications" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDate" runat="server" Text="Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDateFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtDateTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblType" runat="server" Text="Type:" ToolTip="'New': New in Human Resources. 'Change': Office change. 'Disable': Removed in Human Resources."></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlType" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>New</asp:ListItem>
                                        <asp:ListItem>Change</asp:ListItem>
                                        <asp:ListItem>Disable</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblResultsCountPrefix" runat="server" Text="Users:"></asp:Label>
            <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <asp:CheckBox ID="cbSelectAll1" runat="server" Checked="false" Text="Select All"
                OnCheckedChanged="cbSelectAll1_CheckedChanged" AutoPostBack="true" />
            <asp:CheckBox ID="cbSystemAccount" runat="server" Checked="false" Text="System Account" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="2000px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Title
                            </th>
                            <th scope="col" style="width: 150px;">
                                Last Name
                            </th>
                            <th scope="col" style="width: 150px;">
                                First Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Login
                            </th>
                            <th scope="col" style="width: 300px;">
                                Email
                            </th>
                            <th scope="col" style="width: 100px;">
                                Phone
                            </th>
                            <th scope="col" style="width: 150px;">
                                Unit
                            </th>
                            <th scope="col" style="width: 150px;">
                                Building
                            </th>
                            <th scope="col" style="width: 150px;">
                                Office
                            </th>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Active (Fl)
                            </th>
                            <th scope="col" style="width: 100px;">
                                Act
                            </th>
                            <th scope="col" style="width: 100px;">
                                Local
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 100px;">
                                PeoId
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 250px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="2000px" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Title" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="LastName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="FirstName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Login" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Email" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="Phone" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Unit" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Building" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Office" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Fl" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Act" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Local" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PeoId" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="float: left; width: 77%;">
        <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="lblHistoryResultsCountPrefix" runat="server" Text="User Modifications:"></asp:Label>
                <asp:Label ID="lblHistoryResultsCount" runat="server" Font-Bold="true" ForeColor="#4fad4f"
                    Text="0"></asp:Label>
                <asp:ImageButton ID="ibtnExportItems2" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportItems2_Click" />
                <div id="divHeader2" class="divHeader">
                    <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="900px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 100px;">
                                    Date
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Last Name
                                </th>
                                <th scope="col" style="width: 150px;">
                                    First Name
                                </th>
                                <th scope="col" style="width: 100px;">
                                    User
                                </th>
                                <th scope="col" style="width: 150px;">
                                    New Office
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Old Office
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Type
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer2" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(2);">
                    <asp:GridView ID="gvItems2" runat="server" DataKeyNames="Id" Width="900px">
                        <Columns>
                            <asp:BoundField DataField="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="LastName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="FirstName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="User" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="NewOffice" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="OldOffice" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Type" ItemStyle-Width="100px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="float: right; width: 22%;">
        <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="lblGroupsCountPrefix" runat="server" Text="Groups:"></asp:Label>
                <asp:Label ID="lblGroupsCount" runat="server" Font-Bold="true" ForeColor="#9562ae"
                    Text="0"></asp:Label>
                <asp:ImageButton ID="ibtnExportItems3" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportItems3_Click" />
                <div id="divHeader3" class="divHeader">
                    <table id="tblStaticHeader3" class="tblStaticHeader" rules="all" width="220px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 250px;">
                                    Name
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer3" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(3);">
                    <asp:GridView ID="gvItems3" runat="server" Width="220px">
                        <Columns>
                            <asp:BoundField DataField="Group" ItemStyle-Width="220px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems4CountPrefix" runat="server" Text="Open Tickets:"></asp:Label>
            <asp:Label ID="lblItems4Count" runat="server" Font-Bold="true" ForeColor="#d1982f"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems4" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems4_Click" />
            <div id="divHeader4" class="divHeader">
                <table id="tblStaticHeader4" class="tblStaticHeader" rules="all" width="1180px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Code
                            </th>
                            <th scope="col" style="width: 150px;">
                                Created
                            </th>
                            <th scope="col" style="width: 580px;">
                                Title
                            </th>
                            <th scope="col" style="width: 300px;">
                                Nature
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer4" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(4);">
                <asp:GridView ID="gvItems4" runat="server" Width="1180px">
                    <Columns>
                        <asp:BoundField DataField="Code" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="CreationDate" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Title" ItemStyle-Width="580px" />
                        <asp:BoundField DataField="Nature" ItemStyle-Width="300px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems5CountPrefix" runat="server" Text="Resolved & Closed Tickets:"></asp:Label>
            <asp:Label ID="lblItems5Count" runat="server" Font-Bold="true" ForeColor="#85c62a"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems5" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems5_Click" />
            <div id="divHeader5" class="divHeader">
                <table id="tblStaticHeader5" class="tblStaticHeader" rules="all" width="1180px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Code
                            </th>
                            <th scope="col" style="width: 150px;">
                                Resolved
                            </th>
                            <th scope="col" style="width: 580px;">
                                Title
                            </th>
                            <th scope="col" style="width: 300px;">
                                Nature
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer5" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(5);">
                <asp:GridView ID="gvItems5" runat="server" Width="1180px">
                    <Columns>
                        <asp:BoundField DataField="Code" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="ActionGroupResolutionDate" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Title" ItemStyle-Width="580px" />
                        <asp:BoundField DataField="Nature" ItemStyle-Width="300px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems8CountPrefix" runat="server" Text="Software License Assignments:"></asp:Label>
            <asp:Label ID="lblItems8Count" runat="server" Font-Bold="true" ForeColor="#57b797"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems8" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems8_Click" />
            <asp:ImageButton ID="ibtnAddLicenseAssignment" runat="server" ImageUrl="~/Images/AddGray.png"
                ToolTip="Add" OnClick="ibtnAddSoftwareLicenseAssignment_Click" Enabled="false" />
            <div id="divHeader8" class="divHeader">
                <table id="tblStaticHeader8" class="tblStaticHeader" rules="all" width="1600px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 150px;">
                                Status
                            </th>
                            <th scope="col" style="width: 250px;">
                                Product Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Prod. Version
                            </th>
                            <th scope="col" style="width: 150px;">
                                Product Company
                            </th>
                            <th scope="col" style="width: 100px;">
                                Assignment Date
                            </th>
                            <th scope="col" style="width: 100px;">
                                Installation Date
                            </th>
                            <th scope="col" style="width: 100px;">
                                Unassignment Date
                            </th>
                            <th scope="col" style="width: 500px;">
                                Comment
                            </th>
                            <th scope="col" style="width: 100px;">
                                License Id
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer8" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(8);">
                <asp:GridView ID="gvItems8" runat="server" DataKeyNames="Id" Width="1600px">
                    <Columns>
                        <asp:BoundField DataField="Status" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="SoftwareProductName" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="SoftwareProductVersion" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareProductCompanyName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="DateAssigned" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateInstalled" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateUnassigned" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Comment" ItemStyle-Width="500px" />
                        <asp:BoundField DataField="SoftwareLicenseId" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDelete" runat="server" OnClientClick="showConfirm(this); return false;"
                                    OnClick="ibtnSoftwareLicenseAssignmentDelete_Click" ImageUrl="~/Images/Delete.png"
                                    ToolTip="Delete" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems6CountPrefix" runat="server" Text="Hardware Items:"></asp:Label>
            <asp:Label ID="lblItems6Count" runat="server" Font-Bold="true" ForeColor="#ca8383"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems6" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems6_Click" />
            <div id="divHeader6" class="divHeader">
                <table id="tblStaticHeader6" class="tblStaticHeader" rules="all" width="2850px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Local
                            </th>
                            <th scope="col" style="width: 150px;">
                                Inventory No
                            </th>
                            <th scope="col" style="width: 150px;">
                                Olaf Name
                            </th>
                            <th scope="col" style="width: 200px;">
                                Model
                            </th>
                            <th scope="col" style="width: 350px;">
                                Description
                            </th>
                            <th scope="col" style="width: 150px;">
                                Last Name
                            </th>
                            <th scope="col" style="width: 150px;">
                                First Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Phone
                            </th>
                            <th scope="col" style="width: 100px;">
                                Building
                            </th>
                            <th scope="col" style="width: 100px;">
                                Office
                            </th>
                            <th scope="col" style="width: 200px;">
                                Serial
                            </th>
                            <th scope="col" style="width: 100px;">
                                Unit
                            </th>
                            <th scope="col" style="width: 300px;">
                                RmtCtgNm
                            </th>
                            <th scope="col" style="width: 300px;">
                                RmtParentCtgNm
                            </th>
                            <th scope="col" style="width: 100px;">
                                RmtMatId
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer6" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(6);">
                <asp:GridView ID="gvItems6" runat="server" DataKeyNames="Id" Width="2850px">
                    <Columns>
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Local" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="InventoryNo" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="OlafName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Model" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="Description" ItemStyle-Width="350px" />
                        <asp:BoundField DataField="LastName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="FirstName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Phone" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Building" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Office" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Serial" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="Unit" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="RmtCtgNm" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="RmtParentCtgNm" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="RmtMatId" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbLoans" runat="server" Enabled="false" OnClick="lbLoans_Click">Show Loans</asp:LinkButton>
            <div id="divLoans" runat="server" visible="false">
                <asp:Label ID="lblItems7CountPrefix" runat="server" Text="Loans:"></asp:Label>
                <asp:Label ID="lblItems7Count" runat="server" Font-Bold="true" ForeColor="#6189af"
                    Text="0"></asp:Label>
                <asp:ImageButton ID="ibtnExportItems7" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportItems7_Click" />
                <div id="divHeader7" class="divHeader">
                    <table id="tblStaticHeader7" class="tblStaticHeader" rules="all" width="1650px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 100px;">
                                    Date Start
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Date End
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Category
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Status
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Ticket Code
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Is Active
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Is Permanent
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Inventory No
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Olaf Name
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Model
                                </th>
                                <th scope="col" style="width: 350px;">
                                    Description
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer7" class="divContainer" style="height: 70px;" onscroll="scrollDivHeader(7);">
                    <asp:GridView ID="gvItems7" runat="server" Width="1650px">
                        <Columns>
                            <asp:BoundField DataField="DateStart" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="DateEnd" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="Category" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="TicketCode" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="IsActive" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="IsPermanent" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="InventoryNo" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="OlafName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Model" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Description" ItemStyle-Width="350px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
