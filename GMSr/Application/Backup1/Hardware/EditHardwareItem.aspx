<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditHardwareItem.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Hardware.EditHardwareItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Hardware Item Details
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOlafName" runat="server" Text="Olaf Name:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOlafName" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tblExtra" runat="server" visible="false">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblInventoryNo" runat="server" Text="Inventory No:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtInventoryNo" runat="server"></asp:TextBox>
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
                        <asp:Label ID="lblSerial" runat="server" Text="Serial:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSerial" runat="server"></asp:TextBox>
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
                <tr>
                    <td align="right">
                        <asp:Label ID="lblLogin" runat="server" Text="Login:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
