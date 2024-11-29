<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Planes.aspx.cs" Inherits="SistemaGestionGim.Planes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-check-label {
            color: black; /* Color negro para el texto */
        }

        .form-check-input:checked {
            background-color: blue; /* O verde si prefieres, cambia a 'green' */
            border-color: blue; /* O verde si prefieres, cambia a 'green' */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center mt-4">
            <div class="text-center">
                <h6 class="fw-bold my-3">Planes Disponibles</h6>

                <!-- Tarjeta para mostrar el plan actual del usuario -->
                <div class="card my-4" style="width: 100%;">
                    <div class="card-body">
                        <h5 class="card-title">Tu Plan Actual</h5>
                        <p class="card-text">
                            <asp:Label runat="server" ID="lblDescripcionPlanActual" />
                            <br />
                            <asp:Label runat="server" ID="lblImportePlanActual" />
                        </p>
                    </div>
                </div>

                <!-- Tarjeta para mostrar el plan nuevo que quiere el usuario -->
                <asp:Panel runat="server" ID="panelNuevoPlan" Visible="false">
                    <div class="card my-4" style="width: 100%;">
                        <div class="card-body">
                            <h5 class="card-title">Tu Plan Nuevo</h5>
                            <p class="card-text">
                                <asp:Label runat="server" ID="lblDescripcionPlanNuevo" />
                                <br />
                                <asp:Label runat="server" ID="lblImportePlanNuevo" />
                            </p>
                        </div>
                    </div>
                </asp:Panel>

                <!-- Repetir el bloque de tarjeta para cada plan disponible -->
                <div class="row">
                    <asp:Repeater ID="repeaterPlanes" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4">
                                <div class="card my-4">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                        <p class="card-text">
                                            Importe: $<%# Eval("Importe", "{0:N2}") %><br />
                                            Descuento: <%# Eval("DescuentoClases") %>%
                                        </p>

                                        <div class="form-check">
                                            <asp:CheckBox runat="server" CssClass="form-check-input"
                                                ID="chkMaquinas"
                                                Checked='<%# Eval("Maquinas") %>'
                                                Enabled="false" />
                                            <label class="form-check-label" for="chkMaquinas">Acceso a Maquinas</label>
                                        </div>
                                        <div class="form-check">
                                            <asp:CheckBox runat="server" CssClass="form-check-input"
                                                ID="chkSeguimiento"
                                                Checked='<%# Eval("Seguimiento") %>'
                                                Enabled="false" />
                                            <label class="form-check-label" for="chkSeguimiento">Seguimiento Personalizado</label>
                                        </div>
                                        <div class="form-check">
                                            <asp:CheckBox runat="server" CssClass="form-check-input"
                                                ID="chkLocker"
                                                Checked='<%# Eval("Locker") %>'
                                                Enabled="false" />
                                            <label class="form-check-label" for="chkLocker">Acceso a Locker</label>
                                        </div>

                                        <asp:Button Text="Cambiar Plan" CssClass="btn btn-warning mt-3" runat="server"
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClick="btnCambiarPlan_Click" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!-- Botón para confirmar el cambio de plan -->
                <asp:Button ID="btnConfirmarCambio" runat="server" Text="Confirmar cambio de plan"
                    CssClass="btn btn-success mt-3" Visible="false" OnClick="btnConfirmarCambio_Click" />

                    <% if ((Session["validacionPlan"] != null))
                        { %>
                    <asp:label ID="lblValidacion" runat="server" class="text-danger form-label" Visible="true" ><%= Session["validacionPlan"] %></asp:label>
                    <% } %>

            </div>
        </div>
    </div>
</asp:Content>
