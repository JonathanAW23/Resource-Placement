

$(document).ready(function () {
    var table = $('#datajob').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/Jobs",
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
                "data": "title"

            },
            {
                "data": "companyId", render: function (toFormat) {
                    var companyName;
                    companyName = company_name(toFormat);
                    return companyName;
                },
                "orderable": false
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<center><button id= "btn-detail" class="btn btn-primary" data-toogle="modal" data-target="#GetJob" onclick="detail('${row["id"]}')">Details</button></center>`;
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
        obj_register.Title = $("#title").val();
        obj_register.CompanyId = parseInt($('#company').val());
        obj_register.Description = $('#description').val();
      
        validate = false;
        if ($("#title").val() == "" && validate==false) {
            document.getElementById("title").className = "form-control is-invalid";
            $("#msgT").html("Job Title can't be empty");
            validate = false;
        } else {
            document.getElementById("title").className = "form-control is-valid";
            obj_register.Title = $("#title").val();
            validate = true;

        }
        if ($("#description").val() == "") {
            document.getElementById("description").className = "form-control is-invalid";
            $("#msgDescription").html("Job's description can't be empty");
            validate = false;
        } else {
            document.getElementById("description").className = "form-control is-valid";
            obj_register.Description = $("#description").val();
            validate = true;
        }


        Swal.fire({
            title: "Are you sure that you want to submit this data?",
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Submit Data!',
        }).then((result) => {
            if (result.isConfirmed) {
                console.log(JSON.stringify(obj_register));
                if (validate == true) {
                    $.ajax({
                        url: "/Jobs",
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
                            $('#register').modal('hide')
                            Swal.fire({
                                title: 'Success Inserting Data!',
                                text: 'Press Any Button to Continue',
                                icon: 'success',
                                confirmButtonText: 'Okay'
                            })

                            $('#Register').modal('hide');
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
                        title: 'The form you submit is not correct',
                        text: 'Please check your form and try submitting again!',
                    })
                    event.stopPropagation();
                }
            }
        })
       
    })

    $.ajax({
        url: '/Companies'
    }).done(res => {
        let selectItem = '';
        console.log(res)
        $.each(res, (key, val) => {
            selectItem += `<option value="${val.id}">${val.name}</option>`
        });
        $('#company').html(selectItem);
    }).fail(res => console.log(res));

})

function company_name(id) {
    var tmp = null;
    $.ajax({
        'async': false,
        'type': "Get",
        'url': "/Companies",
        'success': function (result) {
            tmp = result;
        }
    });
    var name = "";
    $.each(tmp, (key, val) => {
        if (id == val.id) {
            name = val.name;
        }
    });

    return name;
}

    

function detail(id) {
    $.ajax({
        url: "GetJob/"+id
    }).done((result) => {
        console.log(result);
        var text = "";
        console.log(result.companyId);
        companyName= company_name(result.companyId);
  
        title = `<h5>Detail of Job </h5>`;

        text = `
                <ul>
                            <li class="list-group">: ${result.id}</li>
                            <li class="list-group">: ${result.title}</li>
                            <li class="list-group">: ${companyName}</li>
                            <li class="list-group">: ${result.description}</li>
                        
                </ul>
             `;
        /*console.log(text);*/
        $("#GetJob").modal('show');
        $("#GetJoblabel").html(title);
     
        $("#detailjob").html(text);
        

    }).fail((result) => {
        console.log(result);
    });
}

function del(nik) {
    console.log(nik)
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
                url: "https://localhost:5001/api/Persons/" + nik,
                method: 'DELETE'
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





