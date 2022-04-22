var $document = $(document),
	$window = $(window),
	forms = {
        manageoffer: $('#manageoffer')
	};
$document.ready(function () {
   

    if (forms.manageoffer.length) {
        var $manageoffer = forms.manageoffer;
        $manageoffer.validate({
			rules: {
                Title: {
					required: true,
					//minlength: 2
				},
               // OfferImageFile: {
                   // required: true,

              // }
                

			},
			messages: {
                Title: {
                    required: "Please Enter Title"
					//minlength: "Your name must consist of at least 2 characters"
				},
				
            ////    OfferImageFile: {
            //        required: "Please select Image",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
             //   },
              
			},
			submitHandler: function submitHandler(form) {
				
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/AppInfo/SaveUpdateOffer",
                    success: function success(data) {
                        $(".offerGridDiv").empty();
                        $(".offerGridDiv").append('<div id="offerGrid"></div>')
                        ClearFields()
                        InitOfferGrid(data)
					},
					error: function error() {
						//	debugger
                        alert($manageoffer)
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
$("#OfferImageFile").change(function () {
    readURL(this);
});




GetAllOffers()







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



function GetAllOffers() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetOffers",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            InitOfferGrid(data)


        }
    });


}

function InitOfferGrid(cdrData) {
    $("#offerGrid").kendoGrid({
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
    $("#offerGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#offerGrid tbody tr", function (e) {
   //debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#offerGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#Title").val(dataItem.title);
    $("#TitleAr").val(dataItem.titleAr);
    $("textarea#Description").val(dataItem.description);
    $("textarea#DescriptionAr").val(dataItem.descriptionAr);
    
 
    $("#OfferImage").val(dataItem.offerImage);
    //$("#SubServiceImageName").val(dataItem.subServiceImageName);
   // document.querySelector("#SubServiceImage").src = "/admin-custom/Images/SubServices/"+dataItem.subServiceImageName
    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    if (dataItem.offerImage != "" && dataItem.offerImage != null) {
        $("#subservice-img-tag").attr("src", "/medlab/images/content/Offers/" + dataItem.offerImage);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }


    //$("#tabs").tabs("select", "Store-Update")
});













