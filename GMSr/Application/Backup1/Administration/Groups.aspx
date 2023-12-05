<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Groups.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.Groups"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Groups
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblKeyword" runat="server" Text="Keyword:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Groups:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="400px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 200px;">
                                Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Is Active
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 400px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="400px">
                    <Columns>
                        <asp:BoundField DataField="Name" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="IsActive" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" OnClick="ibtnEdit_Click" ImageUrl="~/Images/Edit.png"
                                    ToolTip="Edit" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDelete" runat="server" OnClientClick="showConfirm(this); return false;"
                                    OnClick="ibtnDelete_Click" ImageUrl="~/Images/Delete.png" ToolTip="Delete" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:ImageButton ID="ibtnAdd" runat="server" ImageUrl="~/Images/AddBig.png" ToolTip="Add"
        OnClick="ibtnAdd_Click" />
</asp:Content>
