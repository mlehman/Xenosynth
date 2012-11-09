<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="Modules.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Configuration.Modules" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>

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
				
	<asp:DataGrid ID="DataGridModules" Runat="Server"
		AutoGenerateColumns="False"
		DataKeyField="ID"
		CellPadding="4"
		GridLines="None"
		AllowSorting="True"
		Width="100%"
		HeaderStyle-CssClass="gridHeader"
		AlternatingItemStyle-CssClass="altRow"
		OnDeleteCommand="DataGridModules_OnDeleteCommand"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="Name">
				<ItemTemplate>
					<asp:HyperLink Runat="server" CssClass="module action" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditModule.aspx?ModuleID={0}") %>'  ID="Hyperlink1"><%# DataBinder.Eval(Container.DataItem, "Name") %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Description">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Description") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Is Enabled?">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "IsEnabled") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:HyperLink Title="Edit Module Registration" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditModule.aspx?ModuleID={0}") %>'></asp:HyperLink>
					<asp:HyperLink Title="Configure Module" CssClass="action settings" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ConfigurationUrl") %>'></asp:HyperLink>
					<asp:LinkButton Title="Unregister Module" CssClass="action delete" ID="DeleteModuleButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteModuleButton" Message='<%# DataBinder.Eval(Container.DataItem, "Name", "Unregister module {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridModules" ID="Emptyrepeaterpanel1" NAME="Emptyrepeaterpanel1">
		<span class="warning">There are not any registered modules.</span>
	</erp:EmptyRepeaterPanel>
	
</asp:Content>