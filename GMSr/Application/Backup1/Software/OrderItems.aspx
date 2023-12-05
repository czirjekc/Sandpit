<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="OrderItems.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.OrderItems"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Entries
    </div>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrder" runat="server" Text="Order:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOrder" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOlafRef" runat="server" Text="OLAF Ref:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOlafRef" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblType" runat="server" Text="Entry Type:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductSource" runat="server" Text="Product Source:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductSource" runat="server">                                        
                                    </asp:DropDownList>                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductName" runat="server" Text="Product Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductVersion" runat="server" Text="Product Version:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductVersion" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDateStart" runat="server" Text="Maintenance Start Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDateStartFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDateStartFrom_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateStartFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtDateStartTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDateStartTo_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateStartTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDateEnd" runat="server" Text="Maintenance End Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDateEndFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDateEndFrom_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateEndFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtDateEndTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDateEndTo_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateEndTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblWithoutUpgrade" runat="server" Text="Only:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="cbWithoutUpgrade" runat="server" Text="Entries without upgrade"
                                        Checked="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPreviousConcatenation" runat="server" Text="Previous Entry:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPreviousConcatenation" runat="server"></asp:TextBox>
                                </td>
                            </tr>
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
                                    <asp:Label ID="lblCreator" runat="server" Text="Creator Login:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCreator" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCreationDate" runat="server" Text="Creation Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCreationDateFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtCreationDateFrom_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtCreationDateFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtCreationDateTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtCreationDateTo_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtCreationDateTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
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
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Entries:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="2850px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 200px;">
                                Order
                            </th>
                            <th scope="col" style="width: 100px;">
                                OLAF Ref
                            </th>
                            <th scope="col" style="width: 200px;">
                                Type
                            </th>
                            <th scope="col" style="width: 400px;">
                                Product Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Product Version
                            </th>
                            <th scope="col" style="width: 100px;">
                                Maintenance Start Date
                            </th>
                            <th scope="col" style="width: 100px;">
                                Maintenance End Date
                            </th>
                            <th scope="col" style="width: 600px;">
                                Previous Entry
                            </th>
                            <th scope="col" style="width: 400px;">
                                Comment
                            </th>
                            <th scope="col" style="width: 100px;">
                                Creator
                            </th>
                            <th scope="col" style="width: 100px;">
                                Creation Date
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
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 400px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="2850px" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToProduct" runat="server" OnClick="ibtnGoToProduct_Click"
                                    ImageUrl="~/Images/P_Green.png" ImageAlign="Middle" ToolTip="Go To Product" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToLicenses" runat="server" OnClick="ibtnGoToLicenses_Click"
                                    ImageUrl="~/Images/L_Blue.png" ImageAlign="Middle" ToolTip="Go To License(s)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToPredecessor" runat="server" OnClick="ibtnGoToPredecessor_Click"
                                    ImageUrl="~/Images/E_LeftPurple.png" ImageAlign="Middle" ToolTip="Go To Predecessor" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToSuccessors" runat="server" OnClick="ibtnGoToSuccessors_Click"
                                    ImageUrl="~/Images/E_RightPurple.png" ImageAlign="Middle" ToolTip="Go To Successors (Upgrades and/or Maintenances)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="OrderFormName" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="OlafRef" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareOrderItemTypeName" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="SoftwareProductName" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="SoftwareProductVersion" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="MaintenanceStartDate" DataFormatString="{0:dd/MM/yyyy}"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="MaintenanceEndDate" DataFormatString="{0:dd/MM/yyyy}"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="PreviousSoftwareOrderItemConcatenation" ItemStyle-Width="600px" />
                        <asp:BoundField DataField="Comment" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="CreatorLogin" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="CreationDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareProductSource" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnAddUpgrade" runat="server" OnClick="ibtnAddUpgrade_Click"
                                    ImageUrl="~/Images/AddPurpleSmall.png" ToolTip="Add Upgrade" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnAddMaintenance" runat="server" OnClick="ibtnAddMaintenance_Click"
                                    ImageUrl="~/Images/AddOrangeSmall.png" ToolTip="Add Maintenance" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" OnClick="ibtnEdit_Click" ImageUrl="~/Images/Edit.png"
                                    ImageAlign="Middle" ToolTip="Edit" />
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
    <br />
    <asp:UpdatePanel ID="upAdd" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblAddOrderType" runat="server" Text="Entry Type:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAddOrderType" runat="server" OnSelectedIndexChanged="ddlAddOrderType_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:ImageButton ID="ibtnAdd" runat="server" ImageUrl="~/Images/AddBigGray.png" Enabled="false"
                ToolTip="Add" OnClick="ibtnAdd_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
