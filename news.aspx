<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="news.aspx.vb" Inherits="news" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <asp:Literal ID="titleLabel" runat="server" Text="News"></asp:Literal> | Swift Gaming Network 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <asp:Literal ID="newsPostings" runat="server"></asp:Literal>

</asp:Content>