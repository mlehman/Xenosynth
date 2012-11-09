<%@ Control Language="c#" AutoEventWireup="false" Inherits="Xenosynth.Admin.Templates.StandardTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register TagPrefix="nav" Namespace="Fluent.Navigation" Assembly="Fluent.Navigation" %>
<%@ Register TagPrefix="xs" TagName="ModeSwitcher" Src="~/Controls/ModeSwitcher.ascx" %>
<%@ Register TagPrefix="xs" TagName="ModuleTasks" Src="~/Modules/ModuleTasks.ascx" %>
<div class="frame">
	<div class="header">
		<a href='<%= ResolveUrl("~/") %>'><div class="logo"><span>Xenosynth</span></div></a>
		<asp:Button CssClass="logout" Runat="server" Text="Log Out" CausesValidation="False" OnClick="ButtonLogOut_OnClick" ID="Button1" NAME="Button1"></asp:Button>
		<br class="clear" />
	</div>
	
	<nav:TabControl ID="TabControlNavigation" SelectedEnabled="true" runat="Server" CssClass="tabs" >
		<nav:Tab Text="Directories" NavigateUrl="~/Content"></nav:Tab>
		<nav:Tab Text="Media"  NavigateUrl="~/MediaGalleries"></nav:Tab>
		<nav:Tab Text="Modules" NavigateUrl="~/Modules"></nav:Tab>
		<nav:Tab Text="Templates" Visible="False" NavigateUrl="~/Templates"></nav:Tab>
		<nav:Tab Text="Resources" Visible="False" NavigateUrl="~/Resources"></nav:Tab>
		<nav:Tab Text="Tools" Visible="False" NavigateUrl="~/Tools"></nav:Tab>
		<nav:Tab Text="Configuration" Visible="False" NavigateUrl="~/Configuration"></nav:Tab>
	</nav:TabControl>
	
	<div class="middle">
		<div class="side">
			<xs:ModuleTasks runat="server" ID="ModuleTasks1"/>	
			<fluent:TemplateSectionHolder runat="server" ID="Side" />
			<%-- <xs:ModeSwitcher runat="server" /> --%>
		</div>
		<div class="main">
			<fluent:TemplateSectionHolder runat="server" ID="Main" />
		</div>
		<div class="footer">
			<div class="copyright">
				Copyright 2004 Fluent Consulting
			</div>
			<div class="poweredby">
				<img runat="server" src="../images/PoweredByAsp.Net.gif" ID="Img1"/>
				<img runat="server" src="../images/PoweredByInform.gif" ID="Img2"/>
			</div>
			<br class="clear" />
		</div>
	</div>
	
	
</div>