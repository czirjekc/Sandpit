<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="AuditTrail.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.AuditTrail"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Audit Trail
    </div>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="tblSearchAuditItems" runat="server">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTimestamp" runat="server" Text="Timestamp Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtTimestampFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtTimestampFrom_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtTimestampFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtTimestampTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtTimestampTo_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtTimestampTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblEntitySet" runat="server" Text="Entity Set:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEntitySet" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUserLogin" runat="server" Text="User Login:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserLogin" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAction" runat="server" Text="Action:"></asp:Label>
                                </td>
                                <td align="left">                                    
                                    <asp:DropDownList ID="ddlAction" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Insert</asp:ListItem>
                                    <asp:ListItem>Update</asp:ListItem>
                                    <asp:ListItem>Delete</asp:ListItem>
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
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Audit items:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="650px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 150px;">
                                Timestamp
                            </th>
                            <th scope="col" style="width: 250px;">
                                Entity Set
                            </th>
                            <th scope="col" style="width: 100px;">
                                User Login
                            </th>
                            <th scope="col" style="width: 100px;">
                                Action
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 300px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Guid" Width="650px" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Timestamp" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="EntitySet" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="UserLogin" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Action" ItemStyle-Width="100px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <br />
    <div style="float: left; width: 49%;">
        <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                Old Data:
                <div id="divHeader3" class="divHeader">
                    <table id="tblStaticHeader3" class="tblStaticHeader" rules="all" width="570px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 200px;">
                                    Property
                                </th>
                                <th scope="col" style="width: 340px;">
                                    Value
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer3" class="divContainer" style="height: 200px;" onscroll="scrollDivHeader(3);">
                    <asp:GridView ID="gvItems3" runat="server" Width="570px" OnRowDataBound="gvItems3_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Name" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Value" ItemStyle-Width="340px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="float: right; width: 49%;">
        <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                New Data:
                <div id="divHeader4" class="divHeader">
                    <table id="tblStaticHeader4" class="tblStaticHeader" rules="all" width="570px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 200px;">
                                    Property
                                </th>
                                <th scope="col" style="width: 340px;">
                                    Value
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer4" class="divContainer" style="height: 200px;" onscroll="scrollDivHeader(4);">
                    <asp:GridView ID="gvItems4" runat="server" Width="570px" OnRowDataBound="gvItems4_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Name" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Value" ItemStyle-Width="340px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
