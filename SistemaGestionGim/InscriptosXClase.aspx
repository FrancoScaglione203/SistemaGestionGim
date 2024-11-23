<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InscriptosXClase.aspx.cs" Inherits="SistemaGestionGim.InscriptosXClase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex flex-column align-items-center mt-4" aria-labelledby="hostingTitle">
         <!-- Título de la clase -->
        <asp:Label ID="lblTituloClase" runat="server" CssClass="h3 mb-4"></asp:Label>
        <asp:Label ID="Label1" runat="server" CssClass="h5 mb-4">Inscriptos</asp:Label>
        <!-- GridView centrado -->
        <asp:GridView ID="dgvUsuarios" runat="server" DataKeyNames="Id"
            CssClass="table table-striped text-center" AutoGenerateColumns="false" AllowPaging="True" PageSize="10">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
            </Columns>
        </asp:GridView>

        <!-- Botón de "Volver" -->
        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3" OnClick="btnVolver_Click" />
    </section>
</asp:Content>
 