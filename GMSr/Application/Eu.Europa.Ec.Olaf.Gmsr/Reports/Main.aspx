<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Main.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Reports.Main"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Reports
    </div>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowOpenGroups" runat="server" OnClick="lbShowOpenGroups_Click">Show open action groups</asp:LinkButton>
            <asp:Panel ID="Panel1" runat="server" Visible="false">
                <br />
                <asp:DropDownList ID="ddlOpenGroups" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOpenGroups_SelectedIndexChanged">
                    <asp:ListItem>
                Select group
                    </asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Open action groups:"></asp:Label>
                <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"></asp:Label>
                <div id="divHeader1" class="divHeader">
                    <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="2200px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 60px;">
                                    Code
                                </th>
                                <th scope="col" style="width: 40px;">
                                    State
                                </th>
                                <th scope="col" style="width: 550px;">
                                    Description
                                </th>
                                <th scope="col" style="width: 550px;">
                                    Report
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer1" class="divContainer" style="height: 400px;" onscroll="scrollDivHeader(1);">
                    <asp:GridView ID="gvItems1" runat="server" Width="2200px">
                        <Columns>
                            <asp:BoundField DataField="Code" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="State" ItemStyle-Width="40px" />
                            <asp:BoundField DataField="Description" ItemStyle-Width="550px" />
                            <asp:BoundField DataField="Report" ItemStyle-Width="550px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Button ID="btnExportItems1" runat="server" Text="Export to Excel" Width="110px"
                Visible="false" OnClick="btnExportItems1_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowLUNSpace" runat="server" OnClick="lbShowLUNSpace_Click">Show PRTG's LUN space</asp:LinkButton>
            <asp:Panel ID="Panel2" runat="server" Visible="false">
                <asp:Label ID="lblItems2CountPrefix" runat="server" Text="PRTG's LUN space items:"></asp:Label>
                <asp:Label ID="lblItems2Count" runat="server" Font-Bold="true" ForeColor="#4fad4f"></asp:Label>
                <div id="divHeader2" class="divHeader">
                    <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="1300px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 150px;">
                                    Objid
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Group
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Device
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Status
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Sensor
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Message
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Lastvalue
                                </th>
                                <th scope="col" style="width: 150px;">
                                    Priority
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer2" class="divContainer" onscroll="scrollDivHeader(2);">
                    <asp:GridView ID="gvItems2" runat="server" Width="1300px">
                        <Columns>
                            <asp:BoundField DataField="Objid" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Group" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Device" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Status" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Sensor" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Message" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Lastvalue" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Priority" ItemStyle-Width="150px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Button ID="btnExportItems2" runat="server" Text="Export to Excel" Width="110px"
                Visible="false" OnClick="btnExportItems2_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowUnknownHardware" runat="server" OnClick="lbShowUnknownHardware_Click">Show unknown hardware</asp:LinkButton>
            <asp:Panel ID="Panel3" runat="server" Visible="false">
                <asp:Label ID="lblItems3CountPrefix" runat="server" Text="Unknown hardware items:"></asp:Label>
                <asp:Label ID="lblItems3Count" runat="server" Font-Bold="true" ForeColor="#9562ae"></asp:Label>
                <div id="divHeader3" class="divHeader">
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
                <div id="divContainer3" class="divContainer" style="height: 400px;" onscroll="scrollDivHeader(3);">
                    <asp:GridView ID="gvItems3" runat="server" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="ComputerName" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Source" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="FoundOn" ItemStyle-Width="150px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Button ID="btnExportItems3" runat="server" Text="Export to Excel" Width="110px"
                Visible="false" OnClick="btnExportItems3_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
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
            <asp:Button ID="btnExportItems4" runat="server" Text="Export to Excel" Width="110px"
                Visible="false" OnClick="btnExportItems4_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upnogrid1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowPrtgVolumeOccupationGraphs" runat="server" OnClick="lbShowPrtgVolumeOccupationGraphs_Click">Show PRTG volume occupation graphs</asp:LinkButton>
            <asp:Panel ID="Panelnogrid1" runat="server" Visible="false">
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3754&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3757&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3758&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3759&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3760&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3761&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3762&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3763&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3764&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=3837&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:HyperLink runat="server" Target="_self" NavigateUrl="http://158.166.176.171/api/historicdata.csv?id=3754&avg=0&sdate=&edate=&username=lsa&password=Prtgmonitoring">Export to CSV</asp:HyperLink>
                <br />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upnogrid2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowPrtgVolumeAvailabilityGraphs" runat="server" OnClick="lbShowPrtgVolumeAvailabilityGraphs_Click">Show PRTG volume availability graphs</asp:LinkButton>
            <asp:Panel ID="Panelnogrid2" runat="server" Visible="false">
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4899&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4900&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4901&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4902&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4903&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4904&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4905&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4906&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4907&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:Image runat="server" Height="200px" Width="300px" ImageUrl="http://158.166.176.171/chart.png?id=4908&avg=100&sdate=&edate=&username=lsa&password=Prtgmonitoring" />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_self" NavigateUrl="http://158.166.176.171/api/historicdata.csv?id=3754&avg=0&sdate=&edate=&username=lsa&password=Prtgmonitoring">Export to CSV</asp:HyperLink>
                <br />
            </asp:Panel>
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
            <asp:Button ID="btnExportItems5" runat="server" Text="Export to Excel" Width="110px"
                Visible="false" OnClick="btnExportItems5_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:HyperLink NavigateUrl="N:\DIRD\COMMON\COMMON\GSM" runat="server">GSM ownerships</asp:HyperLink>
    <br />
    <br />
    <asp:UpdatePanel ID="upnogrid3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton runat="server" ID="lkbtnShowMPR" OnClick="lkbtnShowMPR_Click">Show MPR export options</asp:LinkButton>
            <asp:Panel ID="panelnogrid3" runat="server" Visible="false">
                <asp:Calendar ID="calMPR" runat="server" OnSelectionChanged="calMPR_SelectionChanged"
                    OnVisibleMonthChanged="calMPR_VisibleMonthChanged" Caption="Select any day of the desired month:">
                </asp:Calendar>
                <asp:ImageButton ID="ibtnExportMPR" runat="server" ImageUrl="~/Images/ExportToExcelBig.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportMPR_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel><br />
    <asp:UpdatePanel runat="server" ID="upnogrid4" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton runat="server" ID="lkbtnShowOITS" 
                onclick="lkbtnShowOITS_Click">Show OITS stats export options</asp:LinkButton>
            <asp:Panel runat="server" ID="panelnogrid4" Visible="false">
                <asp:Calendar runat="server" ID="calOitsFrom" Caption="From:" CssClass="InlineBlock"
                    OnSelectionChanged="calOitsFrom_SelectionChanged" OnVisibleMonthChanged="calOitsFrom_VisibleMonthChanged">
                </asp:Calendar>
                &nbsp;
                <asp:Calendar runat="server" ID="calOitsTo" Caption="To:" CssClass="InlineBlock"
                    OnSelectionChanged="calOitsTo_SelectionChanged" OnVisibleMonthChanged="calOitsTo_VisibleMonthChanged">
                </asp:Calendar>
                <br />
                <asp:Button ID="btnExportOits" runat="server" Text="Export to Excel " Enabled="False"
                    OnClick="btnExportOits_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal runat="server" ID="TestControl"></asp:Literal>
</asp:Content>
