<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="Search.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content.Search" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="SearchFiles.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls" Assembly="Xenosynth.Admin" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="./Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="apb" Namespace="Fluent"  Assembly="Fluent.AutoPostBack" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
		<xs:RecentFiles Runat="server" ID="RecentFiles1" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />

    <fieldset>
        <legend>Search Options</legend>
 
	    <label for="TextBoxText"> Containing Text:</label>
	    <asp:TextBox CssClass="input" Runat="server" ID="TextBoxText" />
	    <apb:AutoPostBack runat="server" TextBoxSource="TextBoxText" ControlToPostBack="ButtonSearch" ID="Autopostback2" NAME="Autopostback2"/><br />

	      <asp:Button Runat="server" CssClass="submit"
		    Text="Search &raquo;" ID="ButtonSearch" OnClick="ButtonSearch_OnClick"/>
    </fieldset>
	
	<% if(IsValidSearch){ %>
	
	<h2>Search Results</h2>
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
		OnDeleteCommand="DataGridFiles_OnDeleteCommand"
		>
		<Columns>
			<asp:TemplateColumn  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn" HeaderStyle-Wrap="false">
			    <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="Title">Title
						<asp:Image runat="server" ImageUrl='<%# "~/App_Themes/" + Theme + "/images/icon_" + DataGridAdapterFiles.GetSortOrder("Title") + ".gif" %>' /></asp:LinkButton>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:HyperLink Runat="server" 
					    Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					    CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  " action") %>' 
					    NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "Url") %>' 
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
				        <asp:HyperLink ID="HyperLink2" Runat="server" Title="Edit" 
				            CssClass='<%# "action " + DataBinder.Eval(Container.DataItem, "FileType.CssClass", "{0}") + "Edit" %>' 
				            NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}")  %>'  ></asp:HyperLink>
				        <asp:LinkButton ID="DeletePageButton" Title="Delete" Runat="server" CssClass="action delete" CommandName="Delete"></asp:LinkButton>
					    <ab:AlertButton ID="AlertButton1" runat="server" Control="DeletePageButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Delete {0}?") %>' DialogMode="Confirm" />
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
	                <asp:LinkButton Runat="server"  Title="Publish" CssClass="action publish" ID="ButtonPublishSelected" OnClick="ButtonPublishSelected_OnClick"> Publish </asp:LinkButton>
		            <ab:AlertButton runat="server" Control="ButtonPublishSelected" Message="Publish selected files?" DialogMode="Confirm" ID="Alertbutton3"/>
		            <asp:LinkButton Runat="server"  Title="Delete" CssClass="action delete" ID="ButtonDeleteSelected" OnClick="ButtonDeleteSelected_OnClick"> Delete </asp:LinkButton>
		            <ab:AlertButton runat="server" Control="ButtonDeleteSelected" Message="Permanently delete selected files?" DialogMode="Confirm" ID="Alertbutton4"/>
	            </fieldset>
	        </div>
	        <% } %>
	
	<erp:EmptyRepeaterPanel runat="server" control="DataGridFiles" >
		<span class="warning">Your search did not find any files.</span>
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
	
	<% } %>

	

</asp:Content>
