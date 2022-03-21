var $document = $(document),
	$window = $(window),
	forms = {
		LoginRequest: $('#LoginRequest')
	};
$document.ready(function () {

	if (forms.LoginRequest.length) {
		var $LoginRequest = forms.LoginRequest;
		$LoginRequest.validate({
			rules: {
				UserName: {
					required: true,
					//minlength: 2
				},
				Password: {
					required: true,

				}

			},
			messages: {
				UserName: {
					required: "Please Enter Username"
					//minlength: "Your name must consist of at least 2 characters"
				},
				//Name: {
				//	required: "Please enter your Name",
				//	//minlength: "Your message must consist of at least 20 characters"
				//},
				Password: {
					required: "Please enter your Password",
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
					url: "/Admin/Accounts/Validate",
					success: function success(data) {
						if (data == 0) {

							alert("Username or Password is incorrect")
						}
						else if (data == -1) {
							alert("You are not authorized to access this application pleas contact IT Administrator");

						}
						else {
							window.location.href = "/Dashboard";
						}
					},
					error: function error() {
						//	debugger
						alert($LoginRequest)
						//$('.errorform', $LoginRequest).fadeIn();
					}
				});
			}
		});
	}


});