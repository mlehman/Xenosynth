<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogTasks.ascx.cs" Inherits="Xenosynth.Modules.Blog.Admin.BlogTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="~/Modules/Cms/Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="~/Modules/Cms/Controls/PublishingTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>

<div class="actionPanel">
	<div class="title">Blog Tasks</div>
	<div class="body">
	    <a class="view action"  href='<%# CmsContext.Current.ResolveUrl((string)CurrentBlog.FullPath) %>' >View Blog</a>
		<a class="blogBrowse action" title="Browse <%= CurrentBlog.FullPath %>" href="<%= ResolveUrl(CurrentBlog.FileType.BrowseUrl) %>?FileID=<%= CurrentBlog.ID %>">Browse Blog Posts</a>
	    <a class="blogEdit action" title="Edit <%= CurrentBlog.FullPath %>" href="<%= ResolveUrl(CurrentBlog.FileType.EditUrl) %>?FileID=<%= CurrentBlog.ID %>">Edit Blog Settings</a>
		<a class="copy action" href="<%= ResolveUrl("../MoveFile.aspx") %>?FileID=<%# CurrentBlog.ID %>">Copy/Move Blog</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentBlog %>" />
	</div>
</div>

<div class="actionPanel">
	<div class="title">Create New</div>
	<div class="body">
	<asp:Repeater runat="server" ID="RepeaterCreateNew" DataSource="<%# CurrentBlog.FileType.AllowedFileTypes %>">
		    <ItemTemplate>
		        <asp:HyperLink ID="HyperLink1" runat="server" 
		            NavigateUrl='<%# Eval("CreateUrl") + "?FileID=" + CurrentBlog.ID %>'
		            CssClass='<%# Eval("CssClass") + "New action" %>'
		            >Create new <%# Eval("Name") %></asp:HyperLink>
		    </ItemTemplate>
		</asp:Repeater>
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1"  CurrentFile="<%# CurrentBlog %>"/>