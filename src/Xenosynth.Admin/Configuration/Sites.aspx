<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Sites.aspx.cs" Inherits="Xenosynth.Admin.Configuration.Sites" Title="Untitled Page" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="SiteTasks" Src="~/Configuration/SiteTasks.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
        <xs:SiteTasks runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <asp:DataGrid ID="DataGridSites" Runat="server"
		AutoGenerateColumns="False"
		DataKeyField="ID"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="Name">
				<ItemTemplate>
					<asp:HyperLink ToolTip="Edit Site" CssClass="action site" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditSite.aspx?SiteID={0}") %>' ID="Hyperlink1"><%# DataBinder.Eval(Container.DataItem, "Name") %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Description">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Description") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:HyperLink ToolTip="Edit Site" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditSite.aspx?SiteID={0}") %>' ID="Hyperlink2"></asp:HyperLink>
					<asp:LinkButton ToolTip="Delete Site" CssClass="action delete" ID="DeleteSiteButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteSiteButton" Message='<%# DataBinder.Eval(Container.DataItem, "Name", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridSites" ID="Emptyrepeaterpanel1" >
		<span class="warning">There are not any sites.</span>
	</erp:EmptyRepeaterPanel>
</asp:Content>