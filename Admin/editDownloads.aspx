<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="editDownloads.aspx.vb" Inherits="Admin_editDownloads" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

    <div class="box">
        <h2>Edit Page: Downloads</h2>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <CKEditor:CKEditorControl ID="pageEditor" runat="server" Height="500px" Toolbar="Source
                Bold|Italic|Underline|Strike|-|Subscript|Superscript
                NumberedList|BulletedList|-|Outdent|Indent
                /
                Styles|Format|Font|FontSize|TextColor|BGColor|-|About">
            </CKEditor:CKEditorControl>
            <hr />
            <div class="buttonSection">
                <asp:Button CssClass="registerButtons" ID="savePageTextButton" runat="server" Text="Save Page" />
            </div>

        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    

</asp:Content>

