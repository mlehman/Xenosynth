<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentFileTasks.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Controls.RecentFileTasks" %>
<div class="actionPanel">
	<div class="title">Recent Files</div>
	<div class="body">
		<asp:Repeater runat="server" ID="RepeaterRecentFiles" DataSource="<%# Files %>">
		    <ItemTemplate>
		        <asp:HyperLink ID="HyperLink1" runat="server" 
		            NavigateUrl='<%# Eval("Url") %>'
		            CssClass='<%# Eval("FileType.CssClass") + " action" %>'
		            ><%# Eval("Title") %></asp:HyperLink>
		    </ItemTemplate>
		</asp:Repeater>				
	</div>
</div>