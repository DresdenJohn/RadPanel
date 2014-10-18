<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="irisBanForm.aspx.vb" Inherits="Admin_irisBanForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <div class="box">

        <h2>Iris Banning Script</h2>
        <h3>Wield the mighty banhammer! (Swing responsibly)</h3>
        <hr />
        <p><asp:Label ID="resultLabel" runat="server" Text="" Visible="false"></asp:Label></p>
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

            <p>Action:</p>

            <asp:DropDownList ID="actionDropdown" runat="server"></asp:DropDownList>

        </div>

        <div class="buttonSection">

            <asp:Button CssClass="registerButtons" ID="banButton" runat="server" Text="Apply Action" />

        </div>

    </div>
</asp:Content>

