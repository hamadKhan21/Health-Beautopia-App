
var $document = $(document),
    $window = $(window);
$document.ready(function () {
    


    GetServicesBySource()
    GetTotalServicesCount()

});












function GetServicesBySource() {

    $.ajax({
        type: "POST",
        url: "/Admin/ServicesLeads/GetServicesBySource",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            createDonutChartServicesBySource(data)

        }
    });


}

function GetTotalServicesCount() {

    $.ajax({
        type: "POST",
        url: "/Admin/ServicesLeads/GetTotalServicesCount",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            createPieChartTotalServicesCount(data)

        }
    });


}

function createDonutChartServicesBySource(data) {
    $("#ServicesBySource").kendoChart({

        pannable: {
            lock: "y"
        },
        zoomable: {
            mousewheel: {
                lock: "y"
            },
            selection: {
                lock: "y"
            }
        },
        title: {
            position: "top",
            text: "Services By Source"
        },
        legend: {
            // position: "bottom",
            visible: true,
            labels: {
                template: "#= value# #: text #"
            }
            //position: "left"
        },
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            type: "donut",
            //type: "pie",
            startAngle: 150,


        },
        series: [
            {
                name: "Source",
                data: EmpbyTypeData(data),

                labels: {
                    visible: true,
                    background: "transparent",
                    position: "outsideEnd",
                    distance: 30,
                    font: "9px Arial",
                    //center: '5%',
                    //template: "#= category #: \n #= value#%"
                    template: "#= value#"
                },
                padding: 40
            },

        ], seriesDefaults: {
            type: "donut",
            //type: "pie",
            startAngle: 150,


        },
        series: [
            {
                name: "Source",
                data: EmpbyTypeData(data),

                labels: {
                    visible: true,
                    background: "transparent",
                    position: "outsideEnd",
                    distance: 30,
                    font: "9px Arial",
                    //center: '5%',
                    //template: "#= category #: \n #= value#%"
                    template: "#= value#"
                },
                padding: 40
            },

        ],
        seriesColors: ["#42a7ff", "#666666", "#999999", "#cccccc", "red", "blue", "yellow", "indigo", "purple", "green", "tan", "orange", "pink", "brown", "#16717a",
            "#5dd6e2", "#473d7b", "#6e52ff", "#ff7a52", "#6b2915", "#ad2800", "#8aad00", "#515f1b", "#5bff50", "#a06fff", "#020913"],
        tooltip: {
            visible: true,
            template: "#= category # (#= series.name #): #= value #"
        },
        //seriesClick: onSeriesEmpByDeptClick,
    });
}


function createPieChartTotalServicesCount(data) {
    $("#TotalServicesCount").kendoChart({

        pannable: {
            lock: "y"
        },
        zoomable: {
            mousewheel: {
                lock: "y"
            },
            selection: {
                lock: "y"
            }
        },
        title: {
            position: "top",
            text: "Services Count"
        },
        legend: {
            visible: true,
            position: "bottom",
            //position: "left"
            labels: {
                template: "#= text #: #= value#"
            }
            //template: "#= category #: \n #= value#%"
        },
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            type: "pie",
            //type: "pie",
            startAngle: 150,

        },
        series: [
            {
                name: "Services",
                data: EmpbyTypeData(data),

                labels: {
                    visible: true,
                    background: "transparent",
                    position: "outsideEnd",
                    distance: 30,
                    font: "15px Arial",
                    //center: '5%',
                    template: "#= category #: \n #= value#"
                }
            },

        ],
        seriesColors: ["#6e52ff", "#ff7a52", "#6b2915", "#ad2800", "#8aad00", "#515f1b", "#5bff50", "#a06fff", "#020913"],
        tooltip: {
            visible: true,
            template: "#= category # (Services): #= value #"
        },
       // seriesClick: onSeriesEmpByDeptClick,
    });
}





function EmpbyTypeData(data) {
    var listofNationalities = [];
    for (var i = 0; i < data.length; i++) {
        var obj = new Object();
        obj.category = data[i].name;
        obj.value = data[i].nameCount;

        listofNationalities.push(obj);
    }
    return listofNationalities;
}








