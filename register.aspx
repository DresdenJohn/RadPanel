<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<asp:Content ID="titleContent" ContentPlaceHolderID="title" runat="server">
    Register | Swift Gaming Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <div class="box">

        <h2>Swift Gaming Network Registration</h2>
        <h3>You're awesome! We have a lot to offer and can't wait to enjoy your company. Please fill out this form and start engaging in our community! Please make sure to go over our rules and regulations whenever you have a chance. Most importantly: have fun!</h3>
        <hr />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        

        <asp:Label ID="registerResultLabel" runat="server" Text="<br/>" Visible="true"></asp:Label>

        <div class="inputSection">
            <p>
                <abbr class="tooltip">
                    Username:
                    <span class="classic">
                    Who you will be seen as to other people on the network. Choose wisely, it cannot be changed later on.
                    </span>
                </abbr>
            </p>
            <asp:TextBox ID="registerUsernameTextbox" ValidationGroup="newRegisterValidate" CssClass="stretchHundred" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator 
                ID="registerUsernameRequired" 
                runat="server" 
                ErrorMessage="Username Is Required" 
                Text="<abbr class='minitooltip'>*<span class='classic'>Username is Required</span></abbr>"  
                CssClass="validateRed drop"
                ControlToValidate="registerUsernameTextBox" ValidationGroup="newRegisterValidate"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator 
                ID="registerUsernameREV"   ValidationGroup="newRegisterValidate"
                CssClass="validateRed drop"
                runat="server" 
                ErrorMessage="<abbr class='minitooltip'>Invalid Username<span class='classic'>Your username cannot inlclude any special characters.</span></abbr>"
                ControlToValidate="registerUsernameTextBox"
                ValidationExpression="[a-zA-Z0-9]*" >
                </asp:RegularExpressionValidator>
        </div>
        
        

                        <div class="inputSection">
                            <p><abbr class="tooltip">
                                E-Mail:
                                <span class="classic">
                                Your E-mail will serve as your personal login to Swift Gaming Network. Only one account per E-mail is allowed.
                                </span>
                            
                            </abbr></p>
                        
                            <asp:TextBox CssClass="stretchHundred" ValidationGroup="newRegisterValidate" ID="registerEmailTextBox" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newRegisterValidate"
                                ID="registerEmailRequired"  
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="E-mail Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>E-Mail is Required</span></abbr>" 
                                ControlToValidate="registerEmailTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator
                                ID="registerEmailREV"  ValidationGroup="newRegisterValidate"
                                runat="server" 
                                ErrorMessage="Invalid E-mail" 
                                CssClass="validateRed drop"
                                Text="<abbr class='minitooltip'>Invalid E-Mail<span class='classic'>Your e-mail must be typed in the proper format (name@server.***)</span></abbr>" 
                                ControlToValidate="registerEmailTextBox"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                </asp:RegularExpressionValidator>

                        </div>

                        <div class="inputSection">
                        
                            <p><abbr class="tooltip">
                                Password:
                                <span class="classic">
                                Your chosen password. You will be asked for this when logging into Swift Gaming Network.
                                </span>
                            </abbr></p>
                            <asp:TextBox CssClass="stretchHundred" ValidationGroup="newRegisterValidate" id="registerPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator 
                                ID="registerPasswordRequired"  ValidationGroup="newRegisterValidate"
                                runat="server" 
                                ErrorMessage="Password Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Password is Required</span></abbr>"  
                                CssClass="validateRed drop"
                                ControlToValidate="registerPasswordTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator
                                ID="registerPasswordREV"   ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="Invalid Password"
                                Text="<abbr class='minitooltip'>Invalid Password<span class='classic'>Your password may not inlcude any special characters.</span></abbr>" 
                                ControlToValidate="registerPasswordTextBox"
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
                            
                            <asp:TextBox CssClass="stretchHundred" ValidationGroup="newRegisterValidate" ID="registerPasswordVerifyTextBox" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newRegisterValidate"
                                ID="registerVerifyPasswordRequired" 
                                runat="server" 
                                ErrorMessage="Password Verification Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Password Verification is Required</span></abbr>"  
                                CssClass="validateRed drop"
                                ControlToValidate="registerPasswordVerifyTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator  ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="registerVerifyPasswordREV" 
                                runat="server" 
                                ErrorMessage="Invalid Password"
                                Text="<abbr class='minitooltip'>Invalid Password<span class='classic'>Your password may not inlcude any special characters. Remember your password is case sensitive.</span></abbr>" 
                                ControlToValidate="registerPasswordVerifyTextBox"
                                ValidationExpression="[a-zA-Z0-9]*">
                                </asp:RegularExpressionValidator>

                            <asp:CompareValidator   ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="registerPasswordCompare" 
                                runat="server" 
                                ErrorMessage="Passwords do not match"
                                Text="<abbr class='minitooltip'>Password Mismatch<span class='classic'>Passwords do not match, please recheck your password. Remember they are case sensitive.</span></abbr>" 
                                ControlToCompare="registerPasswordTextBox"
                                ControlToValidate="registerPasswordVerifyTextBox"></asp:CompareValidator>

                        </div>
                    
                        <div class="inputSection">
                            <p><abbr class="tooltip">
                                Account Pin:
                                <span class="classic">
                                A secure 4 digit number that is required to make major account changes. Can also be used to recover your forgotten password.
                                </span>
                            </abbr></p>
                        
                            <asp:TextBox CssClass="stretchHundred" ValidationGroup="newRegisterValidate" ID="registerPinTextBox" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator   ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="registerPinRequired" 
                                runat="server" 
                                ErrorMessage="Account Pin Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Account Pin is Required</span></abbr>"
                                ControlToValidate="registerPinTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator  ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="registerPinREV" 
                                runat="server" 
                                ErrorMessage="Invalid Pin"
                                Text="<abbr class='minitooltip'>Invalid Pin<span class='classic'>Your pin can only be 4 numeric characters.</span></abbr>" 
                                ControlToValidate="registerPinTextBox"
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
                            <asp:TextBox CssClass="stretchHundred" ValidationGroup="newRegisterValidate" ID="registerPinVerifyTextBox" runat="server" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newRegisterValidate"
                                ID="registerPinVerifyRequired" 
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="Pin Verification Is Required" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Pin Verification is Required</span></abbr>"       
                                ControlToValidate="registerPinVerifyTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="RegularExpressionValidator1" 
                                runat="server" 
                                ErrorMessage="Invalid Pin"
                                Text="<abbr class='minitooltip'>Invalid Pin<span class='classic'>Your pin can only be 4 numeric characters.</span></abbr>" 
                                ControlToValidate="registerPinVerifyTextBox"
                                ValidationExpression="[0-9][0-9][0-9][0-9]">
                                </asp:RegularExpressionValidator>

                            <asp:CompareValidator   ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="registerPinCompare" 
                                runat="server" 
                                ErrorMessage="Pins do not match"
                                Text="<abbr class='minitooltip'>Pin Mismatch<span class='classic'>Pins do not match, please recheck your pin. Remeber they are 4 digit numbers.</span></abbr>" 
                                ControlToCompare="registerPinTextBox"
                                ControlToValidate="registerPinVerifyTextBox"></asp:CompareValidator>

                        </div>

                        <div class="inputSection">
                            <p>
                            <asp:Label ID="registerVerifyNumberLabel" runat="server" Text="Label"></asp:Label>
                            </p>

                            <asp:TextBox CssClass="stretchHundred" ID="registerVerifyNumberTextBox" runat="server" ValidationGroup="newRegisterValidate"></asp:TextBox>
                            <asp:TextBox ID="verifyNumberAnswer" runat="server" Width="0px" Height="0px" CssClass="trueHidden" ValidationGroup="newRegisterValidate"></asp:TextBox>

                            <asp:RequiredFieldValidator  ValidationGroup="newRegisterValidate"
                                ID="registerVerifyRequired" 
                                CssClass="validateRed drop"
                                runat="server" 
                                ErrorMessage="Required Field" 
                                Text="<abbr class='minitooltip'>*<span class='classic'>Required Field</span></abbr>"       
                                ControlToValidate="registerVerifyNumberTextBox"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="registerVerifyREV" 
                                runat="server" 
                                ErrorMessage="Invalid Entry"
                                Text="<abbr class='minitooltip'>Invalid Entry<span class='classic'>Answer should only be numbers.</span></abbr>" 
                                ControlToValidate="registerVerifyNumberTextBox"
                                ValidationExpression="[0-9]*">
                                </asp:RegularExpressionValidator>

                            <asp:CompareValidator   ValidationGroup="newRegisterValidate"
                                CssClass="validateRed drop"
                                ID="CompareValidator1" 
                                runat="server" 
                                ErrorMessage="Incorrect Code"
                                Text="<abbr class='minitooltip'>Incorrect Entry<span class='classic'>Your answer is incorrect. Please check your answer.</span></abbr>" 
                                ControlToCompare="verifyNumberAnswer"
                                ControlToValidate="registerVerifyNumberTextBox"></asp:CompareValidator>
                            

                        </div>

                        <div class="inputSection">
                            <asp:CheckBox ID="createIrisAccountCheck" Text="<abbr class='tooltip'>Create Flyff Account?<span class='classic'>Use your Network Account info to automatically register an Swift Flight Online account.</span></abbr>" runat="server" />
                        </div>

                        <hr style="margin-top: 20px"/>

                        <div class="buttonSection">
                            <asp:Button ID="registerSubmitButton" runat="server" Text="Register" CausesValidation="true" ValidationGroup="newRegisterValidate"  CssClass="registerButtons" />
                        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>