<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="overridePage.aspx.vb" Inherits="Admin_overridePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Owner Override | Eternal Gaming Network
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <div class="box">

        <h2>Owner Override Form</h2>

        <h3>Do NOT use this unless you know what you're doing. This is for Owners ONLY.</h3>

        <hr />

        <div class="inputSection">

            <p>Password:</p>

            <asp:TextBox ID="overridePassword" runat="server" TextMode="Password"></asp:TextBox>

        </div>

        <div class="inputSection">

            <p>Query:</p>

        </div>

        <br />

        <asp:TextBox ID="queryBox" runat="server" TextMode="MultiLine" Height="300" Width="100%"></asp:TextBox>

        <br />
        <br />

        <div style="margin: 0 auto;text-align: center;">
            <asp:Button ID="runQueryButton" runat="server" CssClass="registerButtons" Text="Run Query" />
        </div>

    </div>

</asp:Content>