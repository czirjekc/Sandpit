<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="OpenSessions.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.OpenSessions"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Open Sessions
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Open Sessions:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="850px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 200px;">
                                SessionID
                            </th>
                            <th scope="col" style="width: 100px;">
                                User Login
                            </th>
                            <th scope="col" style="width: 300px;">
                                User
                            </th>
                            <th scope="col" style="width: 200px;">
                                Start Date
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 500px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Id" Width="850px">
                    <Columns>
                        <asp:BoundField DataField="SessionID" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="UserLogin" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="UserFullName" ItemStyle-Width="300px" />
                        <asp:BoundField DataField="DateStart" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                            ItemStyle-Width="200px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
