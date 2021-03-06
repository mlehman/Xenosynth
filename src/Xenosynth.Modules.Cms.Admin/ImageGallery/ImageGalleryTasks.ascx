<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ImageGalleryTasks.ascx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.ImageGalleryTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>

<div class="actionPanel">
	<div class="title">Image Gallery Tasks</div>
	<div class="body">
		<a class="imagegalleryBrowse action" title="Browse <%= CurrentGallery.FullPath %>" href="<%= ResolveUrl(CurrentGallery.FileType.BrowseUrl) %>?FileID=<%= CurrentGallery.ID %>">Browse this Gallery</a>
	    <a class="imagegalleryEdit action" title="Edit <%= CurrentGallery.FullPath %>" href="<%= ResolveUrl(CurrentGallery.FileType.EditUrl) %>?FileID=<%= CurrentGallery.ID %>">Edit this Gallery</a>
		<a class="copy action" href="<%= ResolveUrl("~/Content/MoveFile.aspx") %>?FileID=<%# CurrentGallery.ID %>">Copy/Move Gallery</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentGallery %>" />
	</div>
</div>

<div class="actionPanel">
	<div class="title">Create New</div>
	<div class="body">
	<asp:Repeater runat="server" ID="RepeaterCreateNew" DataSource="<%# CurrentGallery.FileType.AllowedFileTypes %>">
		    <ItemTemplate>
		        <asp:HyperLink ID="HyperLink1" runat="server" 
		            NavigateUrl='<%# Eval("CreateUrl") + "?FileID=" + CurrentGallery.ID %>'
		            CssClass='<%# Eval("CssClass") + "New action" %>'
		            >Create new <%# Eval("Name") %></asp:HyperLink>
		    </ItemTemplate>
		</asp:Repeater>
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1"  CurrentFile="<%# CurrentGallery %>"/>