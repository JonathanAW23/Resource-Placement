$(document).ready(function () {
    var table = $('#datadepartment').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/Departments",
            "datatype": "json",
            "dataSrc": ""
        },

        "columns": [
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": "id"
            },
            {
                "data": "name"
            },
           
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<button id= "btn-delete" class="btn btn-danger" onclick="del('${row["id"]}')">Delete</button>`;
                    return button
                }
            }
           
        ],

        "select": true,
        "colReorder": true,
        "buttons": [
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    'copy',
                    {
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                    'csv',
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                    'print'
                ]
            }
        ]
    });

    $("#submitdata").click(function (event) {
        event.preventDefault();
        var validate;
        var obj_register = new Object();
        obj_register.Name = $("#departmentinput").val();
    
  
        if ($("#departmentinput").val() == "") {
            document.getElementById("departmentinput").className = "form-control is-invalid";
            $("#msgDepart").html("Department name can't be empty");
            validate = false;
        } else {
            document.getElementById("departmentinput").className = "form-control is-valid";
            obj_register.Name = $("#departmentinput").val();
            validate = true;
        }

        console.log(JSON.stringify(obj_register));
        if (validate == true) {
            $.ajax({
                url: "/Departments",
                method: 'POST',
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded',
                data: obj_register,
                beforeSend: function () {
                    Swal.fire({
                        title: 'Now loading',
                        text: "Please wait",
                        imageUrl: "https://c.tenor.com/5o2p0tH5LFQAAAAi/hug.gif",
                        imageWidth: 200,
                        imageHeight: 200,
                        imageAlt: 'Custom image',
                        showConfirmButton: false,
                        allowOutsideClick: false
                    })
                },
                success: function (data) {
                    $('#Add').modal('hide')
                    Swal.fire({
                        title: 'Success Inserting Data!',
                        text: 'Press Any Button to Continue',
                        icon: 'success',
                        confirmButtonText: 'Okay'
                    })

                    table.ajax.reload();
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseJSON.errors);

                }
            })
        } else {
            event.preventDefault();
            console.log(JSON.stringify(obj_register));
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            })
            event.stopPropagation();
        }
       
    })
})

    

function del(id) {
    console.log(id)
    Swal.fire({
        title: `Are you sure that you want to delete this data?`,
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Departments/DelDep/" + id,
                type: 'DELETE'
            }).done((result) => {
                console.log(result)

            Swal.fire(
                'Deleted!',
                'Your data has been deleted.',
                'success'
                )
                table.ajax.reload(); // user paging is not reset on reload
                
            }).fail((result) => {
                console.log(result);
            });
        }
    })
}





