<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SearchFiles.ascx.cs" Inherits="Xenosynth.Admin.Content.SearchFiles" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="apb" Namespace="Fluent"  Assembly="Fluent.AutoPostBack" %>
<div class="actionPanel">
	<div class="title">Search</div>
	<div class="body">
						
	<asp:TextBox CssClass="input" Runat="server" ID="TextBoxText" />
	<apb:AutoPostBack runat="server" TextBoxSource="TextBoxText" ControlToPostBack="ButtonSearch" ID="Autopostback2" NAME="Autopostback2"/><br />

	  <asp:Button Runat="server" CssClass="submit"
		Text="Search &raquo;" ID="ButtonSearch" OnClick="ButtonSearch_OnClick"/>
		
	</div>
</div>