<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestForm.aspx.cs" Inherits="WebClient.RequestForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ISO 8583 Testing Form</title>
    <script type="text/javascript">
        function cancelNonNumricKey(e) {
            if (!e) e = window.event;
            var iKeyCode = e.charCode ? e.charCode : e.keyCode;
            if (48 <= iKeyCode && iKeyCode <= 57) return; //Numeric keys
            if (iKeyCode == 8) return; //backspace
            if (e.charCode === 0) { //Firefox
                if (iKeyCode == 37 || iKeyCode == 39 || iKeyCode == 46) return; //left arrow, right arrow, delete
            }
            if (e.preventDefault) {
                e.preventDefault();
                e.stopPropagation();
            } else {
                e.returnValue = false;
                e.cancelBubble = true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlInput" runat="server">
        <table border="0">
        <tr>
            <td align="right" nowrap="nowrap">System Audit Trace Number:</td>
            <td><asp:TextBox ID="txtTraceNumber" runat="server" MaxLength="6" Columns="8" onkeypress="cancelNonNumricKey(arguments[0])"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" valign="top" nowrap="nowrap">Additional Data:</td>
            <td><asp:TextBox ID="txtAdtData" runat="server" Rows="5" Columns="50" TextMode="MultiLine" Wrap="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">Network Management Information Code:</td>
            <td><asp:TextBox ID="txtNMICode" runat="server" MaxLength="3" Columns="5" onkeypress="cancelNonNumricKey(arguments[0])"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" /></td>
        </tr>    
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlResponse" runat="server" Visible="false">
        <pre runat="server" id="iso8583Data"></pre><br />
        <asp:LinkButton ID="linkBack" runat="server" onclick="linkBack_Click">Back</asp:LinkButton>
    </asp:Panel>
    </form>
</body>
</html>
