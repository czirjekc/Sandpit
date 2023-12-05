<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Users.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Reports.Users"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Users
    </div>   
    <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowUnknownUsers" runat="server" OnClick="lbShowUnknownUsers_Click">Show unknown users</asp:LinkButton>
            <asp:Panel ID="Panel4" runat="server" Visible="false">
                <asp:Label ID="lblItems4CountPrefix" runat="server" Text="Unknown hardware items:"></asp:Label>
                <asp:Label ID="lblItems4Count" runat="server" Font-Bold="true" ForeColor="#d1982f"></asp:Label>
                <div id="divHeader4" class="divHeader">
                    <table id="tblStaticHeader4" class="tblStaticHeader" rules="all" width="500px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 150px;">
                                    UserName
                                </th>
                                <th scope="col" style="width: 150px;">
                                    SourceName
                                </th>
                                <th scope="col" style="width: 150px;">
                                    FoundOn
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer4" class="divContainer" style="height: 400px;" onscroll="scrollDivHeader(4);">
                    <asp:GridView ID="gvItems4" runat="server" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="UserName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="SourceName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="FoundOn" ItemStyle-Width="150px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:ImageButton ID="btnExportItems4" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                ToolTip="Export to Excel" Visible="false" OnClick="btnExportItems4_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
        Visible="true">
        <ContentTemplate>
            <asp:LinkButton ID="lkbtnDiscrepancies" runat="server" OnClick="lkbtnDiscrepancies_Click">Show AD/HR group discrepancies</asp:LinkButton>
            <asp:Panel ID="Panel6" runat="server" Visible="false">
                <asp:Label ID="lblItems6CountPrefix" runat="server" Text="Discrepancies count"></asp:Label>
                <asp:Label ID="lblItems6Count" runat="server" Font-Bold="true" ForeColor="#ca8383"></asp:Label>
                <div id="divHeader6" class="divHeader">
                    <table id="tblStaticHeader6" class="tblStaticHeader" rules="all" width="1200px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 200px;">
                                    First name
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Last name
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Login
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Unit
                                </th>
                                <th scope="col" style="width: 200px;">
                                    BaseGroup
                                </th>
                                <th scope="col" style="width: 200px;">
                                    OtherGroups
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer6" class="divContainer" onscroll="scrollDivHeader(6);">
                    <asp:GridView ID="gvItems6" runat="server" Width="1200px" Height="300px">
                        <Columns>
                            <asp:BoundField DataField="FirstName" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="LastName" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Username" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Unit" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="BaseGroup" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="OtherGroups" ItemStyle-Width="200px" />
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:ImageButton ID="ibtnExportDiscrepancies" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportDiscrepancies_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal runat="server" ID="TestControl"></asp:Literal>
</asp:Content>
