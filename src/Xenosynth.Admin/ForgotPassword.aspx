<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Xenosynth.Admin.ForgotPassword" %>
<%@ Register TagPrefix="cf" Namespace="Fluent.ControlFocus"  Assembly="Fluent.ControlFocus" %>
<%@ Register TagPrefix="apb" Namespace="Fluent"  Assembly="Fluent.AutoPostBack" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="HEAD1" runat="server">
		<title>Xenosynth - Forgot Password</title>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="login">
				<div class="logo"><span>Xenosynth</span></div>
				<fieldset>
					<legend>Password Recovery</legend>
					
					<asp:PasswordRecovery runat="server">
					    <UserNameTemplate>
					        <p>Enter your username to receive your password.</p>
					        <label>Username</label><asp:TextBox CssClass="input" Runat="server" Width="110" ID="Username" />
					        <br />
					        <cf:ControlFocus ID="ControlFocus1" runat="server" Control="Username" />
        					
					        <asp:Button ID="Submit" CssClass="submit" Runat="server" CommandName="Submit" Text="Submit »" /><br />
					        <apb:AutoPostBack ID="AutoPostBack1" runat="server" TextBoxSource="Username" ControlToPostBack="Submit" /><br />
					        
					        <br />
					        <asp:Label CssClass="error" Runat="server"  id="FailureText" />
					    </UserNameTemplate>
					    
					</asp:PasswordRecovery>
					
					<br />
					<a href="Login.aspx">Return to login.</a>
				</fieldset>
			</div>
		</form>
	</body>
</HTML>
