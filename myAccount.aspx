<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="myAccount.aspx.vb" Inherits="myAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Swift Gaming Network | Account
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <div class="box">

        <asp:PlaceHolder ID="loggedOutPlaceholder" Visible="true" runat="server">
            
            <h2>Oops!</h2>
            <h3>Problem Loading this Page</h3>
            <hr/>
            <p>It looks like you're not logged in! You require an Swift Gaming Network account to view this page. Click <a href="register.aspx">here</a> to register or <a href="login.aspx">here</a> if you're already a member. If you feel like you reached this page in error, please contact an Administrator.</p>
            <p><a href='default.aspx'>Back To Home</a></p>

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="loggedInPlaceholder" Visible="false" runat="server">
            <h2>SGN Account Information</h2>
            <h3>For all your account needs!</h3>
            <hr />
            <p>For now you can only view all your current registered SFO accounts and characters under that account. We will be adding new features to this page soon enough so just stay posted!</p>
            <hr />
            <h3 style="margin-bottom: 5px">Flyff Accounts Registered:</h3>
            <asp:Label ID="flyffAccountsLabel" runat="server" Text=""></asp:Label>
        </asp:PlaceHolder>

    </div>
</asp:Content>

