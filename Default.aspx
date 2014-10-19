<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    Home | Radical Flyff
</asp:Content>

<asp:Content ID="meta" ContentPlaceHolderID="meta" runat="server">
</asp:Content>

<asp:Content ID="headcontent" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="photoContent" ContentPlaceHolderID="photoSlider" runat="server">

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <asp:Literal ID="newsStoriesPlaceHolder" runat="server"></asp:Literal>

    <asp:Literal ID="eventsStoriesPlaceholder" runat="server"></asp:Literal>

    <div class="box">

        <h2 style="color: #E59E7E;">Flyff Character Ranking (Top 10) (<a href="flyffRanking.aspx">Top 50</a>)</h2>
        <h3>Our up to the minute Flyff ranking system! Who knows? <b>You</b> might be on this list! Think you got what it takes to get number One?</h3>
        <hr />

        <asp:Literal ID="rankingPlaceholder" runat="server" Visible="true"></asp:Literal>
        
    </div>

</asp:Content>