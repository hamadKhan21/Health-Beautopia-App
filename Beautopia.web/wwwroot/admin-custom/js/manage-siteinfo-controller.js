var $document = $(document),
	$window = $(window),
	forms = {
        managesiteinfo: $('#managesiteinfo')
	};
$document.ready(function () {
   

    if (forms.managesiteinfo.length) {
        var $managesiteinfo = forms.managesiteinfo;
        $managesiteinfo.validate({
			rules: {
                Address: {
					required: true,
					//minlength: 2
                },
                Contact: {
                    required: true,
                    //minlength: 2
                },
                Email: {
                    required: true,
                    //minlength: 2
                },
              
                

			},
			messages: {
                Address: {
                    required: "Please Enter Address"
					//minlength: "Your name must consist of at least 2 characters"
				},
                Contact: {
                    required: "Please Enter Contact"
                    //minlength: "Your name must consist of at least 2 characters"
                },
                Email: {
                    required: "Please Enter Email"
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
                    url: "/Admin/AppInfo/SaveUpdateSiteInfo",
                    success: function success(data) {
                        //$(".offerGridDiv").empty();
                        //$(".offerGridDiv").append('<div id="offerGrid"></div>')
                      //  ClearFields()
                        //InitOfferGrid(data)
                        BindData(dataItem)
					},
					error: function error() {
						//	debugger
                        alert($managesiteinfo)
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
$("#LogoImageFile").change(function () {
    readURL(this);
});




GetSiteInfo()










function GetSiteInfo() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetSiteInfo",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
           // debugger
           // var cdrData = data;
            BindData(data)


        }
    });


}



function BindData(dataItem) { 
  
    $("#ID").val(dataItem.id);
    $("#Address").val(dataItem.address);
    
 
    $("#Contact").val(dataItem.contact);
    $("#Email").val(dataItem.email);
    $("#Facebook").val(dataItem.facebook);
    $("#Twitter").val(dataItem.twitter);
    $("#Instagram").val(dataItem.instagram);
    $("#GooglePlus").val(dataItem.googlePlus);
    $("#SnapChat").val(dataItem.snapChat);
    $("#TikTok").val(dataItem.tikTok);

    if (dataItem.logoImage != "" && dataItem.logoImage != null) {
        $("#subservice-img-tag").attr("src", "/medlab/images/content/logo/" + dataItem.logoImage);
    }
    else {

        $("#subservice-img-tag").attr("src", "/admin-custom/Images/no image.png");
    }
}













