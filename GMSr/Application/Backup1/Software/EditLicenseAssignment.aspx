<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditLicenseAssignment.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditLicenseAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                License Assignment Details
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblLicense" runat="server" Text="License:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLicense" runat="server" Width="1000px" AutoPostBack="true"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceLicense" TargetControlID="txtLicense"
                            ServiceMethod="GetLicenseCompletionList" MinimumPrefixLength="3" CompletionInterval="1000"
                            EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblUser" runat="server" Text="User:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUser" runat="server" Width="400px" AutoPostBack="true"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceUser" TargetControlID="txtUser" ServiceMethod="GetUserCompletionList"
                            MinimumPrefixLength="3" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblHardwareItem" runat="server" Text="Hardware Item:"></asp:Label>
                    </td>
                    <td align="left">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtHardwareItem" runat="server" Width="800px" AutoPostBack="true"></asp:TextBox>
                                    <act:AutoCompleteExtender runat="server" ID="aceHardwareItem" TargetControlID="txtHardwareItem"
                                        ServiceMethod="GetHardwareItemCompletionList" MinimumPrefixLength="5" CompletionInterval="1000"
                                        EnableCaching="true" CompletionSetCount="20">
                                    </act:AutoCompleteExtender>
                                    <act:ComboBox ID="cbHardwareItem" runat="server" Width="800px" Visible="false">
                                    </act:ComboBox>
                                </td>
                                <td>
                                    &nbsp;*
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblAssigned" runat="server" Text="Assignment Date:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAssigned" runat="server" Width="80px"></asp:TextBox>
                        <act:CalendarExtender ID="txtAssigned_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtAssigned" Format="dd/MM/yyyy">
                        </act:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblInstalled" runat="server" Text="Installation Date:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtInstalled" runat="server" Width="80px"></asp:TextBox>
                        <act:CalendarExtender ID="txtInstalled_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtInstalled" Format="dd/MM/yyyy">
                        </act:CalendarExtender>
                    </td>
                </tr>
                <tr id="trUnassigned" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblUnassigned" runat="server" Text="Unassignment Date:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUnassigned" runat="server" Width="80px"></asp:TextBox>
                        <act:CalendarExtender ID="txtUnassigned_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtUnassigned" Format="dd/MM/yyyy">
                        </act:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtComment" runat="server" Height="50px" Width="400px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <div>
                *: If a textbox appears to be uneditable, then just right-click below this text
                inside the browser window. And try again.
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
