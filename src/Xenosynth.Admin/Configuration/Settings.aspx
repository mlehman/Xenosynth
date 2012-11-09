<%@ Page MasterPageFile="~/Default.Master" language="c#" Codebehind="Settings.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Configuration.Settings" %>
<%@ Register TagPrefix="Fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="SettingTasks" Src="~/Configuration/SettingTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SettingsTabControl" Src="~/Configuration/SettingsTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="ConfigurationSettingEditor" Src="ConfigurationSettingEditor.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
	<xs:SettingTasks runat="server" />
</asp:Content>
			
<asp:Content  ContentPlaceHolderID="Main" runat="server">

<style>
    
    .grid .delete {
       margin-left: 5px;
    }
    
    .grid .textBoxAttribute {
       width: 80%;
    }
    
    .grid .checkBoxAttribute {
        float: left;
    }

</style>

	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:SettingsTabControl ID="SettingsTabControl1" runat="server" />
    <div class="formPanel">
        
        <asp:DataGrid ID="DataGridSettings" Runat="Server"
                AutoGenerateColumns="False"
                ShowHeader="false"
                DataKeyField="Key"
                CssClass="grid"
                HeaderStyle-CssClass="gridHeader"
                AlternatingItemStyle-CssClass="altRow"
                OnDeleteCommand="DataGridSettings_OnDelete"
                >
                <Columns>
	                <asp:TemplateColumn HeaderText="Name" ItemStyle-Width="300" ItemStyle-VerticalAlign="Top" >
		                <ItemTemplate>
			                <label style="margin-left: 100px; font-weight:bold;"><%# Eval("Name") %></label><br />
			                <span class="toolTip"><%# Eval("Description") %></span> 
		                </ItemTemplate>
	                </asp:TemplateColumn>
	                <asp:TemplateColumn HeaderText="Value" ItemStyle-VerticalAlign="Top" >
		                <ItemTemplate>
		                   <xs:ConfigurationSettingEditor ID="ConfigurationSettingEditor" runat="server" Setting="<%# Container.DataItem %>" />
		                   <asp:LinkButton runat="server" ID="DeleteButton" CommandName="Delete" CssClass="action delete" ToolTip="Delete this setting" />
		                   <ab:AlertButton runat="server" Control="DeleteButton" Message='<%# DataBinder.Eval(Container.DataItem, "Key", "Permanently delete setting {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
		                </ItemTemplate>
	                </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
	
	<% if (DataGridSettings.Items.Count > 0) { %>
    <div class="selectedActions" >
        <asp:LinkButton Runat="server" Title="Save" CssClass="action save" ID="ButtonSave" OnClick="ButtonSave_OnClick">Save changes</asp:LinkButton>
    </div>
    <% } %>
        
    
    </div>			
	
</asp:Content>
