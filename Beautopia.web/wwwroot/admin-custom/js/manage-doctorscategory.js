var $document = $(document),
	$window = $(window),
	forms = {
        manageDoctorsCategory: $('#manageDoctorsCategory')
	};
$document.ready(function () {
   

    if (forms.manageDoctorsCategory.length) {
        var $manageDoctorsCategory = forms.manageDoctorsCategory;
        $manageDoctorsCategory.validate({
			rules: {
                DoctorsCategoryEn: {
					required: true,
					//minlength: 2
				},
                DoctorsCategoryAr: {
					required: true,

                }
                

			},
			messages: {
                DoctorsCategoryEn: {
                    required: "Please Enter Name En"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                DoctorsCategoryAr: {
                    required: "Please enter Name Ar",
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
                    url: "/Admin/Settings/SaveUpdateDoctorsCategory",
                    success: function success(data) {
                        $(".DoctorsCategoryGridDiv").empty();
                        $(".DoctorsCategoryGridDiv").append('<div id="DoctorsCategoryGrid"></div>')
                        ClearFields()
                        InitGetDoctorsCategory(data)
					},
					error: function error() {
						//	debugger
                        alert($manageDoctorsCategory)
						//$('.errorform', $LoginRequest).fadeIn();
					}
				});
			}
		});
	}


});

GetAllDoctorsCategory()







function ClearFields() {


    $(".inputClass").val("")
    $("#ID").val("0")
    //$(".filterprop").val("-1")
   // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
    $(".chkClass").prop('checked', false);
}



function GetAllDoctorsCategory() {

    $.ajax({
        type: "POST",
        url: "/Admin/Settings/GetAllDoctorsCategory",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitGetDoctorsCategory(data)


        }
    });


}

function InitGetDoctorsCategory(cdrData) {
    $("#DoctorsCategoryGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        doctorsCategoryEn: {
                            type: "string"
                        },
                        doctorsCategoryAr: {
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
                title: "Department En",
                field: "doctorsCategoryEn",
                //filterable: {
                //    operators: {
                //        string: {
                //            contains: "Contains"
                //        }
                //    }
                //}

            },

            {
                title: "Department Ar",
                field: "doctorsCategoryAr",
                filterable: true

            },

            {
                title: "Active",
                field: "isActive",
                filterable: { multi: true }

            },

           
            { command: { text: "Delete", click: DeleteRecord }, title: " ", width: "100px" }




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
            fileName: "Doctors Category.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#DoctorsCategoryGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}
function DeleteRecord(e) {
    e.preventDefault();

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    // var IsActiveORNot = (dataItem.IsActive == "false" ? true : false);
    //debugger
    var datas = { 'ID': dataItem.id, 'Entity': 'DoctorsCategory' };
    $.ajax({
        type: "POST",
        url: "/Admin/Settings/RemoveTheRecord",
        //data: "{mdate:" + "m" + "}",
        data: datas,
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        //headers: { "Authorization": $("#JSonParaValue").val() },
        success: function (data) {


            //CountofAvailableMarkers = data.length;
            GetAllDoctorsCategory()



        }



    });
}

$(document).on("dblclick", "#DoctorsCategoryGrid tbody tr", function (e) {
   // debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#DoctorsCategoryGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#DoctorsCategoryEn").val(dataItem.doctorsCategoryEn);
    $("#DoctorsCategoryAr").val(dataItem.doctorsCategoryAr);

    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }
    //$("#tabs").tabs("select", "Store-Update")
});













