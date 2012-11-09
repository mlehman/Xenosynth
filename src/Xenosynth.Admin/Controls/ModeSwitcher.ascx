<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ModeSwitcher.ascx.cs" Inherits="Xenosynth.Admin.Controls.ModeSwitcher" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<div class="actionPanel">
	<div class="title">Current Mode</div>
	<div class="body">
		<asp:DropDownList ID="DropDownListMode" Runat="server" 
			AutoPostBack="True"
			OnSelectedIndexChanged="DropDownListMode_OnSelectedIndexChanged"
			>
			<asp:ListItem Value="Unpublished">Unpublished</asp:ListItem>
			<asp:ListItem Value="Published">Published</asp:ListItem>
		</asp:DropDownList>
	</div>
</div>
