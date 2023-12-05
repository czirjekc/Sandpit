<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditOrderItem.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditOrderItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                <asp:Label ID="lblTitle" runat="server" Text="Entry Details"></asp:Label>
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblTypeValue" runat="server" ForeColor="#000000"></asp:Label>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr class="separator">
                    <td colspan="3">
                    </td>
                </tr>
                <tr id="trPrevious" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblPrevious" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPrevious" runat="server" Width="800px" AutoPostBack="true"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="acePrevious" TargetControlID="txtPrevious"
                            ServiceMethod="GetLicensePackCompletionList" MinimumPrefixLength="3" CompletionInterval="1000"
                            EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr id="trProduct" runat="server">
                    <td align="right">
                        <asp:Label ID="lblProduct" runat="server" Text="Product:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtProduct" runat="server" Width="800px" AutoPostBack="true"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceProduct" TargetControlID="txtProduct"
                            ServiceMethod="GetProductCompletionList" MinimumPrefixLength="3" CompletionInterval="1000"
                            EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="ibtnAddProduct" runat="server" ImageUrl="~/Images/Add.png" ToolTip="Add Product"
                            OnClick="ibtnAddProduct_Click" />
                    </td>
                </tr>
                <tr class="separator">
                    <td colspan="3">
                    </td>
                </tr>
                <tr id="trLicenses" runat="server">
                    <td align="right">
                        <asp:Label ID="lblLicense" runat="server" Text="Licenses:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Licenses:"></asp:Label>
                        <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                            Text="0"></asp:Label>
                        <div id="divHeader1" class="divHeader" style="width: 800px;">
                            <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="1320px">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 100px;">
                                            License Id
                                        </th>
                                        <th scope="col" style="width: 200px;">
                                            License Type
                                        </th>
                                        <th scope="col" style="width: 400px;">
                                            File
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                        <th scope="col" style="width: 500px;">
                                            License Serial / Key
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div id="divContainer1" class="divContainer" style="height: 150px; width: 819px;"
                            onscroll="scrollDivHeader(1);">
                            <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="1320px">
                                <Columns>
                                    <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="SoftwareLicenseTypeName" ItemStyle-Width="200px" />
                                    <asp:BoundField DataField="FileInfo" ItemStyle-Width="400px" />
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDownloadLicenseFile" runat="server" OnClick="ibtnDownloadLicenseFile_Click" ImageUrl="~/Images/Download.png"
                                                ToolTip="Download File" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SerialKey" ItemStyle-Width="500px" />
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEditLicense" runat="server" OnClick="ibtnEditLicense_Click"
                                                ImageUrl="~/Images/Edit.png" ImageAlign="Middle" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDeleteLicense" runat="server" OnClientClick="showConfirm(this); return false;"
                                                OnClick="ibtnDeleteLicense_Click" ImageUrl="~/Images/Delete.png" ImageAlign="Middle"
                                                ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="ibtnAddLicense" runat="server" ImageUrl="~/Images/Add.png" ToolTip="Add License"
                            OnClick="ibtnAddLicense_Click" OnClientClick="disableButton(this)" />
                    </td>
                </tr>
                <tr id="trMaintenance" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblFrom" runat="server" Text="Maintenance From:"></asp:Label>
                    </td>
                    <td align="left">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtFrom" runat="server" Width="80px"></asp:TextBox>
                                    <act:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtFrom" Format="dd/MM/yyyy">
                                    </act:CalendarExtender>
                                </td>
                                <td style="width: 30px;" align="right">
                                    To:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtTo" runat="server" Width="80px"></asp:TextBox>
                                    <act:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtTo"
                                        Format="dd/MM/yyyy">
                                    </act:CalendarExtender>
                                </td>
                            </tr>
                        </table>
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
                        <asp:Label ID="lblMediaItem" runat="server" Text="Media Items:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblItems2CountPrefix" runat="server" Text="Media Items:"></asp:Label>
                        <asp:Label ID="lblItems2Count" runat="server" Font-Bold="true" ForeColor="#4fad4f"
                            Text="0"></asp:Label>
                        <div id="divHeader2" class="divHeader" style="width: 800px;">
                            <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="880px">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 100px;">
                                            Media Id
                                        </th>
                                        <th scope="col" style="width: 380px;">
                                            Media Name
                                        </th>
                                        <th scope="col" style="width: 150px;">
                                            Media Type
                                        </th>
                                        <th scope="col" style="width: 150px;">
                                            Media Location
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                        <th scope="col" style="width: 20px; background: none; cursor: default;">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div id="divContainer2" class="divContainer" style="height: 150px; width: 819px;"
                            onscroll="scrollDivHeader(2);">
                            <asp:GridView ID="gvItems2" runat="server" DataKeyNames="Id" Width="880px">
                                <Columns>
                                    <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="Name" ItemStyle-Width="380px" />
                                    <asp:BoundField DataField="MediaItemTypeName" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="MediaItemLocationName" ItemStyle-Width="150px" />
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEditMediaItem" runat="server" OnClick="ibtnEditMediaItem_Click"
                                                ImageUrl="~/Images/Edit.png" ToolTip="Edit" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDeleteMediaItem" runat="server" OnClientClick="showConfirm(this); return false;"
                                                OnClick="ibtnDeleteMediaItem_Click" ImageUrl="~/Images/Delete.png" ToolTip="Delete"
                                                ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="ibtnAddMediaItem" runat="server" ImageUrl="~/Images/Add.png"
                            ToolTip="Add Media Item" OnClick="ibtnAddMediaItem_Click" OnClientClick="disableButton(this)" />
                    </td>
                </tr>
                <tr class="separator">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOrderForm" runat="server" Text="Order:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOrderForm" runat="server" Width="200px" AutoPostBack="true"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceOrderForm" TargetControlID="txtOrderForm"
                            ServiceMethod="GetOrderFormCompletionList" MinimumPrefixLength="3" CompletionInterval="1000"
                            EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="ibtnAddOrderFrom" runat="server" ImageUrl="~/Images/Add.png"
                            ToolTip="Add Order Form" OnClick="ibtnAddOrderForm_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOlafRef" runat="server" Text="OLAF Ref:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOlafRef" runat="server"></asp:TextBox>
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
                        <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtComment" runat="server" Height="50px" Width="400px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblId" runat="server" Text="Id:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblIdValue" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvItems1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
