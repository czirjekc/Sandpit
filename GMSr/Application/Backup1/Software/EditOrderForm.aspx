<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditOrderForm.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditOrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Order Details
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr class="separator">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDocument" runat="server" Text="Documents:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Documents:"></asp:Label>
                        <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                            Text="0"></asp:Label>
                        <div id="divHeader1" class="divHeader" style="width: 600px;">
                            <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="1900px">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 100px;">
                                            Id
                                        </th>
                                        <th scope="col" style="width: 400px;">
                                            Description
                                        </th>
                                        <th scope="col" style="width: 400px;">
                                            File
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                        <th scope="col" style="width: 800px;">
                                            Path
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div id="divContainer1" class="divContainer" style="height: 150px; width: 600px;"
                            onscroll="scrollDivHeader(1);">
                            <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="1900px">
                                <Columns>
                                    <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="Description" ItemStyle-Width="400px" />
                                    <asp:BoundField DataField="FileInfo" ItemStyle-Width="400px" />
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDownloadDocumentFile" runat="server" OnClick="ibtnDownloadDocumentFile_Click"
                                                ImageUrl="~/Images/Download.png" ToolTip="Download File" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Path" ItemStyle-Width="800px" />
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEditDocument" runat="server" OnClick="ibtnEditDocument_Click"
                                                ImageUrl="~/Images/Edit.png" ToolTip="Edit" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDeleteDocument" runat="server" OnClientClick="showConfirm(this); return false;"
                                                OnClick="ibtnDeleteDocument_Click" ImageUrl="~/Images/Delete.png" ToolTip="Delete"
                                                ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="ibtnAddDocument" runat="server" ImageUrl="~/Images/Add.png"
                            ToolTip="Add Document" OnClick="ibtnAddDocument_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvItems1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
