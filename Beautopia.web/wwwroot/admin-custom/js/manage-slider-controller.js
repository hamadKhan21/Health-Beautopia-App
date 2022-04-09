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
                    required: "Please Enter Title1"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                title2: {
                    required: "Please enter title2",
					//minlength: "mobile must be 10 digits",
					//maxlength: "mobile must be 10 digits"
                },
                title3: {
                    required: "Please enter title3",
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
            fileName: "Sliders.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#sliderGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#sliderGrid tbody tr", function (e) {
   //debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#sliderGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#title1").val(dataItem.title1);
    $("#title2").val(dataItem.title2);
    $("#title3").val(dataItem.title3);
 
    $("#SliderImage").val(dataItem.siderImage);
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


    //$("#tabs").tabs("select", "Store-Update")
});













