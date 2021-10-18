$(document).ready(function () {
    console.log(localStorage.getItem("id"));

    

    $('#datainterview').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/JobEmployees/GetInterview/" + localStorage.getItem("id"),
            "datatype": "json",
            "dataSrc": ""
        },

        "columns": [
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                },
                "autoWidth": true
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
                "data": "titleJob",

                "autoWidth": true
            },
            {
                "data": "company",

                "autoWidth": true
            },
        
            {
                data: "interviewDate", render: function (toFormat) {
                    var iDate;
                    iDate = toFormat.toString();
                    return iDate.substring(0, 10);
                    
                },
                "autoWidth": true
            },
            {
                "data": "interviewTime",
                "autoWidth": true
            },
            {
                "data": "interviewer"
            },
            {
                "data": "interviewResult", render: function (toFormat) {
                    var result;
                    if (toFormat == 0) {
                        result = "Rejected";
                        return result;
                    }
                     return result="Accepted"
                }

            },
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
                            columns: [1, 2, 3, 4, 5,6,7,8]
                        }
                    },
                    'csv',
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5,6,7,8]
                        }
                    },
                    'print'
                ]
            }
        ]
    });

    $('#datahistory').DataTable({
        "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "/JobEmployees/GetJobHistory/" + localStorage.getItem("id"),
            "datatype": "json",
            "dataSrc": ""
        },

        "columns": [
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                },
                "autoWidth": true
            },
            {
                "data": "employeeId",

                "autoWidth": true
            },

            {
                "data": "fullName",

                "autoWidth": true
            },
            {
                "data": "jobTitle",

                "autoWidth": true
            },
            {
                "data": "company",

                "autoWidth": true
            },

            {
                data: "startDate", render: function (toFormat) {
                    var iDate;
                    iDate = toFormat.toString();
                    return iDate.substring(0, 10);

                },
                "autoWidth": true
            },

            {
                data: "endDate", render: function (toFormat) {
                    var iDate;
                    iDate = toFormat.toString();
                    return iDate.substring(0, 10);

                },
                "autoWidth": true
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
                            columns: [1, 2, 3, 4, 5,6]
                        }
                    },
                    'csv',
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5,6]
                        }
                    },
                    'print'
                ]
            }
        ]
    });

})