$(document).ready(function () {
      $('#dataemployee').DataTable({
        "filter": true,
        
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
                    return meta.row + 1;
                }
            },
            {
                "data": "id"
            },
            {
                "data": null,
                "render": function (data, type, row) {

                    return row["firstName"] + " " + row["lastName"];
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
                "orderable": false,
                "autoWidth": true
            },
            {
                "data": "email",
                "autoWidth": true
            },
            {
                data: "phoneNumber", render: function (toFormat) {
                    var tPhone;
                    tPhone = toFormat.toString();
                    subsTphone = tPhone.substring(0, 2);
                    if (subsTphone == "08") {
                        tPhone = '(' + '+62' + ')' + tPhone.substring(1, 4) + '-' + tPhone.substring(4, 8) + '-' + tPhone.substring(8, 14);
                        return tPhone
                    } else {
                        tPhone = '(' + '+62' + ')' + tPhone.substring(0, 3) + '-' + tPhone.substring(4, 8) + '-' + tPhone.substring(9, 13);
                        return tPhone
                    }
                },
                "autoWidth": true
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<button class="btn btn-success" id="select"
                                    data-id= ${row["id"]} data-email= ${row["email"]}
                                    data-gender= ${row["gender"]}
                                    data-status= ${row["employmentStatus"]}
                                    data-phone= ${row["phoneNumber"]}
                                    data-salary= ${row["salary"]}
                                    data-first= ${ row["firstName"]}
                                    data-last= ${row["lastName"]}
                                    data-department= ${row["departmentId"]}>Select</button>`;
                    return button
                }


            }
        ]
    });
    
    $(document).on('click', '#select', function (){
        var employee_id = $(this).data('id');
        var email = $(this).data('email');
        var gender = $(this).data('gender');
        var status = $(this).data('status');
        var salary = $(this).data('salary');
        var phoneNumber = $(this).data('phone');
        var emp_firstName = $(this).data('first');
        var emp_lastName = $(this).data('last');
        var emp_departmentId = $(this).data('department');
        
        $('#validationCustom03').val(employee_id);
        $('#validationCustom04').val(email);
        $('#inputGender').val(gender);
        $('#inputFirstName').val(emp_firstName);
        $('#lastName').val(emp_lastName);
        $('#inputGender').val(gender);
        $('#inputStatus').val(status);
        $('#notelp').val(phoneNumber);
        $('#department').val(emp_departmentId);
        $('#validationgaji').val(salary);
       
        $('#selectEmployee').modal('hide');
    })


    var job=$('#datajob').DataTable({
        "filter": true,

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
                "data": "title",
                "autoWidth": true
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
                "data": "description",
                "autoWidth": true
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<a class="btn btn-success" id="selectJob"
                                    data-id= "${row["id"]}" data-title= "${row["title"]}"
                                    data-company= ${row["companyId"]}
                                    data-description= "${row["description"]}">Select</a>`;
                    return button
                }


            }
        ]
    });


    $(document).on('click', '#selectJob', function () {
        var job_id = $(this).data('id');
        var title = $(this).data('title');
        var companyId = $(this).data('company');
        var description = $(this).data('description');

        console.log(job_id);
        console.log(title);
        console.log(companyId);
        console.log(description);
        $('#company').val(companyId);
        $('#description').val(description);
        $('#title').val(title);
        $('#JobID').val(job_id);
        

        $('#SelectJob').modal('hide');
    })
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

