<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditLibraryAttributes.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.EditLibraryAttributes" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTabControl" Src="DocumentLibraryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTasks" Src="DocumentLibraryTasks.ascx" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
     
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
     <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:DocumentLibraryTabControl ID="DocumentLibraryTabControl1" runat="server" DirectoryID="<%# CurrentFile.ID %>" Selected="Attributes" />
	<div class="formPanel">
		    
		<asp:DataGrid ID="DataGridAttributes" Runat="Server"
		AutoGenerateColumns="False"
		GridLines="None"
		DataKeyField="ID"
		CssClass="grid"
		Width="100%"
		HeaderStyle-CssClass="gridHeader"
		AlternatingItemStyle-CssClass="altRow"
		OnItemCommand="DataGridAttributes_OnItemCommand"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="Name" HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Name") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn  HeaderText="Value" HeaderStyle-CssClass="displayNameColumn" ItemStyle-CssClass="displayNameColumn" >
				<ItemTemplate>
					<asp:TextBox Runat="server" ID="TextBoxValue" style="width: 250px;" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'/>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Order">
				<ItemTemplate>
					<asp:LinkButton CssClass="action moveUp" ID="Linkbutton3" Runat="server" CommandName="MoveUp" ></asp:LinkButton>
					<asp:LinkButton CssClass="action moveDown" ID="Linkbutton4" Runat="server" CommandName="MoveDown" ></asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:LinkButton ID="DeleteAttributeButton" Title="Delete Attribute" CssClass="action delete" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteAttributeButton" Message='<%# DataBinder.Eval(Container.DataItem, "Name", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn></Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridAttributes" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		<span class="warning">This file has no attributes.</span>
	</erp:EmptyRepeaterPanel>
	
	<% if (DataGridAttributes.Items.Count > 0) { %>
	
	<div class="selectedActions" >
		<asp:LinkButton Runat="server" Title="Save" CssClass="action save" ID="ButtonSave" OnClick="ButtonSave_OnClick"> Save changes</asp:LinkButton>
	</div>
	<% } %>
	
	<h2>Add Attribute</h2>
	<fieldset>
		<legend>Attribute Properties</legend>
		<label for="TextBoxDisplayName">Name:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxName" /> <br />
		<label for="TextBoxTemplatePath">Value:</label>  <asp:TextBox CssClass="input" Runat="server" Width="300" ID="TextBoxValue" /> <br />
		<asp:Button Runat="server" CssClass="submit" Text="Add &raquo;" OnClick="ButtonAddAttribute_OnClick" ID="Button1"/>
		<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" ID="Button2"/>
	</fieldset>
	
	</div>
	
	<DataBinding:DataBindingManager ID="DataBindingManagerAttribute" runat="server">
		<DataBinding:TextBoxBinding ControlToBind="TextBoxName" DataMember="Name" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxValue" DataMember="Value" />
	</DataBinding:DataBindingManager>


	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:DocumentLibraryTasks ID="DocumentLibraryTasks1" runat="server" CurrentLibrary="<%# CurrentFile %>" />
</asp:Content>

