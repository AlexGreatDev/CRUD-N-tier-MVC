$(function () {
   
    $("#loaderbody").addClass('hide');
    // mask input
    $(document).on("focus", "[mask]", function () {
        if ($('[mask]').attr('ismask') == undefined) {
            $(this).mask($(this).attr('mask'));
            $(this).attr('ismask', '1');
        }
    });

});
var onFailed = function (context) {

    var response = context.responseJSON;
    console.log(response)
    $('.validation-summary-valid ul').html('');
    for (var i = 0; i < response.length; i++) {
        var error = response[i];


        var fieldKey = error["key"];
        var message = error["message"];

        if (fieldKey == "general") {
            $('.validation-summary-valid ul').append(`<li>${message}</li>`);
            continue;
        }

        var errorstring = `span[data-valmsg-for="${fieldKey}"]`;
        var messagestring = `<span id="${fieldKey}-error" >${message}</span>`;
     
        $(errorstring).html(messagestring)
       
    }
}
var onSuccess = function (context) {
    $('#form-modal').modal('hide');
    alert("success");
    showlist("Employee/Showlist")

}
showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}
showlist = (url) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#view-all').html(res);
            
        }
    })
}

// Delete
jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    showlist("Employee/Showlist")
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }

    }

    //prevent default form submit event
    return false;
}