$(document).ready(function () {
    $('#datajobemployee').DataTable({
        "filter": true,
        
        "ajax": {
            "url": "/JobEmployees",
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
                "data": "jobId"
            },
            {
                "data": "jobId",
            },

            {
                "data": "employeeID",
                "autoWidth": true
            },
            {
                "data": "emmployeeID",
                "autoWidth": true
            },
            {
                "data": "status", render: function (toFormat) {
                    var status;
                    console.log(toFormat)
                    if (toFormat === 1) {
                        status = "Invited"
                    } else if(toFormat==2){
                        status="Interview"
                    }
                    else {
                        status = "Job"
                    }
                    return status;
                },
                "orderable": false
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<button class="btn btn-success" id="select"
                                    data-employeeId= ${row["id"]} data-email= ${row["email"]}
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

    $("#inverview").click(function (event) {
        event.preventDefault();
        var d = new Date();
        var today = `${d.getFullYear()}-${d.getMonth() + 1}-${d.getDate()}T${d.getHours()}:${d.getMinutes()}:${d.getSeconds()}`;
        var obj_assign = new Object();
        obj_interview.EmployeeId = $("#validationCustom03").val();
        obj_interview.JobId = $("#JobID").val();
        obj_interview.Status = parseInt('1');
        obj_interview.RecordDate = today;
        obj_interview.InterviewDate = $("#InterviewDate").val();
        obj_interview.InterviewTime = $("#InterviewTime").val().toString();
        obj_interview.Interviewer = $("#Interviewer").val();
        console.log(today);
        console.log(JSON.stringify(obj_interview));

        $.ajax({
            url: "/Assigns/assign",
            method: 'POST',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded',
            data: obj_assign,
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

