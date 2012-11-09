<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Xenosynth.Admin.Security.ViewRoles" Title="Untitled Page" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="RoleTasks" Src="~/Security/RoleTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:RoleTasks runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />

    <asp:DataGrid ID="DataGridRoles" runat="Server" 
        AllowPaging="false"
        OnDeleteCommand="DataGridRoles_OnDelete"
        >
            <Columns>
                <asp:TemplateColumn HeaderText="Role">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" CssClass="role action" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem", "EditRole.aspx?role={0}") %>'>
                            <%# Container.DataItem %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
				    <asp:HyperLink  runat="server" CssClass="edit action" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem", "EditRole.aspx?role={0}") %>'></asp:HyperLink>
					<asp:LinkButton Title="Delete Role" CssClass="action delete" ID="DeleteTemplateButton" Runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItem %>" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteTemplateButton" Message='<%# DataBinder.Eval(Container, "DataItem", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
</asp:Content>

