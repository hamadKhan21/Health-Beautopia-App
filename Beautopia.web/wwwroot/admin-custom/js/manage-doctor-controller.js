var $document = $(document),
	$window = $(window),
	forms = {
        managedoctor: $('#managedoctor')
	};
$document.ready(function () {
   

    if (forms.managedoctor.length) {
        var $managedoctor = forms.managedoctor;
        $managedoctor.validate({
			rules: {
                DoctorName: {
					required: true,
					//minlength: 2
				},
                Designation: {
					required: true,

                },
                DoctorsCategoryID: {
                    required: true,

                },
                
            //    DoctorImageFile: {
                  //  required: true,

              //  }
                

			},
			messages: {
                DoctorName: {
                    required: "Please Enter Doctor Name"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                Designation: {
                    required: "Please enter Designation",
					//minlength: "mobile must be 10 digits",
					//maxlength: "mobile must be 10 digits"
                },
                DoctorsCategoryID: {
                    required: "Please Select Doctors Category",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
               // DoctorImageFile: {
                   // required: "Please Select Image",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
               //},
              
			},
			submitHandler: function submitHandler(form) {
				
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/AppInfo/SaveUpdateDoctor",
                    success: function success(data) {
                        $(".doctorGridDiv").empty();
                        $(".doctorGridDiv").append('<div id="doctorGrid"></div>')
                        ClearFields()
                        InitDoctorGrid(data)
					},
					error: function error() {
						//	debugger
                        alert($managedoctor)
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
$("#DoctorImageFile").change(function () {
    readURL(this);
});


GetDoctorCategoriesLkp();

GetAllDoctors()


function GetDoctorCategoriesLkp() {
    $("#DoctorsCategoryID").empty();
    $("#DoctorsCategoryID").append("<option value=''>Select Doctors Category</option>")

    ////var datas = { 'IsEmployees': 1, 'Section': -1, 'IsActive': 1 };
    $.ajax({
        type: "GET",
        url: "/Admin/Settings/GetAllDoctorsCategory",
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

                    $("#DoctorsCategoryID").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].doctorsCategoryEn + "</option>");


                }

            }



        }
    });



}





function ClearFields() {

    //$("#CategoryID option:contains(" + "Select Sub Service Category" + ")").removeAttr('selected');
    $(".inputClass").val("")
    $("#ID").val("0")
    //$(".filterprop").val("-1")
   // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
    $(".chkClass").prop('checked', false);
  //  $("#CategoryID option:contains(" + "Select Sub Service Category" + ")").attr('selected', 'selected');
    $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
   // $(".selectClass").val();
}



function GetAllDoctors() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetAllDoctors",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitDoctorGrid(data)


        }
    });


}

function InitDoctorGrid(cdrData) {
    $("#doctorGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        doctorName: {
                            type: "string"
                        },
                        doctorNameAr: {
                            type: "string"
                        },
                        designation: {
                            type: "string"
                        },
                        designationAr: {
                            type: "string"
                        },
                        doctorsCategoryEn: {
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
                title: "Doctor Name",
                field: "doctorName",
                filterable: true

            },
            {
                title: "Doctor Name Ar",
                field: "doctorNameAr",
                filterable: true

            },
            {
                title: "Designation",
                field: "designation",
                filterable: true

            },
            {
                title: "Designation Ar",
                field: "designationAr",
                filterable: true

            },
            {
                title: "Doctors Category",
                field: "doctorsCategoryEn",
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
            fileName: "doctorGrid.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#doctorGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#doctorGrid tbody tr", function (e) {
   //debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#doctorGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#DoctorName").val(dataItem.doctorName);
    $("#DoctorNameAr").val(dataItem.doctorNameAr);
    $("#Designation").val(dataItem.designation);
    $("#DesignationAr").val(dataItem.designationAr);
    $("#Description").val(dataItem.description);
    $("#DescriptionAr").val(dataItem.descriptionAr);
    $("#DoctorsCategoryID").val(dataItem.doctorsCategoryID);
 
    $("#DoctorImage").val(dataItem.doctorImage);
    //$("#SubServiceImageName").val(dataItem.subServiceImageName);
   // document.querySelector("#SubServiceImage").src = "/admin-custom/Images/SubServices/"+dataItem.subServiceImageName
    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    if (dataItem.doctorImage != "" && dataItem.doctorImage != null) {
        $("#subservice-img-tag").attr("src", "/medlab/images/content/doctors/" + dataItem.doctorImage);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }


    //$("#tabs").tabs("select", "Store-Update")
});













