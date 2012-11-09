<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ImageTasks.ascx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.ImageTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<div class="actionPanel">
	<div class="title">Image Tasks</div>
	<div class="body">
		<a class="view action"  href='<%# CmsContext.Current.ResolveUrl((string)CurrentFile.FullPath) %>' >View Image</a>
		<a class="copy action" href="<%= ResolveUrl("~/Content/MoveFile.aspx") %>?FileID=<%# CurrentFile.ID %>">Copy/Move Image</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentFile %>" />
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1"  CurrentFile="<%# CurrentFile %>"/>