
function AllowNumberOnly(event) {
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}

function AllowForDate(event) {
    //alert(event.target.value)
    //var val = event.target.value;
    if ((event.which < 44 && event.which != 45) || event.which > 57) {
        event.preventDefault();
    }
    //else {
    //    if (val.indexOf('-') > 1) {
    //        var split = val.split('-')
    //        var month = split[1];
    //        if ((parseInt(month) > 12 || month[1]==0) || month.length > 2) {
    //            event.preventDefault();
    //        }
    //    }
    //    else {
    //        if ((parseInt(val) < 1995 || parseInt(val) > 2099) && val.length>=4) {
    //            event.preventDefault();
    //        }
    //    }
    //}
}

function AllowPositiveNumber(event) {
    var data = $(this).val();
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}