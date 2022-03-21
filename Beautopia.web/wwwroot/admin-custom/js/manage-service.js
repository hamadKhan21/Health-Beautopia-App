var $document = $(document),
	$window = $(window),
	forms = {
		manageService: $('#manageService')
	};
$document.ready(function () {
   

    if (forms.manageService.length) {
        var $ManageService = forms.manageService;
        $ManageService.validate({
			rules: {
                CategoryNameEn: {
					required: true,
					//minlength: 2
				},
                CategoryNameAr: {
					required: true,

                }
                

			},
			messages: {
                CategoryNameEn: {
                    required: "Please Enter Name En"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                CategoryNameAr: {
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
                    url: "/Admin/Settings/SaveUpdateServices",
                    success: function success(data) {
                        $(".ServiceCatereceivedGridDiv").empty();
                        $(".ServiceCatereceivedGridDiv").append('<div id="ServiceCatereceivedGrid"></div>')
                        ClearFields()
                        InitGetAllServiceCategory(data)
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

GetAllServiceCategory()







function ClearFields() {


    $(".inputClass").val("")
    $("ID").val("0")
    //$(".filterprop").val("-1")
   // $("#IsActiveChecked").prop('checked', false);
    //$("#IsActiveChecked").prop('checked', true)
    $(".chkClass").prop('checked', false);
}



function GetAllServiceCategory() {

    $.ajax({
        type: "POST",
        url: "/Admin/Settings/GetAllServiceCategory",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitGetAllServiceCategory(data)


        }
    });


}

function InitGetAllServiceCategory(cdrData) {
    $("#ServiceCatereceivedGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        categoryNameEn: {
                            type: "string"
                        },
                        categoryNameAr: {
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
                title: "Category Name En",
                field: "categoryNameEn",
                //filterable: {
                //    operators: {
                //        string: {
                //            contains: "Contains"
                //        }
                //    }
                //}

            },

            {
                title: "Category Name Ar",
                field: "categoryNameAr",
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
    $("#ServiceCatereceivedGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#ServiceCatereceivedGrid tbody tr", function (e) {
    debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#ServiceCatereceivedGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#CategoryNameAr").val(dataItem.categoryNameAr);
    $("#CategoryNameEn").val(dataItem.categoryNameEn);

    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }
    //$("#tabs").tabs("select", "Store-Update")
});













