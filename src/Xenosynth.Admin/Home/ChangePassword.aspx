<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Xenosynth.Admin.Home.ChangePassword" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="ProfileTasks" Src="~/Home/ProfileTasks.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <fieldset>
        <legend>Change Password</legend>
        <asp:ChangePassword runat="server">
            <ChangePasswordTemplate>
                <label for="CurrentPassword">Current Password:</label> <asp:TextBox CssClass="readOnly input" Runat="server" ID="CurrentPassword" TextMode="Password" />
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="CurrentPassword" Display="Dynamic" ErrorMessage="Required." /><br />
    		    
		        <label for="NewPassword">New Password:</label> <asp:TextBox CssClass="readOnly input" Runat="server" ID="NewPassword" TextMode="Password"  />
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="NewPassword" Display="Dynamic" ErrorMessage="Required." /><br />
    		    
		        <label for="ConfirmPassword">Confirm Password:</label> <asp:TextBox CssClass="readOnly input" Runat="server" ID="ConfirmPassword" TextMode="Password" />
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error" Runat="server" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Required." />
		        <asp:CompareValidator CssClass="error" Runat="server" ControlToValidate="ConfirmPassword" ControlToCompare="NewPassword" Operator="Equal" Display="Dynamic" ErrorMessage="Passwords do not match." /><br />
            
                <asp:Button Runat="server" CssClass="submit" Text="Change Password &raquo;" CommandName="ChangePassword" ID="Button1"/>
		        <asp:Button ID="Button2" Runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False"/><br />
    		    
		        <br />
		        <asp:Label CssClass="error" Runat="server"  id="FailureText" />
            </ChangePasswordTemplate>
            <SuccessTemplate>
                Your password has been successfully changed.
            </SuccessTemplate>
        </asp:ChangePassword>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:ProfileTasks runat="server" />
</asp:Content>
