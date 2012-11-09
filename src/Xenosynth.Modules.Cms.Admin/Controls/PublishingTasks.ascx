<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublishingTasks.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Controls.PublishingTasks" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>
<asp:LinkButton CssClass="publish action" OnClick="Publish_OnClick" Visible="<%# CurrentFile.State != CmsState.Published %>" Runat="server">Publish <%# CurrentFile.FileType.Name %></asp:LinkButton>
<asp:LinkButton ID="LinkButton1" CssClass="unpublish action" OnClick="Unpublish_OnClick" Visible="<%# CurrentFile.State == CmsState.Published %>" Runat="server">Unpublish <%# CurrentFile.FileType.Name %></asp:LinkButton>
<asp:LinkButton ID="LinkButton2" CssClass="archive action" OnClick="Archive_OnClick" Visible="<%# CurrentFile.State != CmsState.Archived %>" Runat="server">Archive <%# CurrentFile.FileType.Name %></asp:LinkButton>
<asp:LinkButton ID="LinkButton3" CssClass="delete action" OnClick="Delete_OnClick" Runat="server">Delete <%# CurrentFile.FileType.Name %></asp:LinkButton>		