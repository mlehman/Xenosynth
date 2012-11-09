<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditRolePermissions.aspx.cs" Inherits="Xenosynth.Admin.Security.EditRolePermissions" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="RoleTasks" Src="~/Security/RoleTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="RoleTabControl" Src="RoleTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:PathBrowser ID="PathBrowser1" runat="server" 
	    RootPage="Roles.aspx" 
	    RootPageName="Roles" 
	    SubPage='<%# "EditRole.aspx?Role=" + CurrentRole %>' 
	    SubPageName='<%# CurrentRole %>'
	    />
    <xs:RoleTabControl ID="RoleTabControl1" runat="server" role="<%# CurrentRole %>" selected="Permissions" />
    <div class="formPanel">
        <fieldset>
		        <legend>Permissions for Role</legend>
    		    
		        <asp:DataGrid ID="DataGridPermissions" runat="Server" DataKeyField="ID">
                <Columns> 
                    <asp:TemplateColumn HeaderText="Permission">
				        <ItemTemplate>
				             <asp:HyperLink ID="HyperLink1" runat="server" CssClass="permission action" NavigateUrl='<%# Eval("ID", "EditPermission.aspx?PermissionID={0}") %>'><%# Eval("Name") %></asp:HyperLink>
				        </ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderText="Category">
				        <ItemTemplate>
					        <%# Eval("Category")%>
				        </ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderText="Description">
				        <ItemTemplate>
					        <%# Eval("Description") %>
				        </ItemTemplate>
			        </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Role Has Permission?">
                        <ItemTemplate>
                           <asp:CheckBox runat="server" ID="CheckBoxRoleHasPermission"  Checked='<%# Xenosynth.Security.Permissions.RoleHasPermission(CurrentRole, (Guid)Eval("ID")) %>' />
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
    <xs:RoleTasks ID="RoleTasks1" runat="server" />
</asp:Content>
