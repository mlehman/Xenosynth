<%@ Page language="c#" Codebehind="EditResource.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Resources.EditResource" %>
<%@ Register TagPrefix="Fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register TagPrefix="xs" TagName="Css" Src="~/StandardCss.ascx" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="ResourceTasks" Src="~/Resources/ResourceTasks.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
  <head>
    <title>Xenosynth - Edit Resource</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    
  </head>
  <body>
	
    <form id="Form1" method="post" runat="server">
		<Fluent:Template runat="server" id=TemplateLayout1 PageLayoutFile="~/StandardTemplate.ascx" >
			<Fluent:TemplateSection id="Side" runat="server">
				<xs:ResourceTasks runat="server" ID="ResourceTasks"/>	
			</Fluent:TemplateSection>
			
			<Fluent:TemplateSection id="Main" runat="server">
				<h1>Edit Resource</h1>
				
				<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
				<fieldset>
					<legend>Resource Properties</legend>
					<label for="TextBoxName">Name:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxName" MaxLength="250" /> <br />
					<label for="TextBoxCategory">Category:</label>  <asp:TextBox CssClass="input" Runat="server" Width="300"  MaxLength="250" ID="TextBoxCategory" /> <br />
					
					<label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
					<br />
					<span class="toolTip">A description of the resource.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
					<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="500" OutputControl="TextBoxDescriptionCount" ErrorMessage="Value can be no longer than 500 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
					<br />
					
					<label for="TextBoxValue">Value:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxValue" />
					<br />
					<span class="toolTip">The value of the resource.<asp:TextBox Runat="server" ID="TextBoxValueCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
					<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxValue" MaxLength="4000" OutputControl="TextBoxValueCount" ErrorMessage="Value can be no longer than 4000 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
					<br />
					
					<asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdateResource_OnClick" ID="Button1"/>
					<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
				</fieldset>
				
				<DataBinding:DataBindingManager ID="DataBindingManagerResource" runat="server">
					<DataBinding:TextBoxBinding ControlToBind="TextBoxName" DataMember="Name" />
					<DataBinding:TextBoxBinding ControlToBind="TextBoxCategory" DataMember="Category" />
					<DataBinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
					<DataBinding:TextBoxBinding ControlToBind="TextBoxValue" DataMember="Value" />
				</DataBinding:DataBindingManager>
			</Fluent:TemplateSection>
		</Fluent:Template>

     </form>
	
  </body>
</html>
