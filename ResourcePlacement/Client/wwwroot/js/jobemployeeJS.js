$(document).ready(function () {
    $('#dataJobEmployee').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/JobEmployees/Invited",
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
                "data": "titleJob"
            },
            {
                "data": "company"
            },
            {
                "data": "idEmployee"
            },
            {
                "data": "fullName",
               
                "autoWidth": true
            },
            
            {
                "data": "status", render: function (toFormat) {
                    var status;
                    console.log(toFormat)
                    if (toFormat === 1) {
                        status = "Invited"
                    } else if (toFormat === 2) {
                        status = "Interviewed"
                    } else {
                        status="Finalized"
                    }
                    return status;
                },
                "orderable": false
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
                            columns: [1, 2, 3, 4, 5]
                        }
                    },
                    'csv',
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5]
                        }
                    },
                    'print'
                ]
            }
        ]
    });

   

    $.ajax({
        url: '/Employees/GetEmployee'
    }).done(res => {
        let selectItem = '';
        console.log(res);
        $.each(res, (key, val) => {
            selectItem += `<option value="${val.id}">${val.id} - ${val.firstName} ${val.lastName}</option>`
        });
        $('#inputEmployee').html(selectItem);
    }).fail(res => console.log(res));

    $('#inputEmployee').select2({
        dropdownParent: $('#Assign')
    });
    

    $.ajax({
        url: '/Jobs'
    }).done(res => {
        let selectItem = '';
        console.log(res);
        $.each(res, (key, val) => {
            selectItem += `<option value="${val.id}">${val.title} - ${company_name(val.companyId)}</option>`
        });
        $('#inputJob').html(selectItem);
    }).fail(res => console.log(res));
   
    $('#inputJob').select2({
        dropdownParent: $('#Assign')
    });
    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }

    $("#assignment").click(function (event) {
        event.preventDefault();
        var d = new Date();
        var today = `${d.getFullYear()}-${addZero(d.getMonth() + 1)}-${addZero(d.getDate())}T${addZero(d.getHours())}:${addZero(d.getMinutes())}:${addZero(d.getSeconds())}`;
        var obj_assign = new Object();
        obj_assign.EmployeeId = $("#inputEmployee").val();
        obj_assign.JobId = $("#inputJob").val();
        obj_assign.Status = parseInt('1');
        obj_assign.RecordDate = today;
        obj_assign.InterviewDate = $("#InterviewDate").val() + 'T' + $("#InterviewTime").val();
        obj_assign.InterviewTime = $("#InterviewTime").val().toString();
        obj_assign.Interviewer = $("#Interviewer").val();
        console.log(today);
        console.log(JSON.stringify(obj_assign));
        var validate = false;

        if ($("#inputEmployee").val() == "" && validate == false) {
            document.getElementById("inputEmployee").className = "form-control is-invalid";
            $("#msgEmployee").html("Employee can't be empty");
            validate = false;
        } else {
            document.getElementById("inputEmployee").className = "form-control is-valid";
            obj_assign.EmployeeId = $("#inputEmployee").val();
            validate = true;
        }

        if ($("#inputJob").val() == "" && validate == false) {
            document.getElementById("inputJob").className = "form-control is-invalid";
            $("#msgJob").html("Job can't be empty");
            validate = false;
        } else {
            document.getElementById("inputJob").className = "form-control is-valid";
            obj_assign.JobId = $("#inputJob").val();
            validate = true;
        }

        validate = false;
        if ($("#InterviewDate").val() == "" && validate == false) {
            document.getElementById("InterviewDate").className = "form-control is-invalid";
            $("#msgInterviewDate").html("Interview Date can't be empty");
            validate = false;
        } else {
            document.getElementById("InterviewDate").className = "form-control is-valid";
            obj_assign.InterviewDate = $("#InterviewDate").val();
            validate = true;
        }

        if ($("#InterviewTime").val() == "" && validate == false) {
            document.getElementById("InterviewTime").className = "form-control is-invalid";
            $("#msgInterviewTime").html("Interview Time can't be empty");
            validate = false;
        } else {
            document.getElementById("InterviewTime").className = "form-control is-valid";
            obj_assign.InterviewTime = $("#InterviewTime").val();
            validate = true;
        }

        if ($("#Interviewer").val() == "") {
            document.getElementById("Interviewer").className = "form-control is-invalid";
            $("#msgUser").html("Interviewer can't be empty");
            validate = false;
        } else {
            document.getElementById("Interviewer").className = "form-control is-valid";
            obj_assign.Interviewer = $("#Interviewer").val();
            validate = true;
        }

        console.log(validate);


        if (validate == true) {
            $.ajax({
                url: "/Assigns/assign",
                method: 'POST',
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded',
                data: obj_assign,
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

                    Swal.fire({
                        title: 'Success Inserting Data!',
                        text: 'Press Any Button to Continue',
                        icon: 'success',
                        confirmButtonText: 'Okay'
                    })

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseJSON.errors);

                }
            })

        } else {
            event.preventDefault();
            console.log(JSON.stringify(obj_assign));
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            })
            event.stopPropagation();
        }


    })
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