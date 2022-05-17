var $document = $(document),
	$window = $(window),
	forms = {
        manageSmileGillary: $('#manageSmileGillary')
	};
$document.ready(function () {
   

    if (forms.manageSmileGillary.length) {
        var $manageSmileGillary = forms.manageSmileGillary;
        $manageSmileGillary.validate({
			rules: {
                Title: {
					required: true,
					//minlength: 2
				},
              //  SmileImageFile: {
                   // required: true,

              // }
                

			},
			messages: {
                Title: {
                    required: "Please Enter Title"
					//minlength: "Your name must consist of at least 2 characters"
				},
				
             //   SmileImageFile: {
                 //   required: "Please select Image",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
               // },
              
			},
			submitHandler: function submitHandler(form) {
				
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/AppInfo/SaveUpdateSmileGillary",
                    success: function success(data) {
                        $(".SmileGillaryGridDiv").empty();
                        $(".SmileGillaryGridDiv").append('<div id="SmileGillaryGrid"></div>')
                        ClearFields()
                        InitSmileGillaryGrid(data)
					},
					error: function error() {
						//	debugger
                        alert($manageSmileGillary)
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
$("#SmileImageFile").change(function () {
    readURL(this);
});

function readURLAr(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#subservice-img-tagAr').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#SmileImageFileAr").change(function () {
    readURLAr(this);
});


GetAllSmileGillary()







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
    $("#subservice-img-tagAr").attr("src", "/admin-custom/Images/no image.png");
   // $(".selectClass").val();
}



function GetAllSmileGillary() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetSmileGillary",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitSmileGillaryGrid(data)


        }
    });


}

function InitSmileGillaryGrid(cdrData) {
    $("#SmileGillaryGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        title: {
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
                title: "Title",
                field: "title",
                //filterable: {
                //    operators: {
                //        string: {
                //            contains: "Contains"
                //        }
                //    }
                //}

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
            fileName: "Offers.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#SmileGillaryGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#SmileGillaryGrid tbody tr", function (e) {
   //debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#SmileGillaryGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#Title").val(dataItem.title);
    
 
    $("#SmileImage").val(dataItem.smileImage);
    $("#SmileImageAr").val(dataItem.smileImageAr);
    //$("#SubServiceImageName").val(dataItem.subServiceImageName);
   // document.querySelector("#SubServiceImage").src = "/admin-custom/Images/SubServices/"+dataItem.subServiceImageName
    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    if (dataItem.smileImage != "" && dataItem.smileImage != null) {
        $("#subservice-img-tag").attr("src", "/medlab/images/content/gallery/" + dataItem.smileImage);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }

    if (dataItem.smileImageAr != "" && dataItem.smileImageAr != null) {
        $("#subservice-img-tagAr").attr("src", "/medlab/images/content/gallery/" + dataItem.smileImageAr);
    }
    else {

        $("#subservice-img-tagAr").attr("src", "/admin-custom/Images/no image.png");
    }
    //$("#tabs").tabs("select", "Store-Update")
});













