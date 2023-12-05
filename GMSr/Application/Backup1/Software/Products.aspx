<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Products.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.Products"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Products
    </div>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table>
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
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblVersion" runat="server" Text="Version:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCompany" runat="server" Text="Company:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOther" runat="server" Text="Other:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOther" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtComment" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSource" runat="server" Text="Source:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSource" runat="server">                                        
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
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Products:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="2260px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 400px;">
                                Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Version
                            </th>
                            <th scope="col" style="width: 200px;">
                                Company
                            </th>
                            <th scope="col" style="width: 200px;">
                                Other
                            </th>
                            <th scope="col" style="width: 200px;">
                                Status
                            </th>
                            <th scope="col" style="width: 800px;">
                                Comment
                            </th>
                            <th scope="col" style="width: 100px;">
                                Source
                            </th>                            
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
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
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="2260px" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToLicense" runat="server" OnClick="ibtnGoToLicense_Click"
                                    ImageUrl="~/Images/L_Blue.png" ImageAlign="Middle" ToolTip="Go To License(s)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToEntries" runat="server" OnClick="ibtnGoToEntries_Click"
                                    ImageUrl="~/Images/E_Purple.png" ImageAlign="Middle" ToolTip="Go To Entries" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Name" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="Version" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="CompanyName" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="Other" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="SoftwareProductStatusName" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="Comment" ItemStyle-Width="800px" />
                        <asp:BoundField DataField="Source" ItemStyle-Width="100px" />                        
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnAddLicenses" runat="server" OnClick="ibtnAddLicenses_Click"
                                    ImageUrl="~/Images/AddPurpleSmall.png" ToolTip="Add License(s)" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
