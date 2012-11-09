<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditContentType.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Page.EditContentType" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PageTasks" Src="PageTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PageTabControl" Src="PageTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="../Directory/DirectoryTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">

        <div class="actionPanel">
			<div class="title">Template Tasks</div>
			<div class="body">
				<a class="templateEdit action" href="<%= ResolveUrl( CurrentPage.Template.DefaultActionUrl )  %>">Edit Template</a>
			</div>
		</div>
		
		<xs:PageTasks Runat="server" CurrentPage="<%# CurrentPage %>" ID="Pagetasks1" NAME="Pagetasks1"/>
		<xs:DirectoryTasks runat="server" CurrentDirectory="<%# CurrentPage.ParentDirectory %>" />
		
		
</asp:Content>
			
<asp:Content ContentPlaceHolderID="Main" runat="server">

	<xs:PathBrowser  runat="server" CurrentFile="<%# CurrentPage %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentPage %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:PageTabControl ID="PageTabControl1" runat="server" FileID="<%# CurrentPage.ID %>" Selected="Content-Type" />
    <div class="formPanel">
	    <fieldset>
	        <legend>Select Content-Type</legend>
        	
	        <label for="DropDownListTemplates"> Content-Type:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListTemplates" /> 
	        <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="DropDownListTemplates" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator3"/>
	        <br />
        	
	        <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdatePage_OnClick" ID="Button1" NAME="Button1"/>
	        <asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" ID="Button2" NAME="Button2"/>
        </fieldset>
    	
    	
	    <h2>Registered Content</h2>
    	
	    <asp:DataGrid ID="DataGridContent" Runat="Server"
		    AutoGenerateColumns="False"
		    CssClass="grid"
		    GridLines="None"
		    Width="100%"
		    HeaderStyle-CssClass="gridHeader"
		    AlternatingItemStyle-CssClass="altRow"
		    >
		    <Columns>
			    <asp:TemplateColumn HeaderText="Content Block ID" HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
				    <ItemTemplate>
					    <%# DataBinder.Eval(Container.DataItem, "Key") %>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:TemplateColumn HeaderText="Type" HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
				    <ItemTemplate>
					    <%# DataBinder.Eval(Container.DataItem, "Value") %>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:TemplateColumn HeaderText="Available" HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
				    <ItemTemplate>
					    <%# DataBinder.Eval(Container.DataItem, "Value.IsAvailable") %>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" Visible="false" >
				    <ItemTemplate>
					    <asp:LinkButton Title="Edit Content" CssClass="action edit" Runat="server" CommandName="Update" ></asp:LinkButton>
					    <asp:LinkButton ID="DeleteContentButton" Title="Delete Content" CssClass="action delete" Runat="server" CommandName="Delete" ></asp:LinkButton>
					    <ab:AlertButton runat="server" Control="DeleteContentButton" Message='' DialogMode="Confirm" ID="Alertbutton1"/>
				    </ItemTemplate>
			    </asp:TemplateColumn></Columns>
	    </asp:DataGrid>
	    <erp:EmptyRepeaterPanel runat="server" control="DataGridContent" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		    <span class="warning">This page has no content.</span>
	    </erp:EmptyRepeaterPanel>
    	
	    <h2>Create Initial Content</h2>
	    <fieldset>
		    <legend>Content Properties</legend>
		    <label for="TextBoxControlID">Content Block ID:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxControlID" /> <br />
		    <label for="TextBoxType">Type:</label>  <asp:DropDownList CssClass="input" Runat="server" Width="300" ID="DropDownListContentType" />  <br />
		    <asp:Button Runat="server" CssClass="submit" OnClick="ButtonAddContent_OnClick" Text="Create &raquo;"/>
		    <asp:Button Runat="server" Text="Cancel" />
	    </fieldset>
    	
	    <DataBinding:DataBindingManager ID="DataBindingManagerAttribute" runat="server">
		    <DataBinding:TextBoxBinding ControlToBind="TextBoxName" DataMember="Name" />
		    <DataBinding:TextBoxBinding ControlToBind="TextBoxValue" DataMember="Value" />
	    </DataBinding:DataBindingManager>
	    
	    
	</div>
	
	 </MainPanelTemplate>
    </xs:SlidePanel>
	
</asp:Content>