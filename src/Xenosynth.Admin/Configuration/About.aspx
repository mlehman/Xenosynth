<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="About.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Configuration.About" %>

<asp:Content ContentPlaceHolderID="Side" runat="server" >
	
</asp:Content>
			
<asp:Content ContentPlaceHolderID="Main" runat="server" >
				
				<h2>Xenosynth Components</h2>
				<table border="0" >
					<tr>
						<td style="width: 150px; font-weight: bold;">Xenosynth Admin</td>
						<td>Version <%= VersionInfo("Xenosynth.Admin.Global,Xenosynth.Admin") %></td>
					</tr>
					<tr>
						<td style="width: 150px; font-weight: bold;">Xenosynth API</td>
						<td>Version <%= VersionInfo("Xenosynth.Web.CmsContext,Xenosynth") %></td>
					</tr>
				</table>
				
				<h2>System Information</h2>
				<table border="0" >
					<tr>
						<td style="width: 150px; font-weight: bold;">.Net Framework</td>
						<td>Version <%= Environment.Version %></td>
					</tr>
					<tr>
						<td style="width: 150px; font-weight: bold;">Operating System</td>
						<td><%= Environment.OSVersion %></td>
					</tr>
				</table>

				<h2>Components</h2>
				<table border="0" >
					<tr>
						<td style="width: 150px; font-weight: bold;">Inform</td>
						<td>Version <%= VersionInfo("Inform.DataStore,Inform") %></td>
					</tr>
					<tr>
						<td style="width: 150px; font-weight: bold;">Configuration</td>
						<td>Version <%= VersionInfo("Fluent.Configuration.ConfigSetting,Fluent.Configuration") %></td>
					</tr>
					<tr>
						<td style="width: 150px; font-weight: bold;">Security</td>
						<td>Version <%= VersionInfo("Fluent.Security.Authentication,Fluent.Security") %></td>
					</tr>
					<tr>
						<td style="width: 150px; font-weight: bold;">FreeTextBox</td>
						<td>Version <%= VersionInfo("FreeTextBoxControls.ImageGallery,FreeTextBox") %></td>
					</tr>
				</table>
				
				<h2>Modules</h2>
				<table border="0" >
				    <asp:Repeater ID="RegisteredModuleVersions" runat="server">
				        <ItemTemplate>
				            <tr>
						        <td style="width: 150px; font-weight: bold;"><%# Eval("Name") %></td>
						        <td>Version <%# VersionInfo((string)Eval("ClassName")) %></td>
					        </tr>
				        </ItemTemplate>
				    </asp:Repeater>
					
				</table>
</asp:Content>

