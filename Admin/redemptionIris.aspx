<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="redemptionIris.aspx.vb" Inherits="Admin_redemptionIris" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    
    <div class="box">
        
        <h2>Iris Donation Form</h2>
        <hr />
        <div class="inputSection">
            <p>Character Name:</p>
            <asp:TextBox ID="characterNameTextbox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="characterRequired" runat="server" ErrorMessage="Character Name Required" Text="<abbr class='minitooltip'>* Required<span class='classic'>Character Name Required</span></abbr>" ValidationGroup="irisRedemption" ControlToValidate="characterNameTextbox"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="characterRegEx"  ValidationGroup="irisRedemption"
                runat="server" 
                ErrorMessage="Invalid Character Name" 
                CssClass="validateRed drop"
                Text="<abbr class='minitooltip'>Invalid Character Name<span class='classic'>The character name cannot include any special characters.</span></abbr>"
                ControlToValidate="characterNameTextbox"
                ValidationExpression="[a-zA-Z0-9]*">
            </asp:RegularExpressionValidator>
        </div>
        <div class="inputSection">
            <p>Package:</p>
            <asp:DropDownList ID="packageDropdown" runat="server"></asp:DropDownList>
        </div>

        <div class="buttonSection">
            <asp:Button CssClass="submitButtons" ID="sendCharItemButton" runat="server" ValidationGroup="irisRedemption" CausesValidation="true" Text="Send Package" />
        </div>

    </div>

</asp:Content>

