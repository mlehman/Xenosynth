<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="Xenosynth.Admin.Security.ViewPermissions" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PermissionTasks" Src="PermissionTasks.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
    <xs:PermissionTasks ID="PermissionTasks1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <asp:DataGrid ID="DataGridPermissions" Runat="Server"
		DataKeyField="ID"
		OnDeleteCommand="DataGridPermissions_OnDeleteCommand"
		>
		<Columns>
		    <asp:TemplateColumn HeaderText="Permission">
				<ItemTemplate>
				    <asp:HyperLink ID="HyperLink1"  Runat="server" CssClass="permission action" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditPermission.aspx?PermissionID={0}") %>' >
					    <%# DataBinder.Eval(Container.DataItem, "Name")%>
					</asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Category">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Category") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Description">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Description")%>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:HyperLink Title="Edit Permission" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditPermission.aspx?PermissionID={0}") %>' ></asp:HyperLink>
					<asp:LinkButton Title="Delete Permission" CssClass="action delete" ID="DeleteButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteButton" Message='<%# DataBinder.Eval(Container.DataItem, "Name", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	
	<erp:EmptyRepeaterPanel runat="server" control="DataGridPermissions" ID="Emptyrepeaterpanel1" NAME="Emptyrepeaterpanel1">
					<span class="warning">There are not any permissions.</span>
				</erp:EmptyRepeaterPanel>
				
	<dga:DataGridAdapter 
      ID="DataGridAdapterPermissions" 
      Runat="Server" 
      DataGridToBind="DataGridPermissions" 
      SortHistory="2" 
      AutoDataBind="True"
      HideEmptyPager="True"
      OnDataGridBinding="DataGridAdapterPermissions_DataGridBinding"
      />
                  
</asp:Content>

