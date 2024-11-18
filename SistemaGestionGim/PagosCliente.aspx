<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PagosCliente.aspx.cs" Inherits="SistemaGestionGim.PagosCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function confirmarPago() {
            return confirm("¿Está seguro de que desea realizar el pago?");
        }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <h2 class="mt-4">Pagos</h2>

        <!-- Sección para Cuotas Mensuales -->
        <h3>Cuotas Mensuales</h3>
        <div class="row">
            <div class="col-md-6">
                <h4>Pagados</h4>
                <asp:GridView ID="gvPagosMensualesPagados" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Mes" HeaderText="Mes" />
                        <asp:BoundField DataField="Anio" HeaderText="Año" />
                        <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido usuario" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha pago" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-6">
                <h4>No Pagados</h4>
                <asp:GridView ID="gvPagosMensualesNoPagados" DataKeyNames="IdUsuario" OnRowCommand="GvPagosMensualesNoPagados_RowCommand" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                         <asp:BoundField DataField="IdUsuario" HeaderText="Id Usuario" Visible="False" />
                        <asp:BoundField DataField="Mes" HeaderText="Mes" />
                        <asp:BoundField DataField="Anio" HeaderText="Año" />
                        <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido usuario" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
                        <asp:ButtonField Text="Pagar" CommandName="Pagar" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Sección para Clases -->
        <h3>Clases</h3>
        <div class="row">
            <div class="col-md-6">
                <h4>Pagados</h4>
                <asp:GridView ID="gvPagosClasePagados"   runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="IdUsuario" HeaderText="Id Usuario" Visible="False" />
                        <asp:BoundField DataField="IdClase" HeaderText="Id Clase" Visible="False" />
                        <asp:BoundField DataField="FechaClase" HeaderText="Fecha clase" />
                        <asp:BoundField DataField="NombreClase" HeaderText="Nombre clase" />
                        <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido usuario" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha pago" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-6">
                <h4>No Pagados</h4>
                <asp:GridView ID="gvPagosClaseNoPagados" DataKeyNames="IdUsuario,IdClase" OnRowCommand="gvPagosClaseNoPagados_RowCommand" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="IdUsuario" HeaderText="Id Usuario" Visible="false" />
                        <asp:BoundField DataField="IdClase" HeaderText="Id Clase" Visible="false" />
                        <asp:BoundField DataField="FechaClase" HeaderText="Fecha clase" />
                        <asp:BoundField DataField="NombreClase" HeaderText="Nombre clase" />
                        <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido usuario" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
                        <asp:ButtonField Text="Pagar" CommandName="Pagar" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Aquí va el modal -->
    <div class="modal fade" id="confirmarPagoModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirmar Pago</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    ¿Está seguro de que desea realizar el pago para este registro?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarPago" runat="server" CssClass="btn btn-primary" Text="Confirmar Pago" OnClick="btnConfirmarPago_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
