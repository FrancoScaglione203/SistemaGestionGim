<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfirmacionPago.aspx.cs" Inherits="SistemaGestionGim.ConfirmacionPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Contenedor centrado */
        .container-centered {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 60vh; /* Ocupa toda la altura de la pantalla */
            background-color: #f8f9fa; /* Fondo claro */
        }

        /* Estilo de la tarjeta */
        .card {
            background-color: #ffffff; /* Fondo blanco */
            padding: 20px 30px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); /* Sombra ligera */
            width: 100%;
            max-width: 500px; /* Ancho máximo de la tarjeta */
            text-align: left;
            font-family: Arial, sans-serif;
        }

            /* Estilo para encabezados */
            .card h2 {
                text-align: center;
                margin-bottom: 20px;
                color: #333;
            }

            /* Estilo de etiquetas y campos */
            .card label {
                display: block;
                margin-bottom: 5px;
                font-weight: bold;
                color: #555;
            }

            .card .form-control {
                width: 100%;
                padding: 10px;
                border: 1px solid #ddd;
                border-radius: 5px;
                margin-bottom: 15px;
            }

            /* Grupo de botones */
            .card .button-group {
                text-align: center;
            }

            .card .btn {
                padding: 10px 20px;
                margin: 5px;
                border-radius: 5px;
                border: none;
                cursor: pointer;
            }

            .card .btn-success {
                background-color: #28a745;
                color: #fff;
            }

            .card .btn-danger {
                background-color: #dc3545;
                color: #fff;
            }
                /* Estilo para el contenedor del cupón */
    .coupon-container {
        display: flex;
        align-items: center;
        gap: 10px; /* Espacio entre el campo de texto y el botón */
    }

    /* Hacer el campo de texto más fino */
    .form-control-cupon {
        width: 200px; /* Ancho más estrecho */
        padding: 5px 10px;
    }

    /* Estilo para el botón de validación */
    .btn-validar {
        padding: 8px 15px;
        font-size: 14px;
        background-color: #007bff; /* Color azul */
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
    }

    .btn-validar:hover {
        background-color: #0056b3; /* Color azul más oscuro cuando se pasa el ratón */
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-centered">
        <asp:Panel ID="pnlConfirmacionPago" runat="server" CssClass="card">
            <h2>Confirmación de Pago</h2>

            <!-- Campo para el Nombre/Descripción de la clase -->
            <asp:Panel ID="pnlClase" runat="server" Visible="false">
                <label for="lblClase">Clase:</label>
                <asp:Label ID="lblClase" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <!-- Campos para Mes y Año -->
            <asp:Panel ID="pnlMensual" runat="server" Visible="false">
                <label for="lblMes">Mes:</label>
                <asp:Label ID="lblMes" runat="server" Text=""></asp:Label>
                <br />
                <label for="lblAnio">Año:</label>
                <asp:Label ID="lblAnio" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <!-- Campo para el importe -->
            <label for="lblImporte">Importe:</label>
            <asp:Label ID="lblImporte" runat="server" Text=""></asp:Label>
            <br />

            <!-- Campo para el cupón y botón de validación -->
            <label for="txtCupon">Cupón:</label>
            <div class="coupon-container">
                <asp:TextBox ID="txtCupon" runat="server" CssClass="form-control form-control-cupon" ></asp:TextBox>
                <asp:Button ID="btnValidar" runat="server" Text="Validar" CssClass="btn-validar" OnClick="btnValidar_Click" />
            </div>

            <% if (!(Session["validacionCupon"] == null)) { %>
                <label class="text-danger form-label"><%= Session["validacionCupon"] %></label>
            <% } %>

            <!-- Campo para el descuento -->
            <label for="lblDescuento">Descuento:</label>
            <asp:Label ID="lblDescuento" runat="server" Text=""></asp:Label>
            <br />

            <!-- Campo para el importe Final -->
            <label for="lblImporteFinal">ImporteFinal:</label>
            <asp:Label ID="lblImporteFinal" runat="server" Text=""></asp:Label>
            <br />

            <!-- Botones -->
            <div class="button-group">
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-success" OnClick="btnConfirmar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

