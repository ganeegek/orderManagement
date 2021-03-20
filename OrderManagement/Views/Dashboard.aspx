<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="OrderManagement.Views.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 329px">
    <form id="form1" runat="server">
        <p style="margin-left: 200px;margin-top:50px">
            <asp:Button ID="Button1" runat="server"  Text="Details" Width="192px" OnClick="Button1_Click" />
        </p>
        <p style="margin-left: 200px;margin-top:20px">
            <asp:Button ID="Button2" runat="server"  Text="Create Order" Width="192px" OnClick="Button2_Click" />
        </p>
        <%-- <p style="margin-left: 200px;margin-top:20px">
            <asp:Button ID="Button3" runat="server"  Text="Supplier Management" Width="192px" OnClick="Button3_Click" />
        </p>
         <p style="margin-left: 200px;margin-top:20px">
            <asp:Button ID="Button4" runat="server"  Text="Product Management" Width="192px" OnClick="Button4_Click" />
        </p>
         <p style="margin-left: 200px;margin-top:20px">
            <asp:Button ID="Button5" runat="server"  Text="Create Order" Width="192px" OnClick="Button5_Click" />
        </p>
        <p style="margin-left: 200px;margin-top:20px">
            <asp:Button ID="Button6" runat="server"  Text="Reports" Width="192px" OnClick="Button6_Click" />
        </p>--%>

    </form>
</body>
</html>
