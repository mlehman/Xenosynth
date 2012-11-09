<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateTasks.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.TemplateGallery.TemplateTasks" %>
<%@ Register TagPrefix="xs" TagName="PublishingTasks" Src="../Controls/PublishingTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>
<div class="actionPanel">
	<div class="title">Template Tasks</div>
	<div class="body">
	    <xs:PublishingTasks runat="server" CurrentFile="<%# CurrentTemplate %>" />
	</div>
</div>