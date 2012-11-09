<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSites.ascx.cs" Inherits="Xenosynth.Admin.Controls.WebParts.WebSites" %>

<asp:DataGrid runat="server" ID="DataGridWebSites">
    <Columns>
	    <asp:TemplateColumn HeaderText="Name">
				<ItemTemplate>
					<asp:HyperLink ToolTip="View Site" CssClass="action site" Runat="server" NavigateUrl='<%# Eval("Url") %>' ID="Hyperlink1"><%# DataBinder.Eval(Container.DataItem, "Name") %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Description">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Description") %>
				</ItemTemplate>
			</asp:TemplateColumn>
    </Columns>
</asp:DataGrid>