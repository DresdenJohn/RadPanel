<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="flyffRanking.aspx.vb" Inherits="flyffRanking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <div class="box">
        <h2 style="color: #E59E7E;">Flyff Character Ranking (Top 50)</h2>
        <h3>Our up to the minute Flyff ranking system! Who knows? <b>You</b> might be on this list! Think you got what it takes to get number One?</h3>
        <hr />

        <asp:Literal ID="rankingPlaceholder" runat="server" Visible="true"></asp:Literal>
    </div>
</asp:Content>

