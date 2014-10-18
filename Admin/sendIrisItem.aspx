<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="sendIrisItem.aspx.vb" Inherits="Admin_sendIrisItem" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">

    <div class="box">
        <h2>Iris Send Item Form</h2>
        <hr />
        <p>Fill out the form, and the item will be sent to the character of your chocie.</p>
        <div class="inputSection">
            <p>Character Name:</p>
            <asp:TextBox ID="characterNameTextbox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="characterNameRequired" runat="server" ErrorMessage="Character Name Required" Text="<abbr class='minitooltip'>* Required<span class='classic'>Character Name Required</span></abbr>" ValidationGroup="sendItem" ControlToValidate="characterNameTextbox"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="characterRegEx"  ValidationGroup="sendItem"
                runat="server" 
                ErrorMessage="Invalid Character Name" 
                CssClass="validateRed drop"
                Text="<abbr class='minitooltip'>Invalid Character Name<span class='classic'>The character name cannot include any special characters.</span></abbr>"
                ControlToValidate="characterNameTextbox"
                ValidationExpression="[a-zA-Z0-9]*">
            </asp:RegularExpressionValidator>
        </div>

        <div class="inputSection">
            <p>Item ID:</p>
            <asp:TextBox ID="itemIDTextbox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="itemIDRequired" runat="server" ErrorMessage="ItemID Required" Text="<abbr class='minitooltip'>* Required<span class='classic'>Item ID Is Required</span></abbr>" ValidationGroup="sendItem" ControlToValidate="itemIDTextbox"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="RegularExpressionValidator1"  ValidationGroup="sendItem"
                runat="server" 
                ErrorMessage="Invalid Item ID" 
                CssClass="validateRed drop"
                Text="<abbr class='minitooltip'>Invalid Item ID<span class='classic'>The Item ID is only Numbers</span></abbr>"
                ControlToValidate="itemIDTextbox"
                ValidationExpression="[0-9]*">
            </asp:RegularExpressionValidator>
        </div>
        <div class="inputSection">
            <p>Amount:</p>
            <asp:TextBox ID="countTextbox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="amountIsRequired" runat="server" ErrorMessage="Amount Required" Text="<abbr class='minitooltip'>* Required<span class='classic'>Amount Is Required</span></abbr>" ValidationGroup="sendItem" ControlToValidate="countTextbox"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="amountREV"  ValidationGroup="sendItem"
                runat="server" 
                ErrorMessage="Invalid Amount" 
                CssClass="validateRed drop"
                Text="<abbr class='minitooltip'>Invalid Amount<span class='classic'>The amount can only be numeric.</span></abbr>"
                ControlToValidate="countTextbox"
                ValidationExpression="[0-9]*">
            </asp:RegularExpressionValidator>
        </div>

        <div class="buttonSection">
            <asp:Button CssClass="registerButtons" ID="sendCharItemButton" runat="server" ValidationGroup="sendItem" CausesValidation="true" Text="Send Item" />
        </div>

    </div>
</asp:Content>

