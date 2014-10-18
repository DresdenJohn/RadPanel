<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="eventManage.aspx.vb" Inherits="Admin_eventManage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

        <div class="box">
            <h4>Add Swift Flight Online Event</h4>
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
            <CKEditor:CKEditorControl ID="NewsPosting" runat="server" Toolbar="Source
                Bold|Italic|Underline|Strike|-|Subscript|Superscript
                NumberedList|BulletedList|-|Outdent|Indent
                /
                Styles|Format|Font|FontSize|TextColor|BGColor|-|About"></CKEditor:CKEditorControl>
            <hr />
            <div class="buttonSection">
                <asp:Button ID="newsPostButton" runat="server" Text="Post Event" />
            </div>
             
        </div>

        <asp:Literal ID="newsStoriesPlaceHolder" runat="server"></asp:Literal>

</asp:Content>


