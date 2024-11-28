<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="SistemaGestionGim.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            max-width: 1200px;
            margin: 0 auto;
        }

        .table-container {
            margin-bottom: 2rem;
            border: 1px solid #ddd;
            padding: 1rem;
            border-radius: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="mt-4">Pagos</h2>


        <!-- Botón para mostrar el panel de filtros -->
        <div class="mb-3">
            <asp:Button ID="btnMostrarFiltrosMensual" runat="server" Text="Filtro" CssClass="btn btn-info" OnClick="btnMostrarFiltrosMensual_Click" />
        </div>

        <!-- Panel de filtros pagos clases oculto -->
        <asp:Panel ID="panelFiltros" runat="server" Visible="false">
            <div class="table-container">
                <h4>Filtro pagos mensuales</h4>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label Text="Campo" runat="server" />
                        <asp:DropDownList ID="ddlCampoMensual" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCampoMensual_SelectedIndexChanged">
                            <asp:ListItem Text="-" />
                            <asp:ListItem Text="Apellido usuario" />
                            <asp:ListItem Text="Fecha" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Criterio" runat="server" ID="lblDdlCriterioMensual" Visible="false" />
                        <asp:DropDownList ID="ddlCriterioMensual" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCriterioMensual_SelectedIndexChanged" Visible="false">
                            <asp:ListItem Text="Contiene" />
                            <asp:ListItem Text="Comienza con" />
                            <asp:ListItem Text="Termina con" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Mes" ID="lblDdlMesMensual" runat="server" Visible="false" />
                        <asp:DropDownList ID="ddlMesMensual" AutoPostBack="true" runat="server" CssClass="form-control" Visible="false">
                            <asp:ListItem Text="1" />
                            <asp:ListItem Text="2" />
                            <asp:ListItem Text="3" />
                            <asp:ListItem Text="4" />
                            <asp:ListItem Text="5" />
                            <asp:ListItem Text="6" />
                            <asp:ListItem Text="7" />
                            <asp:ListItem Text="8" />
                            <asp:ListItem Text="9" />
                            <asp:ListItem Text="10" />
                            <asp:ListItem Text="11" />
                            <asp:ListItem Text="12" />
                        </asp:DropDownList>
                  
                        <asp:Label Text="Año" ID="lblDdlAnioMensual" runat="server" Visible="false" />
                        <asp:DropDownList ID="ddlAnioMensual" AutoPostBack="true" runat="server" CssClass="form-control" Visible="false">
                            <asp:ListItem Text="2024" />
                            <asp:ListItem Text="2023" />
                            <asp:ListItem Text="2022" />
                            <asp:ListItem Text="2021" />
                            <asp:ListItem Text="2020" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Filtro" ID="lblTxtFiltroAvanzadoMensual" runat="server" Visible="false" />
                        <asp:TextBox runat="server" ID="txtFiltroAvanzadoMensual" CssClass="form-control" Visible="false" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Pago" runat="server" />
                        <asp:DropDownList ID="ddlPagosMensual" AutoPostBack="true" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Todos" />
                            <asp:ListItem Text="Si" />
                            <asp:ListItem Text="No" />
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Botones Buscar y Borrar Filtro -->
                <div class="row mt-3">
                    <div class="col-md-1">
                        <asp:Button ID="btnBuscarMensual" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarMensual_Click" />
                    </div>

                    <div class="col-md-9">
                        <asp:Button ID="btnBorrarFiltroMensual" runat="server" Text="Limpiar Filtro" CssClass="btn btn-secondary" OnClick="btnBorrarFiltroMensual_Click" />
                    </div>

                    <div class="col-md-2">
                        <asp:Button ID="btnCancelarFiltrosMensual" runat="server" Text="Cancelar filtro" CssClass="btn btn-info" OnClick="btnCancelarFiltrosMensual_Click" Visible="false" />
                    </div>
                </div>
            </div>
            <!-- Cierre del div.table-container -->
        </asp:Panel>

        <!-- GridView para Pagos Mensuales -->
        <h4>Pagos Mensuales</h4>
        <asp:GridView ID="gvPagosMensuales" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                
                
                <asp:BoundField DataField="Mes" HeaderText="Mes" />
                <asp:BoundField DataField="Anio" HeaderText="Año" />
                <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido usuario" />
                <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
                <asp:BoundField DataField="FechaPago" HeaderText="Fecha pago" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Pagado" HeaderText="Estado pago" DataFormatString="{0:Si;0:No}" />
            </Columns>
        </asp:GridView>




        <!-- Botón para mostrar el panel de filtros -->
        <div class="mb-3">
            <asp:Button ID="btnMostrarFiltrosClase" runat="server" Text="Filtro" CssClass="btn btn-info" OnClick="btnMostrarFiltrosClase_Click" />
        </div>

        <!-- Panel de filtros pagos clases oculto -->
        <asp:Panel ID="panelFiltrosClase" runat="server" Visible="false">
            <div class="table-container">
                <h4>Filtro pagos clases</h4>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label Text="Campo" runat="server" />
                        <asp:DropDownList ID="ddlCampoClase" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCampoClase_SelectedIndexChanged">
                            <asp:ListItem Text="-" />
                            <asp:ListItem Text="Nombre clase" />
                            <asp:ListItem Text="Apellido usuario" />
                            <asp:ListItem Text="Fecha" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Criterio" runat="server" ID="lblDdlCriterioClase" Visible="false" />
                        <asp:DropDownList ID="ddlCriterioClase" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCriterioClase_SelectedIndexChanged" Visible="false">
                            <asp:ListItem Text="Contiene" />
                            <asp:ListItem Text="Comienza con" />
                            <asp:ListItem Text="Termina con" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Mes" ID="lblDdlMesClase" runat="server" Visible="false" />
                        <asp:DropDownList ID="ddlMesClase" AutoPostBack="true" runat="server" CssClass="form-control" Visible="false">
                            <asp:ListItem Text="1" />
                            <asp:ListItem Text="2" />
                            <asp:ListItem Text="3" />
                            <asp:ListItem Text="4" />
                            <asp:ListItem Text="5" />
                            <asp:ListItem Text="6" />
                            <asp:ListItem Text="7" />
                            <asp:ListItem Text="8" />
                            <asp:ListItem Text="9" />
                            <asp:ListItem Text="10" />
                            <asp:ListItem Text="11" />
                            <asp:ListItem Text="12" />
                        </asp:DropDownList>

                        <asp:Label Text="Año" ID="lblDdlAnioClase" runat="server" Visible="false" />
                        <asp:DropDownList ID="ddlAnioClase" AutoPostBack="true" runat="server" CssClass="form-control" Visible="false">
                            <asp:ListItem Text="2024" />
                            <asp:ListItem Text="2023" />
                            <asp:ListItem Text="2022" />
                            <asp:ListItem Text="2021" />
                            <asp:ListItem Text="2020" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Filtro" ID="lblTxtFiltroAvanzadoClase" runat="server" Visible="false" />
                        <asp:TextBox runat="server" ID="txtFiltroAvanzadoClase" CssClass="form-control" Visible="false" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label Text="Pago" runat="server" />
                        <asp:DropDownList ID="ddlPagosClase" AutoPostBack="true" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Todos" />
                            <asp:ListItem Text="Si" />
                            <asp:ListItem Text="No" />
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Botones Buscar y Borrar Filtro -->
                <div class="row mt-3">
                    <div class="col-md-1">
                        <asp:Button ID="btnBuscarClase" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarClase_Click" />
                    </div>

                    <div class="col-md-9">
                        <asp:Button ID="btnBorrarFiltroClase" runat="server" Text="Limpiar Filtro" CssClass="btn btn-secondary" OnClick="btnBorrarFiltroClase_Click" />
                    </div>

                    <div class="col-md-2">
                        <asp:Button ID="btnCancelarFiltrosClase" runat="server" Text="Cancelar filtro" CssClass="btn btn-info" OnClick="btnCancelarFiltrosClase_Click" Visible="false" />
                    </div>
                </div>
            </div>
            <!-- Cierre del div.table-container -->
        </asp:Panel>




        <!-- GridView para Pagos por Clase -->

        <h4>Pagos por Clase</h4>
        <asp:GridView ID="gvPagosClase" runat="server" CssClass="table table-striped table-fixed" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="FechaClase" HeaderText="Fecha clase" />
                <asp:BoundField DataField="NombreClase" HeaderText="Nombre clase" />
                <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido usuario" />
                <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
                <asp:BoundField DataField="FechaPago" HeaderText="Fecha pago" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Pagado" HeaderText="Estado pago" DataFormatString="{0:Si;0:No}" />
            </Columns>
        </asp:GridView>
    </div>
    <!-- Cierre del div.container -->
</asp:Content>
