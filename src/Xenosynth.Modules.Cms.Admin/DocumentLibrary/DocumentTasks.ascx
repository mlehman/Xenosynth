<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentTasks.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.DocumentTasks" %>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<div class="actionPanel">
	<div class="title">Document Tasks</div>
	<div class="body">
		<a class="view action"  href='<%# CmsContext.Current.ResolveUrl((string)CurrentFile.FullPath) %>' >View Document</a>
		<asp:LinkButton runat="server" OnClick="LinkButtonDownload_OnClick" CssClass="download action" >Download Document</asp:LinkButton>
		<a class="copy action" href="<%= ResolveUrl("~/Content/MoveFile.aspx") %>?FileID=<%# CurrentFile.ID %>">Copy/Move Document</a>
		<xs:PublishingTasks runat="server" CurrentFile="<%# CurrentFile %>" />
	</div>
</div>

<xs:RecentFiles Runat="server" ID="RecentFiles1"  CurrentFile="<%# CurrentFile %>"/>