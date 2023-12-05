<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="WebEventLog.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Administration.WebEventLog"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="h1">
        Web Event Log
    </div>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="tblSearchItems" runat="server">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDate" runat="server" Text="Date Between:"></asp:Label>
                    </td>
                    <td align="left">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDateFrom" runat="server" Width="80px"></asp:TextBox>
                                    <act:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtDateFrom" Format="dd/MM/yyyy">
                                    </act:CalendarExtender>
                                </td>
                                <td style="width: 36px;" align="right">
                                    And:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtDateTo" runat="server" Width="80px"></asp:TextBox>
                                    <act:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtDateTo" Format="dd/MM/yyyy">
                                    </act:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:Label ID="lblItems1CountPrefix" runat="server" Text="Web Events:"></asp:Label>
            <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#557dc3"
                Text="0"></asp:Label>
            <asp:ImageButton ID="ibtnExportItems1" runat="server" ImageUrl="~/Images/ExportToExcel.png"
                ToolTip="Export to Excel" OnClick="ibtnExportItems1_Click" />
            <div id="divHeader1" class="divHeader">
                <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="1300px">
                    <thead>
                        <tr>
                            <th scope="col" style="width: 150px;">
                                Time
                            </th>
                            <th scope="col" style="width: 500px;">
                                Message
                            </th>
                            <th scope="col" style="width: 50px;">
                                Seq.
                            </th>
                            <th scope="col" style="width: 50px;">
                                Occ.
                            </th>
                            <th scope="col" style="width: 400px;">
                                Requested Url
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="divContainer1" class="divContainer" style="height: 500px;" onscroll="scrollDivHeader(1);">
                <asp:GridView ID="gvItems1" runat="server" DataKeyNames="Guid" Width="1300px" OnRowCreated="gvItems1_RowCreated" OnRowDataBound="gvItems1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Time" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Message" ItemStyle-Width="500px" />
                        <asp:BoundField DataField="Sequence" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Occurrence" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="RequestUrl" ItemStyle-Width="400px" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
