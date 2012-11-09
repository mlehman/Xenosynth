<%@ Page language="c#" MasterPageFile="~/Default.Master"  Codebehind="EditPageContent.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content.EditPageContent" %>
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
<%@ Register TagPrefix="radE" Namespace="Telerik.WebControls" Assembly="RadEditor.Net2" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
		
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
	        <xs:PageTabControl ID="PageTabControl1" runat="server" FileID="<%# CurrentPage.ID %>" Selected="Content" />
            <div class="formPanel">
                <fieldset>
		            <legend>Content Blocks</legend>
		            
		            
		            <asp:DataList ID="DataListContentBlocks" runat="server" DataKeyField="Key" >
		                <ItemTemplate>
		                    <label for="TextBoxText"><%# Eval("Key") %>:</label>
		                    <br />
		                    <radE:RadEditor ID="ContentBlock" Runat="server" 
					            Width="700px"
					            Height="400px"
					            Editable="True"
					            StripFormattingOnPaste="MSWordRemoveAll"
					            ShowSubmitCancelButtons="False"
					            ToolsFile="~/Modules/Cms/ToolsFile.xml" 
					            HTML='<%# Eval("Value.Text") %>'
					            >
					        </radE:RadEditor>
		                </ItemTemplate>
		            </asp:DataList>
		            
		            
		            <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		            <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
		        </fieldset>	    
	        </div>
	
	    </MainPanelTemplate>
    </xs:SlidePanel>
	
</asp:Content>
     
