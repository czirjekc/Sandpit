<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="MapHardware.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Hardware.MapHardware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="h1">
                Hardware Mapping
            </div>
            <table>
                <tr>
                    <td align="right">
                        Order:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOrder" runat="server" AutoPostBack="true" OnTextChanged="txtOrder_TextChanged"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceOrder" TargetControlID="txtOrder"
                            ServiceMethod="GetOrderCompletionList" MinimumPrefixLength="2" CompletionInterval="1000"
                            EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblLinked" runat="server" Text="Linked Hardware:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lbLinked" runat="server" Width="800px" Height="300px" SelectionMode="Multiple"
                            DataTextField="Id" DataValueField="Id" OnPreRender="lbLinked_PreRender"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="ibtnLink" runat="server" ImageUrl="~/Images/ArrowUpGray.png"
                            OnClick="ibtnLink_Click" Enabled="false" />
                        <asp:ImageButton ID="ibtnUnlink" runat="server" ImageUrl="~/Images/ArrowDownGray.png"
                            OnClick="ibtnUnlink_Click" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUnlinked" runat="server" Text="Unlinked Hardware:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lbUnlinked" runat="server" Width="800px" Height="300px" SelectionMode="Multiple"
                            DataTextField="Model" DataValueField="Id" OnPreRender="lbUnlinked_PreRender">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtUnlinked" runat="server" Width="800px" AutoPostBack="true" OnTextChanged="txtUnlinked_TextChanged"></asp:TextBox>
                        <act:AutoCompleteExtender runat="server" ID="aceUnlinked" TargetControlID="txtUnlinked"
                            ServiceMethod="GetUnlinkedCompletionList" MinimumPrefixLength="2" CompletionInterval="1000"
                            EnableCaching="true" CompletionSetCount="20">
                        </act:AutoCompleteExtender>
                    </td>
                </tr>
            </table>           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
