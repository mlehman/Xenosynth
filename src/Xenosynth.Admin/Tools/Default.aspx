<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Tools._Default" %>
<%@ Register TagPrefix="Fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" TagName="Css" Src="~/StandardCss.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="ToolTasks" Src="~/Tools/ToolTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
  <head>
    <title>Xenosynth - Tools</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    
  </head>
  <body>
	
    <form id="Form1" method="post" runat="server">
		<Fluent:Template runat="server" id=TemplateLayout1 PageLayoutFile="~/StandardTemplate.ascx" >
			<Fluent:TemplateSection id="Side" runat="server">
					<xs:ToolTasks runat="server" />
			</Fluent:TemplateSection>
			
			<Fluent:TemplateSection id="Main" runat="server">
				
				<h1>Tools</h1>
				
				
			</Fluent:TemplateSection>
		</Fluent:Template>
     </form>
	
  </body>
</html>
