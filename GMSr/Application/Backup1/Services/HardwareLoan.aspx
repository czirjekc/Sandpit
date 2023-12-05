<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardwareLoan.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Services.HardwareLoan"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphMain">
<div class="h1">
        Hardware loan
    </div>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div style="width: 700px; background-color: #E1F3F9; color: White;" id="divTimeFrame"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnShowCalendars" Font-Underline="false" Font-Size="15px"
                    ForeColor="#557DCC" Font-Bold="true">1. Select time frame</asp:LinkButton><br />
                <hr />
                <asp:Panel ID="pnlTimeFrame" runat="server">
                    <div class="InlineBlock">
                        <asp:Label runat="server" ForeColor="#666666">Start date</asp:Label>
                        <asp:Calendar ID="calTimeFrameStart" runat="server" OnSelectionChanged="calTimeFrameStart_SelectionChanged"
                            OnVisibleMonthChanged="calTimeFrameStart_VisibleMonthChanged"></asp:Calendar>
                    </div>
                    &nbsp;&nbsp;
                    <div class="InlineBlock">
                        <asp:Label ID="Label1" runat="server" ForeColor="#666666">End date</asp:Label>
                        <asp:Calendar ID="calTimeFrameEnd" runat="server" OnSelectionChanged="calTimeFrameEnd_SelectionChanged"
                            OnVisibleMonthChanged="calTimeFrameEnd_VisibleMonthChanged"></asp:Calendar>
                    </div>
                </asp:Panel>
                <asp:Label runat="server" ID="lblTimeFrame" ForeColor="#666666">No time frame set.</asp:Label>
                <asp:ImageButton ImageUrl="~/Images/Delete.png" runat="server" ID="imgbtnClearTimeFrame"
                    Visible="false" OnClick="imgbtnClearTimeFrame_Click" ToolTip="Clear time frame" />
            </div>
            <act:CollapsiblePanelExtender ID="pnlTimeFrame_CollapsiblePanelExtender" runat="server"
                Enabled="True" TargetControlID="pnlTimeFrame" CollapseControlID="lkbtnShowCalendars"
                ExpandControlID="lkbtnShowCalendars" Collapsed="true" CollapsedText="1. Select time frame (+)"
                ExpandedText="1. Select time frame (-)" TextLabelID="lkbtnShowCalendars">
            </act:CollapsiblePanelExtender>
            <act:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="divTimeFrame"
                BorderColor="#557DC3" Corners="All">
            </act:RoundedCornersExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upItemSelect" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 700px; background-color: #D9EAF6; color: White;" id="divItemSelection"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnShowItemSelection" Font-Underline="false"
                    Font-Size="15px" ForeColor="#557DCC" Font-Bold="true">2. Select item</asp:LinkButton><br />
                <hr />
                <asp:Panel runat="server" ID="pnlItemSelection">
                    <asp:Label runat="server" ID="lblSelectCategory" CssClass="DropDownBox">Select category</asp:Label>
                    <act:DropDownExtender ID="lblSelectCategory_DropDownExtender" runat="server" Enabled="True"
                        TargetControlID="lblSelectCategory" DropDownControlID="upCategoryBox" HighlightBackColor="#FFF3DB">
                    </act:DropDownExtender>
                    <asp:UpdatePanel ID="upCategoryBox" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="pnlCategory" CssClass="ContextMenuPanel">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="upSearchItem" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="tbSearchItem" runat="server" CssClass="SearchBox" Width="300px">Or search by inventory number or ticket code</asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="Image1" ImageUrl="~/Images/Loading2.gif" runat="server" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="upSelectedCategory" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblCategorySelection" ForeColor="#666666" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblCategoryId" runat="server" Visible="false"></asp:Label>
                            <act:AnimationExtender ID="upSelectedCategory_AnimationExtender" runat="server" Enabled="True"
                                TargetControlID="lblCategorySelection">
                                <Animations>
                                    <OnLoad>
                                        <Color AnimationTarget='lblCategorySelection' Duration='1' StartValue='#FFFF7F' EndValue='#D9EAF6' Property='style' PropertyKey='backgroundColor' />
                                        
                                    </OnLoad>
                                </Animations>
                            </act:AnimationExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="upResultBox" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Visible="true">
                                <asp:Label ID="lblItems1CountPrefix" runat="server" ForeColor="#666666">Loan items:</asp:Label>
                                <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#9562ae"></asp:Label>
                                <div id="divHeader1" class="divHeader" style="width: 600px;">
                                    <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="650px">
                                        <thead>
                                            <tr>
                                                <th scope="col" style="width: 150px; color: #666666;">
                                                    Inventory Nr
                                                </th>
                                                <th scope="col" style="width: 300px; color: #666666;">
                                                    Description
                                                </th>
                                                <th scope="col" style="width: 80px; color: #666666;">
                                                    Status
                                                </th>
                                                <th scope="col" style="width: 80px; color: #666666;">
                                                    Perma
                                                </th>
                                                <th scope="col" style="width: 40px; color: #666666;">
                                                    Id
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div id="divContainer1" class="divContainer" style="height: 200px; width: 619px;"
                                    onscroll="scrollDivHeader(1);">
                                    <asp:GridView ID="gvItems1" runat="server" Width="650px" AutoGenerateColumns="False"
                                        DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="InventoryNumber" ItemStyle-Width="150px" />
                                            <asp:BoundField DataField="Description" ItemStyle-Width="300px"></asp:BoundField>
                                            <asp:BoundField DataField="Status" ItemStyle-Width="80px"></asp:BoundField>
                                            <asp:BoundField DataField="IsPermaLoan" ItemStyle-Width="80px"></asp:BoundField>
                                            <asp:BoundField DataField="Id" ItemStyle-Width="40px" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <act:UpdatePanelAnimationExtender ID="upResultBox_UpdatePanelAnimationExtender" runat="server"
                        Enabled="True" TargetControlID="upResultBox">
                        <Animations>
                        <OnUpdated>
                        <Color AnimationTarget='pnlResultBox' Duration='1' StartValue='#FFFF7F' EndValue='#D9EAF6' Property='style' PropertyKey='backgroundColor' />
                        </OnUpdated>
                        
                        </Animations>
                    </act:UpdatePanelAnimationExtender>
                </asp:Panel>
            </div>
            <act:CollapsiblePanelExtender ID="pnlItemSelection_CollapsiblePanelExtender" runat="server"
                Enabled="True" TargetControlID="pnlItemSelection" CollapseControlID="lkbtnShowItemSelection"
                ExpandControlID="lkbtnShowItemSelection" Collapsed="true" CollapsedText="2. Select item (+)"
                ExpandedText="2. Select item (-)" TextLabelID="lkbtnShowItemSelection">
            </act:CollapsiblePanelExtender>
            <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="divItemSelection"
                BorderColor="#557DC3" Corners="All">
            </act:RoundedCornersExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upItemSummary" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div style="width: 700px; background-color: #CFDFF1; color: White;" id="divItemSummary"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnShowItemSummary" Font-Underline="false" Font-Size="15px"
                    ForeColor="#557DCC" Font-Bold="true">3. Item summary & settings</asp:LinkButton><br />
                <hr />
                <asp:Panel runat="server" ID="Panel3">
                    <asp:UpdatePanel ID="upInnerItemSummary" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Panel ID="pnlItemSummary" runat="server" HorizontalAlign="Left">
                                <asp:Label ID="lblSummaryInventoryNbr" runat="server" ForeColor="#666666" Width="200px"
                                    CssClass="SummaryLabel">Inventory number:&nbsp</asp:Label><asp:Label ID="lblSummaryInventoryNbrValue"
                                        runat="server" ForeColor="#666666">-</asp:Label><br />
                                <asp:Label ID="lblSummaryDescription" runat="server" ForeColor="#666666" Width="200px"
                                    CssClass="SummaryLabel">Description:&nbsp</asp:Label><asp:Label ID="lblSummaryDescriptionValue"
                                        runat="server" ForeColor="#666666">-</asp:Label><br />
                                <asp:Label ID="lblSummaryId" runat="server" ForeColor="#666666" Width="200px" CssClass="SummaryLabel">Id:&nbsp</asp:Label><asp:Label
                                    ID="lblSummaryIdValue" runat="server" ForeColor="#666666">-</asp:Label><br />
                                <asp:Label ID="lblSummaryCategory" runat="server" ForeColor="#666666" Width="200px"
                                    CssClass="SummaryLabel">Category:</asp:Label>
                                <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="lblSummaryStatus" runat="server" ForeColor="#666666" Width="200px"
                                    CssClass="SummaryLabel">State:</asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <br />
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <act:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server"
                        Enabled="True" TargetControlID="upInnerItemSummary">
                        <Animations>
                        <OnUpdated>
                        <Color AnimationTarget='pnlItemSummary' Duration='1' StartValue='#FFFF7F' EndValue='#CFDFF1' Property='style' PropertyKey='backgroundColor' />
                        </OnUpdated>
                        
                        </Animations>
                    </act:UpdatePanelAnimationExtender>
                    <hr />
                    <br />
                    <asp:UpdatePanel ID="upAssignmentBox" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server" Visible="true">
                                <asp:Label ID="lblItems2CountPrefix" runat="server" ForeColor="#666666">Assignments:</asp:Label>
                                <asp:Label ID="lblItems2Count" runat="server" Font-Bold="true" ForeColor="#9562ae"></asp:Label>
                                <div id="divHeader2" class="divHeader" style="width: 450px;">
                                    <table id="tblStaticHeader2" class="tblStaticHeader" rules="all" width="470px">
                                        <thead>
                                            <tr>
                                                <th scope="col" style="width: 20px; color: #666666;">
                                                </th>
                                                <th scope="col" style="width: 150px; color: #666666;">
                                                    From
                                                </th>
                                                <th scope="col" style="width: 150px; color: #666666;">
                                                    To
                                                </th>
                                                <th scope="col" style="width: 150px; color: #666666;">
                                                    Ticket
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div id="divContainer2" class="divContainer" style="height: 200px; width: 469px;"
                                    onscroll="scrollDivHeader(2);">
                                    <asp:GridView ID="gvItems2" runat="server" Width="470px" AutoGenerateColumns="False"
                                        OnRowDeleting="gvItems2_RowDeleting" DataKeyNames="Id">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/Delete.png" CommandName="Delete"
                                                ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# ((DateTime)Eval("DateStart")).ToShortDateString() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# ((DateTime)Eval("DateEnd")).ToShortDateString() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TicketCode" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
            <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" Enabled="True"
                TargetControlID="Panel3" CollapseControlID="lkbtnShowItemSummary" ExpandControlID="lkbtnShowItemSummary"
                Collapsed="true" CollapsedText="3. Item summary & settings (+)" ExpandedText="3. Item summary & settings (-)"
                TextLabelID="lkbtnShowItemSummary">
            </act:CollapsiblePanelExtender>
            <act:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="divItemSummary"
                BorderColor="#557DC3" Corners="All">
            </act:RoundedCornersExtender>
            <act:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server"
                Enabled="True" TargetControlID="upAssignmentBox">
                <Animations>
                        <OnUpdated>
                        <Color AnimationTarget='Panel2' Duration='1' StartValue='#FFFF7F' EndValue='#CFDFF1' Property='style' PropertyKey='backgroundColor' />
                        </OnUpdated>
                        
                </Animations>
            </act:UpdatePanelAnimationExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upAddAssignment" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div style="width: 700px; background-color: #C9D7ED; color: White;" id="divAddAssignment"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnAddAssignment" Font-Underline="false" Font-Size="15px"
                    ForeColor="#557DCC" Font-Bold="true">4. Add assignment</asp:LinkButton><br />
                <hr />
                <asp:Panel ID="pnlAddAssignment" runat="server">
                    <div class="InlineBlock">
                        <asp:Label ID="Label2" runat="server" ForeColor="#666666">Start date</asp:Label>
                        <asp:Calendar ID="calAssignmentStart" runat="server" OnSelectionChanged="calAssignmentStart_SelectionChanged"
                            OnVisibleMonthChanged="calAssignmentStart_VisibleMonthChanged"></asp:Calendar>
                    </div>
                    &nbsp;&nbsp;
                    <div class="InlineBlock">
                        <asp:Label ID="Label3" runat="server" ForeColor="#666666">End date</asp:Label>
                        <asp:Calendar ID="calAssignmentEnd" runat="server" OnSelectionChanged="calAssignmentEnd_SelectionChanged"
                            OnVisibleMonthChanged="calAssignmentEnd_VisibleMonthChanged"></asp:Calendar>
                    </div>
                    <br />
                    <br />
                    <div style="vertical-align: middle; height: 40px;">
                        <asp:TextBox ID="tbTicketCode" runat="server" Width="380px" ForeColor="#666666">Enter ticket code here</asp:TextBox>
                        <asp:ImageButton ID="imgbtnAddAssignment" runat="server" ImageUrl="~/Images/AddBig.png"
                            ToolTip="Add assignment" OnClick="imgbtnAddAssignment_Click" />
                    </div>
                    <asp:Label runat="server" ID="lblAddAssignment" ForeColor="#666666"></asp:Label>
                </asp:Panel>
            </div>
            <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" Enabled="True"
                TargetControlID="pnlAddAssignment" CollapseControlID="lkbtnAddAssignment" ExpandControlID="lkbtnAddAssignment"
                Collapsed="true" CollapsedText="4. Add assignment (+)" ExpandedText="4. Add assignment (-)"
                TextLabelID="lkbtnAddAssignment">
            </act:CollapsiblePanelExtender>
            <act:RoundedCornersExtender ID="RoundedCornersExtender4" runat="server" TargetControlID="divAddAssignment"
                BorderColor="#557DC3" Corners="All">
            </act:RoundedCornersExtender>
            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/ExportToExcelBigText.png"  
                onclick="btnExport_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
