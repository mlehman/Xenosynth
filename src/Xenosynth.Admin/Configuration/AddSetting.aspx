<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddSetting.aspx.cs" Inherits="Xenosynth.Admin.Configuration.AddSetting" Title="Untitled Page" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="xs" TagName="SettingTasks" Src="~/Configuration/SettingTasks.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <fieldset>
		<legend>Setting Details</legend>
		
		<label for="TextBoxKey">Key:</label>  <asp:TextBox CssClass="input"  Runat="server" ID="TextBoxKey"  MaxLength="50"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error" Runat="server" ControlToValidate="TextBoxKey" Display="Dynamic" ErrorMessage="Required." />
		<span class="toolTip">The key used to reference the setting. </span>
		
		<label for="TextBoxCategory">Category:</label>  <asp:TextBox CssClass="input"  Runat="server" ID="TextBoxCategory"  MaxLength="50"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="error" Runat="server" ControlToValidate="TextBoxCategory" Display="Dynamic" ErrorMessage="Required." />
		<span class="toolTip">The category of the setting. </span>
		
		<label for="TextBoxName">Name:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxName"  MaxLength="50"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxName" Display="Dynamic" ErrorMessage="Required." />
		<span class="toolTip">The name of the setting. </span>
		
		<label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		<br />
		<span class="toolTip">A description of the use of the setting.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="500" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 500 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		<br />
	
		<label for="TextBoxType">Type:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxType"  MaxLength="150"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxType" Display="Dynamic" ErrorMessage="Required." />
		<span class="toolTip">The type of the setting.</span>
		
		<label for="TextBoxValue">Value:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxValue" />
		<br />
		<span class="toolTip">A value of the setting.<asp:TextBox Runat="server" ID="TextBoxValueCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxValue" MaxLength="500" OutputControl="TextBoxValueCount" ErrorMessage="Value can be no longer than 500 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
		<br />
		
		<asp:Button Runat="server" CssClass="submit" Text="Add &raquo;" OnClick="ButtonAddSetting_OnClick" />
		<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" />
	</fieldset>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
	<xs:SettingTasks runat="server" />
</asp:Content>
