<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="Xenosynth.Admin.Security.EditRole" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="RoleTasks" Src="~/Security/RoleTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="RoleTabControl" Src="RoleTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
 <xs:RoleTasks ID="RoleTasks1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:PathBrowser ID="PathBrowser1" runat="server" 
	    RootPage="Roles.aspx" 
	    RootPageName="Roles" 
	    SubPage='<%# "EditRole.aspx?Role=" + CurrentRole %>' 
	    SubPageName='<%# CurrentRole %>'
	    />
    <xs:RoleTabControl runat="server" role="<%# CurrentRole %>" selected="Users" />
    <div class="formPanel">
        <fieldset>
		        <legend>Users In Role</legend>
    		    
		        <asp:DataGrid ID="DataGridUsers" runat="Server">
                <Columns>
                    <asp:TemplateColumn HeaderText="User Name">
				    <ItemTemplate>
					    <asp:HyperLink ID="HyperLink1" Runat="server" CssClass="user action" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem", "EditUser.aspx?UserName={0}") %>' ><%# DataBinder.Eval(Container, "DataItem")%></asp:HyperLink>
				    </ItemTemplate>
			    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="User In Role?">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenFieldUser" Value="<%# Container.DataItem %>" />
                            <asp:CheckBox runat="server" ID="CheckBoxUserInRole"  Checked="True" />
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
