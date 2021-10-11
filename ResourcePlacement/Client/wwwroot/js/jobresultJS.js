$(document).ready(function () {
    $('#dataJobInterview').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/JobEmployees/Finalized",
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
                        status = "Job"
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
})