<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditMediaItem.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditMediaItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Media Item Details
            </div>
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
                        <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td align="left">                        
                        <asp:DropDownList ID="ddlType" runat="server"  AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                    </td>
                    <td align="left">                        
                        <asp:DropDownList ID="ddlLocation" runat="server"  AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtComment" runat="server" Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
