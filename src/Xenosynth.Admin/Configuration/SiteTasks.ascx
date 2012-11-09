<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteTasks.ascx.cs" Inherits="Xenosynth.Admin.Configuration.SiteTasks" %>

<div class="actionPanel">
	<div class="title">Site Tasks</div>
	<div class="body">
	    <a class="sites action" href="Sites.aspx">View Sites</a>
		<!-- <a class="siteNew action" href="AddSite.aspx">Add Site</a> -->
		
		
		<% if (CurrentSite != null) { %>
		    <a class="action" href="FileExplorer.aspx">Explore File System</a>
		<% } %>
	</div>
</div>