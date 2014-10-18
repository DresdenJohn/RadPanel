<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    Home | Radical Flyff
</asp:Content>

<asp:Content ID="meta" ContentPlaceHolderID="meta" runat="server">
</asp:Content>

<asp:Content ID="headcontent" ContentPlaceHolderID="head" runat="server">

<%--        <link rel="stylesheet" href="./themes/dark/dark.css" type="text/css" media="screen" />
        <link rel="stylesheet" href="./themes/bar/bar.css" type="text/css" media="screen" />
        <link rel="stylesheet" href="./themes/nivo-slider.css" type="text/css" media="screen" />
        
        <link rel="stylesheet" href="./timer/jbclock.css" type="text/css" media="all" />
        <script type="text/javascript" src="./timer/jbclock.js"></script>
                <script type="text/javascript">
                    $(document).ready(function () {
                        JBCountDown({
                            secondsColor: "#ffdc50",
                            secondsGlow: "none",

                            minutesColor: "#9cdb7d",
                            minutesGlow: "none",

                            hoursColor: "#378cff",
                            hoursGlow: "none",

                            daysColor: "#ff6565",
                            daysGlow: "none",

                            startDate: "1357034400",
                            endDate: "1377626400",
                            now: "1376550780"
                        });
                    });
        </script>

        <script type="text/javascript" src="./themes/jquery.nivo.slider.js"></script>
        <script type="text/javascript">
            $(window).load(function () {
                $('#slider').nivoSlider({
                    animSpeed: 500, // Slide transition speed
                    pauseTime: 10000, // How long each slide will show
                });
            });
        </script>--%>

</asp:Content>

<asp:Content ID="photoContent" ContentPlaceHolderID="photoSlider" runat="server">

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    
    <!--
    <div class="slider-wrapper theme-dark" style="margin: 0 auto; text-align:center; padding: 20px; box-shadow: 2px 2px 6px rgba(0,0,0,0.7) inset;">
        <div id="slider" class="nivoSlider">
            <img src="./Assets/slideOne.png" data-thumb="./Assets/slideOne.png" alt="" data-transition="slideInLeft"/>
            <img src="./Assets/slideExp.png" data-thumb="./Assets/slideExp.png" alt="" data-transition="slideInLeft"/>
        </div>
    </div>
    -->

    <asp:Literal ID="newsStoriesPlaceHolder" runat="server"></asp:Literal>

    <asp:Literal ID="eventsStoriesPlaceholder" runat="server"></asp:Literal>

    <div class="box">

        <h2 style="color: #E59E7E;">Flyff Character Ranking (Top 10) (<a href="flyffRanking.aspx">Top 50</a>)</h2>
        <h3>Our up to the minute Flyff ranking system! Who knows? <b>You</b> might be on this list! Think you got what it takes to get number One?</h3>
        <hr />

        <asp:Literal ID="rankingPlaceholder" runat="server" Visible="true"></asp:Literal>
        
    </div>

</asp:Content>