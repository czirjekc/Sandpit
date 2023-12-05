<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserAccess.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.UserAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                User Access
            </div>            
            <table>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblUser" runat="server" Text="User:"></asp:Label>
                    </td>
                    <td align="left">
                        <act:ComboBox ID="cbUser" runat="server" DataTextField="Name" DataValueField="UserID"
                            AutoPostBack="true" OnSelectedIndexChanged="cbUser_SelectedIndexChanged">
                        </act:ComboBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDenied" runat="server" Text="Not Accessible Pages:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbDenied" runat="server" DataTextField="FullName" DataValueField="ID"
                                        OnPreRender="lbDenied_PreRender" Width="500px" Rows="22" SelectionMode="Multiple">
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtnGrant" runat="server" ImageUrl="~/Images/ArrowRightGray.png"
                            OnClick="ibtnGrant_Click" Enabled="false" />
                        <br />
                        <asp:ImageButton ID="ibtnDeny" runat="server" ImageUrl="~/Images/ArrowLeftGray.png"
                            OnClick="ibtnDeny_Click" Enabled="false" />
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGranted" runat="server" Text="User specific Accessible Pages:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbGranted" runat="server" Width="500px" Rows="10" SelectionMode="Multiple"
                                        DataTextField="FullName" DataValueField="ID" OnPreRender="lbGranted_PreRender">
                                    </asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblInherited" runat="server" Text="Inherited Accessible Pages:"></asp:Label><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbInherited" runat="server" Width="500px" Rows="10" SelectionMode="Multiple"
                                        DataTextField="FullName" DataValueField="ID" OnPreRender="lbInherited_PreRender">
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>                        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
