var $document = $(document),
    $window = $(window),
    forms = {
        manageActivity: $('#manageActivity')
    };

$document.ready(function () {



    if (forms.manageActivity.length) {
        var $manageActivity = forms.manageActivity;
        $manageActivity.validate({
            rules: {
                ActivityTypeID: {
                    required: true,
                    //minlength: 2
                },
                Comments: {
                    required: true,

                }
               


            },
            messages: {

                ActivityTypeID: {
                    required: "Please Select Activity Type",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
                Comments: {
                    Comments: "Please Enter Comments"
                    //minlength: "Your name must consist of at least 2 characters"
                },
               
               
               
            },
            submitHandler: function submitHandler(form) {

                //var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
                //debugger
                $(form).ajaxSubmit({
                    type: "POST",
                    data: $(form).serialize(),
                    url: "/Admin/ServicesLeads/InsertRS_Activity",
                    success: function success(data) {

                        GetRS_Activity($("#RequestServiceID").val())

                        ClearFields()

                        InitGetAllSubServiceCategory(data)
                    },
                    error: function error() {
                        //	debugger
                        alert($manageActivity)
                        //$('.errorform', $LoginRequest).fadeIn();
                    }
                });
            }
        });
    }


   


});


GetRequestServices()
GetActivitiesTypeLkp();

function ClearFields() {

  //  $("#CategoryID option:contains(" + "Select Sub Service Category" + ")").removeAttr('selected');
    $(".inputClass").val("")
    $(".selectClass").val("-1")
    $("#RequestServiceID").val("0")
    //$(".filterprop").val("-1")
    // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
  //  $(".chkClass").prop('checked', false);
    //$("#CategoryID option:contains(" + "Select Sub Service Category" + ")").attr('selected', 'selected');
    // $(".selectClass").val();
}


function GetActivitiesTypeLkp() {
    $("#ActivityTypeID").empty();
    $("#ActivityTypeID").append("<option value='-1'>Select ActivityType</option>")

    ////var datas = { 'IsEmployees': 1, 'Section': -1, 'IsActive': 1 };
    $.ajax({
        type: "GET",
        url: "/Admin/ServicesLeads/GetActivityType",
        //data: "{mdate:" + "m" + "}",
        //data: datas,//JSON.stringify(datas),
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        success: function (data) {
            //debugger
            var cdrData = data
            var EmpToDisplay = "";
            if (cdrData.length != 0) {
                for (var i = 0; i < cdrData.length; i++) {

                    $("#ActivityTypeID").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].activityTypeEn + "</option>");


                }

            }



        }
    });



}

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
            { command: { text: "Activity", click: ShowActivityDetails }, title: " ", width: "100px" }




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


function ShowActivityDetails(e) {
   // debugger;
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
   // var grid = $("#employeesGrid").data("kendoGrid");
    $("#RequestServiceID").val(dataItem.id)
    GetRS_Activity(dataItem.id)
    $("#ActivityTypeID").val("-1")
    //}
  ///  if ($('a.Confirm').length > 0) {
      //  ConfirmEmployee(dataItem.salesEventsID, dataItem.employeeNumber)
  //  }
  //  else if ($('a.Delete').length > 0) {
  //      grid.dataSource.remove(dataItem);
   // }

    //grid.removeRow($(this).closest("tr"))
}


//$(document).on("dblclick", "#leads-grid tbody tr", function (e) {
//    // debugger;
//    var element = e.target || e.srcElement;
//    var dataItem = $("#leads-grid").data("kendoGrid").dataItem($(element).closest("tr"));
//    $("#ID").val(dataItem.id);

//    GetAllActivities(EmployeeNumber, 1, "", "")


//});



function GetRS_Activity(RequestServiceID) {


    var datas = { 'RequestServiceID': RequestServiceID };
    $.ajax({
        type: "POST",
        url: "/Admin/ServicesLeads/GetRS_Activity",
        //data: "{mdate:" + "m" + "}",
        data: datas,//JSON.stringify(datas),
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        success: function (data) {
          
            
            InitActivityGrid(data)



        }
    });
}
//var PageSizeCustom=5;
function InitActivityGrid(data) {
    //PageSizeCustom = (IsToday == 1 ? cdrData.length : PageSizeCustom);

  //  debugger

    $("#activitiesGrid").kendoGrid({
        dataSource: {
            data: data,
            pageSize: 10,
         
            schema: {
                model: {
                    fields: {
                        activityTypeEn: {
                            type: "string"
                        },
                        activityTypeAr: {
                            type: "string"
                        },
                        comments: {
                            type: "string"
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
                title: "Type En",
                field: "activityTypeEn",
                //filterable: { multi: true }

            },
            {
                title: "Type Ar",
                field: "activityTypeAr",

            },
            {
                title: "Comments",
                field: "comments",
                //filterable: { multi: true }

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

        ],
        pageable: {
            buttonCount: 10,
            pageSizes: true,
            pageSizes: ['All', 10, 20, 100, 200, 500]
        },
        //toolbar: kendo.template($('#toolbar-template').html()),
        resizable: true,
        //sortable: true,
        filterable: false,

        messages: {
            noRecords: "No records available."
        },
        //toolbar: ["excel"],
        //excel: {
        //    fileName: "Employees Attendance.xlsx",
        //    proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
        //    filterable: true
        //},


    });
    //$("#todaysGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');

   
        var myWindow = $("#DetailAtivities");
    $("#DetailAtivities").css('display', 'inline');
        var undo = $("#undo");

        myWindow.kendoWindow({
            width: "700px",
            title: "Activities",
            visible: false,
            actions: [
                "Pin",
                //"Minimize",
                // "Maximize",
                "Close"
            ],
            close: onClose
        }).data("kendoWindow").center().open();

        function onClose() {
            undo.fadeIn();
            $("#DetailAtivities").css('display', 'none');
        }
        //$(".k-window-title").text(Employee + ": " + FN_Date)

    //$("#DetailAtivities").closest(".k-window").css({
    //        top: currentMousePos.y - 200,
    //        // left: currentMousePos.x
    //    });
    
}