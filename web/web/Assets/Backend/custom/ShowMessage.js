
function getDeleteConfirmationMessage(){
    var message = "Are you sure you want to delete this ?"
    return message;
}

function getApproveConfirmationMessage() {
    var message = "Are you sure you want to approve ?"
    return message;
}

function getRejectConfirmationMessage() {
    var message = "Are you sure you want to reject ?"
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
            title: title+"!",
            text: resp.message,
            icon: icon,
        })
    }
}

function showResultMessage(resp, returnUrl) {
    if (resp == null) {
        //window.location.href = "/Logout";
        swal({
            text: 'You do not have access to perform this action',
            title: "Error",
            icon: "error",
        })
    }
    else {
        var icon = "success";
        var title = "Success";
        if (resp.messageType != "success") {
            icon = "error";
            title = "Error";

            if (resp.message == null || resp.message.trim() == '')
                resp.message = "Url Not Found Or Something error happened,please contact to admin";
        }
        swal({
            title: title + "!",
            text: resp.message,
            icon: icon,
        })
            .then(() => {
                if (returnUrl != undefined &&
                    (resp.messageType == "success" || resp.isOtherSuccess == true)) {
                    window.location.href = returnUrl;
                }
            });

        $("#btnSave").prop('disabled', false)
        $("#btnSave").html('Save')
    }
}
