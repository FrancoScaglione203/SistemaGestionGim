<%@ Page Title="Registro Exitoso" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegisterExitoso.aspx.cs" Inherits="SistemaGestionGim.RegisterExitoso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-5">
        <h2>¡Registro Exitoso!</h2>
        <p>Tu registro ha sido completado satisfactoriamente.</p>
        <p>Serás redirigido a la página de inicio de sesión en 5 segundos.</p>
    </div>

    <script type="text/javascript">
        // Esperar 5 segundos (5000 milisegundos) antes de redirigir
        setTimeout(function () {
            window.location.href = 'Login.aspx'; // Cambia 'Login.aspx' al nombre de tu página de inicio de sesión
        }, 5000);
    </script>
</asp:Content>