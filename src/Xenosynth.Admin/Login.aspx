<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Login" %>
<%@ Register TagPrefix="cf" Namespace="Fluent.ControlFocus"  Assembly="Fluent.ControlFocus" %>
<%@ Register TagPrefix="apb" Namespace="Fluent"  Assembly="Fluent.AutoPostBack" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Xenosynth - Login</title>
		<link runat="server" href="~/Css/style.css" type="text/css" rel="Stylesheet" />
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="login">
				<div class="logo"><span>Xenosynth</span></div>
				<fieldset>
					<legend>Please Log In</legend>
					<label>Username</label><asp:TextBox CssClass="input" Runat="server" Width="110" ID="TextBoxUsername" />
					<br />
					<cf:ControlFocus runat="server" Control="TextBoxUsername" />
					
					<label>Password</label><asp:TextBox CssClass="input" Runat="server" Width="110" ID="TextBoxPassword" TextMode="Password" />
					<br />
					
					<asp:CheckBox Runat="server" CssClass="checkbox" ID="CheckBoxPersist" Text="Remember me" /><br />
					<asp:Button ID="ButtonLogin" CssClass="submit" Runat="server" OnClick="ButtonLogin_OnClick" Text="Login »" /><br />
					<apb:AutoPostBack runat="server" TextBoxSource="TextBoxPassword" ControlToPostBack="ButtonLogin" /><br />
					<asp:Label CssClass="error" ID="LabelError" Runat="server" EnableViewState="False" />
					
					<br />
					<a href="ForgotPassword.aspx">Forgot password?</a>
					
				</fieldset>
			</div>
		</form>
	</body>
</HTML>
