<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectoryTasks.ascx.cs" Inherits="Xenosynth.Admin.Content.DirectoryTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>

<div class="actionPanel">
	<div class="title">Directory Tasks</div>
	<div class="body">
	    <a class="directoryBrowse action" title="Browse <%= CurrentDirectory.FullPath %>" href="<%= ResolveUrl(CurrentDirectory.FileType.BrowseUrl) %>?FileID=<%= CurrentDirectory.ID %>">Browse this Directory</a>
	    <a class="directoryEdit action" title="Edit <%= CurrentDirectory.FullPath %>" href="<%= ResolveUrl(CurrentDirectory.FileType.EditUrl) %>?FileID=<%= CurrentDirectory.ID %>">Edit this Directory</a>
		<a class="copy action" href="<%= ResolveUrl("../MoveFile.aspx") %>?FileID=<%# CurrentDirectory.ID %>">Copy/Move Directory</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentDirectory %>" />
	</div>
</div>
<div class="actionPanel">
	<div class="title">Create New</div>
	<div class="body">
	    <asp:Repeater runat="server" ID="RepeaterCreateNew" DataSource="<%# CurrentDirectory.FileType.AllowedFileTypes %>">
		    <ItemTemplate>
		        <asp:HyperLink ID="HyperLink1" runat="server" 
		            NavigateUrl='<%# Eval("CreateUrl") + "?FileID=" + CurrentDirectory.ID %>'
		            CssClass='<%# Eval("CssClass") + "New action" %>'
		            >Create new <%# Eval("Name") %></asp:HyperLink>
		    </ItemTemplate>
		</asp:Repeater>
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1" />
