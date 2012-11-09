<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="AddPage.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content.AddPage" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="PopupCalendar" Src="~/Controls/PopupCalendar.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="../Directory/DirectoryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PageTabControl" Src="PageTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
		<xs:DirectoryTasks runat="server" CurrentDirectory="<%# ParentDirectory %>" />
</asp:Content>
			
<asp:Content ContentPlaceHolderID="Main" runat="server">

 <xs:PathBrowser  runat="server" CurrentFile="<%# ParentDirectory %>"   ID="PathBrowser1" />
  <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# ParentDirectory %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	
            <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	        <xs:PageTabControl ID="PageTabControl1" runat="server"  Selected="Properties" />
            <div class="formPanel">
	        <fieldset>
		        <legend>Page Properties</legend>
		                		
		        <label for="TextBoxTitle" class="required"> Title:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxTitle"  MaxLength="100"/>
		        <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator1"/>
		        <br />
		        <span class="toolTip">The name that will be displayed.</span>
        		
        		<label for="TextBoxFileName" class="required"> File Name:</label> <asp:TextBox CssClass="input" Runat="server" style="width: 250px;" ID="TextBoxFileName" MaxLength="100" />
		        <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
		        <asp:RegularExpressionValidator ID="RegularExpressionValidatorFileName" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" />
		        <br />
		        <span class="toolTip">The name as it will appear in the URL ( http://www.yoursite.com/filename.aspx ).</span>
		        
		        <label for="DropDownListTemplates"> Content-Type:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListTemplates" /> 
		        <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="DropDownListTemplates" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator2"/>
		        <br />
        		
		        <label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="inputLong" Runat="server" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		        <br />
		        <span class="toolTip">A description of the page for search engines.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		        <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		        <br />
        		
		        <label for="TextBoxKeywords"> Keywords:</label>  <asp:TextBox CssClass="inputLong" Runat="server" Rows="4" TextMode="MultiLine" ID="TextBoxKeywords" />
		        <br />
		        <span class="toolTip">Keywords for search engines. Separate with commas.<asp:TextBox Runat="server" ID="TextBoxKeywordsCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		        <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxKeywords" MaxLength="250" OutputControl="TextBoxKeywordsCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
		        <br />
        		
		        <asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsHidden" Text="Is hidden?" />
		        <span class="toolTip">Whether the page is hidden in the navigation.</span>
        		
		        <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAddPage_OnClick" />
		        <asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" />
	        </fieldset>
	        </div>
        	
        	
	        <DataBinding:DataBindingManager ID="DataBindingManagerPage" runat="server">
		        <DataBinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		        <DataBinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		        <DataBinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
		        <DataBinding:TextBoxBinding ControlToBind="TextBoxKeywords" DataMember="Keywords" />
		        <DataBinding:ListControlBinding ControlToBind="DropDownListTemplates" DataMember="TemplateID" />
		        <DataBinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
	        </DataBinding:DataBindingManager>
			
		</MainPanelTemplate>
    </xs:SlidePanel>
</asp:Content>

