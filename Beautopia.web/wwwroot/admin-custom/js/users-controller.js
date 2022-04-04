var $document = $(document),
	$window = $(window),
	forms = {
        managusers: $('#managusers')
	};
$document.ready(function () {
   

    if (forms.managusers.length) {
        var $managusers = forms.managusers;
        $managusers.validate({
			rules: {
                FullName: {
					required: true,
					//minlength: 2
				},
                UserName: {
					required: true,

                },
                Password: {
                    required: true,

                },
                EmailID: {
                    email: true,

                },
                RoleID: {
                    required: true,

                }
                

			},
			messages: {
                FullName: {
                    required: "Please Enter FullName"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                UserName: {
                    required: "Please enter UserName",
					//minlength: "mobile must be 10 digits",
					//maxlength: "mobile must be 10 digits"
                },
                Password: {
                    required: "Please enter Password",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
                EmailID: {
                    required: "Please enter Password",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
                RoleID: {
                    required: "Please enter Password",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                }
			},
			submitHandler: function submitHandler(form) {
				
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/Users/SaveUpdateUsers",
                    success: function success(data) {
                        $(".ManageUsersdGridDiv").empty();
                        $(".ManageUsersdGridDiv").append('<div id="ManageUsersGrid"></div>')
                        ClearFields()
                        InitGetAllUsers(data)
					},
					error: function error() {
						//	debugger
                        alert($managusers)
						//$('.errorform', $LoginRequest).fadeIn();
					}
				});
			}
		});
	}


});

GetAllUsers()







function ClearFields() {


    $(".inputClass").val("")
    $("ID").val("0")
    //$(".filterprop").val("-1")
   // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
    $(".chkClass").prop('checked', false);
}



function GetAllUsers() {

    $.ajax({
        type: "POST",
        url: "/Admin/Users/GetAllUsers",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitGetAllUsers(data)


        }
    });


}

function InitGetAllServiceCategory(cdrData) {
    $("#ManageUsersGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        FullName: {
                            type: "string"
                        },
                        UserName: {
                            type: "string"
                        },
                        EmailID: {
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
                title: "Full Name",
                field: "fullName",
                //filterable: {
                //    operators: {
                //        string: {
                //            contains: "Contains"
                //        }
                //    }
                //}

            },

            {
                title: "User Name",
                field: "userName",
                filterable: true

            },
            {
                title: "Email",
                field: "emailID",
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
            fileName: "Service Category.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#ManageUsersGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#ManageUsersGrid tbody tr", function (e) {
   // debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#ManageUsersGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#FullName").val(dataItem.fullName);
    $("#UserName").val(dataItem.userName);
    $("#Password").val(dataItem.password);
    $("#EmailID").val(dataItem.emailID);
    $("#RoleID").val(dataItem.roleID);
    //$("#RoleID").val(dataItem.RoleID);

    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }
    //$("#tabs").tabs("select", "Store-Update")
});













