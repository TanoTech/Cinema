<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prenotazioni.aspx.cs" Inherits="Cinema.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Prenotazioni</title>
</head>
<body>
  <form id="form1" runat="server">
 <div>
  <label for="txtNome">Nome:</label>
  <asp:TextBox ID="txtNome" runat="server" Required="true" />

  <label for="txtCognome">Cognome:</label>
  <asp:TextBox ID="txtCognome" runat="server" Required="true" />

  <label for="ddlSala">Sala:</label>
  <asp:DropDownList ID="ddlSala" runat="server">
  </asp:DropDownList>

  <label for="chkPrezzoRidotto">Prezzo Ridotto:</label>
  <asp:CheckBox ID="chkPrezzoRidotto" runat="server"/>

  <asp:Button ID="btnPrenota" runat="server" Text="Prenota" OnClick="btnPrenota_Click" />


 </div>

<asp:PlaceHolder ID="phPrenotazioni" runat="server"></asp:PlaceHolder>

</form>

</body>
</html>