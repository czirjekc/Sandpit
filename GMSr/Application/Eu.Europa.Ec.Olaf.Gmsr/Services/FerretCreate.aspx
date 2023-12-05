<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FerretCreate.aspx.cs" Inherits="Eu.Europa.Ec.Olaf.Gmsr.Services.FerretCreate"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphMain">
    <script type="text/javascript">
     
    </script>
    <div class="h1">
        Ferret - Create
    </div>
    <asp:UpdatePanel ID="upComputers" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div style="width: 700px; background-color: #E1F3F9; color: White;" id="divScript"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnShowScript" Font-Underline="false" Font-Size="15px"
                    ForeColor="#557DCC" Font-Bold="true">1. Script settings</asp:LinkButton><br />
                <hr />
                <asp:Panel ID="pnlScript" runat="server" HorizontalAlign="Left" Width="500px">
                    <asp:UpdatePanel ID="upInnerScript" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Label ID="lblScriptName" runat="server" ForeColor="#666666" Width="70px">Name:&nbsp</asp:Label>
                            <asp:TextBox ID="tbScriptName" runat="server" Width="400px" MaxLength="200"></asp:TextBox><br />
                            <asp:Label ID="lblScriptEnabled" runat="server" ForeColor="#666666" Width="70px">Enabled:&nbsp</asp:Label>
                            <asp:CheckBox ID="cbScriptEnabled" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
            <act:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="divScript"
                BorderColor="Black" Corners="All">
            </act:RoundedCornersExtender>
            <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" Enabled="True"
                TargetControlID="pnlScript" CollapseControlID="lkbtnShowScript" ExpandControlID="lkbtnShowScript"
                Collapsed="true" CollapsedText="1. Script settings (+)" ExpandedText="1. Script settings (-)"
                TextLabelID="lkbtnShowScript">
            </act:CollapsiblePanelExtender>
            <div style="width: 700px; background-color: #D9EAF6; color: White;" id="divCheck"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnShowCheck" Font-Underline="false" Font-Size="15px"
                    ForeColor="#557DCC" Font-Bold="true">2. Check settings</asp:LinkButton><br />
                <hr />
                <asp:Panel ID="pnlCheck" runat="server">
                    <asp:Label ID="lblItems1CountPrefix" runat="server" ForeColor="#666666">Check list:</asp:Label>
                    <asp:Label ID="lblItems1Count" runat="server" Font-Bold="true" ForeColor="#9562ae"></asp:Label>
                    <div id="divHeader1" class="divHeader" style="width: 600px;">
                        <table id="tblStaticHeader1" class="tblStaticHeader" rules="all" width="620px">
                            <thead>
                                <tr>
                                    <th scope="col" style="width: 20px; color: #666666;">
                                    </th>
                                    <th scope="col" style="width: 150px; color: #666666;">
                                        Type
                                    </th>
                                    <th scope="col" style="width: 150px; color: #666666;">
                                        Path
                                    </th>
                                    <th scope="col" style="width: 150px; color: #666666;">
                                        KeyName
                                    </th>
                                    <th scope="col" style="width: 150px; color: #666666;">
                                        Value
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="divContainer1" class="divContainer" style="height: 200px; width: 619px;"
                        onscroll="scrollDivHeader(1);">
                        <asp:UpdatePanel ID="upChecks" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                        <asp:GridView ID="gvItems1" runat="server" Width="620px" 
                                AutoGenerateColumns="False" DataKeyNames="Id" onrowdeleting="gvItems1_RowDeleting"
                            >
                            <Columns>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/Delete.png" CommandName="Delete"
                                    ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Type" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Path" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="KeyName" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Value" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                             </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                    <br />
                    
                            <asp:Panel ID="pnlCheckSettings" HorizontalAlign="Left" runat="server" Width="500px">
                                <asp:Label ID="lblCheckType" runat="server" ForeColor="#666666" Width="70px">Type:&nbsp</asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" Width="400px">
                                    <asp:ListItem Text="Registry" Value="Registry"></asp:ListItem>
                                    <asp:ListItem Text="FileExistence" Value="FileExistence"></asp:ListItem>
                                    <asp:ListItem Text="FileSize" Value="FileSize"></asp:ListItem>
                                    <asp:ListItem Text="FileVersion" Value="FileVersion"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblPath" runat="server" ForeColor="#666666" Width="70px">Path:&nbsp</asp:Label><asp:TextBox
                                    ID="tbPath" runat="server" Width="400px"></asp:TextBox><br />
                                <asp:Label ID="lblKeyName" runat="server" ForeColor="#666666" Width="70px">Key name:&nbsp</asp:Label><asp:TextBox
                                    ID="tbKeyName" runat="server" Width="400px"></asp:TextBox><br />
                                <asp:Label ID="lblValue" runat="server" ForeColor="#666666" Width="70px">Value:&nbsp</asp:Label><asp:TextBox
                                    ID="tbValue" runat="server" Width="400px"></asp:TextBox><br />
                                <div style="width: 470px;">
                                    <asp:ImageButton runat="server" ID="imgbtnAddCheck" ImageUrl="~/Images/AddBig.png"
                                        CssClass="FerretRightFloat" ToolTip="Add check" OnClick="imgbtnAddCheck_Click" /></div>
                            </asp:Panel>
                   
                </asp:Panel>
            </div>
            <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="divCheck"
                BorderColor="Black" Corners="All">
            </act:RoundedCornersExtender>
            <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" Enabled="True"
                TargetControlID="pnlCheck" CollapseControlID="lkbtnShowCheck" ExpandControlID="lkbtnShowCheck"
                Collapsed="true" CollapsedText="2. Check settings (+)" ExpandedText="2. Check settings (-)"
                TextLabelID="lkbtnShowCheck">
            </act:CollapsiblePanelExtender>
            <div style="width: 700px; background-color: #CFDFF1; color: White;" id="divComputers"
                runat="server">
                <asp:LinkButton runat="server" ID="lkbtnShowComputers" Font-Underline="false" Font-Size="15px"
                    ForeColor="#557DCC" Font-Bold="true">3. Show computers</asp:LinkButton><br />
                <hr />
                <asp:Panel ID="pnlComputers" runat="server">
                    <asp:UpdatePanel ID="upListBoxes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div style="width: 462px; margin: auto;">
                                <div style="float: left;">
                                    <asp:Label ID="lblTarget" runat="server" ForeColor="#666666">Targeted groups</asp:Label><br />
                                    <asp:ListBox ID="lbToSave" runat="server" SelectionMode="Multiple" Height="200px"
                                        CssClass="FerretInline"></asp:ListBox>
                                </div>
                                <div style="float: left; margin-top: 50px;">
                                    <asp:ImageButton ID="imgbtnLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png"
                                        OnClick="imgbtnLeft_Click" Height="68px" Width="60px" /><br />
                                    <asp:ImageButton ID="imgbtnRight" runat="server" ImageUrl="~/Images/ArrowRight.png"
                                        OnClick="imgbtnRight_Click" Height="68px" Width="60px" />
                                </div>
                                <div style="float: left;">
                                    <asp:Label ID="lblLANdesk" runat="server" ForeColor="#666666">LANDesk groups</asp:Label><br />
                                    <asp:ListBox ID="lbGroups" runat="server" SelectionMode="Multiple" Height="200px">
                                    </asp:ListBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
            <act:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="divComputers"
                BorderColor="Black" Corners="All">
            </act:RoundedCornersExtender>
            <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" Enabled="True"
                TargetControlID="pnlComputers" CollapseControlID="lkbtnShowComputers" ExpandControlID="lkbtnShowComputers"
                Collapsed="true" CollapsedText="3. Show computers (+)" ExpandedText="3. Show computers (-)"
                TextLabelID="lkbtnShowComputers">
            </act:CollapsiblePanelExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
