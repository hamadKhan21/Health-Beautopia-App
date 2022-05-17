var $document = $(document),
	$window = $(window),
	forms = {
        manageequipment: $('#manageequipment')
	};
$document.ready(function () {
   

    if (forms.manageequipment.length) {
        var $manageequipment = forms.manageequipment;
        $manageequipment.validate({
			rules: {
                Title: {
					required: true,
					//minlength: 2
                },
                TitleAr: {
                    required: true,
                    //minlength: 2
                },
                //EquipmentIcon: {
					//required: true,

               // },

                
              //  EquipmentImageFile: {
               //     required: true,

             //   }
                

			},
			messages: {
                Title: {
                    required: "Please Enter Title En"
					//minlength: "Your name must consist of at least 2 characters"
                },

                TitleAr: {
                    required: "Please Enter Title Ar"
                    //minlength: "Your name must consist of at least 2 characters"
                },
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
                //EquipmentIcon: {
                    //required: "Please enter EquipmentIcon",
					//minlength: "mobile must be 10 digits",
					//maxlength: "mobile must be 10 digits"
                //},
              //  EquipmentImageFile: {
                //    required: "Please Select Image",
                    //minlength: "mobile must be 10 digits",
                    //maxlength: "mobile must be 10 digits"
              //  },
              
			},
            submitHandler: function submitHandler(form) {
               // debugger
              //  var description = tinyMCE.get('DescriptionEditor').getContent();
              //  $("#Description").val(description);
              //  tinyMCE.get('DescriptionEditor').setContent("");

               // var DescriptionArEditorss = tinyMCE.get('DescriptionArEditor').getContent();
               // $("#DescriptionAr").val(DescriptionArEditorss);
               // tinyMCE.get('DescriptionArEditor').setContent("");
               // debuggerDescription
				//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
				//debugger
				$(form).ajaxSubmit({
					type: "POST",
					data: $(form).serialize(),
                    url: "/Admin/AppInfo/SaveUpdateEquipments",
                    success: function success(data) {
                        $(".equipmentGridDiv").empty();
                        $(".equipmentGridDiv").append('<div id="equipmentGrid"></div>')
                        ClearFields()
                        Initequipment(data)
					},
					error: function error(e) {
						//	debugger
                        alert($manageequipment)
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
$("#EquipmentImageFile").change(function () {
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
$("#EquipmentImageFileAr").change(function () {
    readURLAr(this);
});



GetAllDoctors()







function ClearFields() {
  //  tinyMCE.activeEditor.setContent("");
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



function GetAllDoctors() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetEquipments",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            Initequipment(data)


        }
    });


}

function Initequipment(cdrData) {
    $("#equipmentGrid").kendoGrid({
        dataSource: {
            data: cdrData,
            pageSize: 200,//cdrData.length,

            schema: {
                model: {
                    fields: {
                        title: {
                            type: "string"
                        },
                        titleAr: {
                            type: "string"
                        },
                        //equipmentIcon: {
                        //    type: "string"
                        //},
                       
                        //description: {
                        //    type: "string"
                       // },

                        isActive: {
                            type: "string"
                        },

                        
                    }
                }
            }
        },

        columns: [
            {
                title: "Title En",
                field: "title",
              

            },
            {
                title: "Title Ar",
                field: "titleAr",


            },

            //{
            //    title: "Equipment Icon",
            //    field: "equipmentIcon",
            //    filterable: true

            //},
            //{
              //  title: "Description",
             //   field: "description",
              //  filterable: true

           // },

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
            fileName: "EqipmentsGrid.xlsx",
            proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
            filterable: true
        },


    });
    $("#equipmentGrid").data("kendoGrid").dataSource.read();
   // $("#overlay").css('display', 'none');
    // $("#printGrid").css('display','inline')
  

  
}


$(document).on("dblclick", "#equipmentGrid tbody tr", function (e) {
  // debugger;
    var element = e.target || e.srcElement;
    var dataItem = $("#equipmentGrid").data("kendoGrid").dataItem($(element).closest("tr"));
    $("#ID").val(dataItem.id);
    $("#Title").val(dataItem.title);
    $("#TitleAr").val(dataItem.titleAr);
    $("#EquipmentIcon").val(dataItem.equipmentIcon);
    $("textarea#Description").val(dataItem.description);
    $("textarea#DescriptionAr").val(dataItem.descriptionAr);
    //$("#DescriptionAr").val(dataItem.descriptionAr);

    //$("#Description").val(dataItem.description);
   // tinyMCE.activeEditor.setContent(dataItem.description);

    //tinyMCE.get('DescriptionEditor').setContent(dataItem.description);
   // tinyMCE.get('DescriptionArEditor').setContent(dataItem.descriptionAr);

    $("#EquipmentImage").val(dataItem.equipmentImage);
    $("#EquipmentImageAr").val(dataItem.equipmentImageAr);
    //$("#SubServiceImageName").val(dataItem.subServiceImageName);
   // document.querySelector("#SubServiceImage").src = "/admin-custom/Images/SubServices/"+dataItem.subServiceImageName
    if (dataItem.isActive == "true") {

        $("#IsActiveChecked").prop('checked', true);
    }
    else {

        $("#IsActiveChecked").prop('checked', false);
    }

    if (dataItem.equipmentImage != "" && dataItem.equipmentImage != null) {
        $("#subservice-img-tag").attr("src", "/medlab/images/content/devices/" + dataItem.equipmentImage);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }

    if (dataItem.equipmentImageAr != "" && dataItem.equipmentImageAr != null) {
        $("#subservice-img-tagAr").attr("src", "/medlab/images/content/devices/" + dataItem.equipmentImageAr);
    }
    else {

        $("#subservice-img-tagAr").attr("src", "/admin-custom/Images/no image.png");
    }
    //$("#tabs").tabs("select", "Store-Update")
});







// Initialize your tinyMCE Editor with your preferred options
//tinymce.init({
//    selector: 'textarea',
//    height:150,
//    theme: 'modern',
//    plugins: [
//        'advlist autolink lists link image charmap print preview hr anchor pagebreak',
//        'searchreplace wordcount visualblocks visualchars code fullscreen',
//        'insertdatetime media nonbreaking save table contextmenu directionality',
//        'emoticons template paste textcolor colorpicker textpattern imagetools'
//    ],
//    toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
//    toolbar2: 'print preview media | forecolor backcolor emoticons',
//    image_advtab: true,
//    templates: [
//        { title: 'Test template 1', content: 'Test 1' },
//        { title: 'Test template 2', content: 'Test 2' }
//    ],
//    content_css: [
//        '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
//        '//www.tinymce.com/css/codepen.min.css'
//    ]
//});







