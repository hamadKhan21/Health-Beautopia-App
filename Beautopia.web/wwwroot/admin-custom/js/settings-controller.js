
var $document = $(document);
	$document.ready(function () {
		
	

});




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
GetSources();
GetServiceCategories();

// Click Events

$("#Source").change(function () {
	$("#serviceRequestID").val("-1")
	$("#serviceSubCategoryDiv").empty();
	//var URLValue = $("#generatedURL").val()
	$("#generatedURL").val("https://Butopia-Clinic.com/requestservice" + "?SourceID=" + $("#Source").val())
	//GetServiceSubCategory($("#serviceRequestID").val())
})

$("#serviceRequestID").change(function () {
	//var URLValue = $("#generatedURL").val()
	$("#generatedURL").val("https://Butopia-Clinic.com/requestservice" + "?SourceID=" + $("#Source").val() + "&CategoryID=" + $("#serviceRequestID").val())
	GetServiceSubCategory($("#serviceRequestID").val())
})

function GetServiceCategories() {
	$("#serviceRequestID").empty();
	$("#serviceRequestID").append("<option value='-1'>Select Service Category</option>")

	////var datas = { 'IsEmployees': 1, 'Section': -1, 'IsActive': 1 };
	$.ajax({
		type: "GET",
		url: "/Admin/Settings/GetServiceCategory",
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

					$("#serviceRequestID").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].categoryNameAr + "</option>");


				}

			}

		

		}
	});



}


function GetServiceSubCategory(CategoryID) {

	$("#serviceSubCategoryDiv").empty();
	var datas = { 'CategoryID': CategoryID };
	$.ajax({
		type: "GET",
		url: "/Admin/Settings/GetServiceSubCategory",
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

					AppendServiceCheckboxes(cdrData[i].subCategoryNameAr, cdrData[i].id, cdrData[i].servicePrice)

					//$("#serviceRequestID").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].categoryNameAr + "</option>");


				}

				$(".subserviceClass").click(function () {
					var SubCategoryID = "";
					$('.subserviceClass').each(function () {
						//debugger
						var ICHecked = $('#' + $(this).attr('id')).prop('checked');
						if (ICHecked == true) {
							SubCategoryID = (SubCategoryID == "" ? $(this).attr('id').split("_")[1] : SubCategoryID + "_" + $(this).attr('id').split("_")[1])
						}

					});
					//debugger
					//var URLValue = $("#generatedURL").val()
					//var SubURL = URLValue.split("&")
					//if (SubURL.length > 0) {
					//	SubURL = SubURL[0] + SubURL[0]
					//}
					//else {
					//	SubURL = URLValue
					//}
					$("#generatedURL").val("https://Butopia-Clinic.com/requestservice" + "?SourceID=" + $("#Source").val() + "&CategoryID=" + $("#serviceRequestID").val() + "&SubCategoryID=" + SubCategoryID)
					SubCategoryID = "";
				})
			}

			
		

		}
	});



}


function AppendServiceCheckboxes(SubCategoryService,SubCategoryServiceID,price) {

	$("#serviceSubCategoryDiv").append('<div class="col-md-4" style="margin-bottom:10px">'//;border: 1px solid;box-shadow: 1px 1px 1px #514eff;border-radius: 4px
		+ '<div class="custom-checkbox">'
	
		+ '<span style="font-weight: bold;color: #31326e;font-size: 14px;">'
		+ SubCategoryService + ' ' + price +'ريال'
		+ '</span>'
		+ '<input type="checkbox" class="subserviceClass servicecheckCLass_' + SubCategoryServiceID +'" name="servicecheck" id="service_' + SubCategoryServiceID + '" style="margin-left: 3px;" />'
		
		+ '</div></div>'
	);
	


}


function GetSources() {
	$("#Source").empty();
	$("#Source").append("<option value='-1'>Select Campeign Source</option>")

	////var datas = { 'IsEmployees': 1, 'Section': -1, 'IsActive': 1 };
	$.ajax({
		type: "GET",
		url: "/Admin/Settings/GetSources",
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

					$("#Source").append("<option value='" + cdrData[i].id + "'>" + cdrData[i].sourceNameEn + "</option>");


				}

			}



		}
	});



}



//function getUrlVars() {
//	var vars = [], hash;
//	var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
//	for (var i = 0; i < hashes.length; i++) {
//		hash = hashes[i].split('=');
//		vars.push(hash[0]);
//		vars[hash[0]] = hash[1];
//	}
//	return vars;
//}