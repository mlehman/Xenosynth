<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PageTasks.ascx.cs" Inherits="Xenosynth.Admin.Content.PageTasks" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="xs" TagName="RecentFiles" Src="../Controls/RecentFileTasks.ascx" %>

<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>
<div class="actionPanel">
	<div class="title">Page Tasks</div>
	<div class="body">
		<a class="view action"  href='<%# CmsContext.Current.ResolveUrl((string)CurrentPage.FullPath) %>' >View Page</a>
		<a class="copy action" href="../MoveFile.aspx?FileID=<%# CurrentPage.ID %>">Copy/Move Page</a>
		<asp:LinkButton CssClass="publish action" OnClick="Publish_OnClick" Visible="<%# CurrentPage.State != CmsState.Published %>" Runat="server">Publish Page</asp:LinkButton>
		<asp:LinkButton CssClass="unpublish action" OnClick="Unpublish_OnClick" Visible="<%# CurrentPage.State == CmsState.Published %>" Runat="server">Unpublish Page</asp:LinkButton>
		<asp:LinkButton CssClass="archive action" OnClick="Archive_OnClick" Visible="<%# CurrentPage.State != CmsState.Archived %>" Runat="server">Archive Page</asp:LinkButton>
		
		<asp:LinkButton CssClass="delete action" OnClick="Delete_OnClick" Runat="server">Delete Page</asp:LinkButton>
	</div>
</div>

