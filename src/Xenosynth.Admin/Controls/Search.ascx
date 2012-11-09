<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="Xenosynth.Admin.Controls.Search" %>
<%@ Register TagPrefix="apb" Namespace="Fluent"  Assembly="Fluent.AutoPostBack" %>
<div class="actionPanel">
	<div class="title">Global Search</div>
	<div class="body">
						
	<label for="TextBoxDisplayName"> Search:</label>
	<asp:TextBox CssClass="input" Runat="server" ID="TextBoxTerms" />
	<apb:AutoPostBack runat="server" TextBoxSource="TextBoxTerms" ControlToPostBack="ButtonSearch" ID="Autopostback1"/><br />

	<asp:Button Runat="server" CssClass="submit"
		Text="Search &raquo;" ID="ButtonSearch" OnClick="ButtonSearch_OnClick"/>
		
	</div>
</div>