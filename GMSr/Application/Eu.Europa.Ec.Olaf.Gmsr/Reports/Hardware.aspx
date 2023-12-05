<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Hardware.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Reports.Hardware"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Hardware
    </div>    
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowUnknownHardware" runat="server" OnClick="lbShowUnknownHardware_Click">Show unknown hardware</asp:LinkButton>
            <asp:Panel ID="Panel3" runat="server" Visible="false">
                <asp:Label ID="lblItems3CountPrefix" runat="server" Text="Unknown hardware items:"></asp:Label>
                <asp:Label ID="lblItems3Count" runat="server" Font-Bold="true" ForeColor="#9562ae"></asp:Label>
                <div id="divHeader3" class="divHeader" style="width: 400px;">
                    <table id="tblStaticHeader3" class="tblStaticHeader" rules="all" width="500px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 150px;">
                                    ComputerName
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Source
                                </th>
                                <th scope="col" style="width: 150px;">
                                    FoundOn
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer3" class="divContainer" style="height: 400px; width: 419px;"
                    onscroll="scrollDivHeader(3);">
                    <asp:GridView ID="gvItems3" runat="server" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="ComputerName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Source" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="FoundOn" ItemStyle-Width="150px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:ImageButton ID="btnExportItems3" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                ToolTip="Export to Excel" Visible="false" OnClick="btnExportItems3_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowSmartphoneOwnerships" runat="server" OnClick="lbShowSmartphoneOwnerships_Click">Show smartphone ownerships</asp:LinkButton>
            <asp:Panel ID="Panel5" runat="server" Visible="false">
                <asp:Label ID="lblItems5CountPrefix" runat="server" Text="PRTG's LUN space items:"></asp:Label>
                <asp:Label ID="lblItems5Count" runat="server" Font-Bold="true" ForeColor="#6189af"></asp:Label>
                <div id="divHeader5" class="divHeader">
                    <table id="tblStaticHeader5" class="tblStaticHeader" rules="all" width="1100px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 200px;">
                                    Unit
                                </th>
                                <th scope="col" style="width: 200px;">
                                    User
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Model
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Label
                                </th>
                                <th scope="col" style="width: 200px;">
                                    Delivery year
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer5" class="divContainer" onscroll="scrollDivHeader(5);">
                    <asp:GridView ID="gvItems5" runat="server" Width="1100px">
                        <Columns>
                            <asp:BoundField DataField="Unit" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="User" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Model" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Label" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="DeliveryYear" ItemStyle-Width="200px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:ImageButton ID="btnExportItems5" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                ToolTip="Export to Excel" Visible="false" OnClick="btnExportItems5_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:HyperLink NavigateUrl="N:\DIRD\COMMON\COMMON\GSM" runat="server">GSM ownerships</asp:HyperLink>
    <br />
    <asp:Literal runat="server" ID="TestControl"></asp:Literal>
</asp:Content>
