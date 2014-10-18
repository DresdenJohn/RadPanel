<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="registerFlyffAccount.aspx.vb" Inherits="registerIrisAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Flyff Registration | Swift Gaming Network
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <asp:UpdatePanel runat="server" ID="updatePanel1">
        <ContentTemplate>
            <div class="box">
                    <asp:PlaceHolder ID="loggedOutPlaceholder" Visible="true" runat="server">
            
                        <h2>Oops!</h2><h3>Problem Loading Swift Flight Online Registration</h3><hr/><p>It looks like you're not logged in! You require an Swift Gaming Network account to register Swift Flight Online accounts. Click <a href="register.aspx">here</a> to register or <a href="login.aspx">here</a> if you're already a member. If you feel like you reached this page in error, please contact an Administrator.</p><p><a href='default.aspx'>Back To Home</a></p>

                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="loggedInPlaceholder" Visible="false" runat="server">
                        <h2 class="drop">Swift Flight Online Registration</h2>
                        <h3>This Swift Flight Online account, as well as other Swift Flight Online accounts you choose to create in the future will be associated with your Swift Gaming Network Account.</h3>
                        <hr />    
            
                        <asp:Label ID="irisRegisterResult" runat="server" Text="<br/>"  CssClass="drop" Visible="false"></asp:Label>

                        <div class="inputSection">
                        
                            <p>
                                <abbr class="tooltip">
                                    Username
                                    <span class="classic">
                                    Who you will be seen as ingame. Choose wisely, it cannot be changed later on.
                                    </span>
                                </abbr>
                            </p>
                            <asp:TextBox CssClass="stretchHundred" ID="irisRegisterUsernameText" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="Username Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Username is Required</span></abbr>"  
                                CssClass="validateRed drop"
                                ControlToValidate="irisRegisterUsernameText" ValidationGroup="newIrisRegisterValidate"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator2"   ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="<abbr class='minitooltip'>Invalid Username<span class='classic'>Your username cannot inlclude any special characters.</span></abbr>"
                                ControlToValidate="irisRegisterUsernameText"
                                ValidationExpression="[a-zA-Z0-9]*" >
                                </asp:RegularExpressionValidator>

                        </div>

                        <div class="inputSection">
                        
                            <p><abbr class="tooltip">
                                Password:
                                <span class="classic">
                                Your chosen password. You will be asked for this when logging into Swift Flight Online.
                                </span>
                            </abbr></p>
                            <asp:TextBox CssClass="stretchHundred" id="irisRegisterPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator3"  ValidationGroup="newIrisRegisterValidate"
                                runat="server" 
                                ErrorMessage="Password Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Password is Required</span></abbr>"  
                                CssClass="validateRed drop"
                                ControlToValidate="irisRegisterPasswordTextbox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator4"   ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="Invalid Password"
                                Text="<abbr class='minitooltip'>Invalid Password<span class='classic'>Your password may not inlcude any special characters.</span></abbr>" 
                                ControlToValidate="irisRegisterPasswordTextbox"
                                ValidationExpression="[a-zA-Z0-9]*">
                                </asp:RegularExpressionValidator>

                        </div>

                        <div class="inputSection">
                            <p><abbr class="tooltip">
                                Verify Password:
                                <span class="classic">
                                To make sure your password is correct we ask you to type it in twice. Typical boring protocol.
                                </span>
                                
                            </abbr></p>
                            
                            <asp:TextBox CssClass="stretchHundred" ID="irisRegisterPasswordVerifyTextBox" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newIrisRegisterValidate"
                                ID="RequiredFieldValidator4" 
                                runat="server" 
                                ErrorMessage="Password Verification Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Password Verification is Required</span></abbr>"  
                                CssClass="validateRed drop"
                                ControlToValidate="irisRegisterPasswordVerifyTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator  ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="RegularExpressionValidator5" 
                                runat="server" 
                                ErrorMessage="Invalid Password"
                                Text="<abbr class='minitooltip'>Invalid Password<span class='classic'>Your password may not inlcude any special characters. Remember your password is case sensitive.</span></abbr>" 
                                ControlToValidate="irisRegisterPasswordVerifyTextBox"
                                ValidationExpression="[a-zA-Z0-9]*">
                                </asp:RegularExpressionValidator>

                            <asp:CompareValidator   ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="CompareValidator2" 
                                runat="server" 
                                ErrorMessage="Passwords do not match"
                                Text="<abbr class='minitooltip'>Password Mismatch<span class='classic'>Passwords do not match, please recheck your password. Remember they are case sensitive.</span></abbr>" 
                                ControlToCompare="irisRegisterPasswordTextbox"
                                ControlToValidate="irisRegisterPasswordVerifyTextBox"></asp:CompareValidator>

                        </div>
                    
                        <div class="inputSection">
                            <p><abbr class="tooltip">
                                Account Pin:
                                <span class="classic">
                                A secure 4 digit number that is required to make major account changes. Can also be used to recover your forgotten password.
                                </span>
                            </abbr></p>
                        
                            <asp:TextBox CssClass="stretchHundred" ID="irisRegisterPinText" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator   ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="RequiredFieldValidator5" 
                                runat="server" 
                                ErrorMessage="Account Pin Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Account Pin is Required</span></abbr>"
                                ControlToValidate="irisRegisterPinText"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator  ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="RegularExpressionValidator6" 
                                runat="server" 
                                ErrorMessage="Invalid Pin"
                                Text="<abbr class='minitooltip'>Invalid Pin<span class='classic'>Your pin can only be 4 numeric characters.</span></abbr>" 
                                ControlToValidate="irisRegisterPinText"
                                ValidationExpression="[0-9][0-9][0-9][0-9]">
                                </asp:RegularExpressionValidator>



                        </div>

                        <div class="inputSection">
                            <p><abbr class="tooltip">
                                Verify Pin:
                                <span class="classic">
                                Just for the boss's sake could you please type that pin again? Security measures you see, he's so picky about it.
                                </span>
                            </abbr></p>
                            <asp:TextBox CssClass="stretchHundred" ID="irisRegisterPinVerifyText" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newIrisRegisterValidate"
                                ID="RequiredFieldValidator6" 
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="Pin Verification Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Pin Verification is Required</span></abbr>"       
                                ControlToValidate="irisRegisterPinVerifyText"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="RegularExpressionValidator7" 
                                runat="server" 
                                ErrorMessage="Invalid Pin"
                                Text="<abbr class='minitooltip'>Invalid Pin<span class='classic'>Your pin can only be 4 numeric characters.</span></abbr>" 
                                ControlToValidate="irisRegisterPinVerifyText"
                                ValidationExpression="[0-9][0-9][0-9][0-9]">
                                </asp:RegularExpressionValidator>

                            <asp:CompareValidator   ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="CompareValidator3" 
                                runat="server" 
                                ErrorMessage="Pins do not match"
                                Text="<abbr class='minitooltip'>Pin Mismatch<span class='classic'>Pins do not match, please recheck your pin. Remeber they are 4 digit numbers.</span></abbr>" 
                                ControlToCompare="irisRegisterPinText"
                                ControlToValidate="irisRegisterPinVerifyText"></asp:CompareValidator>

                        </div>

                        <div class="inputSection">
                            <p>
                            <asp:Label ID="irisCaptchaLabel" runat="server" Text="Label"></asp:Label>
                            </p>

                            <asp:TextBox CssClass="stretchHundred" ID="irisCaptchaText" runat="server"></asp:TextBox>
                            <asp:TextBox ID="irisRealCaptchaText" runat="server" Width="0px" Height="0px" CssClass="trueHidden"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newIrisRegisterValidate"
                                ID="RequiredFieldValidator7" 
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="Required Field" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Required Field</span></abbr>"       
                                ControlToValidate="irisCaptchaText"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="RegularExpressionValidator8" 
                                runat="server" 
                                ErrorMessage="Invalid Entry"
                                Text="<abbr class='minitooltip'>Invalid Entry<span class='classic'>Answer should only be numbers.</span></abbr>" 
                                ControlToValidate="irisCaptchaText"
                                ValidationExpression="[0-9]*">
                                </asp:RegularExpressionValidator>

                            <asp:CompareValidator   ValidationGroup="newIrisRegisterValidate"
                                CssClass="validateRed drop"
                                ID="CompareValidator4" 
                                runat="server" 
                                ErrorMessage="Incorrect Code"
                                Text="<abbr class='minitooltip'>Incorrect Entry<span class='classic'>Your answer is incorrect. Please check your answer.</span></abbr>" 
                                ControlToCompare="irisCaptchaText"
                                ControlToValidate="irisRealCaptchaText"></asp:CompareValidator>

                        </div>

                        <hr style="margin-top: 20px"/>

                        <div class="buttonSection">
                            <asp:Button ID="irisRegisterSubmitButton" runat="server" Text="Register"  CssClass="registerButtons" ValidationGroup="newIrisRegisterValidate" />
                        </div>
                    </asp:PlaceHolder>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

