<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                User Details
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblSurname" runat="server" Text="Surname:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
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
                <tr>
                    <td align="right">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="cbActive" runat="server" Text="Active" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
