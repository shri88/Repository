<%@ Page Title="RID" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RID.aspx.cs" Inherits="NG_Portal.RID" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="margin-left:380px">ABOUT ELIMS-NG</h3>
    <div id="elimsNG" runat="server">
    <table>
        <tr>            
            <td>
                <asp:Label ID="lblNGCodes" Text="OPERATIONAL LINKS" Font-Bold="true" runat="server"></asp:Label>
                
            </td>
        </tr>
    </table>
    </div>
    <div id="divTopPanel" runat="server" style="width:750px">
        <br />
        <asp:Button ID="btnOperaLink" Text="NG LABS" runat="server" OnClick="btnOperaLink_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNGLABCodes" runat="server" Text="NG LAB CODES" OnClick="btnNGLABCodes_Click"  />
         &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPCMVariables" runat="server" Text="PCM Variables" OnClick="btnPCMVariables_Click"  />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExportVar" runat="server" Text="Export Variables" OnClick="btnExportVar_Click"  />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRFCForm" runat="server" Text="RFC Form" OnClick="btnRFCForm_Click" />
        <br />
        <br />       
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="90px" Text="kamalhasan" Visible="false"></asp:TextBox>
        <br /><br />      
    </div>
    <div id="divGdv" runat="server" style="width:1500px">
        <asp:GridView ID="gdvNgCodes" runat="server" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" Width="200px" OnRowEditing="gdvNgCodes_RowEditing"
            OnRowCancelingEdit="gdvNgCodes_RowCancelingEdit">
                <AlternatingRowStyle BackColor="#99CCFF" />
                <HeaderStyle VerticalAlign="Middle" Font-Bold="True" BackColor="#000066" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>           
            </asp:GridView>

       <asp:GridView ID="gdvPCMVariables" runat="server" AutoGenerateColumns="false" style="word-break:break-word" Width="1000px"
           OnSelectedIndexChanged="gdvPCMVariables_SelectedIndexChanged" OnRowEditing="gdvPCMVariables_RowEditing" 
           OnRowCancelingEdit="gdvPCMVariables_RowCancelingEdit" OnRowUpdating="gdvPCMVariables_RowUpdating" OnRowUpdated="gdvPCMVariables_RowUpdated"
           OnRowDataBound="gdvPCMVariables_RowDataBound">
           
                <AlternatingRowStyle BackColor="#99CCFF" />
                <HeaderStyle VerticalAlign="Middle" Font-Bold="True" BackColor="#000066" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>           
            <Columns>
               <asp:TemplateField HeaderText="Variable" SortExpression="Variable">
                        <ItemTemplate>
                            <asp:Label ID="lblVariable" runat="server" Text='<%# Bind("Variable") %>'></asp:Label>
                        </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtVariable" runat="server" Text='<%# Bind("Variable") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle HorizontalAlign="Justify" />
                        <ItemStyle HorizontalAlign="Left" Wrap="True" Width="230px" />
               </asp:TemplateField> 
                <asp:TemplateField HeaderText="Expanded Value" SortExpression="ExpandedValue">
                        <ItemTemplate>
                            <asp:Label ID="lblExpValue" runat="server" Text='<%# Bind("ExpandedValue") %>'></asp:Label>
                        </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtExpValue" runat="server" Text='<%# Bind("ExpandedValue") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Justify" />
                        <ItemStyle HorizontalAlign="Left" Wrap="True" Width="450px" />
               </asp:TemplateField> 
                <asp:TemplateField HeaderText="Lab" SortExpression="Lab">
                        <ItemTemplate>
                            <asp:Label ID="lblLab" runat="server" Text='<%# Bind("Lab") %>'></asp:Label>
                        </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLab" runat="server" Text='<%# Bind("Lab") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="80px" />
               </asp:TemplateField> 
               
                <asp:CommandField HeaderText="Modify" ButtonType="Button" ShowEditButton="true" ItemStyle-HorizontalAlign="Center" ShowCancelButton="true" ItemStyle-Width="150px" />
            </Columns>           
        </asp:GridView>
    </div>
        <asp:Table id="tblRFC" runat="server" BorderStyle="Solid">
            <asp:TableHeaderRow BorderStyle="Inset" BorderColor="Black">
                <asp:TableCell>
                    <asp:Label ID="tblLblRequestor" Text="Requestor :" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tblTxtRequestor" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow BorderStyle="Inset" BorderColor="Black">
                <asp:TableCell>
                    <asp:Label ID="tblLblCorr" Text="Is it a corrective IT Change or enhancement IT Change?" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBoxList ID="chkboxOp1" runat="server">
                        <asp:ListItem>Corrective IT Change</asp:ListItem>
                        <asp:ListItem>Enhancement IT Change</asp:ListItem>
                    </asp:CheckBoxList>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow BorderStyle="Inset" BorderColor="Black">
                <asp:TableCell>
                    <asp:Label ID="tblLblReason" Text="Reason for IT Change :" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbltxtReason" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow BorderStyle="Inset" BorderColor="Black">
                <asp:TableCell>
                    <asp:Label ID="tblLblExecTime" Text="Preferred execution date/time :" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbltxtExecTime" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow BorderStyle="Inset" BorderColor="Black">
                    <asp:TableCell>
                        <asp:Label ID="tblLblStake" Text="Stakeholders of Affected (PUID) :" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbltxtStake" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableHeaderRow>
            <asp:TableHeaderRow BorderStyle="Inset" BorderColor="Black">
                <asp:TableCell>
                    <asp:Label ID="tblLblChangeCat" Text="Change Category :" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbltxtChangeCat" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <asp:Button ID="btnSendMail" runat="server" Text="Submit" OnClick="btnSendMail_Click" />
</asp:Content>
