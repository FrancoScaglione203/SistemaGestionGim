<%@ Page Title="Cupones" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cupones.aspx.cs" Inherits="SistemaGestionGim.Cupones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel {
            border: 1px solid #ddd;
            padding: 1rem;
            margin-bottom: 1rem;
            border-radius: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="mt-4">Cupones</h2>

        <asp:Button ID="btnAgregarCupon" runat="server" Text="Agregar Cupón" CssClass="btn btn-primary" OnClick="btnAgregarCupon_Click" Visible="true" />

        <asp:Repeater ID="repeaterCupones" runat="server" Visible="true">
            <ItemTemplate>
                <div class="panel">
                    <p>Código: <%# Eval("Codigo") %></p>
                    <p>Descuento: <%# Eval("Descuento") %>%</p>
                    <p>Fecha de Vencimiento: <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %></p>
                    <asp:Button ID="btnCancelarCupon" runat="server" Text="Cancelar" CssClass="btn btn-danger" CommandArgument='<%# Eval("ID") %>' OnClick="btnCancelarCupon_Click" />
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Panel para agregar/editar cupones -->
        <asp:Panel ID="panelAgregarCupon" runat="server" Visible="false" CssClass="panel">
            <div class="card my-4">
                <div class="card-body">
                    <h5 class="card-title">Agregar Cupón</h5>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCodigo" />
                        <label for="txtCodigo" class="form-label">Código</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDescuento" TextMode="Number" />
                        <label for="txtDescuento" class="form-label">Porcentaje de Descuento</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaVencimiento" TextMode="Date" />
                        <label for="txtFechaVencimiento" class="form-label">Fecha de Vencimiento</label>
                    </div>
                    <div class="mt-3">
                        <asp:Button ID="btnGuardarCupon" runat="server" Text="Guardar" CssClass="btn btn-success w-50" OnClick="btnGuardarCupon_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary w-50 mt-2" OnClick="btnCancelar_Click" />
                    </div>
                    <% if (!(Session["validacionCupon"] == null)) { %>
                        <label class="text-danger form-label"><%= Session["validacionCupon"] %></label>
                    <% } %>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
