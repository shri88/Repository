<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NG_Portal._Default" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <div style="align-content:center; margin: 0 auto;">
            <asp:Label ID="lblDeploymentList" runat="server"><h2 style="margin-left:370px">DEPLOYMENT DASHBOARD</h2></asp:Label>
            
        </div>
    
    <table id="tblDepReport" style="width:auto">         
        
        <tr>
            <td>
                <asp:Label ID="lblfromDate" runat="server" Text="From Date" Width="70px"/>
            </td>
            <td>
                 <asp:TextBox ID="txtFromDate" runat="server" Text="mm/dd/yyyy" ToolTip="From Date"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calAjaxFromDate" runat="server" PopupButtonID="imgPopUp" ClearTime="true" Animated="true" TargetControlID="txtFromDate" Format="MM/dd/yyyy" />
                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Select the date"></asp:RequiredFieldValidator>
            </td>
            </tr>
        <tr>
            <td>
                <asp:Label ID="lblToDate" runat="server" Text="To Date" Width="70px"/>
            </td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" Text="mm/dd/yyyy" ToolTip="To Date" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calAjaxToDate" runat="server" PopupButtonID="imgPopUp" ClearTime="true" Animated="true" TargetControlID="txtToDate" Format="MM/dd/yyyy" />
            </td>
        </tr>        
    </table>
    <br />
    <div id="divFilters" runat="server" visible="false">
        <asp:Label ID="lblMilestone" runat="server" Text="Milestone"></asp:Label>&nbsp;
        <asp:DropDownList ID="ddlMilestone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMilestone_SelectedIndexChanged">
            <asp:ListItem Value="Select">--Select--</asp:ListItem>
            <asp:ListItem>3.2</asp:ListItem>
            <asp:ListItem>4.1</asp:ListItem>
            <asp:ListItem>4.2</asp:ListItem>                   
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Label ID="lblScope" runat="server" Text="Scope"></asp:Label>
        <asp:DropDownList ID="ddlScope" runat="server">
            <asp:ListItem>--Select--</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Label ID="lblLab" runat="server" Text="Lab"></asp:Label>
        <asp:DropDownList ID="ddlLab" runat="server">
            <asp:ListItem>--Select--</asp:ListItem>
        </asp:DropDownList>        
    </div>

    <br />
    <div id="divDeployLog" style="width:1700px">

        <asp:Button ID="btnShowReleaseLog" runat="server" Text="Show Releases" OnClick="btnShowReleaseLog_Click"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExportReleaseLog" runat="server" Text="Export Releases" OnClick="btnExportReleaseLog_Click"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnShowFilter" runat="server" Text="Filtered Results" OnClick="btnShowFilter_Click" />
         &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDateFilter" runat="server" Text="Date Filtered Results" OnClick="btnDateFilter_Click" />
         &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExportFilterLog" runat="server" Text="Export Filtered Results" OnClick="btnExportFilterLog_Click" />
        <br /><br />
        <asp:GridView ID="gdvDeployLog" runat="server" style="word-break:break-word" Width="1400px" AutoGenerateColumns="false"
            AllowPaging="True" OnPageIndexChanging="gdvDeployLog_PageIndexChanging"  PageSize="20" GridLines="None" BorderColor="Black" BorderStyle="Solid" OnSelectedIndexChanged="gdvDeployLog_SelectedIndexChanged" 
            >
            <AlternatingRowStyle BackColor="#99CCFF" />
           <Columns>

               <asp:BoundField DataField="Environment" HeaderText="Environment" ItemStyle-Width="110px">                    
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:BoundField>

               <asp:BoundField DataField="milestone" HeaderText="Milestone" ItemStyle-Width="90px">
                   <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:BoundField>

               <asp:BoundField DataField="scope" HeaderText="Scope">                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

               <asp:BoundField DataField="site" HeaderText="Site">                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

               <asp:BoundField DataField="lab" HeaderText="Lab">                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

               <asp:BoundField DataField="created_dt" HeaderText="Created Date" ItemStyle-Width="180px">                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField> 

               <asp:TemplateField HeaderText="Release ID" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>                       
                       <asp:Label ID="lblRelId" runat="server" Text='<%# Bind("Release_Id") %>'></asp:Label>
                   </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Deployed By" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>                       
                       <asp:Label ID="lblDeployedBy" runat="server" Text='<%# Bind("DeployedBy") %>'></asp:Label>
                   </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
               </asp:TemplateField>

              <asp:TemplateField HeaderText="Start Time" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>                       
                       <asp:Label ID="lblDepStartTime" runat="server" Text='<%# Bind("startTime") %>'></asp:Label>
                   </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="End Time" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>                       
                       <asp:Label ID="lblDepEndTime" runat="server" Text='<%# Bind("endTime") %>'></asp:Label>
                   </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Duration (mins)" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>                       
                       <asp:Label ID="lblDepEndTime" runat="server" Text='<%# Bind("MinuteDiff") %>'></asp:Label>
                   </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Description" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>                       
                       <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                   </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
               </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#000099" BorderColor="Black" ForeColor="White" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
            <PagerSettings Position="TopAndBottom" NextPageImageUrl="~/fonts/NextPage.jpg" NextPageText="" PageButtonCount="5" />
           </asp:GridView>

    </div>
    
    <%-- Component Details --%>

    <%--<asp:Button ID="Button1" runat="server" Text="Show Components" />--%>

 

<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">  
    <iframe style=" width: 350px; height: 300px;" id="irm1" src="Component_details.aspx" runat="server"></iframe>
   <br/>
    <asp:Button ID="Button2" runat="server" Text="Close" />    
</asp:Panel>

<%-- Style Sheet --%>
    <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .lbl
        {
            font-size:16px;
            font-style:italic;
            font-weight:bold;
        }
    </style>

</asp:Content>
