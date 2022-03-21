var $document = $(document);
$document.ready(function () {

    GetRequestServices()


});


function GetRequestServices() {
    //$(".employeeList").empty();
    //$(".employeeList").append("<option value='-1'>Select</option>")

    //var datas = { 'IsAssetDetailPart': 0 };
    $.ajax({
        type: "GET",
        url: "/Admin/ServicesLeads/GetRequestServices",
        //data: "{mdate:" + "m" + "}",
       // data: datas,
        //  dataType: "json",
        // contentType: "application/json; charset=utf-8",
        success: function (data) {
            //debugger;
            //GlobalAssetsData = data;
            InitServiceRequestGrid(data)
            // InitAssetFilterGrid(data)
            //InitAssetFilterGrid(data)
        }
    });



}

function InitServiceRequestGrid(data) {
   // $("#gridTopDev").empty();

    //$("#gridTopDev").append('<div id="receivedGrid"></div>')

    $("#leads-grid").kendoGrid({
        dataSource: {
            data: data,
            pageSize: 100,//cdrData.length,

            //group: [{ field: "DEPARTMENT" }, { field: "SECTION" }, { field: "Employee" }] ,
            //sort: {
            //    field: 'DATE_ENTRY',
            //    dir: "asc"
            //},

            schema: {
                model: {
                    fields: {
                        //employeeName: {
                        //    type: "string"
                        //},
                        //department: {
                        //    type: "string"
                        //},
                        id: {
                            type: "number"
                        },
                        name: {
                            type: "string"
                        },
                        mobile: {
                            type: "string"
                        },
                        email: {
                            type: "string",
                        },
                       
                        comments: {
                            type: "string",
                        },
                        source: {
                            type: "string",
                        },
                        _listOfServices: {
                            type: "string",
                        },
                        createdOn: {
                            type: "string",
                        },
                    }
                }
            }
        },

        columns: [
     

            {
                title: "Customer Name",
                field: "name",
                filterable: true

            },



            {
                title: "Mobile",
                field: "mobile",
                filterable: true
               // filterable: { multi: true }
                //encoded: true
            },
            {
                title: "Email",
                field: "email",
                filterable: true

            },
        
            {
                title: "Comments",
                field: "comments",
                filterable: false,
                //filterable: false,
                //encoded: true
            },
            {
                title: "Source",
                field: "source",
                filterable: { multi: true }
            },
            {
                title: "Services",
                field: "_listOfServices",
                filterable: true
            },
            {
                title: "Date",
                field: "createdOn",
                filterable: {
                    extra: false,
                    operators: {
                        string: { contains: "Contains" }
                    }
                },
                template: '#= kendo.toString(createdOn,"M/dd/yyyy") #'
            },
            //{ command: { text: "Return", click: showDetails }, title: " ", width: "100px" }




        ],
        pageable: {
            buttonCount: 10,
            pageSizes: true,
            pageSizes: ['All', 100, 200, 500, 1000, 1500, 2000, 5000, 10000]
        },
        //  toolbar: kendo.template($('#toolbar-template').html()),
        resizable: true,
        //sortable: true,
        filterable: true,
        detailInit: detailInit,
        //dataBound: function () {
        //    this.expandRow(this.tbody.find("tr.k-master-row").first());
        //},
        //filterable: {
        //    extra: false,
        //    operators: {
        //        string: {
        //            contains: "Contains",
        //            startswith: "Starts"
        //        }

        //    }
        //},
        selectable: "multiple",
        persistSelection: true,
        //selectable: "multiple",
        //page: onPaging,
        //dataBinding: Onstart,
        // dataBound: ChangeColor,
        // filter: onFiltering,
        //change: onChange,

        //serverPaging: true,

        messages: {
            noRecords: "No records available."
        },
        toolbar: ["excel"],
        excel: {
            fileName: "Services Leads.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });

    function detailInit(e) {
      //  debugger;
        $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: {
                type: "odata",
                data: e.data.subCategoryByServiceID,
                // serverPaging: true,
                // serverSorting: true,
                // serverFiltering: true,
                pageSize: 10,
                filter: { field: "requestServiceID", operator: "eq", value: e.data.requestServiceID }
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            filterable: true,
            columns: [
                { field: "subCategoryNameEn", title: "Service En", filterable: true },
                { field: "subCategoryNameAr", title: "Service Ar", filterable: true },
                { field: "servicePrice", title: "Price", filterable: false },
                //{ field: "notes", title: "Notes", filterable: false }
            ]
        });
    }

    //if (FromSaved == 1) {
    //    var grid = $("#leads-grid").data("kendoGrid");
    //    grid.select("tr:eq(0)");
    //}
}