<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditUserRoles.aspx.cs" Inherits="Xenosynth.Admin.Security.EditUserRoles" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="UserTabControl" Src="~/Security/UserTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="UserTasks" Src="~/Security/UserTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchUsers" Src="~/Security/SearchUsers.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
    <xs:UserTasks ID="UserTasks1" runat="server" />
	<xs:SearchUsers ID="SearchUsers1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser ID="PathBrowser1" runat="server" 
	    RootPage="Default.aspx" 
	    RootPageName="Users" 
	    SubPage='<%# "EditUser.aspx?UserID=" + UserID %>' 
	    SubPageName='<%# CurrentUser.UserName %>'
	    />
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />

    <xs:UserTabControl ID="UserTabControl1" runat="server" Selected="Roles" />
    
    <div class="formPanel">
        <fieldset>
            <legend>Roles for User</legend>
            <asp:DataGrid ID="DataGridUserRoles" runat="Server">
                <Columns>
                    <asp:TemplateColumn HeaderText="Role">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="user action" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem", "EditRole.aspx?Role={0}") %>'><%# Container.DataItem %></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="User In Role?">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenFieldRole" Value="<%# Container.DataItem %>" />
                            <asp:CheckBox runat="server" ID="CheckBoxUserInRole"  Checked="<%# UserIsInRole((string)Container.DataItem) %>" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <div class="selectedActions" >
	         <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
	         </div>
	     </fieldset>
	</div>

</asp:Content>
