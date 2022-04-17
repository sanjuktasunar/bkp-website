
function getDeleteConfirmationMessage() {
    var message = "Are you sure you want to delete ???";
    return message;
}


function showMessage_Only(resp) {
    if (resp != null) {
        var icon = "success";
        var title = "Success";
        if (resp.messageType != "success") {
            icon = "error";
            title = "Error";
        }
        swal({
            title: title + "!",
            text: resp.message,
            icon: icon,
        })
    }
}

function showResultMessage(resp, returnUrl) {
    if (resp == null) {
        swal({
            text: 'You do not have access to perform this action',
            title: "Error",
            icon: "error",
        })
    }
    else {
        var icon = "success";
        var title = "Success";
        var msg = GetMessageString(resp);
        if (resp.messageType != "success") {
            icon = "error";
            title = "Error";

            if (msg == null || msg.trim() == '')
                resp.message = "Url Not Found Or Something error happened,please contact to admin";
        }
        const wrapper = document.createElement('div');
        wrapper.innerHTML = msg;
        swal({
            title: title + "!",
            content: wrapper,
            icon: icon,
        })
            .then(() => {
                if (returnUrl != undefined && resp.messageType == "success") {
                    window.location.href = returnUrl;
                }
            });

        $("#btnSave").prop('disabled', false)
        $("#btnSave").html('Save')
    }
}

function GetMessageString(resp) {
    console.log("response is : "+resp);
    debugger;
    var msg = "";
    if (resp.message != null && resp.message != "") {
        msg = resp.message;
    }
    if (resp.messageList != null) {
        $.each(resp.messageList, function (i, r) {
            if (r != null && r != "") {
                if (msg == "") {
                    msg = r;
                }
                else {
                    msg += "<br />" + r;
                }
            }
        });
    }
    return msg;
}