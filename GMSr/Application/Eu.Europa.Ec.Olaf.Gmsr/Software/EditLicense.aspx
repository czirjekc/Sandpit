<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditLicense.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.EditLicense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                <asp:Label ID="lblTitle" runat="server" Text="License Details"></asp:Label>
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblId" runat="server" Text="Id:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblIdValue" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trType" runat="server">
                    <td align="right">
                        <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trQuantity" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblQuantity" runat="server" Text="Multi User Quantity:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtQuantity" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblSerial" runat="server" Text="Serial No:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSerial" runat="server" Width="400px"></asp:TextBox>
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
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
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
                        <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtComment" runat="server" Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tMultiple" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:CheckBox ID="cbMultiple" runat="server" Text="Add multiple licenses with these details."
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr id="trAmount" runat="server" visible="false">
                    <td>
                        Licenses amount:<asp:TextBox ID="txtAmount" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
