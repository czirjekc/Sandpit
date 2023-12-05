<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Hardware.Main" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
<style type="text/css">
		.tooltip {
			
			text-decoration: none;
			position: relative;
		}
		.tooltip span {
			margin-left: -999em;
			position: absolute;
			text-align:left;
			color:Black;
		}
		.tooltip:hover span {
			border-radius: 5px 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; 
			box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.1); -webkit-box-shadow: 5px 5px rgba(0, 0, 0, 0.1); -moz-box-shadow: 5px 5px rgba(0, 0, 0, 0.1);
			font-family: Calibri, Tahoma, Geneva, sans-serif;
			position: absolute; left: 1em; top: 2em; z-index: 99;
			margin-left: 0; width: 250px;
		}
		.tooltip:hover img {
			border: 0; margin: -10px 0 0 -55px;
			float: left; position: absolute;
		}
		.tooltip:hover em {
			font-family: Candara, Tahoma, Geneva, sans-serif; font-size: 1.2em; font-weight: bold;
			display: block; padding: 0.2em 0 0.6em 0;
		}
		.classic { padding: 0.8em 1em; }
		.custom { padding: 0.5em 0.8em 0.8em 2em; }
		* html a:hover { background: transparent; }
		.classic {background: #FFFFAA; border: 1px solid #FFAD33; }
		.critical { background: #FFCCAA; border: 1px solid #FF3334;	}
		.help { background: #9FDAEE; border: 1px solid #2BB0D7;	}
		.info { background: #9FDAEE; border: 1px solid #2BB0D7;	}
		.warning { background: #FFFFAA; border: 1px solid #FFAD33; }
		</style>
    <div class="h1">
        Main
    </div>
    <asp:UpdatePanel ID="upSearchType" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:RadioButtonList ID="rblSearchType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSearchType_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Value="0" Selected="True">Search in Hardware Items</asp:ListItem>
                <asp:ListItem Value="1">Search in Hardware Item Modifications</asp:ListItem>
            </asp:RadioButtonList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="tblSearchHardwareItems" runat="server">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="'A': Active. 'D': Decommissioned.">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Selected="True">A</asp:ListItem>
                                        <asp:ListItem>D</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLocal" runat="server" Text="Local:"></asp:Label>
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
                                <%--<asp:Label ID="lblInventoryNo" runat="server" Text="Inventory No:"></asp:Label>--%>
                                    <asp:Label ID="lblInventoryNo" runat="server" Text="Inventory No:" CssClass="tooltip">Inventory No: <span id="Span1" class="classic" runat="server">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.<br /> Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</span></asp:Label>
                                    <%--<a class="tooltip" href="#" runat="server">Inventory No:<span class="classic" runat="server">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</span></a>--%>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInventoryNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOlafName" runat="server" Text="OlafName:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOlafName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblModel" runat="server" Text="Model:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                    <td>
                        <table>
                        <tr>
                                <td align="right">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLastName" runat="server" Text="LastName:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFirstName" runat="server" Text="FirstName:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
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
                            
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSerial" runat="server" Text="Serial:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSerial" runat="server"></asp:TextBox>
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
                                    <asp:Label ID="lblRmtCtgNm" runat="server" Text="RmtCtgNm:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRmtCtgNm" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRmtParentCtgNm" runat="server" Text="RmtParentCtgNm:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRmtParentCtgNm" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMaintenanceEndDate" runat="server" Text="Mnt Date End Between:"
                                        ToolTip="Mnt: Maintenance"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtMaintenanceEndDateFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtMaintenanceEndDateFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtMaintenanceEndDateFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtMaintenanceEndDateTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtMaintenanceEndDateTo_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtMaintenanceEndDateTo" Format="dd/MM/yyyy">
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
            <table id="tblSearchHardwareItemModifications" runat="server" visible="false">
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
                                    <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
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
            <asp:Label ID="lblResultsCountPrefix" runat="server" Text="Hardware Items:"></asp:Label>
            <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="#557dc3" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="3140px">
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
                            <th scope="col" style="width: 500px;">
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
                                Mnt Date End
                            </th>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 100px;">
                                RmtMatId
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 250px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="3140px" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Status" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Local" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="InventoryNo" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="OlafName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Model" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="Description" ItemStyle-Width="500px" />
                        <asp:BoundField DataField="LastName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="FirstName" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Phone" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Building" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Office" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Serial" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="Unit" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="RmtCtgNm" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="RmtParentCtgNm" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="MaintenanceEndDate" DataFormatString="{0:dd/MM/yyyy}"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="RmtMatId" ItemStyle-Width="100px" ItemStyle-ForeColor="#88bfeb"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" OnClick="ibtnEditHardwareItem_Click"
                                    ImageUrl="~/Images/Edit.png" ImageAlign="Middle" ToolTip="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblHistoryResultsCountPrefix" runat="server" Text="Hardware Item Modifications:"></asp:Label>
            <asp:Label ID="lblHistoryResultsCount" runat="server" Font-Bold="true" ForeColor="#4fad4f" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems2" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems2_Click" />
            <div id="divHeader2" class="divHeader">
                <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="1100px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Date
                            </th>
                            <th scope="col" style="width: 150px;">
                                Inventory No
                            </th>
                            <th scope="col" style="width: 500px;">
                                Description
                            </th>
                            <th scope="col" style="width: 100px;">
                                New User
                            </th>
                            <th scope="col" style="width: 100px;">
                                Old User
                            </th>
                            <th scope="col" style="width: 100px;">
                                Type
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer2" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(2);">
                <asp:GridView ID="gvItems2" runat="server" DataKeyNames="Id" Width="1100px">
                    <Columns>
                        <asp:BoundField DataField="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="InventoryNo" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Description" ItemStyle-Width="500px" />
                        <asp:BoundField DataField="NewUser" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="OldUser" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Type" ItemStyle-Width="100px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems3CountPrefix" runat="server" Text="Software License Assignments:"></asp:Label>
            <asp:Label ID="lblItems3Count" runat="server" Font-Bold="true" ForeColor="#9562ae" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems3" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems3_Click" />
            <asp:ImageButton ID="ibtnAddLicenseAssignment" runat="server" ImageUrl="~/Images/AddGray.png"
                ToolTip="Add" OnClick="ibtnAddSoftwareLicenseAssignment_Click" Enabled="false" />
            <div id="divHeader3" class="divHeader">
                <table id="tblStaticHeader3" class="tblStaticHeader" rules="all" width="1600px">
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
            <div id="divContainer3" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(3);">
                <asp:GridView ID="gvItems3" runat="server" DataKeyNames="Id" Width="1600px">
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
                                    OnClick="ibtnDeleteSoftwareLicenseAssignment_Click" ImageUrl="~/Images/Delete.png"
                                    ToolTip="Delete" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems4CountPrefix" runat="server" Text="Detected Software Installations:"></asp:Label>
            <asp:Label ID="lblItems4Count" runat="server" Font-Bold="true" ForeColor="#d1982f" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems4" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems4_Click" />
            <div id="divHeader4" class="divHeader">
                <table id="tblStaticHeader4" class="tblStaticHeader" rules="all" width="1050px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 400px;">
                                Product Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Prod. Version
                            </th>
                            <th scope="col" style="width: 100px;">
                                Source
                            </th>
                            <th scope="col" style="width: 100px;">
                                Detection Date
                            </th>
                            <th scope="col" style="width: 300px;">
                                Additional Info
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer4" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(4);">
                <asp:GridView ID="gvItems4" runat="server" Width="1050px">
                    <Columns>
                        <asp:BoundField DataField="DetectedSoftwareProductName" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="DetectedSoftwareProductVersion" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Source" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DetectionDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="AdditionalInfo" ItemStyle-Width="300px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems5CountPrefix" runat="server" Text="Detected Software Uninstallations:"></asp:Label>
            <asp:Label ID="lblItems5Count" runat="server" Font-Bold="true" ForeColor="#85c62a" Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems5" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems5_Click" />
            <div id="divHeader5" class="divHeader">
                <table id="tblStaticHeader5" class="tblStaticHeader" rules="all" width="1200px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 400px;">
                                Product Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Prod. Version
                            </th>
                            <th scope="col" style="width: 100px;">
                                Source
                            </th>
                            <th scope="col" style="width: 100px;">
                                Detection Date
                            </th>
                            <th scope="col" style="width: 300px;">
                                Additional Info
                            </th>
                            <th scope="col" style="width: 150px;">
                                Installation Date
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer5" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(5);">
                <asp:GridView ID="gvItems5" runat="server" Width="1200px">
                    <Columns>
                        <asp:BoundField DataField="DetectedSoftwareProductName" ItemStyle-Width="400px" />
                        <asp:BoundField DataField="DetectedSoftwareProductVersion" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Source" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DetectionDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="AdditionalInfo" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="InstallationDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="150px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
