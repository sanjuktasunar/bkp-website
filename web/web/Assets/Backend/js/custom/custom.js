
$('input[type="file"]').change(function () {
    var idName = $(this).attr('class');
    var element_name = $(this).attr('name');
    idName = idName.replace('form-control', '').trim();
    var id = "#" + idName;
    var data = new FormData();
    var files = $(this).get(0).files;

    var val = $(this).val();
    var extension = (val.substr((val.lastIndexOf('.') + 1))).trim().toLowerCase();

    if (extension == 'png' || extension == 'jpg' || extension == 'jpeg') {
        showUploading(idName);
        data.append("Files", files[0]);
        $.ajax({
            type: "POST",
            url: "/Ajax/ConvertFileToString",
            data: data,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.messageType == "success") {
                    $(id).val(response.imageBase64String);
                    hideUploading(idName);
                    DisplayImageInDiv(idName, response.imageBase64String);
                }
            },
            error: function (response) {
                showMessage_Only(response)
            }
        })
    }
    else {
        $('input[name="' + element_name + '"]').val('');
        var resp = {
            messageType: "error",
            message: "Please select jpg,png or jpeg file"
        };
        showMessage_Only(resp);
        ClearImageInDiv(idName);
    }
})

function showUploading(elementName) {
    var divId = "#Div" + elementName;
    $(divId).empty();
    $(divId).show();
    $(divId).append('<i class="bx bx-cog"></i> Uploading..');
}
function hideUploading(elementName) {
    var divId = "#Div" + elementName;
    $(divId).empty();
}
function DisplayImageInDiv(elementName, imageString) {
    var divId = "#Div" + elementName;
    if (imageString.length > 0) {
        $(divId).show();
        $(divId).html('');
        $('<img>', {
            src: imageString
        }).appendTo($(divId));
    }
}

function ClearImageInDiv(elementName) {
    var divId = "#Div" + elementName;
    $(divId).empty();
}

function ElementValidation(elementId, required = null, maxlength = null, maxlengthNumber = null) {
    if (required == 'required') {
        var isValid = RequiredHandling(elementId);
        if (isValid == false) return false;
    }
    if (maxlength == 'maxlength') {
        var isValid = MaxlengthHandling(elementId, maxlengthNumber);
        if (isValid == false) return false;
    }
    return true;
}
function FileHandling(elementId) {
    debugger;
    var element = "#" + elementId;
    var $el = $(element);
    var val = $el.val();
    removeErrorClasses(element);
    if ($el.attr('type') == 'file' && val.length > 0) {
        var extension = val.substr((val.lastIndexOf('.') + 1));
        if (extension.toLowerCase() != 'jpg' && extension.toLowerCase() != 'png' && extension.toLowerCase() != 'jpeg') {
            $el.val('');
            AddErrorClasses(element)
            AddErrorMessage(element,
                $el.attr('name') + ' must be type of jpg,png or jpeg ')
            return false;
        }
    }
    return true;
}
function RequiredHandling(elementId) {
    var element = "#" + elementId;
    var $el = $(element);
    removeErrorClasses(element);
    if (!$el.val()) {
        AddErrorClasses(element)
        AddErrorMessage(element,
            $el.attr('name') + ' is required ')
        return false;
    }
    return true;
}
function MaxlengthHandling(elementId, length) {
    var element = "#" + elementId;
    var $el = $(element);
    removeErrorClasses(element);
    if ($el.val().length > length) {
        AddErrorClasses(element)
        AddErrorMessage(element,
            $el.attr('name') + ' must be less than ' + length);
        return false;
    }
    return true;
}

function removeErrorClasses(element) {
    var $el = $(element);
    $el.parent().addClass('form-group').removeClass('error');
    $el.parent().removeClass('.error-message');
    $el.parent().find('.error-message').remove();
}
function AddErrorClasses(element) {
    var $el = $(element);
    $el.parent().removeClass('form-group');
    $el.parent().addClass('error');
}
function AddErrorMessage(element, message) {
    var $el = $(element);
    $el.parent().append('<label class="error-message">' + message + '</label>');
}

function ValidateCitizenshipNumber(event) {
    var $field_value = event.target.value;

    //alert(event.which)
    if ($field_value == null || $field_value.trim() == '') {
        if (event.which == 47 || event.which == 45) {
            event.preventDefault();
        }
    }
    else if ($field_value.length > 0) {
        if (($field_value.charAt($field_value.length - 1) == "/" || $field_value.charAt($field_value.length - 1) == "-") && (event.which == 47 || event.which == 45)) {
            event.preventDefault();
        }
    }

    if ((event.which < 48 || event.which > 57) && (event.which != 47) && event.which != 45) {
        event.preventDefault();
    }
}

function ValidateNepaliDate(event) {
    var $field_value = event.target.value;
    if ($field_value == null || $field_value.trim() == '') {
        if (event.which != 49 && event.which != 50) {
            event.preventDefault();
        }
    }
    
    //else if ($field_value.length > 0) {
    //    if ($field_value.charAt($field_value.length - 1) == "/" && event.which == 47) {
    //        event.preventDefault();
    //    }
    //    else {
    //        if ($field_value.length == 1) {
    //            if (parseInt($field_value) == 1) {
    //                if (event.which != 57) {
    //                    event.preventDefault();
    //                }
    //            }
    //            if (parseInt($field_value) == 2) {
    //                if (event.which != 48) {
    //                    event.preventDefault();
    //                }
    //            }
    //        }
    //        else if ($field_value.length == 2) {
    //            if (event.which == 56 || event.which == 57) {
    //                event.preventDefault();
    //            }
    //        }
    //        else if ($field_value.length == 4) {
    //            if(event.which != 47) {
    //                event.preventDefault();
    //             }
    //        }
    //        else if ($field_value.length>4){
    //            var split = $field_value.split('/');
    //            var month = split[1];
    //            if (month.length == 1) {
    //                if (month > 1) {
    //                    if (event.which != 47)
    //                        event.preventDefault();
    //                    else if (event.which == 47) {
    //                        event.target.value = split[0] + "/" + "0" + month;
    //                    }
    //                }
    //                else {
    //                    if (event.which == 48) {
    //                        event.preventDefault();
    //                    }
    //                }
                    
    //            }
    //            else if (month.length > 1 && event.which != 47) {
    //                event.preventDefault();
    //            }
    //        }
    //    }
    //}

    if ((event.which < 48 || event.which > 57) && (event.which != 47)) {
        event.preventDefault();
    }
}


function ValidateMobileNumber(event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57) && event.which!=43) {
        event.preventDefault();
    }
}