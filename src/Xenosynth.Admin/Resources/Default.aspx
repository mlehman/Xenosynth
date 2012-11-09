<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Resources._Default" %>
<%@ Register TagPrefix="Fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" TagName="Css" Src="~/StandardCss.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="ResourceTasks" Src="~/Resources/ResourceTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
  <head>
    <title>Xenosynth - Resources</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    
  </head>
  <body>
	
    <form id="Form1" method="post" runat="server">
		<Fluent:Template runat="server" id=TemplateLayout1 PageLayoutFile="~/StandardTemplate.ascx" >
			<Fluent:TemplateSection id="Side" runat="server">
					<xs:ResourceTasks runat="server" />
			</Fluent:TemplateSection>
			
			<Fluent:TemplateSection id="Main" runat="server">
				
				<h1>Manage Resources</h1>
				
				<asp:DataGrid ID="DataGridResources" Runat="Server"
					AutoGenerateColumns="False"
					DataKeyField="ID"
					CellPadding="4"
					AllowPaging="True"
					AllowSorting="True"
					GridLines="None"
					Width="100%"
					HeaderStyle-CssClass="gridHeader"
					AlternatingItemStyle-CssClass="altRow"
					OnDeleteCommand="DataGridResources_OnDeleteCommand"
					PageSize='<%# Fluent.Configuration.ConfigManager.Current["xenosynth.preferences.paging.pageSize"].Value %>'
					>
					<Columns>
						<asp:TemplateColumn HeaderText="Name" SortExpression="Name">
							<ItemTemplate>
								<asp:HyperLink Runat="server" title='<%# DataBinder.Eval(Container.DataItem, "Description") %>'  NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditResource.aspx?ResourceID={0}") %>'  ID="Hyperlink1"><%# DataBinder.Eval(Container.DataItem, "Name") %></asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Category" SortExpression="Category" ItemStyle-CssClass="directory action" HeaderText="Category" />
						<asp:BoundColumn DataField="Value" SortExpression="Value" HeaderText="Value" />
						<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
							<ItemTemplate>
								<asp:HyperLink Title="Edit Resource" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditResource.aspx?ResourceID={0}") %>' ID="Hyperlink2"></asp:HyperLink>
								<asp:LinkButton Title="Delete Resource" CssClass="action delete" ID="DeleteTemplateButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
								<ab:AlertButton runat="server" Control="DeleteTemplateButton" Message='<%# DataBinder.Eval(Container.DataItem, "Name", "Pernamently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
				
				<erp:EmptyRepeaterPanel runat="server" control="DataGridResources" ID="Emptyrepeaterpanel1" NAME="Emptyrepeaterpanel1">
					<span class="warning">There are not any resources.</span>
				</erp:EmptyRepeaterPanel>
				
				<dga:DataGridAdapter 
                  ID="DataGridAdapterResources" 
                  Runat="Server" 
                  DataGridToBind="DataGridResources" 
                  SortHistory="2" 
                  AutoDataBind="False"
                  HideEmptyPager="True"
                  OnDataGridBinding="DataGridAdapterResources_DataGridBinding"
                  />
				
			</Fluent:TemplateSection>
		</Fluent:Template>
     </form>
	
  </body>
</html>
