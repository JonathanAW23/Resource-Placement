$(document).ready(function () {
    $.ajax({
        url: '/Employees/',
       
    }).done((result) => {
        console.log(result);
        var female = result.filter(data => data.gender === 1).length;
        var male = result.filter(data => data.gender === 0).length;
        console.log(male);
        console.log(result[0].gender);


        /*ini untuk char gender*/

        var options = {
            series: [male, female],
            chart: {
                width: 280,
                height: 350,
                type: 'pie',
            },
            labels: ['Male', 'Female'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        show: true,
                        position: 'right',
                    }
                }
            }]
        };
        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Show',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });


    $.ajax({
        url: '/JobEmployees/Finalized',

    }).done((result) => {
        console.log(result);
        var accepted = result.filter(data => data.interviewResult === 1).length;
        var decline = result.filter(data => data.interviewResult === 0).length;
        console.log(accepted);
        console.log(result[0].interviewResult);


        /*ini untuk chart interview result*/

        var options = {
            series: [decline, accepted],
            chart: {
                width: 280,
                height: 350,
                type: 'pie',
            },
            labels: ['Decline', 'Accepted'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        show: true,
                        position: 'right',
                    }
                }
            }]
        };
        var chart = new ApexCharts(document.querySelector("#chartinterview"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Show',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });




    $.ajax({
        url: '/Jobs',

    }).done((result) => {

        var BNI = result.filter(data => data.companyId === 1).length;
        var BCA = result.filter(data => data.companyId === 2).length;
        var PLN = result.filter(data => data.companyId === 3).length;
        var Pertamina = result.filter(data => data.companyId === 4).length;

        var companies = {
            series: [{
                data: [BNI,BCA,PLN,Pertamina]
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    dataLabels: {
                        position: 'center', // top, center, bottom
                    },
                }
            },
            dataLabels: {
                enabled: true,
                formatter: function (val) {
                    return val;
                },
                offsetY: -20,
                style: {
                    fontSize: '12px',
                    colors: ["#304758"]
                }
            },
            xaxis: {
                categories: ["BNI", "BCA", "PLN", "Pertamina"],
                position: 'top',
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false
                },
                crosshairs: {
                    fill: {
                        type: 'gradient',
                        gradient: {
                            colorFrom: '#D8E3F0',
                            colorTo: '#BED1E6',
                            stops: [0, 100],
                            opacityFrom: 0.4,
                            opacityTo: 0.5,
                        }
                    }
                },
                tooltip: {
                    enabled: true,
                }
            },
            yaxis: {
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false,
                },
                labels: {
                    show: false,
                    formatter: function (val) {
                        return val;
                    }
                }
            }
        };
        var charcompany= new ApexCharts(document.querySelector("#chartcompany"), companies);
        charcompany.render();
        
        
        //var chart = new ApexCharts(document.querySelector("#chart"), options);
        //chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Show',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });
})