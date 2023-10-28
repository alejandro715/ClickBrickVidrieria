let datatable;

$(document).ready(function () {


    loadDataTable();

});
function loadDataTable() {


    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Inventario/Inventario/ObtenerTodos"
        },
        "columns": [
            { "data": "bodega.nombre" },
            { "data": "producto.descripcion" },
            {
                "data": "producto.costo", "className": "text-end",
                "render": function (data) {
                    var d = data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    return d
                }
            },
            { "data": "cantidad", "className": "text-end"},

        ],

        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.13.3/i18n/es-ES.json"
        }


    });
}
