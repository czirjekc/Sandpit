<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Software.Add" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Add
    </div>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblAddOrderType" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAddOrderType" runat="server" OnSelectedIndexChanged="ddlAddOrderType_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:ImageButton ID="ibtnAdd" runat="server" ImageUrl="~/Images/AddBigGray.png" Enabled="false"
                ToolTip="Add" OnClick="ibtnAdd_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
