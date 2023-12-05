<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditDocument.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Document Details
    </div>
    <table>
        <tr>
            <td align="right">
                <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
            </td>
        </tr>        
        <tr class="separator">
            <td colspan="2">
            </td>
        </tr>        
        <tr>
            <td align="right">
                <asp:Label ID="lblDocument" runat="server" Text="To Upload:"></asp:Label>
            </td>
            <td align="left">
                <asp:FileUpload ID="fuDocument" runat="server" Width="400px" />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"/>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblUploaded" runat="server" Text="File:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblUploadedInfo" runat="server" Text="" ForeColor="#000000"></asp:Label>
            </td>
        </tr>
        <tr class="separator">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblPath" runat="server" Text="Path:"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtPath" runat="server" Width="600px"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
