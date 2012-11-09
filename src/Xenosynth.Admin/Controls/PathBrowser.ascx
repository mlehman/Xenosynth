<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PathBrowser.ascx.cs" Inherits="Xenosynth.Admin.Controls.PathBrowser" %>
<table class="filePathBrowser">
    <tr>
        <td class="label">Path:</td>
        <td width="100%">
            <div class="path">
             / <a title="Browse - Products" href="<%= RootPage %>"><%= RootPageName %></a>
             / <a title="View - Product" href="<%= SubPage %>"><%= SubPageName %></a>
            </div>
        </td> 
    </tr>
</table>