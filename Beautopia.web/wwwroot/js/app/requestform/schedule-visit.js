
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
					
					Mobile: {
						required: ($("#Lang").val()!= "Ar" ? "الرجاء إدخال هاتفك المحمول" : "Please enter your Mobile #"),
						minlength: ($("#Lang").val() != "Ar" ? "يجب أن يتكون الهاتف المحمول من ۱۰ أرقام" : "mobile must be 10 digits"),
						maxlength: ($("#Lang").val() != "Ar" ? "يجب أن يتكون الهاتف المحمول من ۱۰ أرقام" : "mobile must be 10 digits")
					}
				},
				submitHandler: function submitHandler(form) {
					
					$(form).ajaxSubmit({
						type: "POST",
						data: $(form).serialize(),
						url: "/Home/SaveScheduleVisit",
						success: function success() {
							$('.errorform').css("display", "none")
							$('.successform', $serviceRequest).fadeIn();
							$serviceRequest.get(0).reset();
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
debugger

//var RequestType = getUrlVars()["RequestType"];
//$('#RequestType').val(RequestType)
//var NameOfRequest = getUrlVars()["NameOfRequest"];
//$('#NameOfRequest').val(NameOfRequest.replace("_", " "))





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