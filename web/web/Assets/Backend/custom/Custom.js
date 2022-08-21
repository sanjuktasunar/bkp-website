
function getTodayNepaliDate() {
    var obj = NepaliFunctions.GetCurrentBsDate();
    var nepaliMonth = obj.month.toString();
    if (nepaliMonth.length == 1)
        nepaliMonth = "0" + nepaliMonth;

    return (obj.year + '-' + nepaliMonth + '-' + obj.day);
}

function DisableWebPage() {
    $("body").append('<div id="overlay" style="background-color:grey;position:absolute;top:0;left:0;height:100%;width:100%;z-index:999"></div>');
}

function EnableWebPage() {
    $("#overlay").remove();
}

function $_searchUrl_ByParam(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    return decodeURI(results[1]) || 0;
}

function AllowNumberOnly(event) {
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}

function AllowNumberWithDecimal(event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}

function AllowPositiveNumber(event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}

function NumberWithCommaSeparate(event) {
    //if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
    //    event.preventDefault();
    //}
    //else {
    //    var keycode = String.fromCharCode(event.keyCode);
    //    var $fieldValue = (event.target.value + keycode).toString();
    //    var afterPoint = '';
    //    if ($fieldValue.indexOf('.') > 0)
    //        afterPoint = $fieldValue.substring($fieldValue.indexOf('.'), $fieldValue.length);
    //    $fieldValue = Math.floor($fieldValue);
    //    $fieldValue = $fieldValue.toString();
    //    var lastThree = $fieldValue.substring($fieldValue.length - 3);
    //    var otherNumbers = $fieldValue.substring(0, $fieldValue.length - 3);
    //    if (otherNumbers != '')
    //        lastThree = ',' + lastThree;
    //    var res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree + afterPoint;
    //    event.target.value = res;
    //    //alert(res);
    //}
}

function GetNumberWithComma(number_value) {
    var $fieldValue = number_value.toString();
    var afterPoint = '';
    if ($fieldValue.indexOf('.') > 0)
        afterPoint = $fieldValue.substring($fieldValue.indexOf('.'), $fieldValue.length);
    $fieldValue = Math.floor($fieldValue);
    $fieldValue = $fieldValue.toString();
    var lastThree = $fieldValue.substring($fieldValue.length - 3);
    var otherNumbers = $fieldValue.substring(0, $fieldValue.length - 3);
    if (otherNumbers != '')
        lastThree = ',' + lastThree;
    var res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree + afterPoint;
    return res;
}

function ConvertNumberToWords(num_str) {
    var num = 0;
    num_1 = num_str.split(".")[0];
    num = parseFloat(num_1.replace(',', ''));
    var ones = ["", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine ", "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "];
    var tens = ["", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"];
    if ((num = num.toString()).length > 9) return "Overflow: Maximum 9 digits supported";
    n = ("000000000" + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return;

    var str = "";
    str += n[1] != 0 ? (ones[Number(n[1])] || tens[n[1][0]] + " " + ones[n[1][1]]) + "Crore " : "";
    str += n[2] != 0 ? (ones[Number(n[2])] || tens[n[2][0]] + " " + ones[n[2][1]]) + "Lakh " : "";
    str += n[3] != 0 ? (ones[Number(n[3])] || tens[n[3][0]] + " " + ones[n[3][1]]) + "Thousand " : "";
    str += n[4] != 0 ? (ones[Number(n[4])] || tens[n[4][0]] + " " + ones[n[4][1]]) + "Hundred " : "";
    str += n[5] != 0 ? (str != "" ? "and " : "") + (ones[Number(n[5])] || tens[n[5][0]] + " " + ones[n[5][1]]) : "";

    return str;
}

function getVatPercentage() {
    return 13;
}

function unhighlightElement(element) {
    $(element).parents('.form-group').find('.is-invalid').removeClass('is-invalid');
}

function highlightElement(element) {
    var $el = $(element);
    var $parent = $el.parents('.form-group');

    $el.addClass('is-invalid');

    // Select2 and Tagsinput
    if ($el.hasClass('select2-hidden-accessible') || $el.attr('data-role') === 'tagsinput') {
        $el.parent().addClass('is-invalid');
    }
}

function ValidateCitizenshipNumber(event) {
    var $field_value = event.target.value;
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