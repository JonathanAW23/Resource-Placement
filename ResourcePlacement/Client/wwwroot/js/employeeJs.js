$(document).ready(function () {

    var table = $('#dataemployee').DataTable({
       "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/Employees/GetEmployee",
            "datatype": "json",
            "dataSrc": ""
        },
        
        "columns": [
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, full, meta) {
                    return meta.row+1;
                },
                "autoWidth": true
            },
            {
                "data": "id",

                "autoWidth": true
            },
            {
                "data": null,
                "render": function (data, type, row) {

                    return row["firstName"] + " "+ row["lastName"];
                },
                "autoWidth": true
            },
            {
                "data": "gender", render: function (toFormat) {
                    var gender;
                    console.log(toFormat)
                    if (toFormat === 0) {
                        gender = "Male"
                    } else {
                        gender = "Female"
                    }
                    return gender;
                },
                "orderable": false
            },
            {
                "data": "email"
            },
            {
                data: "phoneNumber", render: function (toFormat) {
                    var tPhone;
                    tPhone = toFormat.toString();
                    subsTphone = tPhone.substring(0, 2);
                    /*console.log(data.nik);*/
                    if (subsTphone == "08") {
                        tPhone = '(' + '+62' + ')' + tPhone.substring(1, 4) + '-' + tPhone.substring(4, 8) + '-' + tPhone.substring(8, 14);
                        return tPhone
                    } else {
                        tPhone = '(' + '+62' + ')' + tPhone.substring(0, 3) + '-' + tPhone.substring(4, 8) + '-' + tPhone.substring(9, 13);
                        return tPhone
                    }
                },
                "width": "20%"
            },
           
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    const button = `<center> <button id= "btn-detail" class="btn btn-primary" data-toogle="modal" data-target="#GetEmployee" onclick="detail('${row["id"]}')">Details</button>
                    <a id="detail" class="btn btn-success" data-id ="${row["id"]}">History</a> </center>`;
                    
                    
                    return button;
                },
                "width": "20%",
                
                   
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
                           columns: [ 1, 2, 3,4, 5 ]
                       }
                   },
                   'csv',
                   {
                       extend: 'pdfHtml5',
                       exportOptions: {
                           columns: [ 1, 2, 3,4, 5 ]
                       }
                   },
                   'print'
               ]
           }
       ]
    });

    $('#dataemployee tbody').on('click', 'a', function () {
        console.log("This is Job Interview");
        var Id = $(this).data('id');
        console.log(Id);
        localStorage.setItem("id",Id);
        window.location = '/details/detail-assign';
    });


    $("#submitdata").click(function (event) {
        event.preventDefault();
        var obj_register = new Object();
        obj_register.Id = $("#validationCustom03").val();
        obj_register.FirstName = $("#firstName").val();
        obj_register.LastName = $("#lastName").val();      
        obj_register.PhoneNumber = $("#notelp").val();
        obj_register.Email = $("#validationCustom04").val();
        obj_register.Gender = parseInt($('#inputGender').val());
        obj_register.EmploymentStatus = parseInt($('#inputStatus').val());
        obj_register.DepartmentId = parseInt($('#department').val());


        var validate = false;
        if ($("#validationgaji").val() == "" && validate==false) {
            document.getElementById("validationgaji").className = "form-control is-invalid";
            $("#msgSalary").html("Salary can't be empty");
            validate = false;
        } else {
            document.getElementById("validationgaji").className = "form-control is-valid";
            obj_register.salary = $("#validationgaji").val();
            validate = true;
        }
        if ($("#validationCustom03").val() == "" && validate==false) {
            document.getElementById("validationCustom03").className = "form-control is-invalid";
            $("#msgID").html("ID can't be empty");
            validate = false;
        } else {
            document.getElementById("validationCustom03").className = "form-control is-valid";
            obj_register.ID = $("#validationCustom03").val();
            validate = true;
        }

        if ($("#firstName").val() == "" && validate==false) {
            document.getElementById("firstName").className = "form-control is-invalid";
            $("#msgFN").html("First Name can't be empty");
            validate = false;
        } else {
            document.getElementById("firstName").className = "form-control is-valid";
            obj_register.FirstName = $("#firstName").val();
            validate = true;
        }

        if ($("#lastName").val() == "" && validate == false) {
            document.getElementById("lastName").className = "form-control is-invalid";
            $("#msgLN").html("Last Name can't be empty");
            validate = false;
        } else {
            document.getElementById("lastName").className = "form-control is-valid";
            obj_register.LastName = $("#lastName").val();
            validate = true;
        }

        if ($("#validationCustom04").val() == "" && validate == false) {
            document.getElementById("validationCustom04").className = "form-control is-invalid";
            $("#msgEmail").html("Your Email can't be empty");
            validate = false;
        } else {
            document.getElementById("validationCustom04").className = "form-control is-valid";
            obj_register.Email = $("#validationCustom04").val();
            validate = true;
        }

        if ($("#notelp").val() == "") {
            document.getElementById("notelp").className = "form-control is-invalid";
            $("#msgPhone").html("Phone Number can't be empty");
            validate = false;
        } else {
            document.getElementById("notelp").className = "form-control is-valid";
            obj_register.PhoneNumber = $("#notelp").val();
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

                if (validate == true) {
                    $.ajax({
                        url: "/Employees/Add",
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
                            $('.modal.in').modal('hide')
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
                            if (xhr.responseJSON.errors != undefined) {
                                checkValid(xhr.responseJSON.errors.NIK, "validationCustom03", "#msgID");
                                checkValid(xhr.responseJSON.errors.Email, "validationCustom04", "#msgEmail");
                                checkValid(xhr.responseJSON.errors.Phone, "notelp", "#msgPhone");
                            }

                        }
                    })
                } else {
                    event.preventDefault();
                    console.log(JSON.stringify(obj_register));
                    $('#Register').modal('hide');
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
        url: '/Departments'
    }).done(res => {
        let selectItem = '';
        console.log(res)
        $.each(res, (key, val) => {
            selectItem += `<option value="${val.id}">${val.name}</option>`
        });
        $('#department').html(selectItem);
    }).fail(res => console.log(res));
})

 
function moneyMaker(bilangan) {
    var number_string = bilangan.toString(),
        sisa = number_string.length % 3,
        rupiah = number_string.substr(0, sisa),
        ribuan = number_string.substr(sisa).match(/\d{3}/g);

    if (ribuan) {
        separator = sisa ? '.' : '';
        rupiah += separator + ribuan.join('.');
    }
    return rupiah;
}

function detail(id) {
    $.ajax({
        url: "/Employees/GetEmployee/"+id
    }).done((result) => {
        console.log(result);
        var text = "";
        var sPhone;
        var salary = "Rp " + moneyMaker(result.salary);
        var Gender=null;
        if (result.gender == 0) {
            Gender = "Male";
        } else {
            Gender = "Female";
        }
        sPhone = result.phoneNumber.toString();
        var subsTphone = sPhone.substring(0, 2);
        /*console.log(data.nik);*/
        if (subsTphone == "08") {
            sPhone = '(' + '+62' + ')' + sPhone.substring(1, 4) + '-' + sPhone.substring(4, 8) + '-' + sPhone.substring(8, 14);
            
        } else {
            sPhone = '(' + '+62' + ')' + sPhone.substring(0, 3) + '-' + sPhone.substring(4, 8) + '-' + sPhone.substring(9, 13);
            
        }

        var status = "";
        if (result.employmentStatus == 1) {
            status += `<span class="badge badge-success">ACTIVE</span>`
        } else {
            status += `<span class="badge badge-danger">INACTIVE</span>`
        }
        title = `<h5>Detail of ${result.firstName} ${result.lastName} ${status} </h5>`;

        text = `
                <ul>
                            <li class="list-group">: ${result.id}</li>
                            <li class="list-group">: ${result.firstName} ${result.lastName}</li>
                            <li class="list-group">: ${Gender}</li>
                            <li class="list-group">: ${result.email}</li>
                            <li class="list-group">: ${sPhone}</li>
                            <li class="list-group">: ${salary}</li>
                        
                </ul>
             `;
        /*console.log(text);*/
        $("#GetEmployee").modal('show');
        $("#GetEmployeelabel").html(title);
     
        $("#detailemployee").html(text);
        

    }).fail((result) => {
        console.log(result);
    });
}

function del(ID) {
    console.log(ID)
    Swal.fire({
        title: "Are you sure that you want to delete this data?",
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Employees/DeleteEmployee" + ID,
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