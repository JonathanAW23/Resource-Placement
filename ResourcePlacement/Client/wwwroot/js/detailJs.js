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
                data: "intervewDate", render: function (toFormat) {
                    var iDate;
                    iDate = toFormat.toString();
                    return iDate.substring(0, 2);
                    
                },
                "width": "20%"
            },
            {
                "data": "interviewTime"
            },
            {
                "data": "interviewer"
            },
            {
                "data": "interviewResult", render: function (toFormat) {
                    var result;
                    if (toFormat == 0) {
                        result = "Decline";
                        return result;
                    }
                    result="Accept"
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