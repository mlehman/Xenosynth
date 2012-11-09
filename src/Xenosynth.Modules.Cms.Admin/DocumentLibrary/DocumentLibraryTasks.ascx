<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentLibraryTasks.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.DocumentLibraryTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>

<div class="actionPanel">
	<div class="title">Document Library Tasks</div>
	<div class="body">
		<a class="documentlibraryBrowse action" title="Browse <%= CurrentLibrary.FullPath %>" href="<%= ResolveUrl(CurrentLibrary.FileType.BrowseUrl) %>?FileID=<%= CurrentLibrary.ID %>">Browse this Library</a>
	    <a class="documentlibraryEdit action" title="Edit <%= CurrentLibrary.FullPath %>" href="<%= ResolveUrl(CurrentLibrary.FileType.EditUrl) %>?FileID=<%= CurrentLibrary.ID %>">Edit this Library</a>
		<a class="copy action" href="<%= ResolveUrl("../MoveFile.aspx") %>?FileID=<%# CurrentLibrary.ID %>">Copy/Move Library</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentLibrary %>" />
	</div>
</div>

<div class="actionPanel">
	<div class="title">Create New</div>
	<div class="body">
	<asp:Repeater runat="server" ID="RepeaterCreateNew" DataSource="<%# CurrentLibrary.FileType.AllowedFileTypes %>">
		    <ItemTemplate>
		        <asp:HyperLink ID="HyperLink1" runat="server" 
		            NavigateUrl='<%# Eval("CreateUrl") + "?FileID=" + CurrentLibrary.ID %>'
		            CssClass='<%# Eval("CssClass") + "New action" %>'
		            >Create new <%# Eval("Name") %></asp:HyperLink>
		    </ItemTemplate>
		</asp:Repeater>
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1"  CurrentFile="<%# CurrentLibrary %>"/>