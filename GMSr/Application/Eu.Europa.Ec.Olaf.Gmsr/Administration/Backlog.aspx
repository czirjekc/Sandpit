<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Backlog.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.Backlog"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Product Backlog
            </div>
            <asp:UpdatePanel ID="upSearch" runat="server">
                <ContentTemplate>
                    <table id="tblSearchItems" runat="server">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlStatus" runat="server">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Completed</asp:ListItem>
                                                <asp:ListItem>Current Sprint</asp:ListItem>
                                                <asp:ListItem>Not Started</asp:ListItem>
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
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="1150px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 800px;">
                                Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date Start
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date End
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 500px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="1150px" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Name" ItemStyle-Width="800px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateStart" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateEnd" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
