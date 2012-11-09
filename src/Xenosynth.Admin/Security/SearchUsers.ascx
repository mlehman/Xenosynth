<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchUsers.ascx.cs" Inherits="Xenosynth.Admin.Security.SearchUsers" %>
<%@ Register TagPrefix="apb" Namespace="Fluent"  Assembly="Fluent.AutoPostBack" %>
<div class="actionPanel">
	<div class="title">Search Users</div>
	<div class="body">
						
	<label for="TextBoxDisplayName"> User Name:</label><br />
	<asp:TextBox CssClass="input" Runat="server" ID="TextBoxUserName" />
	<apb:AutoPostBack runat="server" TextBoxSource="TextBoxUserName" ControlToPostBack="ButtonSearch" ID="Autopostback1" NAME="Autopostback1"/><br />
    
	<label for="TextBoxText"> Email:</label><br />
	<asp:TextBox CssClass="input" Runat="server" ID="TextBoxEmail" />
	<apb:AutoPostBack runat="server" TextBoxSource="TextBoxEmail" ControlToPostBack="ButtonSearch" ID="Autopostback2" NAME="Autopostback2"/><br />

	  <asp:Button Runat="server" CssClass="submit"
		Text="Search &raquo;" ID="ButtonSearch" OnClick="ButtonSearch_OnClick"/>
		
	</div>
</div>