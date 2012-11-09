<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditPermissionRoles.aspx.cs" Inherits="Xenosynth.Admin.Security.EditPermissionRoles" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PermissionTasks" Src="~/Security/PermissionTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PermissionTabControl" Src="PermissionTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:PathBrowser ID="PathBrowser1" runat="server" 
	    RootPage="Permissions.aspx" 
	    RootPageName="Permissions" 
	    SubPage='<%# "EditPermission.aspx?PermissionID=" + PermissionID %>' 
	    SubPageName='<%# CurrentPermission.Name %>'
	    />
    <xs:PermissionTabControl ID="PermissionTabControl" runat="server" PermissionID="<%# PermissionID %>" selected="Roles" />
    <div class="formPanel">
         <fieldset>
		        <legend>Roles</legend>
    		    
		        <asp:DataGrid ID="DataGridRoles" runat="Server">
                <Columns> 
                    <asp:TemplateColumn HeaderText="Role Has Permission?">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" CssClass="role action" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem", "EditRole.aspx?role={0}") %>'>
                                <%# Container.DataItem %>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Role Has Permission?">
                        <ItemTemplate>
                           <asp:HiddenField runat="server" ID="HiddenFieldRole" Value="<%# Container.DataItem %>" /> 
                           <asp:CheckBox runat="server" ID="CheckBoxRoleHasPermission"  Checked='<%# Xenosynth.Security.Permissions.RoleHasPermission((string)Container.DataItem, PermissionID) %>' />
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
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
     <xs:PermissionTasks ID="PermissionTasks1" runat="server" />
</asp:Content>
