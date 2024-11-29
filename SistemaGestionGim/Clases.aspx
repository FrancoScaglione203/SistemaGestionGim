<%@ Page Title="Clases Disponibles" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Clases.aspx.cs" Inherits="SistemaGestionGim.Clases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-horizontal {
            display: flex;
            flex-direction: row;
            margin-bottom: 1rem;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0px 2px 5px rgba(0,0,0,0.1);
        }

        .card-body {
            padding: 1rem;
            flex: 1;
        }

        .card-details {
            display: flex;
            justify-content: space-between;
            margin-bottom: 0.5rem;
        }

        .card-actions {
            display: flex;
            justify-content: flex-end;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <h2 class="mt-4">Clases Disponibles</h2>
        <asp:Button ID="btnOcultarClasesDisp" runat="server" Text="Ocultar lista de clases disponibles" CssClass="btn btn-primary" OnClick="btnOcultarClasesDisp_Click" />
        <div>
            <% if ((Session["validacionInscripcion"] != null))
                { %>
            <label for="txtClave2" class="text-danger form-label"><%= Session["validacionInscripcion"] %></label>
            <% } %>
        </div>
        <div class="row">
            <!-- Repeater para mostrar las tarjetas de las clases disponibles -->
            <asp:Repeater ID="repeaterClasesDisponibles" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <div class="col-12">
                        <div class="card-horizontal">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                <div class="card-details">
                                    <div>
                                        <p>
                                            <img style="height: 100px; width: 100px; border-radius: 80%;" src='<%# GetImageUrl(Eval("Id").ToString()) %>'>
                                        </p>
                                        <p>Fecha y Hora: <%# Eval("FechaHorario", "{0:dd/MM/yyyy HH:mm}") %></p>
                                        <p>Capacidad: <%# Eval("Capacidad") %> personas</p>
                                        <p>Inscriptos: <%# Eval("Inscriptos") %></p>
                                        <p>Importe: $<%# Eval("Importe") %></p>
                                    </div>
                                </div>
                                <div class="card-actions">
                                    <asp:Button ID="btnInscribirse" runat="server" Text="Inscribirse" CssClass="btn btn-primary"
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnInscribirse_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

        <h2 class="mt-4">Mis Clases Inscriptas</h2>
        <asp:Button ID="btnOcultarClasesInsc" runat="server" Text="Ocultar lista de clases inscriptas" CssClass="btn btn-primary" OnClick="btnOcultarClasesInsc_Click" />
        <div class="row">
            <!-- Repeater para mostrar las tarjetas de las clases en las que el usuario está inscrito -->
            <asp:Repeater ID="repeaterClasesInscriptas" runat="server">
                <ItemTemplate>
                    <div class="col-12">
                        <div class="card-horizontal">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                <div class="card-details">
                                    <div>
                                        <p>
                                            <img style="height: 100px; width: 100px; border-radius: 80%;" src='<%# GetImageUrl(Eval("Id").ToString()) %>'>
                                        </p>
                                        <p>Fecha y Hora: <%# Eval("FechaHorario", "{0:dd/MM/yyyy HH:mm}") %></p>
                                        <p>Capacidad: <%# Eval("Capacidad") %> personas</p>
                                        <p>Importe: $<%# Eval("Importe") %></p>
                                    </div>
                                </div>
                                <div class="card-actions">
                                    <asp:Button ID="btnCancelarSuscripcion" runat="server" Text="Cancelar Suscripción" CssClass="btn btn-danger"
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnCancelarSuscripcion_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
