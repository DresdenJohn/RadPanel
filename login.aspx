<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<asp:Content ID="titleContent" ContentPlaceHolderID="title" runat="server">
    Login | Swift Gaming Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <div class="box">

        <h2>Swift Gaming Network Login Form</h2>
        <h3>Woah! Welcome back! We totally missed you! So much has happened while you were gone. Anyway, you know the drill. E-Mail and Password.</h3>
        <hr />

        <asp:Label ID="loginResult" Text="<br/>" runat="server"></asp:Label>

        <div class="inputSection">

            <p>
                <abbr class="tooltip">
                    E-mail:
                    <span class="classic">
                    Type your E-mail. It's not rocket science; just a semi-complex VB script.
                    </span>
                </abbr>
            </p>
            <asp:TextBox ID="loginEmailTextbox" CssClass="stretchHundred pushMarginRight" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="loginEmailRequired" 
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="E-mail Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>E-Mail is Required</span></abbr>" 
                                ControlToValidate="loginEmailTextBox" ValidationGroup="newLoginValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                                ID="loginEmailREV" ValidationGroup="newLoginValidate"
                                runat="server" 
                                ErrorMessage="Invalid E-mail" 
                                CssClass="validateRed drop"
                                Text="<abbr class='minitooltip'>Invalid E-Mail<span class='classic'>Your e-mail must be typed in the proper format (name@server.***)</span></abbr>" 
                                ControlToValidate="loginEmailTextBox"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                </asp:RegularExpressionValidator>

        </div>

        <div class="inputSection">

            <p><abbr class="tooltip">
                Password:
                <span class="classic">
                Type your chosen password when you first created your Eternal account. Unless you changed it, then you should use that one instead.
                </span>
                                
            </abbr></p>

            <asp:TextBox ID="loginPasswordTextBox"  CssClass="stretchHundred pushMarginRight" runat="server" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ValidationGroup="newLoginValidate"
                ID="loginPasswordRequired" 
                runat="server" 
                ErrorMessage="Password Is Required" 
                Text="<abbr class='minitooltip'>*<span class='classic'>Password is Required</span></abbr>"  
                CssClass="validateRed drop"
                ControlToValidate="loginPasswordTextBox"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ValidationGroup="newLoginValidate"
                CssClass="validateRed drop"
                ID="loginPasswordREV" 
                runat="server" 
                ErrorMessage="Invalid Password"
                Text="<abbr class='minitooltip'>Invalid Password<span class='classic'>Your password may not inlcude any special characters. Remember your password is case sensitive.</span></abbr>" 
                ControlToValidate="loginPasswordTextBox"
                ValidationExpression="[a-zA-Z0-9]*">
                </asp:RegularExpressionValidator>

        </div>

        <div class="inputSection">
                           <asp:CheckBox ID="loginRememberMeCheck" runat="server" Text="Keep Me Logged In"/>
                        </div>
                        <hr style="margin-top: 20px"/>

        <div class="buttonSection">

            <asp:Button ID="loginSubmitButton" CssClass="registerButtons" CausesValidation="true" ValidationGroup="newLoginValidate" runat="server" Text="Sign In"/>

        </div>

    </div>

</asp:Content>