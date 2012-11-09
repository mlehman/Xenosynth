<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Cache.aspx.cs" Inherits="Xenosynth.Admin.Configuration.Cache" Title="Untitled Page" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <asp:DataGrid ID="DataGridFileCache" Runat="Server"
		AutoGenerateColumns="False"
		CellPadding="4"
		GridLines="None"
		AllowSorting="True"
		Width="100%"
		HeaderStyle-CssClass="gridHeader"
		AlternatingItemStyle-CssClass="altRow"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="Name">
				<ItemTemplate>
					<%# Eval("Name") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Files">
				<ItemTemplate>
					<%# GetFileCount((System.IO.DirectoryInfo)Container.DataItem)%>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Size">
				<ItemTemplate>
					<%# GetTotalSize((System.IO.DirectoryInfo)Container.DataItem) / 1024 %> KB
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:LinkButton Title="Clear Cache" CssClass="action delete" ID="DeleteCacheButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteCacheButton" Message='<%# DataBinder.Eval(Container.DataItem, "Name", "Clear cache {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridFileCache" ID="Emptyrepeaterpanel1" NAME="Emptyrepeaterpanel1">
		<span class="warning">The cache is empty.</span>
	</erp:EmptyRepeaterPanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
</asp:Content>
