var $document = $(document),
    $window = $(window),
    forms = {
        managusersRole: $('#managusersRole')
    };
$document.ready(function () {


    if (forms.managusersRole.length) {
        var $managusersRole = forms.managusersRole;
        $managusersRole.validate({
            rules: {
                RoleNameEn: {
                    required: true,
                    //minlength: 2
                },
                RoleNameAr: {
                    required: true,

                },
                


            },
            messages: {
                RoleNameEn: {
                    required: "Please Enter Role Name En"
                    //minlength: "Your name must consist of at least 2 characters"
                },
                //Name: {
                //	required: "Please enter your Name",
                //	//minlength: "Your message must consist of at least 20 characters"
                //},
                RoleNameAr: {
                    required: "Please enter Role Name Ar",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
                
            },
            submitHandler: function submitHandler(form) {
               // debugger;
                var selectedChk = GetSelectedMenusCheckboxe();
                if (selectedChk.length> 0) {
                    $("#_listOfMenus").val(JSON.stringify(selectedChk))
                }

                $(form).ajaxSubmit({
                    type: "POST",
                    data: $(form).serialize(),
                    url: "/Admin/User/SaveUpdateUsersRole",
                    success: function success(data) {
                        $(".ManageUsersRoledGridDiv").empty();
                        $(".ManageUsersRoledGridDiv").append('<div id="ManageUsersRoledGrid"></div>')
                        ClearFields()
                        InitGetAllUsersRole(data)
                    },
                    error: function error() {
                        //	debugger
                        alert($managusersRole)
                        //$('.errorform', $LoginRequest).fadeIn();
                    }
                });
            }
        });
    }


});


var selectedCheckboxes = [];
function GetSelectedMenusCheckboxe() {
    selectedCheckboxes = [];
    $('.subserviceClass').each(function () {

        var ICHecked = $('#' + $(this).attr('id')).prop('checked');
        if (ICHecked == true) {
            selectedCheckboxes.push($(this).attr('id').split("_")[1]);
        }

    });

    return selectedCheckboxes;
}


GetAllUserRoles()

GetAllMenus();


function GetAllMenus() {
    $("#menuPopulateDiv").empty();
  

    ////var datas = { 'IsEmployees': 1, 'Section': -1, 'IsActive': 1 };
    $.ajax({
        type: "GET",
        url: "/Admin/User/GetAllMenus",
        //data: "{mdate:" + "m" + "}",
        //data: datas,//JSON.stringify(datas),
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        success: function (data) {
            //debugger
         
            if (data.length != 0) {
                for (var i = 0; i < data.length; i++) {

                    $("#menuPopulateDiv").append("<Label>" + data[i].menuNameEn + ":</Label>"
                        + "<div class='row row-xs-space mt-1' id='menuPopulateDiv_" + data[i].menuID+"'></div>"

                    );

                    for (var j = 0; j < data[i].subMenus.length;j++) {

                        $("#menuPopulateDiv_" + data[i].menuID).append('<div class="col-sm-3">'
                            + '<div class="custom-checkbox" style="text-align: center;">'
                            + '<span>'
                            + data[i].subMenus[j].menuNameEn
                            + '</span>'
                            + '<input type="checkbox"   class="subserviceClass form-control" name="servicecheck" id="service_' + data[i].subMenus[j].menuID + '" />'
                            +'</div></div>'

                        );
                    }


                }

            }



        }
    });



}



function ClearFields() {

    
    $(".inputClass").val("")
    $("#ID").val("0")
    //$(".filterprop").val("-1")
    // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
    $(".chkClass").prop('checked', false);
    $(".subserviceClass").prop('checked', false);
}



function GetAllUserRoles() {

    $.ajax({
        type: "POST",
        url: "/Admin/User/GetUserRoles",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            // debugger
            // var cdrData = data;
            InitGetAllUsersRole(data)


        }
    });


}

function InitGetAllUsersRole(cdrData) {
    $("#ManageUsersRoledGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        roleNameEn: {
                            type: "string"
                        },
                        roleNameAr: {
                            type: "string"
                        },
                       
                        isActive: {
                            type: "string"
                        },


                    }
                }
            }
        },

        columns: [
            {
                title: "Role Name En",
                field: "roleNameEn",
                filterable: true

            },

            {
                title: "Role Name Ar",
                field: "roleNameAr",
                filterable: true

            },
           

            {
                title: "Active",
                field: "isActive",
                filterable: { multi: true }

            },


            //{ command: { text: "Return", click: showDetails }, title: " ", width: "100px" }




        ],
        pageable: {
            buttonCount: 10,
            pageSizes: true,
            pageSizes: ['All', 100, 200, 500, 1000, 1500, 2000, 5000, 10000]
        },
        //   toolbar: kendo.template($('#toolbar-template').html()),
        resizable: true,
        selectable: "multiple",
        persistSelection: true,
        //sortable: true,
        filterable: true,

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
            fileName: "Users Role.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#ManageUsersRoledGrid").data("kendoGrid").dataSource.read();
    // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')



}


$(document).on("dblclick", "#ManageUsersRoledGrid tbody tr", function (e) {
     debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#ManageUsersRoledGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#RoleNameAr").val(dataItem.roleNameAr);
    $("#RoleNameEn").val(dataItem.roleNameEn);
 
    //$("#RoleID").val(dataItem.RoleID);

    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    $(".subserviceClass").prop('checked', false);
    if (dataItem.roleMenuMappings.length != 0) {
        for (var i = 0; i < dataItem.roleMenuMappings.length; i++) {

            $("#service_" + dataItem.roleMenuMappings[i].menuID).prop('checked', true);

        }
    }

    //$("#tabs").tabs("select", "Store-Update")
});













