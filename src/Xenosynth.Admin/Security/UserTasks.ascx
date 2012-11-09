<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserTasks.ascx.cs" Inherits="Xenosynth.Admin.Users.UserTasks" %>
<div class="actionPanel">
	<div class="title">User Tasks</div>
	<div class="body">
		<a class="users action" href="Default.aspx">View All Users</a>
		<a class="userNew action" href="AddUser.aspx">Create a New User</a>
		
		<% if (CurrentUser != null) { %>
		    <% if (CurrentUser.IsLockedOut) { %>
			    <asp:LinkButton  CssClass="userUnlock action" ID="LinkButton1" runat="server" OnClick="ButtonUnlock_OnClick">Unlock User</asp:LinkButton>
			<% } %>
		<% } %>
	</div>
</div>