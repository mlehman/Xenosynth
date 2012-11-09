<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Xenosynth.Admin.Security.AddUser" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="UserTabControl" Src="~/Security/UserTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="UserTasks" Src="~/Security/UserTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchUsers" Src="~/Security/SearchUsers.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:UserTabControl ID="UserTabControl1" runat="server" Selected="Properties" />
    <div class="formPanel">
	    <fieldset>
		    <legend>User Properties</legend>
		    <label for="TextBoxUserName">User Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxUserName" />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxUserName" Display="Dynamic" ErrorMessage="Required." />
		    <br />
		    
		    <label for="TextBoxPassword" class="required">Password:</label>  <asp:TextBox CssClass="input" TextMode="Password" Runat="server" ID="TextBoxPassword" /> 
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error" Runat="server" ControlToValidate="TextBoxPassword" Display="Dynamic" ErrorMessage="Required." />	<br />			
		    <span class="toolTip">Password must be <%= Membership.MinRequiredPasswordLength %> characters and contain <%= Membership.MinRequiredNonAlphanumericCharacters %> non alphanumeric characters.</span>
		    
		    <label for="TextBoxEmail" class="required">Email:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxEmail" />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxEmail" Display="Dynamic" ErrorMessage="Required." /> <br />				
		    
		    <label for="TextBoxComment">Comments:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxComment" />
			<br />
			<span class="toolTip">Information for the user.<asp:TextBox Runat="server" ID="TextBoxCommentCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
			<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxComment" MaxLength="3000" OutputControl="TextBoxCommentCount" ErrorMessage="Description can be no longer than 3000 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
			<br />
			
			<asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsApproved" Text="Is Approved?" />
			<span class="toolTip">Whether the user can be authenticated.</span>
					
    		
		    <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerUser" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxComment" DataMember="Comment" />
		<DataBinding:CheckBoxBinding ControlToBind="CheckBoxIsApproved" DataMember="IsApproved" />
	</databinding:DataBindingManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:UserTasks ID="UserTasks1" runat="server" />
	<xs:SearchUsers ID="SearchUsers1" runat="server" />
</asp:Content>
