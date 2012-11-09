<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="WebParts.aspx.cs" Inherits="Xenosynth.Admin.Configuration.WebParts" Title="Untitled Page" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="WebPartTasks" Src="~/Configuration/WebPartTasks.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
        <xs:WebPartTasks ID="WebPartTasks1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <asp:DataGrid ID="DataGridWebParts" Runat="server"
		AutoGenerateColumns="False"
		DataKeyField="ID"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="Name">
				<ItemTemplate>
					<asp:HyperLink ToolTip="Edit Web Part" CssClass="action webpart" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditWebPart.aspx?WebPartID={0}") %>' ID="Hyperlink1"><%# DataBinder.Eval(Container.DataItem, "Title") %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Desription">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Description") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:HyperLink ToolTip="Edit Web Part" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditWebPart.aspx?WebPartID={0}") %>' ID="Hyperlink2"></asp:HyperLink>
					<asp:LinkButton ToolTip="Delete Web Part" CssClass="action delete" ID="DeleteButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridWebParts" ID="Emptyrepeaterpanel1" >
		<span class="warning">There are not any registered web parts.</span>
	</erp:EmptyRepeaterPanel>
</asp:Content>