<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.Main" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Licenses
    </div>
    <asp:UpdatePanel ID="upSearchType" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:RadioButtonList ID="rblSearchType" runat="server" RepeatDirection="Horizontal"
                OnSelectedIndexChanged="rblSearchType_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0" Selected="True">Search in Licenses</asp:ListItem>
                <asp:ListItem Value="1">Search in License Assignments</asp:ListItem>
                <asp:ListItem Value="2">Search in Detected Product Installations</asp:ListItem>
                <asp:ListItem Value="3">Search in Detected Product Uninstallations</asp:ListItem>
            </asp:RadioButtonList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="tblSearchLicenses" runat="server">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductName" runat="server" Text="Product Name:" ForeColor="#008800"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductVersion" runat="server" Text="Product Version:" ForeColor="#008800"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductVersion" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductCompany" runat="server" Text="Product Company:" ForeColor="#008800"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductCompany" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductStatus" runat="server" Text="Product Status:" ForeColor="#008800"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductSource" runat="server" Text="Product Source:" ForeColor="#008800"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductSource" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSerial" runat="server" Text="Serial / Key:" ForeColor="#133984"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSerial" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblWithoutUpgrade" runat="server" Text="Only:" ForeColor="#133984"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="cbWithoutUpgrade" runat="server" Text="Licenses without upgrade"
                                        ForeColor="#133984" Checked="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOneLicensePerProduct" runat="server" Text="Only:" ForeColor="#133984"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="cbOneLicensePerProduct" runat="server" Text="One license per product"
                                        ForeColor="#133984" Checked="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblType" runat="server" Text="License Type:" ForeColor="#133984"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblId" runat="server" Text="License Id Between:" ForeColor="#133984"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtIdFrom" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                            <td style="width: 36px; color: #133984;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtIdTo" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAvailability" runat="server" Text="Availability:" ForeColor="#133984"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlAvailability" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="[Free]" Selected="True">Free</asp:ListItem>
                                        <asp:ListItem Value="[Not Free]">Not Free (Fully Assigned)</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDateStart" runat="server" Text="Maintenance Start Date Between:"
                                        ForeColor="#7b3b9a"></asp:Label>
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
                                            <td style="width: 36px; color: #7b3b9a;" align="right">
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
                                    <asp:Label ID="lblDateEnd" runat="server" Text="Maintenance End Date Between:" ForeColor="#7b3b9a"></asp:Label>
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
                                            <td style="width: 36px; color: #7b3b9a;" align="right">
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
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOlafRef" runat="server" Text="OLAF Ref:" ForeColor="#7b3b9a"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOlafRef" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderType" runat="server" Text="Entry Type:" ForeColor="#7b3b9a"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlOrderType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderForm" runat="server" Text="Order:" ForeColor="#7b3b9a"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOrderForm" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSpecificContract" runat="server" Text="Specific Contract:" ForeColor="#7b3b9a"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSpecificContract" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblContractFramework" runat="server" Text="Contract Framework:" ForeColor="#7b3b9a"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtContractFramework" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderId" runat="server" Text="Entry Id Between:" ForeColor="#7b3b9a"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtOrderIdFrom" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                            <td style="width: 36px; color: #7b3b9a;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtOrderIdTo" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblSearchLicenseAssignments" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductNameA" runat="server" Text="Product Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductNameA" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductVersionA" runat="server" Text="Product Version:" ></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductVersionA" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Pending for Installation</asp:ListItem>
                                        <asp:ListItem>Installed</asp:ListItem>
                                        <asp:ListItem>Pending for Uninstallation</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUser" runat="server" Text="User:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblHardwareItem" runat="server" Text="Hardware Item:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHardwareItem" runat="server"></asp:TextBox>
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
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAssignmentDate" runat="server" Text="Assignment Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAssignmentDateFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtAssignmentDateFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtAssignmentDateFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtAssignmentDateTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtAssignmentDateTo_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtAssignmentDateTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblInstallationDate" runat="server" Text="Installation Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtInstallationDateFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtInstallationDateFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtInstallationDateFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtInstallationDateTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtInstallationDateTo_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtInstallationDateTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUnassignmentDate" runat="server" Text="Unassignment Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtUnassignmentDateFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtUnassignmentDateFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtUnassignmentDateFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtUnassignmentDateTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtUnassignmentDate_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtUnassignmentDateTo" Format="dd/MM/yyyy">
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
            <table id="tblSearchDetectedProductInstallations" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUserI" runat="server" Text="User:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserI" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblInventoryI" runat="server" Text="Hardware Inventory No:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInventoryI" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductNameI" runat="server" Text="Product Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductNameI" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductVersionI" runat="server" Text="Product Version:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductVersionI" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSourceI" runat="server" Text="Source:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSourceI" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>LANDesk9</asp:ListItem>
                                        <asp:ListItem>LANDeskCBIS</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDetectionDateI" runat="server" Text="Detection Date Between:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDetectionDateIFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDetectionDateIFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtDetectionDateIFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtDetectionDateITo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDetectionDateITo_CalendarExtender6" runat="server" Enabled="True"
                                                    TargetControlID="txtDetectionDateITo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAdditionalInfoI" runat="server" Text="Additional Info:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAdditionalInfoI" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblSearchDetectedProductUninstallations" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUserU" runat="server" Text="User:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserU" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblInventoryU" runat="server" Text="Hardware Inventory No:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInventoryU" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductNameU" runat="server" Text="Product Name:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductNameU" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProductVersionU" runat="server" Text="Product Version:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductVersionU" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSourceU" runat="server" Text="Source:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSourceU" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>LANDesk9</asp:ListItem>
                                        <asp:ListItem>LANDeskCBIS</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDetectionDateU" runat="server" Text="Detection Date:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDetectionDateUFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDetectionDateUFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtDetectionDateUFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtDetectionDateUTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtDetectionDateUTo_CalendarExtender8" runat="server" Enabled="True"
                                                    TargetControlID="txtDetectionDateUTo" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAdditionalInfoU" runat="server" Text="Additional Info:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAdditionalInfoU" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblInstallationDateU" runat="server" Text="Installation Date:"></asp:Label>
                                </td>
                                <td align="left">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtInstallationDateUFrom" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtInstallationDateUFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtInstallationDateUFrom" Format="dd/MM/yyyy">
                                                </act:CalendarExtender>
                                            </td>
                                            <td style="width: 36px;" align="right">
                                                And:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtInstallationDateUTo" runat="server" Width="80px"></asp:TextBox>
                                                <act:CalendarExtender ID="txtInstallationDateUTo_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtInstallationDateUTo" Format="dd/MM/yyyy">
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
            <asp:Label ID="lblResultsCountPrefix" runat="server" Text="Licenses:"></asp:Label>
            <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="3480px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 100px;">
                                Product Id
                            </th>
                            <th scope="col" style="width: 250px;">
                                Product Name
                            </th>
                            <th scope="col" style="width: 100px;">
                                Product Version
                            </th>
                            <th scope="col" style="width: 150px;">
                                Product Company
                            </th>
                            <th scope="col" style="width: 200px;">
                                Product Status
                            </th>
                            <th scope="col" style="width: 100px;">
                                Product Source
                            </th>
                            <th scope="col" style="width: 100px;">
                                License Id
                            </th>
                            <th scope="col" style="width: 100px;">
                                Assigned
                            </th>
                            <th scope="col" style="width: 200px;">
                                License Type
                            </th>
                            <th scope="col" style="width: 100px;">
                                Multi User Quantity
                            </th>
                            <th scope="col" style="width: 500px;">
                                Serial / Key
                            </th>
                            <th scope="col" style="width: 400px;">
                                File
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 100px;">
                                Entry Id
                            </th>
                            <th scope="col" style="width: 100px;">
                                OLAF Ref
                            </th>
                            <th scope="col" style="width: 200px;">
                                Entry Type
                            </th>
                            <th scope="col" style="width: 100px;">
                                Previous Entry Id
                            </th>
                            <th scope="col" style="width: 100px;">
                                Maintenance Start Date
                            </th>
                            <th scope="col" style="width: 100px;">
                                Maintenance End Date
                            </th>
                            <th scope="col" style="width: 100px;">
                                Order
                            </th>
                            <th scope="col" style="width: 100px;">
                                Specific Contract
                            </th>
                            <th scope="col" style="width: 100px;">
                                Contract Framework
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 250px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="3480px" OnRowCreated="gvItems1_RowCreated" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToProduct" runat="server" OnClick="ibtnGoToProduct_Click"
                                    ImageUrl="~/Images/P_Green.png" ImageAlign="Middle" ToolTip="Go To Product" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnGoToEntry" runat="server" OnClick="ibtnGoToEntry_Click"
                                    ImageUrl="~/Images/E_Purple.png" ImageAlign="Middle" ToolTip="Go To Entry" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SoftwareProductId" ItemStyle-ForeColor="#94b892" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareProductName" ItemStyle-ForeColor="#008800" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="SoftwareProductVersion" ItemStyle-ForeColor="#008800"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareProductCompanyName" ItemStyle-ForeColor="#008800"
                            ItemStyle-Width="150px" />
                        <asp:BoundField DataField="SoftwareProductStatusName" ItemStyle-ForeColor="#008800"
                            ItemStyle-Width="200px" />
                        <asp:BoundField DataField="SoftwareProductSource" ItemStyle-ForeColor="#008800" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Availability" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareLicenseTypeName" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="MultiUserQuantity" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SerialKey" ItemStyle-Width="500px" />
                        <asp:BoundField DataField="FileInfo" ItemStyle-Width="400px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDownload" runat="server" OnClick="ibtnDownload_Click" ImageUrl="~/Images/Download.png"
                                    ToolTip="Download File" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SoftwareOrderItemId" ItemStyle-ForeColor="#d6bde2" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareOrderItemOlafRef" ItemStyle-ForeColor="#7b3b9a"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareOrderItemTypeName" ItemStyle-ForeColor="#7b3b9a"
                            ItemStyle-Width="200px" />
                        <asp:BoundField DataField="SoftwareOrderItemPreviousId" ItemStyle-ForeColor="#d6bde2"
                            ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareOrderItemMaintenanceStartDate" ItemStyle-ForeColor="#7b3b9a"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SoftwareOrderItemMaintenanceEndDate" ItemStyle-ForeColor="#7b3b9a"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="OrderFormName" ItemStyle-ForeColor="#7b3b9a" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="SpecificContractName" ItemStyle-ForeColor="#7b3b9a" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="ContractFrameworkName" ItemStyle-ForeColor="#7b3b9a" ItemStyle-Width="100px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditLicense" runat="server" OnClick="ibtnEditLicense_Click"
                                    ImageUrl="~/Images/Edit.png" ImageAlign="Middle" ToolTip="Edit License" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDelete" runat="server" OnClientClick="showConfirm(this); return false;"
                                    OnClick="ibtnDelete_Click" ImageUrl="~/Images/Delete.png" ToolTip="Delete License"
                                    ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>      
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems2CountPrefix" runat="server" Text="License Assignments:"></asp:Label>
            <asp:Label ID="lblItems2Count" runat="server" Font-Bold="true" ForeColor="#4fad4f"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems2" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems2_Click" />
            <asp:ImageButton ID="ibtnAddLicenseAssignment" runat="server" ImageUrl="~/Images/AddGray.png"
                ToolTip="Add" OnClick="ibtnAddSoftwareLicenseAssignment_Click" Enabled="false" />
            <div id="divHeader2" class="divHeader">
                <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="2070px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 100px;">
                                Id
                            </th>
                            <th scope="col" style="width: 150px;">
                                Status
                            </th>
                            <th scope="col" style="width: 300px;">
                                User
                            </th>
                            <th scope="col" style="width: 600px;">
                                Hardware Item
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
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                            <th scope="col" style="width: 20px; background: none; cursor: default;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer2" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(2);">
                <asp:GridView ID="gvItems2" runat="server" DataKeyNames="Id" Width="2070px">
                    <Columns>
                        <asp:BoundField DataField="Id" ItemStyle-ForeColor="#88bfeb" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Status" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="OlafUserConcatenation" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="HardwareItemConcatenation" ItemStyle-Width="600px" />
                        <asp:BoundField DataField="DateAssigned" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateInstalled" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DateUnassigned" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Comment" ItemStyle-Width="500px" />
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditSoftwareLicenseAssignment" runat="server" OnClick="ibtnEditSoftwareLicenseAssignment_Click"
                                    ImageUrl="~/Images/Edit.png" ToolTip="Edit" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="gvImageButton" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDeleteSoftwareLicenseAssignment" runat="server" OnClientClick="showConfirm(this); return false;"
                                    OnClick="ibtnDeleteSoftwareLicenseAssignment_Click" ImageUrl="~/Images/Delete.png"
                                    ToolTip="Delete" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems3CountPrefix" runat="server" Text="Detected Product Installations:"></asp:Label>
            <asp:Label ID="lblItems3Count" runat="server" Font-Bold="true" ForeColor="#9562ae"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems3" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems3_Click" />
            <div id="divHeader3" class="divHeader">
                <table id="tblStaticHeader3" class="tblStaticHeader" rules="all" width="1500px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 300px;">
                                User
                            </th>
                            <th scope="col" style="width: 150px;">
                                Hardware Inventory No
                            </th>
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
            <div id="divContainer3" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(3);">
                <asp:GridView ID="gvItems3" runat="server" Width="1500px">
                    <Columns>
                        <asp:BoundField DataField="HardwareItemFullName" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="HardwareItemInventoryNo" ItemStyle-Width="150px" />
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
    <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems4CountPrefix" runat="server" Text="Detected Product Uninstallations:"></asp:Label>
            <asp:Label ID="lblItems4Count" runat="server" Font-Bold="true" ForeColor="#d1982f"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems4" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems4_Click" />
            <div id="divHeader4" class="divHeader">
                <table id="tblStaticHeader4" class="tblStaticHeader" rules="all" width="1650px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 300px;">
                                User
                            </th>
                            <th scope="col" style="width: 150px;">
                                Hardware Inventory No
                            </th>
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
            <div id="divContainer4" class="divContainer" style="height: 150px;" onscroll="scrollDivHeader(4);">
                <asp:GridView ID="gvItems4" runat="server" Width="1650px">
                    <Columns>
                        <asp:BoundField DataField="HardwareItemFullName" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="HardwareItemInventoryNo" ItemStyle-Width="150px" />
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
