$(document).ready(function () {
    $('#dataJobInterview').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/JobEmployees/Interview",
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
                "data": "idJob"
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
                "data": "interviewer",

                "autoWidth": true
            },
            {
                
                "data": "interviewDate", render: function (toFormat) {
                    var Date=toFormat.toString().substring(0,10);
                    
                    return Date;
                },
                "orderable": true,
                "autoWidth": true
            },
            {
                "data": "interviewTime",

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
                        status = "Finalized"
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
        url: '/JobEmployees/Invited/'
    }).done(res => {
        let selectItem = '<option></option>';
        console.log(res);
        $.each(res, (key, val) => {
            selectItem += `<option value="${val.id}">${val.fullName} - ${val.titleJob} - ${val.company}</option>`
            
        });
        
        $('#inputJEmployee').html(selectItem);


        $('#inputJEmployee').on('select2:select', function (e) {
            let data = e.params.data.id;
            console.log(data);
            console.log("select");
            console.log(JobEmployee(data));
            interview = JobEmployee(data);
            $('#JobId').val(interview.idJob);
            $('#EmployeeId').val(interview.idEmployee);
            $('#InterviewDate').val(interview.interviewDate.toString().substring(0,10));
            $('#InterviewTime').val(interview.interviewTime);
            $('#Interviewer').val(interview.interviewer);
        });
    }).fail(res => console.log(res));

    $('#inputJEmployee').select2({
        placeholder: "Select a record",
        dropdownParent: $('#Interview')
    });

    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }

    $("#interview").click(function (event) {
        event.preventDefault();
        var d = new Date();
        var today = `${d.getFullYear()}-${addZero(d.getMonth() + 1)}-${addZero(d.getDate())}T${addZero(d.getHours())}:${addZero(d.getMinutes())}:${addZero(d.getSeconds())}`;
        var obj_interview = new Object();
        obj_interview.EmployeeId = $("#EmployeeId").val();
        obj_interview.JobId = $("#JobId").val();
        obj_interview.Status = parseInt('2');
        obj_interview.RecordDate = today;
        obj_interview.InterviewDate = $("#InterviewDate").val();
        obj_interview.InterviewTime = $("#InterviewTime").val().toString();
        obj_interview.Interviewer = $("#Interviewer").val();

        console.log(JSON.stringify(today));
        console.log(JSON.stringify(obj_interview));

        var validate = false;

        if ($("#EmployeeId").val() == "" && validate == false) {
            document.getElementById("EmployeeId").className = "form-control is-invalid";
            $("#msgEmployee").html("Employee ID can't be empty");
            validate = false;
        } else {
            document.getElementById("EmployeeId").className = "form-control is-valid";
            obj_interview.EmployeeId = $("#EmployeeId").val();
            validate = true;
        }

        if ($("#JobId").val() == "" && validate == false) {
            document.getElementById("JobId").className = "form-control is-invalid";
            $("#msgJob").html("Job ID can't be empty");
            validate = false;
        } else {
            document.getElementById("JobId").className = "form-control is-valid";
            obj_interview.JobId = $("#JobId").val();
            validate = true;
        }

        if ($("#InterviewDate").val() == "" && validate == false) {
            document.getElementById("InterviewDate").className = "form-control is-invalid";
            $("#msgInterviewDate").html("Interview Date can't be empty");
            validate = false;
        } else {
            document.getElementById("InterviewDate").className = "form-control is-valid";
            obj_interview.InterviewDate = $("#InterviewDate").val();
            validate = true;
        }

        if ($("#InterviewTime").val() == "" && validate == false) {
            document.getElementById("InterviewTime").className = "form-control is-invalid";
            $("#msgInterviewTime").html("Interview Time can't be empty");
            validate = false;
        } else {
            document.getElementById("InterviewTime").className = "form-control is-valid";
            obj_interview.InterviewTime = $("#InterviewTime").val();
            validate = true;
        }

        if ($("#Interviewer").val() == "") {
            document.getElementById("Interviewer").className = "form-control is-invalid";
            $("#msgUser").html("Interviewer can't be empty");
            validate = false;
        } else {
            document.getElementById("Interviewer").className = "form-control is-valid";
            obj_interview.Interviewer = $("#Interviewer").val();
            validate = true;
        }

        console.log(validate);


        Swal.fire({
            title: "Are you sure that you want to submit this data?",
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Submit Data!',
        }).then((result) => {

            if (validate == true) {
                $.ajax({
                    url: "/Interviews/interview",
                    method: 'POST',
                    dataType: 'json',
                    contentType: 'application/x-www-form-urlencoded',
                    data: obj_interview,
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
                        $('#dataJobInterview').DataTable().ajax.reload();

                        Swal.fire({
                            title: 'Success Inserting Data!',
                            text: 'Press Any Button to Continue',
                            icon: 'success',
                            confirmButtonText: 'Okay'
                        })
                        $('#Interview').modal('hide');
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseJSON.errors);

                    }
                })
            } else {
                event.preventDefault();
                console.log(JSON.stringify(obj_interview));
                Swal.fire({
                    icon: 'error',
                    title: 'The form you submit is not correct',
                    text: 'Please check your form and try submitting again!',

                })
                event.stopPropagation();
            }
        }
       )
    })
})

function JobEmployee(id) {
    var tmp = null;
    $.ajax({
        'async': false,
        'type': "Get",
        'url': "/JobEmployees",
        'success': function (result) {
            tmp = result;
        }
    });
    var JE = new Object();
    $.each(tmp, (key, val) => {
        if (id == val.id) {
            JE.idEmployee = val.employeeId;
            JE.idJob = val.jobId;
            JE.interviewDate = val.interviewDate;
            JE.interviewTime = val.interviewTime;
            JE.interviewer = val.interviewer;

        }
    });

    return JE;
}