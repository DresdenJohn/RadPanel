<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="addGuildPoints.aspx.vb" Inherits="Admin_addGuildPoints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Add Guild Points - Eternal Admin Panel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="meta" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    <div class="box">

        <h2>Add Points to Guild</h2>
        <h3>Cause they deserve it! Totally!</h3>
        <hr />

        <asp:Label ID="resultLabel" Visible="false" runat="server"></asp:Label>

        <div class="inputSection">
            <p>Amount of Levels:</p>
            <asp:TextBox ID="guildLevelsTextbox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="characterRequired" runat="server" ErrorMessage="Levels Required" Text="<abbr class='minitooltip'>* Required<span class='classic'>Character Name Required</span></abbr>" ValidationGroup="irisRedemption" ControlToValidate="guildLevelsTextbox"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="characterRegEx"  ValidationGroup="irisRedemption"
                runat="server" 
                ErrorMessage="Invalid Amount" 
                CssClass="validateRed drop"
                Text="<abbr class='minitooltip'>Invalid Amount<span class='classic'>The amount must be a number.</span></abbr>"
                ControlToValidate="guildLevelsTextbox"
                ValidationExpression="[0-9]*">
            </asp:RegularExpressionValidator>
        </div>
        <div class="inputSection">
            <p>Amount of Points:</p>
            <asp:TextBox ID="guildPointsTextbox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Points Required" Text="<abbr class='minitooltip'>* Required<span class='classic'>Character Name Required</span></abbr>" ValidationGroup="irisRedemption" ControlToValidate="guildPointsTextbox"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="RegularExpressionValidator1"  ValidationGroup="irisRedemption"
                runat="server" 
                ErrorMessage="Invalid Amount" 
                CssClass="validateRed drop"
                Text="<abbr class='minitooltip'>Invalid Amount<span class='classic'>The amount must be a number.</span></abbr>"
                ControlToValidate="guildPointsTextbox"
                ValidationExpression="[0-9]*">
            </asp:RegularExpressionValidator>
        </div>

        <div class="buttonSection">
            <asp:Button CssClass="submitButtons" ID="addGuildPointsButton" runat="server" ValidationGroup="irisRedemption" CausesValidation="true" Text="Apply" />
        </div>

    </div>
</asp:Content>

