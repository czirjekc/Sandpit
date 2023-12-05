<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ServerMonitoring.aspx.cs"
    Inherits="Eu.Europa.Ec.Olaf.Gmsr.Reports.ServerMonitoring" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Server Monitoring
    </div>
    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton ID="lbShowLUNSpace" runat="server" OnClick="lbShowLUNSpace_Click">Show PRTG's Lun occupation & availability</asp:LinkButton>
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
            <asp:ImageButton ID="btnExportItems2" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                ToolTip="Export to Excel" Visible="false" OnClick="btnExportItems2_Click" />
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
    <asp:UpdatePanel ID="up7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
        Visible="true">
        <ContentTemplate>
            <asp:LinkButton ID="lkbtnPrtgStorage" runat="server" OnClick="lkbtnPrtgStorage_Click">Show PRTG storage report</asp:LinkButton>
            <asp:Panel ID="Panel7" runat="server" Visible="false">
                <asp:Label ID="lblItems7CountPrefix" runat="server" Text="VHD count"></asp:Label>
                <asp:Label ID="lblItems7Count" runat="server" Font-Bold="true" ForeColor="#6189af"></asp:Label>
                <div id="divHeader7" class="divHeader">
                    <table id="tblStaticHeader7" class="tblStaticHeader" rules="all" width="2600px">
                        <thead>
                            <tr>
                                <th scope="col" style="width: 100px;">
                                    VHD object id
                                </th>
                                <th scope="col" style="width: 500px;">
                                    VHD path
                                </th>
                                <th scope="col" style="width: 100px;">
                                    VHD size
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Volume object id
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Volume LunId
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Volume volume name
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Volume cluster name
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun object id
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun LunId
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun size
                                </th>
                                <th scope="col" style="width: 300px;">
                                    Lun path
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun volume name
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun volume used space
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun volume size
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun aggregate name
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun aggregate used space
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun aggregate size
                                </th>
                                <th scope="col" style="width: 100px;">
                                    Lun san name
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="divContainer7" class="divContainer" onscroll="scrollDivHeader(7);">
                    <asp:GridView ID="gvItems7" runat="server" Width="2600px" Height="300px">
                        <Columns>
                            <asp:TemplateField HeaderText="VhdObjectId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" Target="_blank" NavigateUrl='<%# "http://158.166.176.171/sensor.htm?id=" + Eval("VhdObjectId") + "&username=lsa&password=Prtgmonitoring" %>'><%# Eval("VhdObjectId")%></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#287DD6" ForeColor="White" />
                                <ItemStyle ForeColor="#287DD6" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="VhdVhdPath" HeaderText="VhdVhdPath" ItemStyle-Width="500px">
                                <HeaderStyle BackColor="#287DD6" ForeColor="White" />
                                <ItemStyle ForeColor="#287DD6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VhdVhdSize" HeaderText="VhdVhdSize" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#287DD6" ForeColor="White" />
                                <ItemStyle ForeColor="#287DD6" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="VolumeObjectId" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# "http://158.166.176.171/sensor.htm?id=" + Eval("VolumeObjectId") + "&username=lsa&password=Prtgmonitoring" %>'><%# Eval("VolumeObjectId")%></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#7DB0E6" ForeColor="White" />
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="VolumeLunId" HeaderText="VolumeLunId" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#7DB0E6" ForeColor="White" />
                                <ItemStyle ForeColor="#7DB0E6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VolumeVolumeName" HeaderText="VolumeVolumeName" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#7DB0E6" ForeColor="White" />
                                <ItemStyle ForeColor="#7DB0E6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VolumeClusterName" HeaderText="VolumeClusterName" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#7DB0E6" ForeColor="White" />
                                <ItemStyle ForeColor="#7DB0E6" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="LunObjectId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# "http://158.166.176.171/sensor.htm?id=" + Eval("LunObjectId") + "&username=lsa&password=Prtgmonitoring" %>'><%# Eval("LunObjectId")%></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="Red" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LunLunId" HeaderText="LunLunId" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunLunSize" HeaderText="LunLunSize" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunLunPath" HeaderText="LunLunPath" ItemStyle-Width="300px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunLunVolumeName" HeaderText="LunLunVolumeName" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunLunVolumeUsedSpace" HeaderText="LunLunVolumeUsedSpace"
                                ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunLunVolumeSize" HeaderText="LunLunVolumeSize" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunAggregateName" HeaderText="LunAggregateName" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunAggregateUsedSpace" HeaderText="LunAggregateUsedSpace"
                                ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunAggregateSize" HeaderText="LunAggregateSize" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LunSanName" HeaderText="LunSanName" ItemStyle-Width="100px">
                                <HeaderStyle BackColor="#2843D6" ForeColor="White" />
                                <ItemStyle ForeColor="#2843D6" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:ImageButton ID="ibtnExportPrtgStorage" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportPrtgStorage_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal runat="server" ID="TestControl"></asp:Literal>
</asp:Content>
