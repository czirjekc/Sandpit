<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditProduct.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Product Details
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
                        <asp:Label ID="lblVersion" runat="server" Text="Version:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblCompany" runat="server" Text="Company:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOther" runat="server" Text="Other Info:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOther" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                    </td>
                    <td align="left">                        
                        <asp:DropDownList ID="ddlStatus" runat="server">
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
                <tr class="separator">
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDetectedProduct" runat="server" Text="Detected Product:"></asp:Label>
                    </td>                    
                    <td align="left">
                        <asp:TextBox ID="txtDetectedProduct" runat="server" Width="800px" AutoPostBack="true"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceDetectedProduct" TargetControlID="txtDetectedProduct" ServiceMethod="GetDetectedProductCompletionList"
                            MinimumPrefixLength="3" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
