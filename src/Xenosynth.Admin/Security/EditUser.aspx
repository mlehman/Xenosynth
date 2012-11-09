<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Xenosynth.Admin.Security.EditUser" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="UserTabControl" Src="~/Security/UserTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="UserTasks" Src="~/Security/UserTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchUsers" Src="~/Security/SearchUsers.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
    <xs:UserTasks ID="UserTasks1" runat="server" CurrentUser="<%# CurrentUser %>" />
	<xs:SearchUsers ID="SearchUsers1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser runat="server" 
	    RootPage="Default.aspx" 
	    RootPageName="Users" 
	    SubPage='<%# "EditUser.aspx?UserID=" + UserID %>' 
	    SubPageName='<%# CurrentUser.UserName %>'
	    /> 
    <xs:UserTabControl ID="UserTabControl1" runat="server" Selected="Properties" />
    <div class="formPanel">
	    <fieldset>
		    <legend>User Properties</legend>
		    <label for="TextBoxUserName">User Name:</label> <asp:TextBox CssClass="readOnly input" ReadOnly="true" Runat="server" ID="TextBoxUserName" /><br />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxUserName" Display="Dynamic" ErrorMessage="Required." />
		    
		    <label for="TextBoxEmail" class="required">Email:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxEmail" /> <br />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxEmail" Display="Dynamic" ErrorMessage="Required." />				
		    
		    <label for="TextBoxComment">Comments:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxComment" />
			<br />
			<span class="toolTip">Information for the user.<asp:TextBox Runat="server" ID="TextBoxCommentCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
			<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxComment" MaxLength="3000" OutputControl="TextBoxCommentCount" ErrorMessage="Description can be no longer than 3000 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
			<br />
			
			<asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsApproved" Text="Is Approved?" />
			<span class="toolTip">Whether the user can be authenticated.</span>
					
			<asp:CheckBox CssClass="checkbox readOnly" Enabled="false" Runat="server" ID="CheckBoxIsLockedOut" Text="Is Locked Out?" />
			
			<span class="toolTip">Whether the user is locked out and unable to be validated.</span>
    		
		    <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerUser" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxUserName"  DataMember="UserName" ReadOnly="true" />
		<databinding:TextBoxBinding ControlToBind="TextBoxEmail" DataMember="Email" />
		<databinding:TextBoxBinding ControlToBind="TextBoxComment" DataMember="Comment" />
		<DataBinding:CheckBoxBinding ControlToBind="CheckBoxIsApproved" DataMember="IsApproved" />
		<DataBinding:CheckBoxBinding ControlToBind="CheckBoxIsLockedOut" ReadOnly="true" DataMember="IsLockedOut" />
	</databinding:DataBindingManager>
</asp:Content>

