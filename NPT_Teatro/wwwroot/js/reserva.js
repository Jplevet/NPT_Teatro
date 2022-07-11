var dataTable;

$(document).ready(function () {
    cargarDatatable();

});



function cargarDatatable() {
    dataTable = $("#tblFunciones").DataTable({
        "ajax": {
            "url": "/admin/funciones/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "fecha", "width": "25%" },
            { "data": "obra.nombre", "width": "15%" },
            { "data": "cupo", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Cliente/Home/Reservar/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-edit'></i> Editar
                            </a>
                            &nbsp;
                            <a onclick=Delete("/Admin/Funciones/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-trash-trash-atl'></i> Borrar
                            </a>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}




