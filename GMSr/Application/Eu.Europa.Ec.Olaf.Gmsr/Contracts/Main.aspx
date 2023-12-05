<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Contracts.Main" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Main
    </div>
    <asp:UpdatePanel ID="upSearchType" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSearch_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Value="0" Selected="True">Search Framework Contracts</asp:ListItem>
                <asp:ListItem Value="1">Search Amendments</asp:ListItem>
                <asp:ListItem Value="2">Search Specific Contracts </asp:ListItem>
                <asp:ListItem Value="3">Search Order Forms</asp:ListItem>
            </asp:RadioButtonList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tblSearchFrameworkContracts" runat="server">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFrameworkName" runat="server" Text="Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFrameworkName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFrameworkDateBegin" runat="server" Text="Date Begin Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFrameworkDateBeginFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFrameworkDateBeginFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtFrameworkDateBeginTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtFrameworkDateBeginTo"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFrameworkDateEnd" runat="server" Text="Date End Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFrameworkDateEndFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtFrameworkDateEndFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtFrameworkDateEndTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtFrameworkDateEndTo"
                                                    Format="dd/MM/yyyy">
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
            <table id="tblSearchAmendments" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAmendmentName" runat="server" Text="Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAmendmentName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAmendmentDateBegin" runat="server" Text="Date Begin Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAmendmentDateBeginFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" TargetControlID="txtAmendmentDateBeginFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtAmendmentDateBeginTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" TargetControlID="txtAmendmentDateBeginTo"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAmendmentDateEnd" runat="server" Text="Date End Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAmendmentDateEndFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" TargetControlID="txtAmendmentDateEndFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtAmendmentDateEndTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" TargetControlID="txtAmendmentDateEndTo"
                                                    Format="dd/MM/yyyy">
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
            <table id="tblSearchSpecificContracts" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSpecificName" runat="server" Text="Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSpecificName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSpecificDateBegin" runat="server" Text="Date Begin Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSpecificDateBeginFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" TargetControlID="txtSpecificDateBeginFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtSpecificDateBeginTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="True" TargetControlID="txtSpecificDateBeginTo"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSpecificDateEnd" runat="server" Text="Date End Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSpecificDateEndFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender11" runat="server" Enabled="True" TargetControlID="txtSpecificDateEndFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtSpecificDateEndTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender12" runat="server" Enabled="True" TargetControlID="txtSpecificDateEndTo"
                                                    Format="dd/MM/yyyy">
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
            <table id="tblSearchOrders" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderFormName" runat="server" Text="Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOrderFormName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderFormDateBegin" runat="server" Text="Date Begin Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtOrderFormDateBeginFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender13" runat="server" Enabled="True" TargetControlID="txtOrderFormDateBeginFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtOrderFormDateBeginTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender14" runat="server" Enabled="True" TargetControlID="txtOrderFormDateBeginTo"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderFormDateEnd" runat="server" Text="Date End Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtOrderFormDateEndFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender15" runat="server" Enabled="True" TargetControlID="txtOrderFormDateEndFrom"
                                                    Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtOrderFormDateEndTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="CalendarExtender16" runat="server" Enabled="True" TargetControlID="txtOrderFormDateEndTo"
                                                    Format="dd/MM/yyyy">
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
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Framework Contracts:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="2420px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date Begin
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date End
                            </th>
                            <th scope="col" style="width: 100px;">
                                Prices
                            </th>
                            <th scope="col" style="width: 300px;">
                                Title
                            </th>
                            <th scope="col" style="width: 400px;">
                                Object
                            </th>
                            <th scope="col" style="width: 100px;">
                                Duration
                            </th>
                            <th scope="col" style="width: 100px;">
                                Is SLA
                            </th>
                            <th scope="col" style="width: 100px;">
                                Is Intra Muros
                            </th>
                            <th scope="col" style="width: 250px;">
                                CE Contact
                            </th>
                            <th scope="col" style="width: 250px;">
                                Company Contact
                            </th>
                            <th scope="col" style="width: 250px;">
                                Olaf Contact
                            </th>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="2420px">
                    <Columns>
                        <asp:BoundField DataField="Name" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateBegin" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateEnd" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Prices" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Title" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="Object" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="Duration" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="IsSLA" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="IsIntraMuros" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="CE_Contact" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="CompanyContact" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="OlafContact" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Images/Edit.png" ImageAlign="Middle"
                                    ToolTip="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblItems2CountPrefix" runat="server" Text="Amendements:"></asp:Label>
            <asp:Label ID="lblItems2Count" runat="server" Font-Bold="true" ForeColor="#4fad4f" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems2" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems2_Click" />
            <div id="divHeader2" class="divHeader">
                <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="1470px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date Begin
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date End
                            </th>
                            <th scope="col" style="width: 100px;">
                                Prices
                            </th>
                            <th scope="col" style="width: 300px;">
                                Title
                            </th>
                            <th scope="col" style="width: 400px;">
                                Object
                            </th>
                            <th scope="col" style="width: 100px;">
                                Duration
                            </th>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer2" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(2);">
                <asp:GridView ID="gvItems2" runat="server" DataKeyNames="Id" Width="1470px">
                    <Columns>
                        <asp:BoundField DataField="Name" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateBegin" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateEnd" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Prices" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Title" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="Object" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="Duration" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Images/Edit.png" ImageAlign="Middle"
                                    ToolTip="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblItems3CountPrefix" runat="server" Text="Specific Contracts:"></asp:Label>
            <asp:Label ID="lblItems3Count" runat="server" Font-Bold="true" ForeColor="#9562ae" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems3" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems3_Click" />
            <div id="divHeader3" class="divHeader">
                <table id="tblStaticHeader3" class="tblStaticHeader" rules="all" width="2770px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date Begin
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date End
                            </th>
                            <th scope="col" style="width: 400px;">
                                Object
                            </th>
                            <th scope="col" style="width: 100px;">
                                Duration
                            </th>
                            <th scope="col" style="width: 100px;">
                                Acceptance Type
                            </th>
                            <th scope="col" style="width: 100px;">
                                Acceptance Delay
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price Total
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price1 Def
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price1
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price2 Def
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price2
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price3 Def
                            </th>
                            <th scope="col" style="width: 100px;">
                                Price3
                            </th>
                            <th scope="col" style="width: 100px;">
                                Invoicing Period
                            </th>
                            <th scope="col" style="width: 100px;">
                                Is Penalties
                            </th>
                            <th scope="col" style="width: 100px;">
                                Garantees
                            </th>
                            <th scope="col" style="width: 100px;">
                                Patch
                            </th>
                            <th scope="col" style="width: 400px;">
                                Comment
                            </th>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer3" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(3);">
                <asp:GridView ID="gvItems3" runat="server" DataKeyNames="Id" Width="2770px">
                    <Columns>
                        <asp:BoundField DataField="Name" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateBegin" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateEnd" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Object" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="Duration" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="AcceptanceType" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="AcceptanceDelay" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="PriceTotal" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Price1Def" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Price1" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Price2Def" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Price2" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Price3Def" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Price3" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="InvoicingPeriod" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="IsPenalties" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Garantees" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SpecificContractPatch" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Comment" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Images/Edit.png" ImageAlign="Middle"
                                    ToolTip="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblItems4CountPrefix" runat="server" Text="Order Forms:"></asp:Label>
            <asp:Label ID="lblItems4Count" runat="server" Font-Bold="true" ForeColor="#d1982f" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems4" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems4_Click" />
            <div id="divHeader4" class="divHeader">
                <table id="tblStaticHeader4" class="tblStaticHeader" rules="all" width="1170px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Name
                            </th>
                            <th scope="col" style="width: 300px;">
                                Title
                            </th>
                            <th scope="col" style="width: 100px;">
                                Scope
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date Begin
                            </th>
                            <th scope="col" style="width: 100px;">
                                Date End
                            </th>
                            <th scope="col" style="width: 100px;">
                                Duration
                            </th>
                            <th scope="col" style="width: 100px;">
                                Service Type
                            </th>
                            <th scope="col" style="width: 100px;">
                                Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer4" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(4);">
                <asp:GridView ID="gvItems4" runat="server" DataKeyNames="Id" Width="1170px">
                    <Columns>
                        <asp:BoundField DataField="Name" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Title" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="Scope" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateBegin" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateEnd" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Duration" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="ServiceType" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Images/Edit.png" ImageAlign="Middle"
                                    ToolTip="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
