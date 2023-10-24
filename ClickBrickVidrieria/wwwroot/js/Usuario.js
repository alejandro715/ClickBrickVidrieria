let datatable;

$(document).ready(function () {


    loadDataTable();

});
function loadDataTable() {


    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Usuario/ObtenerTodos"
        },
        "columns": [
            { "data": "email" },
            { "data": "nombres" },
            { "data": "apellidos" },
            { "data": "phoneNumber" },
            { "data": "role" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                    },
                "render": function (data) {
                    console.log("id:", data);
                    let hoy = new Date().getTime();
                    let bloqueo = new Date(data.lockoutEnd).getTime();
                    if (bloqueo > hoy) {
                        //usuario bloqueado
                        return `
                    <div class="text-center">
                        
                        </a>
                        <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-danger text-white" style="cursor-pointer", width: 150px>
                        <i class="bi bi-unlock"></i> Desbloquear
                        </a>
                    </div>
                    `;
                    }
                    else {
                        return `
                    <div class="text-center">
                        
                        </a>
                        <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-success text-white" style="cursor-pointer" width: 150px>
                        <i class="bi bi-lock"></i> Bloquear
                        </a>
                    </div>
                    `;
                    }
                    
                }
            }
        ],

        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.13.3/i18n/es-ES.json"
        }


    });
}

function BloquearDesbloquear(id) {

                $.ajax({
                    type: "POST",
                    url: '/Admin/Usuario/BloquearDesbloquear',
                    data: JSON.stringify(id),
                    contentType: "application/json",
                    success: function (data) {

                        if (data.success) {

                            toastr.success(data.message);
                            datatable.ajax.reload();
                        }
                        else {

                            toastr.error(data.message);

                        }

                    }

                });
    }
