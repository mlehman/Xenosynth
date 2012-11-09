<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddImage.aspx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.AddImage" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="ImageTabControl" Src="ImageTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTasks" Src="ImageGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:ImageGalleryTasks ID="ImageGalleryTasks1" runat="server" CurrentGallery="<%# CurrentGallery %>" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentGallery %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentGallery %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" /> 
	<xs:ImageTabControl ID="ImageTabControl1" runat="server" Selected="Properties" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Image Properties</legend>
		    
		    <label for="TextBoxTitle" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> 
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." /><br />				
		    <span class="toolTip">This is the name that will be displayed.</span> 
		    
		    <label for="TextBoxID">Select File:</label><input runat="server" type="file" id="HtmlInputFileAttach" class="file" onchange="javascript: updateFileName(this);" /><br />
		    <span class="toolTip">Image file (*.gif, *.jpg, *.png)</span>
		    
		    <label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." /> <br />
		    <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename ).</span>
    	
		    <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	<databinding:DataBindingManager ID="DataBindingManagerImage" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
	</databinding:DataBindingManager>
	
	<script language="javascript">
	    function getFileName(url) {
            var index = Math.max(url.lastIndexOf('/'),url.lastIndexOf('\\'));
            if (index != -1) {
                return url.substring(index + 1);
            } else {
                return url;
            }
        }
        
	    function updateFileName(inputFile){
	        document.getElementById('<%= TextBoxFileName.ClientID %>').value = getFileName(inputFile.value);
	    }
	</script>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>

