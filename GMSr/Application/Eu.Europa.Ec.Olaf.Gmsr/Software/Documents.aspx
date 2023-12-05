<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Documents.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.Documents"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Order Documents
    </div>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Documents:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="2100px">
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
                            <th scope="col" style="width: 200px;">
                                Order
                            </th>
                            <th scope="col" style="width: 100px;">
                                Source
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
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="2100px" OnRowCreated="gvItems1_RowCreated">
                    <Columns>
                        <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Description" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="FileInfo" ItemStyle-Width="400px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDownload" runat="server" OnClick="ibtnDownload_Click"
                                    ImageUrl="~/Images/Download.png" ToolTip="Download File" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Path" ItemStyle-Width="800px" />
                        <asp:BoundField DataField="OrderFormName" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="SoftwareProductSource" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" OnClick="ibtnEdit_Click"
                                    ImageUrl="~/Images/Edit.png" ToolTip="Edit" ImageAlign="Middle" />
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
</asp:Content>
