<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogPostTasks.ascx.cs" Inherits="Xenosynth.Modules.Blog.Admin.BlogPostTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="~/Modules/Cms/Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="~/Modules/Cms/Controls/PublishingTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<div class="actionPanel">
	<div class="title">Blog Post Tasks</div>
	<div class="body">
		<a class="view action"  href='<%# CmsContext.Current.ResolveUrl((string)CurrentFile.FullPath) %>' >View Post</a>
		<a class="copy action" href="<%= ResolveUrl("~/Content/MoveFile.aspx") %>?FileID=<%# CurrentFile.ID %>">Copy/Move Post</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentFile %>" />
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1"  CurrentFile="<%# CurrentFile %>"/>