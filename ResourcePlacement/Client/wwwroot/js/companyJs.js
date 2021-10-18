$(document).ready(function () {
    var table = $('#datacompany').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/Companies",
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
                "data": "address"
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
        var validate=false;
        var obj_register = new Object();
        obj_register.Name = $("#companyinput").val();
        obj_register.Address = $("#companyaddress").val();
    
  
        if ($("#companyinput").val() == "" && validate==false) {
            document.getElementById("companyinput").className = "form-control is-invalid";
            $("#msgComp").html("Company name can't be empty");
            validate = false;
        } else {
            document.getElementById("companyinput").className = "form-control is-valid";
            obj_register.Name = $("#companyinput").val();
            validate = true;
        }

        if ($("#companyaddress").val() == "") {
            document.getElementById("companyaddress").className = "form-control is-invalid";
            $("#msgAddress").html("Company's Address can't be empty");
            validate = false;
        } else {
            document.getElementById("companyinput").className = "form-control is-valid";
            obj_register.Address = $("#companyaddress").val();
            validate = true;
        }

        console.log(JSON.stringify(obj_register));
        if (validate == true) {
            $.ajax({
                url: "/Companies",
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

                    $('#datacompany').DataTable().ajax.reload();
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
                url: "/Companies/DelComp/" + id,
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





