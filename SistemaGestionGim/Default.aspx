<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaGestionGim.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <!-- Columna izquierda: Clases del día actual y siguiente -->
            <div class="col-md-6">
                <!-- Clases del día actual -->
                <h4 class="text-center">Clases de Hoy</h4>
                <asp:Repeater ID="repeaterClasesHoy" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <div class="col-12 mb-3">
                            <div class="card-horizontal border shadow-sm p-3">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                    <div class="card-details">
                                        <div>
                                            <p>Fecha y Hora: <%# Eval("FechaHorario", "{0:dd/MM/yyyy HH:mm}") %></p>
                                            <p>Capacidad: <%# Eval("Capacidad") %> personas</p>
                                            <p>Inscriptos: <%# Eval("Inscriptos") %></p>
                                            <p>Importe: $<%# Eval("Importe") %></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <!-- Clases del día siguiente -->
                <h4 class="text-center">Clases de Mañana</h4>
                <asp:Repeater ID="repeaterClasesMañana" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <div class="col-12 mb-3">
                            <div class="card-horizontal border shadow-sm p-3">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                    <div class="card-details">
                                        <div>
                                            <p>Fecha y Hora: <%# Eval("FechaHorario", "{0:dd/MM/yyyy HH:mm}") %></p>
                                            <p>Capacidad: <%# Eval("Capacidad") %> personas</p>
                                            <p>Inscriptos: <%# Eval("Inscriptos") %></p>
                                            <p>Importe: $<%# Eval("Importe") %></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Columna derecha: Cartelera de novedades -->
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header bg-primary text-white text-center">
                        <h4>Cartelera de Novedades</h4>
                    </div>
                    <div class="card-body" style="height: 400px; overflow-y: auto;">
                        <asp:Label ID="lblNovedades" runat="server" Text="Aquí aparecerán las novedades..." CssClass="text-justify" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
