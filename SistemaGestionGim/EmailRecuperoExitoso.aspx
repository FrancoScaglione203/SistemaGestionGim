<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmailRecuperoExitoso.aspx.cs" Inherits="SistemaGestionGim.EmailRecuperoExitoso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-5">
        <h2>¡Mail enviado!</h2>
        <p>Se envio un mail a la direccion de correo de tu usuario para restablecer la contraseña</p>
        <p>Serás redirigido a la página de inicio de sesión en 15 segundos.</p>
    </div>

    <script type="text/javascript">
        
        setTimeout(function () {
            window.location.href = 'Login.aspx'; // Cambia 'Login.aspx' al nombre de tu página de inicio de sesión
        }, 15000);
    </script>
</asp:Content>
