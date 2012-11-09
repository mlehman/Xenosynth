<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="EditSiteHostHeaders.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Configuration.EditSiteHostHeaders" %>
<%@ Register TagPrefix="Fluent" namespace="Fluent.Presentation" assembly="PageTemplate" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="SiteTasks" Src="~/Configuration/SiteTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SiteTabControl" Src="~/Configuration/SiteTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
	<xs:SiteTasks runat="server" CurrentSite='<%# CurrentSite %>'/>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser runat="server" 
	    RootPage="Sites.aspx" 
	    RootPageName="Sites" 
	    SubPage='<%# "EditSite.aspx?SiteID=" + SiteID %>' 
	    SubPageName='<%# CurrentSite.Name %>'
	    />
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:SiteTabControl ID="SiteTabControl1" runat="server" Selected="Host Headers" SiteID="<%# SiteID %>" />
    <div class="formPanel">
	    <asp:DataGrid ID="DataGridHostHeaders" Runat="Server"
		    AutoGenerateColumns="False"
		    DataKeyField="ID"
		    CellPadding="4"
		    GridLines="None"
		    AllowSorting="True"
		    Width="100%"
		    HeaderStyle-CssClass="gridHeader"
		    AlternatingItemStyle-CssClass="altRow"
		    OnDeleteCommand="DataGridHostHeaders_OnDeleteCommand"
		    >
		    <Columns>
			    <asp:TemplateColumn HeaderText="Name">
				    <ItemTemplate>
				        <asp:TextBox Runat="server" ID="TextBoxName" style="width: 500px;" Text='<%# DataBinder.Eval(Container.DataItem, "HostHeaderName") %>'/>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:TemplateColumn HeaderText="Is Default?">
				    <ItemTemplate>
				        <asp:CheckBox Runat="server" ID="CheckBoxIsDefault" Checked='<%# DataBinder.Eval(Container.DataItem, "IsDefault") %>'/>
				    </ItemTemplate>
			    </asp:TemplateColumn>
			    <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				    <ItemTemplate>
					    <asp:HyperLink Title="Edit Host Header Mapping" CssClass="action edit" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ID", "EditHostHeader.aspx?HostHeaderID={0}") %>' ID="Hyperlink2"></asp:HyperLink>
					    <asp:LinkButton Title="Delete Host Header Mapping" CssClass="action delete" ID="DeleteTemplateButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					    <ab:AlertButton runat="server" Control="DeleteTemplateButton" Message='<%# DataBinder.Eval(Container.DataItem, "HostHeaderName", "Permanently delete {0}?") %>' DialogMode="Confirm" ID="Alertbutton1"/>
				    </ItemTemplate>
			    </asp:TemplateColumn>
		    </Columns>
	    </asp:DataGrid>
	    <erp:EmptyRepeaterPanel runat="server" control="DataGridHostHeaders" ID="Emptyrepeaterpanel1" NAME="Emptyrepeaterpanel1">
		    <span class="warning">There are not any host headers.</span>
	    </erp:EmptyRepeaterPanel>
	    <% if (DataGridHostHeaders.Items.Count > 0) { %>
	
	    <div class="selectedActions" >
		    <asp:LinkButton Runat="server" Title="Save" CssClass="action save" ID="ButtonSave" OnClick="ButtonSave_OnClick"> Save changes</asp:LinkButton>
	    </div>
	    <% } %>
	    
	    <h2>Add Host Header</h2>
	    <fieldset>
		    <legend>Properties</legend>
		    
		    <label for="TextBoxHostHeader">Host Header:</label>  <asp:TextBox CssClass="input" Runat="server" style="width: 500px;" ID="TextBoxHostHeader"  MaxLength="250"/>
			<span class="toolTip">example: www.yoursite.com </span>
					
		    <asp:Button Runat="server" CssClass="submit" Text="Add &raquo;" OnClick="ButtonAddHostHeader_OnClick" ID="Button1"/>
		    <asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" ID="Button2"/>
	    </fieldset>
	</div>
				
</asp:Content>
