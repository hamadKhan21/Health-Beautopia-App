var $document = $(document),
	$window = $(window),
	forms = {
        manageSubService: $('#manageSubService')
	};
$document.ready(function () {
   

    if (forms.manageSubService.length) {
        var $ManageSubService = forms.manageSubService;
        $ManageSubService.validate({
			rules: {
                SubCategoryNameEn: {
					required: true,
					//minlength: 2
				},
                SubCategoryNameAr: {
					required: true,

                },
               // ServicePrice: {
                 //   required: true,

              //  },
                 CategoryID: {
                    required: true,

                },
               // SubServiceImage: {
                   // required: true,

                //}
                

			},
			messages: {
                SubCategoryNameEn: {
                    required: "Please Enter Name En"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                SubCategoryNameAr: {
                    required: "Please enter Name Ar",
					//minlength: "mobile must be 10 digits",
					//maxlength: "mobile must be 10 digits"
                },
                //ServicePrice: {
                   // required: "Please enter Price",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
              //  },
                CategoryID: {
                    required: "Please Select Service",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
                //SubServiceImage: {
                   // required: "Please Select Image",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
               // }
			},
			submitHandler: function submitHandler(form) {
				
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/Settings/SaveUpdateSubServices",
                    success: function success(data) {
                        $(".ServiceSubCatereceivedGridDiv").empty();
                        $(".ServiceSubCatereceivedGridDiv").append('<div id="ServiceSubCatereceivedGrid"></div>')
                        ClearFields()
                        InitGetAllSubServiceCategory(data)
					},
					error: function error() {
						//	debugger
                        alert($ManageService)
						//$('.errorform', $LoginRequest).fadeIn();
					}
				});
			}
		});
	}


});


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#subservice-img-tag').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#SubServiceImage").change(function () {
    readURL(this);
});



GetServiceCategoriesLkp();
GetAllSubServiceCategory()

function GetServiceCategoriesLkp() {
    $("#CategoryID").empty();
    $("#CategoryID").append("<option value=''>Select Service Category</option>")

    ////var datas = { 'IsEmployees': 1, 'Section': -1, 'IsActive': 1 };
    $.ajax({
        type: "GET",
        url: "/Home/GetServiceCategory",
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

                    $("#CategoryID").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].categoryNameAr + "</option>");


                }

            }

    

        }
    });



}





function ClearFields() {

    $("#CategoryID option:contains(" + "Select Sub Service Category" + ")").removeAttr('selected');
    $(".inputClass").val("")
    $("#ID").val("0")
    //$(".filterprop").val("-1")
   // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
    $(".chkClass").prop('checked', false);
    $("#CategoryID option:contains(" + "Select Sub Service Category" + ")").attr('selected', 'selected');
    $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
   // $(".selectClass").val();
}



function GetAllSubServiceCategory() {

    $.ajax({
        type: "POST",
        url: "/Admin/Settings/GetAllSubServiceCategory",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitGetAllSubServiceCategory(data)


        }
    });


}

function InitGetAllSubServiceCategory(cdrData) {
    $("#ServiceSubCatereceivedGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        subCategoryNameEn: {
                            type: "string"
                        },
                        subCategoryNameAr: {
                            type: "string"
                        },
                        servicePrice: {
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
                title: "Name En",
                field: "subCategoryNameEn",
                //filterable: {
                //    operators: {
                //        string: {
                //            contains: "Contains"
                //        }
                //    }
                //}

            },

            {
                title: "Name Ar",
                field: "subCategoryNameAr",
                filterable: true

            },
            {
                title: "Price",
                field: "servicePrice",
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
    $("#ServiceSubCatereceivedGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#ServiceSubCatereceivedGrid tbody tr", function (e) {
   //debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#ServiceSubCatereceivedGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#SubCategoryNameEn").val(dataItem.subCategoryNameEn);
    $("#SubCategoryNameAr").val(dataItem.subCategoryNameAr);
    $("#ServicePrice").val(dataItem.servicePrice);
    $("#CategoryID").val(dataItem.categoryID);
    $("#SubServiceImageName").val(dataItem.subServiceImageName);
    //$("#SubServiceImageName").val(dataItem.subServiceImageName);
   // document.querySelector("#SubServiceImage").src = "/admin-custom/Images/SubServices/"+dataItem.subServiceImageName
    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    if (dataItem.subServiceImageName != "" && dataItem.subServiceImageName != null) {
        $("#subservice-img-tag").attr("src", "/admin-custom/Images/SubServices/" + dataItem.subServiceImageName);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }


    //$("#tabs").tabs("select", "Store-Update")
});













