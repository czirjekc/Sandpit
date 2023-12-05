<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Membership.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.Membership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                User Membership
            </div>            
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblUser" runat="server" Text="User:"></asp:Label>
                    </td>
                    <td align="left">
                        <act:ComboBox ID="cbUser" runat="server" DataTextField="FullName" DataValueField="Id"
                            AutoPostBack="true" OnSelectedIndexChanged="cbUser_SelectedIndexChanged">
                        </act:ComboBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblDenied" runat="server" Text="Not member of:"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblGranted" runat="server" Text="Member of:"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblOther" runat="server" Text="Other members:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lbDenied" runat="server" DataTextField="Name" DataValueField="Id"
                            OnPreRender="lbDenied_PreRender" Rows="20" SelectionMode="Multiple" OnSelectedIndexChanged="lbDenied_SelectedIndexChanged"
                            AutoPostBack="true"></asp:ListBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnGrant" runat="server" ImageUrl="~/Images/ArrowRightGray.png"
                            OnClick="ibtnGrant_Click" Enabled="false" />
                        <br />
                        <asp:ImageButton ID="ibtnDeny" runat="server" ImageUrl="~/Images/ArrowLeftGray.png"
                            OnClick="ibtnDeny_Click" Enabled="false" />
                    </td>
                    <td>
                        <asp:ListBox ID="lbGranted" runat="server" Rows="20" SelectionMode="Multiple" DataTextField="Name"
                            DataValueField="Id" OnPreRender="lbGranted_PreRender" OnSelectedIndexChanged="lbGranted_SelectedIndexChanged"
                            AutoPostBack="true"></asp:ListBox>
                    </td>
                    <td style="width: 25px;">
                    </td>
                    <td>
                        <asp:ListBox ID="lbOtherMembers" runat="server" Width="300px" Rows="20" SelectionMode="Multiple"
                            DataTextField="FullName" DataValueField="Id" OnPreRender="lbOtherMembers_PreRender">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
