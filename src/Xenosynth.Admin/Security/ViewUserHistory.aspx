<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewUserHistory.aspx.cs" Inherits="Xenosynth.Admin.Security.ViewUserHistory" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="UserTabControl" Src="~/Security/UserTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="UserTasks" Src="~/Security/UserTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchUsers" Src="~/Security/SearchUsers.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser ID="PathBrowser1" runat="server" 
	    RootPage="Default.aspx" 
	    RootPageName="Users" 
	    SubPage='<%# "EditUser.aspx?UserID=" + UserID %>' 
	    SubPageName='<%# CurrentUser.UserName %>'
	    /> 
    <xs:UserTabControl ID="UserTabControl1" runat="server" UserID="<%# UserID %>" Selected="History" />
    <div class="formPanel">
        <fieldset>
        <legend>Account Activity</legend>
            <label>Last Login:</label> <input class="input" readonly="true" value="<%= CurrentUser.LastLoginDate %>" /><br />
            <label>Last Activity:</label> <input class="input" readonly="true" value="<%= CurrentUser.LastActivityDate %>" /><br />
            <label>Last Lockout:</label> <input class="input" readonly="true" value="<%= CurrentUser.LastLockoutDate %>" /><br />
            <label>Last Password Change:</label> <input class="input" readonly="true" value="<%= CurrentUser.LastPasswordChangedDate %>" /><br />
        </fieldset>
    
        <h2>Event History</h2>
	    <asp:DataGrid ID="DataGridAuditLog" Runat="Server" 
		    AutoGenerateColumns="False"
		    GridLines="None"
		    DataKeyField="ID"
		    CssClass="grid"
		    Width="100%"
		    HeaderStyle-CssClass="gridHeader"
		    AlternatingItemStyle-CssClass="altRow"
		    PageSize='<%# Xenosynth.XenosynthContext.Current.Configuration["xenosynth.preferences.paging.pageSize"].Value %>'
		    AllowPaging="true"
		    >
		    <Columns>
			    <asp:BoundColumn DataField="EventDate" HeaderText="Event Date" SortExpression="EventDate" DataFormatString="{0:g}" HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn"/>
			    <asp:BoundColumn DataField="EventName" HeaderText="Event" SortExpression="EventName" />
			    <asp:BoundColumn DataField="Username" HeaderText="Username" SortExpression="Username" />
			    <asp:BoundColumn DataField="IP" HeaderText="IP" SortExpression="IP" />
			    <asp:BoundColumn DataField="Detail" HeaderText="Detail" SortExpression="Detail" />
		    </Columns>
	    </asp:DataGrid>
	    <erp:EmptyRepeaterPanel runat="server" control="DataGridAuditLog" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		    <span class="warning">This user has no activity in the last 30 days.</span>
	    </erp:EmptyRepeaterPanel>
	    <dga:DataGridAdapter 
              ID="DataGridAdapterAuditLog" 
              Runat="Server" 
              DataGridToBind="DataGridAuditLog" 
              SortHistory="1" 
              AutoDataBind="False"
              HideEmptyPager="True"
              OnDataGridBinding="DataGridAdapterAuditLog_DataGridBinding"
              />
	</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:UserTasks ID="UserTasks1" runat="server" CurrentUser="<%# CurrentUser %>" />
	<xs:SearchUsers ID="SearchUsers1" runat="server" />
</asp:Content>
