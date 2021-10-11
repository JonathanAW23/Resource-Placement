$(document).ready(function () {
    $('#datajobemployee').DataTable({
        "filter": true,
        
        "ajax": {
            "url": "/JobEmployees/GetJobEmployee",
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
                "data": "company",
            },

            {
                "data": "idEmployee",
                "autoWidth": true
            },
            {
                "data": "fullName",
                "autoWidth": true
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<button class="btn btn-success" id="select"
                                    data-employee ="${row["idEmployee"]}" data-employeename= "${row["fullName"]}"
                                    data-jobid= "${row["idJob"]}"
                                    data-title= "${row["titleJob"]}"
                                    data-company= "${row["company"]}"
                                    data-date= "${ row["interviewDate"]}"
                                    data-time= "${row["interviewTime"]}"
                                    data-interviewer= "${row["interviewer"]}">Select</button>`;
                    return button
                }


            }
        ]
    });
    
    $(document).on('click', '#select', function (){
        var employee_id = $(this).data('employee');
        var employee_name = $(this).data('employeename');
        var job_id = $(this).data('jobid');
        var job_title = $(this).data('title');
        var company = $(this).data('company');
        var date = $(this).data('date').toString().substring(0, 10);
        var time = $(this).data('time');
        var interviewer = $(this).data('interviewer');
 
        $('#validationCustom03').val(employee_id);
        $('#inputName').val(employee_name);
        $('#JobID').val(job_id);
        $('#title').val(job_title);
        $('#company').val(company);
        $('#InterviewDate').val(date);
        $('#InterviewTime').val(time);
        $('#InterviewTime').val(time);
        $('#Interviewer').val(interviewer);
       
       
        $('#selectEmployee').modal('hide');


    })
    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }

    $("#interview").click(function (event) {
        event.preventDefault();
        var d = new Date();
        var today = `${d.getFullYear()}-${d.getMonth() + 1}-${d.getDate()}T${addZero(d.getHours())}:${addZero(d.getMinutes())}:${addZero(d.getSeconds())}`;
        var obj_interview = new Object();
        obj_interview.EmployeeId = $("#validationCustom03").val();
        obj_interview.JobId = $("#JobID").val();
        obj_interview.Status = parseInt('2');
        obj_interview.RecordDate = today;
        obj_interview.InterviewDate = $("#InterviewDate").val()+'T'+$("#InterviewTime").val();
        obj_interview.InterviewTime = $("#InterviewTime").val().toString();
        obj_interview.Interviewer = $("#Interviewer").val();
        obj_interview.InterviewResult = $("#result").val();
        
        console.log(JSON.stringify(today));
        console.log(JSON.stringify(obj_interview));

        $.ajax({
            url: "url to input data",
            method: 'POST',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded',
            data: obj_interview,
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

