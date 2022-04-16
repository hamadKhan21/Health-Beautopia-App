
var $document = $(document),
    $window = $(window);
$document.ready(function () {
    


    GetAboutUS()

});

// Initialize your tinyMCE Editor with your preferred options

tinymce.init({
    selector: 'textarea',
    height: 700,
    theme: 'modern',
    plugins: [
        'advlist autolink lists link image charmap print preview hr anchor pagebreak',
        'searchreplace wordcount visualblocks visualchars code fullscreen',
        'insertdatetime media nonbreaking save table contextmenu directionality',
        'emoticons template paste textcolor colorpicker textpattern imagetools'
    ],
    toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
    toolbar2: 'print preview media | forecolor backcolor emoticons',
    image_advtab: true,
    templates: [
        { title: 'Test template 1', content: 'Test 1' },
        { title: 'Test template 2', content: 'Test 2' }
    ],
    content_css: [
        '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
        '//www.tinymce.com/css/codepen.min.css'
    ]
});


$("#saveit").click(function () {

    var description = tinyMCE.get('AboutUSTextEditor').getContent();
   
    if (description == "") {

        alert("Please enter About Us Details")
        return false;
    }

    var datas = { 'ID': $("#ID").val(), "AboutUSText": description};

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/SaveUpdateAboutUs",
        //data: "{mdate:" + "m" + "}",
        data: datas,
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#ID").val(data.id);
            tinyMCE.activeEditor.setContent(data.aboutUSText);
         
           
        }
    });

})











function GetAboutUS() {

    $.ajax({
        type: "POST",
        url: "/Admin/AppInfo/GetAboutUs",
        //data: "{mdate:" + "m" + "}",
        //data: JSON.stringify(datas),
        // dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
          //  Editor()
           
            $("#ID").val(data.id);
            setTimeout(function () {
                tinyMCE.activeEditor.setContent(data.aboutUSText);

            },2000);

        }
    });


}

















