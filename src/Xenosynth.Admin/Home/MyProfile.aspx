<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="Xenosynth.Admin.MyProfile" Title="Untitled Page" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="ProfileTasks" Src="~/Home/ProfileTasks.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <fieldset>
	    <legend>My Profile</legend>
	    <label for="TextBoxUserName">User Name:</label> <asp:TextBox CssClass="readOnly input" ReadOnly="true" Runat="server" ID="TextBoxUserName" /><br />
	    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxUserName" Display="Dynamic" ErrorMessage="Required." />
	    
	    <label for="TextBoxEmail" class="required">Email:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxEmail" /> <br />
	    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxEmail" Display="Dynamic" ErrorMessage="Required." />				
	    
	    <label for="TextBoxComment">Comments:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxComment" />
		<br />
		<span class="toolTip">Information for the user.<asp:TextBox Runat="server" ID="TextBoxCommentCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxComment" MaxLength="3000" OutputControl="TextBoxCommentCount" ErrorMessage="Description can be no longer than 3000 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
		<br />
		
	    <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
	    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
    </fieldset>
    <databinding:DataBindingManager ID="DataBindingManagerUser" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxUserName"  DataMember="UserName" ReadOnly="true" />
		<databinding:TextBoxBinding ControlToBind="TextBoxEmail" DataMember="Email" />
		<databinding:TextBoxBinding ControlToBind="TextBoxComment" DataMember="Comment" />
	</databinding:DataBindingManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:ProfileTasks runat="server" />
</asp:Content>
