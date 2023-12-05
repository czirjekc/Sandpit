<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="GroupAccess.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.GroupAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Group Access
            </div>
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblGroup" runat="server" Text="Group:"></asp:Label>
                    </td>
                    <td align="left">
                        <act:ComboBox ID="cbGroup" runat="server" DataTextField="Name" DataValueField="UserID"
                            AutoPostBack="true" OnSelectedIndexChanged="cbGroup_SelectedIndexChanged">
                        </act:ComboBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblDenied" runat="server" Text="Not Accessible Pages:"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblGranted" runat="server" Text="Accessible Pages:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lbDenied" runat="server" DataTextField="FullName" DataValueField="ID"
                            OnPreRender="lbDenied_PreRender" Width="500px" Rows="20" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnGrant" runat="server" ImageUrl="~/Images/ArrowRightGray.png"
                            OnClick="ibtnGrant_Click" Enabled="false" />
                        <br />
                        <asp:ImageButton ID="ibtnDeny" runat="server" ImageUrl="~/Images/ArrowLeftGray.png"
                            OnClick="ibtnDeny_Click" Enabled="false" />
                    </td>
                    <td>
                        <asp:ListBox ID="lbGranted" runat="server" DataTextField="FullName" DataValueField="ID"
                            OnPreRender="lbGranted_PreRender" Width="500px" Rows="20" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>                        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
