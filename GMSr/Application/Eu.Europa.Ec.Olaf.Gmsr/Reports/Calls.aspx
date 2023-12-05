<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Calls.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Reports.Calls"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Calls
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
                <div class="test">
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
                        <asp:GridView ID="gvItems1" runat="server" Width="2200px" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Label1" runat="server" Text='<%# Eval("Code") %>' NavigateUrl='<%# "http://d-olaf/CallTools/Wimt/Default.aspx?ID=" + Eval("Code") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="State" ItemStyle-Width="40px"></asp:BoundField>
                                <asp:BoundField DataField="Description" ItemStyle-Width="550px"></asp:BoundField>
                                <asp:BoundField DataField="Report" ItemStyle-Width="550px"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
            <asp:ImageButton ID="btnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                ToolTip="Export to Excel" Visible="false" OnClick="btnExportItems1_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upnogrid3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton runat="server" ID="lkbtnShowMPR" OnClick="lkbtnShowMPR_Click">Show MPR export options</asp:LinkButton>
            <asp:Panel ID="panelnogrid3" runat="server" Visible="false">
                <asp:Calendar ID="calMPR" runat="server" OnSelectionChanged="calMPR_SelectionChanged"
                    OnVisibleMonthChanged="calMPR_VisibleMonthChanged" Caption="Select any day of the desired month:">
                </asp:Calendar>
                <asp:ImageButton ID="ibtnExportMPR" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                    ToolTip="Export to Excel" OnClick="ibtnExportMPR_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel runat="server" ID="upnogrid4" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:LinkButton runat="server" ID="lkbtnShowOITS" OnClick="lkbtnShowOITS_Click">Show OITS stats export options</asp:LinkButton>
            <asp:Panel runat="server" ID="panelnogrid4" Visible="false">
                <asp:Calendar runat="server" ID="calOitsFrom" Caption="From:" CssClass="InlineBlock"
                    OnSelectionChanged="calOitsFrom_SelectionChanged" OnVisibleMonthChanged="calOitsFrom_VisibleMonthChanged">
                </asp:Calendar>
                &nbsp;
                <asp:Calendar runat="server" ID="calOitsTo" Caption="To:" CssClass="InlineBlock"
                    OnSelectionChanged="calOitsTo_SelectionChanged" OnVisibleMonthChanged="calOitsTo_VisibleMonthChanged">
                </asp:Calendar>
                <br />
                <asp:ImageButton ID="btnExportOits" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"
                    ToolTip="Export to Excel" Enabled="False" OnClick="btnExportOits_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal runat="server" ID="TestControl"></asp:Literal>
</asp:Content>
