
	var $document = $(document),
		$window = $(window),
		forms = {
			serviceRequest: $('#ServiceRequest')
		};
	$document.ready(function () {
		
		if (forms.serviceRequest.length) {
			var $serviceRequest = forms.serviceRequest;
			$serviceRequest.validate({
				rules: {
					Service: {
						required: true,
						//minlength: 2
					},
					Email: {
						email: true,
						
					},
					Mobile: {
						required: true,
						minlength: 10,
						maxlength: 10
					}

				},
				messages: {
					Service: {
						required: ($("#Lang").val() != "Ar" ? "الرجاء تحديد الخدمة" : "Please select the service")
						//minlength: "Your name must consist of at least 2 characters"
					},
					//Name: {
					//	required: "Please enter your Name",
					//	//minlength: "Your message must consist of at least 20 characters"
					//},
					Mobile: {
						required: ($("#Lang").val() != "Ar" ? "الرجاء إدخال هاتفك المحمول" : "Please enter your Mobile #"),
						minlength: ($("#Lang").val() != "Ar" ? "يجب أن يتكون الهاتف المحمول من ۱۰ أرقام" : "mobile must be 10 digits"),
						maxlength: ($("#Lang").val() != "Ar" ? "يجب أن يتكون الهاتف المحمول من ۱۰ أرقام" : "mobile must be 10 digits")
					}
				},
				submitHandler: function submitHandler(form) {
					var selectedChk = GetSelectedServicesCheckboxe();
					//debugger
					if (selectedChk.length == 0) {
						$('.errorform').text(($("#Lang").val() != "Ar" ? "الرجاء تحديد الخدمات التي تهتم بها" : "Please select the services you are interested in"));
						$('.errorform').css("display","inline")
						return false;
					}
					//$("", { name: "_listOfServices", value: JSON.stringify(selectedChk) }).appendTo(form);
					////form.append("listOfServices", JSON.stringify(selectedChk));
					//form.append("listOfServices", "ASD");
					//var formData = new FormData(form);
					//formData.append("listOfServices", SON.stringify(selectedChk));
					//debugger;
					$("#_listOfServices").val(JSON.stringify(selectedChk))
					//var submitData = $(form).serialize() + "&_listOfServices=asdf"; //+ JSON.stringify(selectedChk);
					//debugger
					$(form).ajaxSubmit({
						type: "POST",
						data: $(form).serialize(),
						url: "/Home/SaveRequestService",
						success: function success() {
							$('.errorform').css("display", "none")
							$('.successform', $serviceRequest).fadeIn();
							$serviceRequest.get(0).reset();

							$("#serviceSubCategoryDiv").empty();
						},
						error: function error() {
						//	debugger
							$('.errorform', $serviceRequest).fadeIn();
						}
					});
				}
			});
		}


});


var SourceID = getUrlVars()["SourceID"];
$('#Source').val(SourceID)

var selectedCheckboxes = [];
function GetSelectedServicesCheckboxe() {
	selectedCheckboxes = [];
	$('.subserviceClass').each(function () {

		var ICHecked = $('#' + $(this).attr('id')).prop('checked');
		if (ICHecked == true) {
			selectedCheckboxes.push($(this).attr('id').split("_")[1]);
		}
		
	});

	return selectedCheckboxes;
}

//Initialization of Lookups

GetServiceCategories();

// Click Events

$("#serviceRequestID").change(function () {

	GetServiceSubCategory($("#serviceRequestID").val())
})

function GetServiceCategories() {
	$("#serviceRequestID").empty();
	$("#serviceRequestID").append("<option value='-1'>" + ($("#Lang").val() == "En" ? "حدد فئة الخدمة" : "Select Service Category")+"</option>")

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

					$("#serviceRequestID").append("<option value='" + cdrData[i].id + "'>" + ($("#Lang").val() == "En" ? cdrData[i].categoryNameAr  : cdrData[i].categoryNameEn)+ "</option>");


				}

			}

			var CategoryIDQuery = getUrlVars()["CategoryID"];
			if (CategoryIDQuery != 0 && CategoryIDQuery != null) {

				$("#serviceRequestID").val(CategoryIDQuery);
				GetServiceSubCategory(CategoryIDQuery)
			}

		}
	});



}


function GetServiceSubCategory(CategoryID) {

	$("#serviceSubCategoryDiv").empty();
	var datas = { 'CategoryID': CategoryID };
	$.ajax({
		type: "GET",
		url: "/Home/GetServiceSubCategory",
		//data: "{mdate:" + "m" + "}",
		 data: datas,//JSON.stringify(datas),
		//dataType: "json",
		// contentType: "application/json; charset=utf-8",
		success: function (data) {
			//debugger
			var cdrData = data
			var EmpToDisplay = "";
			if (cdrData.length != 0) {
				
				for (var i = 0; i < cdrData.length; i++) {


					var ServiceCategory = ($("#Lang").val() == "En" ? cdrData[i].subCategoryNameAr : cdrData[i].subCategoryNameEn);
					var ServicePrice = ($("#Lang").val() == "En" ? cdrData[i].servicePrice + 'ريال ' : cdrData[i].servicePrice+" SAR");
					AppendServiceCheckboxes(ServiceCategory, cdrData[i].id, ServicePrice, cdrData[i].subServiceImageName)

					//$("#serviceRequestID").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].categoryNameAr + "</option>");


				}

			}

			
		

		}
	});



}
//https://Butopia-Clinic.com/requestservice?SourceID=2&CategoryID=2&SubCategoryID=5_8_9

function AppendServiceCheckboxes(SubCategoryService, SubCategoryServiceID, price, subServiceImageName) {
	//debugger;
	var CheckValue = "";
	var SubCategoryIDQuery = getUrlVars()["SubCategoryID"];
	if (SubCategoryIDQuery != null && SubCategoryIDQuery != "" && SubCategoryIDQuery != undefined) {
		SubCategoryIDQuery = SubCategoryIDQuery.split("_")
		if (SubCategoryIDQuery.length > 1) {
			CheckValue = CheckIfSUbCateExists(SubCategoryIDQuery, SubCategoryServiceID)
		}
		else if (SubCategoryIDQuery.length == 1) {
			if (SubCategoryServiceID == SubCategoryIDQuery[0]) { 
			CheckValue = SubCategoryIDQuery[0];

			}
		}
	}

	if (CheckValue != null && CheckValue!="") {
		//$("#serviceSubCategoryDiv").append('<div class="col-sm-3" style="margin-bottom:10px">'//;border: 1px solid;box-shadow: 1px 1px 1px #514eff;border-radius: 4px
		//	+ '<div class="custom-checkbox">'

		//	+ '<span style="font-weight: bold;color: #31326e;font-size: 15px;">'
		//	+ SubCategoryService + ' ' + price + 'ريال'
		//	+ '</span>'
		//	+ '<input type="checkbox" checked class="subserviceClass" name="servicecheck" id="service_' + SubCategoryServiceID + '" style="margin-left: 3px;" />'

		//	+ '</div></div>'
		//);

		$("#serviceSubCategoryDiv").append('<div class="col-md-6 col-lg-2">'//;border: 1px solid;box-shadow: 1px 1px 1px #514eff;border-radius: 4px
			+ '<div class="service-card">'
			+ '<div class="service-card-photo">'
			+ '<a href="#"><img src="/admin-custom/Images/SubServices/' + subServiceImageName+'" class="img-fluid" alt=""></a>'
			+ '</div><h5 class="service-card-name">'
			+ '<input type="checkbox" checked class="subserviceClass form-control" name="servicecheck" id="service_' + SubCategoryServiceID + '" />'
			+ '<div class="service-requested-h-decor"></div>'
			+'<p class="services-desc-cl">'
			+ SubCategoryService + ' ' + price
			+ '</p></div></div>'
		);


	}
	else {

		//$("#serviceSubCategoryDiv").append('<div class="col-sm-3" style="margin-bottom:10px">'//;border: 1px solid;box-shadow: 1px 1px 1px #514eff;border-radius: 4px
		//	+ '<div class="custom-checkbox">'

		//	+ '<span style="font-weight: bold;color: #31326e;font-size: 15px;">'
		//	+ SubCategoryService + ' ' + price + 'ريال'
		//	+ '</span>'
		//	+ '<input type="checkbox" class="subserviceClass" name="servicecheck" id="service_' + SubCategoryServiceID + '" style="margin-left: 3px;" />'

		//	+ '</div></div>'
		//);

		$("#serviceSubCategoryDiv").append('<div class="col-md-6 col-lg-2">'//;border: 1px solid;box-shadow: 1px 1px 1px #514eff;border-radius: 4px
			+ '<div class="service-card">'
			+ '<div class="service-card-photo">'
			+ '<a href="#"><img src="/admin-custom/Images/SubServices/' + subServiceImageName + '" class="img-fluid" alt=""></a>'
			+ '</div><h5 class="service-card-name">'
			+ '<input type="checkbox" class="subserviceClass form-control" name="servicecheck" id="service_' + SubCategoryServiceID + '" style="margin-left: 3px;" />'
			+ '<div class="service-requested-h-decor"></div>'
			+ '<p class="services-desc-cl">'
			+ SubCategoryService + ' ' + price 
			+ '</p></div></div>'
		);
	}


}


function CheckIfSUbCateExists(SubCateList,SubCatID) {
	var returnValue = "";
	$.each(SubCateList, function (index, value) {
		console.log(value);
		if (value == SubCatID) {

			returnValue= value
		}
		
		
	});
	return returnValue;
}

function getUrlVars() {
	var vars = [], hash;
	var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	for (var i = 0; i < hashes.length; i++) {
		hash = hashes[i].split('=');
		vars.push(hash[0]);
		vars[hash[0]] = hash[1];
	}
	return vars;
}