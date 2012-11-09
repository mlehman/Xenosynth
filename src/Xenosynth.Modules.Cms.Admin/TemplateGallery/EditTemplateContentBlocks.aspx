<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditTemplateContentBlocks.aspx.cs" Inherits="Xenosynth.Admin.Content.TemplateGallery.EditTemplateContentBlocks" Title="Untitled Page" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="TemplateTabControl" Src="TemplateTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="TemplateGalleryTasks" Src="TemplateGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="TemplateTasks" Src="TemplateTasks.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentTemplate %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentTemplate %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

     <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:TemplateTabControl ID="TemplateTabControl1" runat="server" Selected="Content Blocks" FileID="<%# CurrentTemplate.ID %>" />
	<div class="formPanel">
	    
	<asp:DataGrid ID="DataGridRegisteredContent" Runat="Server"
		DataKeyField="ID"
		OnDeleteCommand="DataGridRegisteredContent_OnDeleteCommand"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="Control ID">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "ControlID") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="ContentTypeName" HeaderText="Type"  />
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<asp:LinkButton ToolTip="Delete Registered Content" CssClass="action delete" ID="DeleteRegisteredContentButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteRegisteredContentButton" Message='<%# DataBinder.Eval(Container.DataItem, "ControlID", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridRegisteredContent" ID="Emptyrepeaterpanel1">
		<span class="warning">There are not any registered content blocks.</span>
	</erp:EmptyRepeaterPanel>
	
	<h2>Register Content Block</h2>
	<fieldset>
		<legend>Content Block Details</legend>
		<label for="TextBoxControlID">Content Block ID:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxControlID" /> <br />
		<label for="DropDownListContentType">Content Type:</label>  <asp:DropDownList CssClass="input" Runat="server" Width="300" ID="DropDownListContentType" /> <br />
		<asp:Button Runat="server" CssClass="submit" Text="Register &raquo;" OnClick="ButtonRegisterContent_OnClick" ID="Button3"/>
		<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancelContent_OnClick" ID="Button4"/>
	</fieldset>
	
	<DataBinding:DataBindingManager ID="DataBindingRegisteredContent" runat="server"> 
		<DataBinding:TextBoxBinding ControlToBind="TextBoxControlID" DataMember="ControlID" />
		<DataBinding:ListControlBinding ControlToBind="DropDownListContentType" DataMember="ContentTypeName" />
	</DataBinding:DataBindingManager>
	
	</div>
	
            </MainPanelTemplate>
        </xs:SlidePanel>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:TemplateTasks ID="TemplateTasks1" runat="server" CurrentTemplate="<%# CurrentTemplate %>" />
    <xs:TemplateGalleryTasks ID="TemplateGalleryTasks1" runat="server" CurrentGallery="<%# CurrentTemplate.ParentDirectory %>" />
</asp:Content>
