<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaGestionGim.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="col-md-4" aria-labelledby="hostingTitle">
    <asp:GridView ID="dgvUsuarios" runat="server" DataKeyNames="Id"
        CssClass="table table-striped text-center" AutoGenerateColumns="false" AllowPaging="True" PageSize="10">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
        </Columns>
    </asp:GridView>
</section>
</asp:Content>
