<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" CodeFile="manageUsers.aspx.vb" Inherits="Admin_manageUsers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    
    <div class="box">
        <h2>User Management</h2>
        <p>For managing users...?</p>
        <hr />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            DataKeyNames="a_Username" DataSourceID="memberListing" ForeColor="#333333" 
            GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="a_Username" HeaderText="Username" ReadOnly="True" 
                    SortExpression="a_Username" />
                <asp:BoundField DataField="a_Email" HeaderText="Email" 
                    SortExpression="a_Email" />
                <asp:BoundField DataField="a_GlobalPin" HeaderText="GlobalPin" 
                    SortExpression="a_GlobalPin" />
                <asp:BoundField DataField="a_Authority" HeaderText="Authority" 
                    SortExpression="a_Authority" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:SqlDataSource ID="memberListing" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="<%$ ConnectionStrings:webPanelConnectionString %>" 
            DeleteCommand="DELETE FROM [WEB_ACCOUNTS_TBL] WHERE [a_Username] = @original_a_Username AND [a_Email] = @original_a_Email AND [a_Password] = @original_a_Password AND [a_GlobalPin] = @original_a_GlobalPin AND [a_Authority] = @original_a_Authority AND [a_DateRegistered] = @original_a_DateRegistered AND [a_LastLogin] = @original_a_LastLogin" 
            InsertCommand="INSERT INTO [WEB_ACCOUNTS_TBL] ([a_Username], [a_Email], [a_Password], [a_GlobalPin], [a_Authority], [a_DateRegistered], [a_LastLogin]) VALUES (@a_Username, @a_Email, @a_Password, @a_GlobalPin, @a_Authority, @a_DateRegistered, @a_LastLogin)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT * FROM [WEB_ACCOUNTS_TBL] ORDER BY [a_Username] DESC" 
            UpdateCommand="UPDATE [WEB_ACCOUNTS_TBL] SET [a_Email] = @a_Email, [a_Password] = @a_Password, [a_GlobalPin] = @a_GlobalPin, [a_Authority] = @a_Authority, [a_DateRegistered] = @a_DateRegistered, [a_LastLogin] = @a_LastLogin WHERE [a_Username] = @original_a_Username AND [a_Email] = @original_a_Email AND [a_Password] = @original_a_Password AND [a_GlobalPin] = @original_a_GlobalPin AND [a_Authority] = @original_a_Authority AND [a_DateRegistered] = @original_a_DateRegistered AND [a_LastLogin] = @original_a_LastLogin">
            <DeleteParameters>
                <asp:Parameter Name="original_a_Username" Type="String" />
                <asp:Parameter Name="original_a_Email" Type="String" />
                <asp:Parameter Name="original_a_Password" Type="String" />
                <asp:Parameter Name="original_a_GlobalPin" Type="Int32" />
                <asp:Parameter Name="original_a_Authority" Type="String" />
                <asp:Parameter Name="original_a_DateRegistered" Type="DateTime" />
                <asp:Parameter Name="original_a_LastLogin" Type="DateTime" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="a_Username" Type="String" />
                <asp:Parameter Name="a_Email" Type="String" />
                <asp:Parameter Name="a_Password" Type="String" />
                <asp:Parameter Name="a_GlobalPin" Type="Int32" />
                <asp:Parameter Name="a_Authority" Type="String" />
                <asp:Parameter Name="a_DateRegistered" Type="DateTime" />
                <asp:Parameter Name="a_LastLogin" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="a_Email" Type="String" />
                <asp:Parameter Name="a_Password" Type="String" />
                <asp:Parameter Name="a_GlobalPin" Type="Int32" />
                <asp:Parameter Name="a_Authority" Type="String" />
                <asp:Parameter Name="a_DateRegistered" Type="DateTime" />
                <asp:Parameter Name="a_LastLogin" Type="DateTime" />
                <asp:Parameter Name="original_a_Username" Type="String" />
                <asp:Parameter Name="original_a_Email" Type="String" />
                <asp:Parameter Name="original_a_Password" Type="String" />
                <asp:Parameter Name="original_a_GlobalPin" Type="Int32" />
                <asp:Parameter Name="original_a_Authority" Type="String" />
                <asp:Parameter Name="original_a_DateRegistered" Type="DateTime" />
                <asp:Parameter Name="original_a_LastLogin" Type="DateTime" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>

</asp:Content>

