<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShortcutTasks.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Shortcut.ShortcutTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<div class="actionPanel">
	<div class="title">Shortcut Tasks</div>
	<div class="body">
		<a class="view action"  href='<%= CurrentFile.Url %>' >View Shortcut</a>
		<a class="copy action" href="<%= ResolveUrl("~/Content/MoveFile.aspx") %>?FileID=<%= CurrentFile.ID %>">Copy/Move Shortcut</a>
	
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentFile %>" />
	</div>
</div>
