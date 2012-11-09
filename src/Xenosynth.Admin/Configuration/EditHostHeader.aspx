<%@ Page language="c#" Codebehind="EditHostHeader.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Configuration.EditHostHeader" %>
<%@ Register TagPrefix="xs" TagName="Css" Src="~/StandardCss.ascx" %>
<%@ Register TagPrefix="Fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="ConfigurationTasks" Src="~/Configuration/ConfigurationTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
  <head>
    <title>Xenosynth - Edit Host Header Mapping</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    
  </head>
  <body >
	
    <form id="Form1" method="post" runat="server">
		<Fluent:Template runat="server" id=TemplateLayout1 PageLayoutFile="~/StandardTemplate.ascx" >
			<Fluent:TemplateSection id="Side" runat="server">
					
					<xs:ConfigurationTasks Runat="server" />
					
			</Fluent:TemplateSection>
			
			<Fluent:TemplateSection id="Main" runat="server">
				
				<h1>Edit Host Header Mapping</h1>
				
				<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
				<fieldset>
					<legend>Host Header Details</legend>
					<label for="TextBoxHostHeader">Host Header:</label>  <asp:TextBox CssClass="input" Runat="server" style="width: 250px;" ID="TextBoxHostHeader"  MaxLength="250"/> 
					<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxHostHeader" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator1" NAME="Requiredfieldvalidator1"/>
					<span class="toolTip">example: www.yoursite.com </span>
				
					<label for="DropDownListDirectories"> Directory:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListDirectories" /> 
					<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="DropDownListDirectories" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator2"/>
					<br />
					<span class="toolTip">The directory mapped to the host header.</span>
					
					
					<asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdateHostHeader_OnClick" ID="Button1"/>
					<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
				</fieldset>
				
				<DataBinding:DataBindingManager ID="DataBindingManagerHostHeader" runat="server">
					<DataBinding:TextBoxBinding ControlToBind="TextBoxHostHeader" DataMember="HostHeaderName" />
					<DataBinding:ListControlBinding ControlToBind="DropDownListDirectories" DataMember="CmsDirectoryID" />
				</DataBinding:DataBindingManager>
				
			</Fluent:TemplateSection>
		</Fluent:Template>

     </form>
	
  </body>
</html>