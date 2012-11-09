<%@ Page MasterPageFile="~/Default.Master"  Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Xenosynth.Admin.Users.Default" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="UserTasks" Src="~/Security/UserTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchUsers" Src="~/Security/SearchUsers.ascx" %>
<%@ Register TagPrefix="dga" Namespace="Fluent"  Assembly="Fluent.DataGridAdapter" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Side">
	<xs:UserTasks ID="UserTasks1" runat="server" />
	<xs:SearchUsers ID="SearchUsers1" runat="server" />
</asp:Content>
			
<asp:Content ID="Content2" runat="server"  ContentPlaceHolderID="Main">

    <asp:DataGrid ID="DataGridUsers" Runat="Server"
		DataKeyField="ProviderUserKey"
		OnDeleteCommand="DataGridUsers_OnDeleteCommand"
		AllowSorting="True"
		AllowPaging="True"
		PageSize="15"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="User Name">
				<ItemTemplate>
					<asp:HyperLink Runat="server" CssClass="user action" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ProviderUserKey", "EditUser.aspx?UserID={0}") %>' ><%# DataBinder.Eval(Container.DataItem, "UserName") %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Email">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Email") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Comment">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Comment") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Is Approved">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Is Locked Out">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "IsLockedOut") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Is Online">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "IsOnline") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:HyperLink Title="Edit User" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ProviderUserKey", "EditUser.aspx?UserID={0}") %>' ID="Hyperlink2"></asp:HyperLink>
					<asp:LinkButton Title="Delete User" CssClass="action delete" ID="DeleteTemplateButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteTemplateButton" Message='<%# DataBinder.Eval(Container.DataItem, "UserName", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	
	<dga:DataGridAdapter 
      ID="DataGridAdapterUsers" 
      Runat="Server" 
      DataGridToBind="DataGridUsers" 
      SortHistory="1" 
      AutoDataBind="True"
      HideEmptyPager="True"
      OnDataGridBinding="DataGridAdapterUsers_DataGridBinding"
      />
</asp:Content>
