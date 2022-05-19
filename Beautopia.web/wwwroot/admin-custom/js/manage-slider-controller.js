var $document = $(document),
	$window = $(window),
	forms = {
        manageslider: $('#manageslider')
	};
$document.ready(function () {
   

    if (forms.manageslider.length) {
        var $manageslider = forms.manageslider;
        $manageslider.validate({
			rules: {
                title1: {
					required: true,
					//minlength: 2
				},
                title2: {
					required: true,

                },
                title3: {
                    required: true,

                },
                
               // SubServiceImage: {
                   // required: true,

                //}
                

			},
			messages: {
                title1: {
                    required: "Please Enter Title1 En"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                title2: {
                    required: "Please enter title2 En",
					//minlength: "mobile must be 10 digits",
					//maxlength: "mobile must be 10 digits"
                },
                title3: {
                    required: "Please enter title3 En",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
                },
              
			},
			submitHandler: function submitHandler(form) {
				
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/AppInfo/SaveUpdateSlider",
                    success: function success(data) {
                        $(".sliderGridDiv").empty();
                        $(".sliderGridDiv").append('<div id="sliderGrid"></div>')
                        ClearFields()
                        InitSliderGrid(data)
					},
					error: function error() {
						//	debugger
                        alert($manageslider)
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
$("#SliderImageFile").change(function () {
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
$("#SliderImageFileAr").change(function () {
    readURLAr(this);
});


GetAllSliders()







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



function GetAllSliders() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetAllSliders",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitSliderGrid(data)


        }
    });


}

function InitSliderGrid(cdrData) {
    $("#sliderGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        title1: {
                            type: "string"
                        },
                        title2: {
                            type: "string"
                        },
                        title3: {
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
                title: "Title1",
                field: "title1",
                //filterable: {
                //    operators: {
                //        string: {
                //            contains: "Contains"
                //        }
                //    }
                //}

            },

            {
                title: "Title2",
                field: "title2",
                filterable: true

            },
            {
                title: "Title3",
                field: "title3",
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
            fileName: "Sliders.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#sliderGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}
function DeleteRecord(e) {
    e.preventDefault();

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    // var IsActiveORNot = (dataItem.IsActive == "false" ? true : false);
    //debugger
    var datas = { 'ID': dataItem.id, 'Entity': 'Slider' };
    $.ajax({
        type: "POST",
        url: "/Admin/Settings/RemoveTheRecord",
        //data: "{mdate:" + "m" + "}",
        data: datas,
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        //headers: { "Authorization": $("#JSonParaValue").val() },
        success: function (data) {
            GetAllSliders()

        }



    });
}

$(document).on("dblclick", "#sliderGrid tbody tr", function (e) {
   //debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#sliderGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#title1").val(dataItem.title1);
    $("#title2").val(dataItem.title2);
    $("#title3").val(dataItem.title3);


    $("#title1Ar").val(dataItem.title1Ar);
    $("#title2Ar").val(dataItem.title2Ar);
    $("#title3Ar").val(dataItem.title3Ar);

    $("#SliderImage").val(dataItem.sliderImage);
    $("#SliderImageAr").val(dataItem.sliderImageAr);
    //$("#SubServiceImageName").val(dataItem.subServiceImageName);
   // document.querySelector("#SubServiceImage").src = "/admin-custom/Images/SubServices/"+dataItem.subServiceImageName
    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    if (dataItem.sliderImage != "" && dataItem.sliderImage != null) {
        $("#subservice-img-tag").attr("src", "/medlab/images/content/slider/" + dataItem.sliderImage);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }


    if (dataItem.sliderImageAr != "" && dataItem.sliderImageAr != null) {
        $("#subservice-img-tagAr").attr("src", "/medlab/images/content/slider/" + dataItem.sliderImageAr);
    }
    else {

        $("#subservice-img-tagAr").attr("src", "/admin-custom/Images/no image.png");
    }

    //$("#tabs").tabs("select", "Store-Update")
});













