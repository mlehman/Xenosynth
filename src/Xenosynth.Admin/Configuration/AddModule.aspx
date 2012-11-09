<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="AddModule.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Configuration.AddModule" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ContentPlaceHolderID="Side" runat="server" >					
		<div class="actionPanel">
			<div class="title">Module Tasks</div>
			<div class="body">
			    <a class="module action" href="Modules.aspx">View Modules</a>
				<a class="moduleRegister action" href="AddModule.aspx">Register Module</a>
			</div>
		</div>	
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server" >	
	
	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<fieldset>
		<legend>Module Details</legend>
		<label for="TextBoxName">Name:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxName"  MaxLength="250"/>
		<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxName" Display="Dynamic" ErrorMessage="Required." />
		<span class="toolTip">The name of the module. (example: My Module) </span>
		
		<label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		<br />
		<span class="toolTip">A description of the page for search engines.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="500" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 500 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		<br />
	
		<label for="TextBoxResourceFolder">Resource Folder:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxResourceFolder"  MaxLength="50"/>
		<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxResourceFolder" Display="Dynamic" ErrorMessage="Required." />
		<span class="toolTip">The name of the folder where files are located.</span>
		
		<label for="TextBoxClassName">Class Name:</label>  <asp:TextBox CssClass="input" style="width: 500px;" Runat="server" ID="TextBoxClassName"  MaxLength="100"/>
		<span class="toolTip">The name of the module class if implemented. (ex: Type, Assembly)</span>
		
		<label for="TextBoxInitOrder">Init Order:</label>  <asp:TextBox CssClass="input" style="width: 30px;" Runat="server" ID="TextBoxInitOrder"  MaxLength="3"/>
		<span class="toolTip">The order in which the module will be initialized.</span>
		
		<asp:Button Runat="server" CssClass="submit" Text="Add &raquo;" OnClick="ButtonAddModule_OnClick" ID="Button1"/>
		<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" ID="Button2" NAME="Button2"/>
	</fieldset>
	
	<DataBinding:DataBindingManager ID="DataBindingManagerModule" runat="server">
		<DataBinding:TextBoxBinding ControlToBind="TextBoxName" DataMember="Name" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxResourceFolder" DataMember="ResourceFolder" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxClassName" DataMember="ClassName" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxInitOrder" DataMember="InitOrder" />
	</DataBinding:DataBindingManager>
	
</asp:Content>
