<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="editEvent.aspx.vb" Inherits="Admin_editEvent" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

    <asp:PlaceHolder ID="errorBox" runat="server" Visible="true">
        <div class="box">
            <h2>Oops!</h2>
            <h3>A problem has occured while processing your request.</h3>
            <hr />
            <p>It looks like the news posting you wish to edit no longer exists or never existed in the first place! If you feel as though you have reached this page in error, please contact an administrator.</p>
            <p><a href="default.aspx">Back to ACP</a></p>
        </div>

    </asp:PlaceHolder>

    <asp:PlaceHolder ID="editBox" runat="server" Visible="false">
        <div class="box">
            <h2>Edit News Post</h2>
            <p><asp:Label ID="resultLabel" runat="server" Text="" Visible="false"></asp:Label></p>
            <hr />
            <div class="inputSection" style="margin-top: 20px;">
                <p>Title:</p>
                <asp:TextBox ID="newsTitleTextBox" runat="server" Width="200"></asp:TextBox>
            </div>

            <div class="inputSection">
                <p>Post to Usergroup:</p>
                <asp:DropDownList ID="usergroupDropdown" runat="server" Width="205">
                </asp:DropDownList>
            </div>
            <hr />
            <CKEditor:CKEditorControl Height="500" ID="NewsPosting" runat="server"></CKEditor:CKEditorControl>
            <hr />
            <div class="buttonSection">
                <asp:Button ID="newsPostButton" runat="server" Text="Update News" />
            </div>
             
        </div>
    </asp:PlaceHolder>
</asp:Content>

