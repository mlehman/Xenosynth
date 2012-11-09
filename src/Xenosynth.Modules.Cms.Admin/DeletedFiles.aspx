<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="DeletedFiles.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DeletedFiles" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="SearchFiles.ascx" %>
<%@ Register TagPrefix="xs" TagName="RecentFileTasks" Src="./Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls" Assembly="Xenosynth.Admin" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
		<xs:SearchFiles ID="SearchFiles1" runat="server" />
		<xs:RecentFileTasks ID="RecentFileTasks" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	
	<asp:DataGrid ID="DataGridFiles" Runat="Server"
		AutoGenerateColumns="False"
		AllowPaging="True"
		PagerStyle-Mode="NumericPages"
		AllowSorting="True"
		DataKeyField="ID"
		CssClass="grid"
		GridLines="None"
		Width="100%"
		HeaderStyle-CssClass="gridHeader"
		AlternatingItemStyle-CssClass="altRow"
		OnItemCommand="DataGridFiles_OnItemCommand"
		>
		<Columns>
			<asp:TemplateColumn  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn" HeaderStyle-Wrap="false">
			    <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="Title">Title
						<asp:Image runat="server" ImageUrl='<%# "~/App_Themes/" + Theme + "/images/icon_" + DataGridAdapterFiles.GetSortOrder("Title") + ".gif" %>' /></asp:LinkButton>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:HyperLink Runat="server" 
					    Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					    CssClass='<%# Eval("FileType.CssClass") + "Edit action" %>' 
					    NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") %>'
					    ><%# RestrictedLengthColumn.FormatRestrictedLength(DataBinder.Eval(Container.DataItem, "Title"), 35, "...", false)%></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			
			<asp:TemplateColumn  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
			    <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="FileName">File Name
						<asp:Image runat="server" ImageUrl='<%# "~/App_Themes/" + Theme + "/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileName") + ".gif" %>' /></asp:LinkButton>
				</HeaderTemplate>
				<ItemTemplate><%# DataBinder.Eval(Container.DataItem, "FileName") %></ItemTemplate>
			</asp:TemplateColumn>
			
			<asp:TemplateColumn HeaderText="Directory">
				<ItemTemplate>
					<asp:HyperLink Runat="server" Title="Browse Directory" CssClass="directory action" 
					    NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ParentDirectory.FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ParentDirectory.ID", "?FileID={0}")  %>' 
					><%# DataBinder.Eval(Container.DataItem, "ParentDirectory.FullPath") %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			
			<asp:TemplateColumn  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
			    <HeaderTemplate><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="FileTypeID">Type
						<asp:Image  runat="server" ImageUrl='<%# "~/App_Themes/" + Theme + "/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileTypeID") + ".gif" %>' /></asp:LinkButton>
				</HeaderTemplate>
				<ItemTemplate><%# DataBinder.Eval(Container.DataItem, "FileType.Name") %></ItemTemplate>
			</asp:TemplateColumn>
			
			<asp:TemplateColumn HeaderStyle-CssClass="versionColumn" ItemStyle-CssClass="versionColumn">
			    <HeaderTemplate ><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="Version">Version
						<asp:Image ID="Image1"  runat="server" ImageUrl='<%# "~/App_Themes/" + Theme + "/images/icon_" + DataGridAdapterFiles.GetSortOrder("Version") + ".gif" %>' /></asp:LinkButton>
				</HeaderTemplate>
				<ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Version") %></ItemTemplate>
			</asp:TemplateColumn>
			
			<asp:TemplateColumn HeaderStyle-CssClass="stateColumn" ItemStyle-CssClass="stateColumn">
			    <HeaderTemplate><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="State">State
						<asp:Image  runat="server" ImageUrl='<%# "~/App_Themes/" + Theme + "/images/icon_" + DataGridAdapterFiles.GetSortOrder("State") + ".gif" %>' /></asp:LinkButton>
				</HeaderTemplate>
				<ItemTemplate><%# DataBinder.Eval(Container.DataItem, "State") %></ItemTemplate>
			</asp:TemplateColumn>
			
			<asp:BoundColumn DataField="DateModified" SortExpression="DateModified"  HeaderText="Modified" DataFormatString="{0:d}" HeaderStyle-CssClass="orderColumn" ItemStyle-CssClass="orderColumn"/>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn">
				<ItemTemplate>
				    <nobr>
				        <asp:LinkButton ID="RestoreButton" Title="Restore" Runat="server" CssClass="action restore" CommandName="Restore"></asp:LinkButton>
				        <asp:HyperLink ID="HyperLink2" Runat="server" Title="Edit" 
				            CssClass='<%# "action " + DataBinder.Eval(Container.DataItem, "FileType.CssClass", "{0}") + "Edit" %>' 
				            NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}")  %>'  ></asp:HyperLink>
				        <asp:LinkButton ID="DeleteButton" Title="Delete" Runat="server" CssClass="action delete" CommandName="Delete"></asp:LinkButton>
					    <ab:AlertButton ID="AlertButton1" runat="server" Control="DeleteButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Permanently Delete {0}?") %>' DialogMode="Confirm" />
				    </nobr>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Select" HeaderStyle-CssClass="selectColumn" ItemStyle-CssClass="selectColumn">
			    <HeaderTemplate>
			        <input type="checkbox" onclick="javascript:toggleCheckBoxes(this);" />
			    </HeaderTemplate>
				<ItemTemplate> 
					<asp:CheckBox ID="CheckBoxSelect" Runat="server" name="selected" />
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	
	 <% if(DataGridFiles.Visible) { %>
	        <div class="selectedActions" >
	            <fieldset style="display: inline;">
	                <legend>Selected Files</legend>
	                <asp:LinkButton Runat="server"  Title="Restore" CssClass="action restore" ID="ButtonRestoreSelected" OnClick="ButtonRestoreSelected_OnClick"> Restore </asp:LinkButton>
		            <ab:AlertButton runat="server" Control="ButtonRestoreSelected" Message="Restore selected files?" DialogMode="Confirm" ID="Alertbutton3"/>
		            <asp:LinkButton Runat="server"  Title="Delete" CssClass="action delete" ID="ButtonDeleteSelected" OnClick="ButtonDeleteSelected_OnClick"> Delete </asp:LinkButton>
		            <ab:AlertButton runat="server" Control="ButtonDeleteSelected" Message="Permanently delete selected files?" DialogMode="Confirm" ID="Alertbutton4"/>
	            </fieldset>
	        </div>
	        <% } %>
	
	<erp:EmptyRepeaterPanel runat="server" control="DataGridFiles" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		<span class="warning">There are not any deleted files.</span>
	</erp:EmptyRepeaterPanel>
	
	<dga:DataGridAdapter 
      ID="DataGridAdapterFiles" 
      Runat="Server" 
      DataGridToBind="DataGridFiles" 
      SortHistory="1" 
      AutoDataBind="False"
      BaseTypeName="Xenosynth.Web.UI.CmsFile,Xenosynth.Modules.Cms"
      HideEmptyPager="True"
      OnDataGridBinding="DataGridAdapterFiles_DataGridBinding"
      />

</asp:Content>

