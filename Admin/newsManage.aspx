<%@ Page Title="" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="newsManage.aspx.vb" Inherits="Admin_newsManage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

        <div class="box">
            <h4>Add News Post</h4>
            <p><asp:Label ID="resultLabel" runat="server" Text="" Visible="false"></asp:Label></p>
            <hr />
            <div class="inputSection">
                <p>Title:</p>
                <asp:TextBox ID="newsTitleTextBox" runat="server" Width="200"></asp:TextBox>
            </div>

            <div class="inputSection">
                <p>Post to Usergroup:</p>
                <asp:DropDownList ID="usergroupDropdown" runat="server" Width="205">
                </asp:DropDownList>
            </div>
            <hr />
            <CKEditor:CKEditorControl ID="NewsPosting" runat="server"></CKEditor:CKEditorControl>
            <hr />
            <div class="buttonSection">
                <asp:Button ID="newsPostButton" runat="server" Text="Post News" />
            </div>
             
        </div>

        <asp:Literal ID="newsStoriesPlaceHolder" runat="server"></asp:Literal>

</asp:Content>

