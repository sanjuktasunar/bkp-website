
//form validation during submission starts
function formValidationAndSubmission(current) {
    if (current == 1) {
        var valid = FirstNameValidation();
        var valid1 = MiddleNameValidation();
        var valid2 = LastNameValidation();
        var valid3 = GenderIdValidation();
        var valid4 = DateOfBirthBSValidation();
        var valid5 = CitizenshipNumberValidation();
        if (valid == false || valid1 == false || valid2 == false || valid3 == false
            || valid4 == false || valid5 == false) {
            return false;
        }
        return true;
    }
    else if (current == 2) {
        var valid = MobileNumberValidation();
        var valid1 = EmailValidation();
        if (valid == false || valid1 == false) {
            return false;
        }
        return true;
    }
    else if (current == 3) {
        var valid = PermanentProvinceIdValidation();
        var valid1 = PermanentDistrictIdValidation();
        var valid2 = PermanentMunicipalityTypeIdValidation();
        var valid3 = PermanentMunicipalityValidation();
        var valid4 = PermanentWardNumberValidation();
        var valid5 = PermanentToleNameValidation();

        var valid6 = TemporaryProvinceIdValidation();
        var valid7 = TemporaryDistrictIdValidation();
        var valid8 = TemporaryMunicipalityTypeIdValidation();
        var valid9 = TemporaryMunicipalityValidation();
        var valid10 = TemporaryWardNumberValidation();
        var valid11 = TemporaryToleNameValidation();

        var valid12 = PermanentCountryIdValidation();
        var valid13 = PermanentAddressValidation();
        var valid14 = TemporaryCountryIdValidation();
        var valid15 = TemporaryAddressValidation();
        if (!valid || !valid1 || !valid2 || !valid3 || !valid4 || !valid5 || !valid6
            || !valid7 || !valid8 || !valid9 || !valid10 || !valid11
            || !valid12 || !valid13 || !valid14 || !valid15) {
            return false;
        }
        return true;
    }
    else if (current == 4) {
        var valid = OccupationIdValidation();
        var valid1 = OtherOccupationRemarksValidation();
        var valid2 = MemberFieldIdValidation();
        if (!valid || !valid1 || !valid2) {
            return false;
        }
        return true;
    }
    else if (current == 5) {
        var valid = MemberImageValidation();
        var valid1 = CitizenshipFrontImageValidation();
        var valid2 = CitizenshipBackImageValidation();
        if (!valid || !valid1 || !valid2) return false;
        return true;
    }
    else if (current == 6) {
        var valid = AmountValidation();
        var valid1 = VoucherImageFileValidation();
        var valid2 = ReferalCodeValidation();
        if (!valid || !valid1 || !valid2) return false;
        return true;
    }
    return true;
}
//form validation during submission starts


//step1 starts
function CitizenshipNumberValidation() {
    //var CitizenshipNumber = $('input[name="CitizenshipNumber"]');
    //var isValid = RequiredHandling(CitizenshipNumber);
    //if (isValid == false) {
    //    return false;
    //}
    //isValid = MaxlengthHandling(CitizenshipNumber, 20);
    //if (isValid == false) {
    //    return false;
    //}
    //return true;
    var valid = ElementValidation('CitizenshipNumber', 'required', 'maxlength', 20)
    return valid;
}
function DateOfBirthBSValidation() {
    var valid = ElementValidation('DateOfBirthBS', 'required', 'maxlength', 10)
    return valid;
}
function GenderIdValidation() {
    var valid = ElementValidation('GenderId', 'required')
    return valid;
}
function LastNameValidation() {
    var valid = ElementValidation('LastName', 'required', 'maxlength', 20)
    return valid;
}
function FirstNameValidation() {
    var valid = ElementValidation('FirstName', 'required', 'maxlength', 20)
    return valid;
}
function MiddleNameValidation() {
    var valid = ElementValidation('MiddleName', null, 'maxlength', 20)
    return valid;
}
//step1 ends
//step 2 starts
function MobileNumberValidation() {
    var valid = ElementValidation('MobileNumber', 'required', 'maxlength', 20)
    return valid;
}
function EmailValidation() {
    var valid = ElementValidation('Email', 'required', 'maxlength', 150)
    //var testemail = EmailFormat($('#Email').val());
    var testemail = true;
    if (!valid || !testemail)
        return false;
    return true;
}
function EmailFormat(emailAddress) {
    if (emailAddress.length > 0) {
      var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;

       /* var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(gmail.com)/;*/
        var test = pattern.test(emailAddress);
        removeErrorClasses("#Email");
        if (test == false) {
            AddErrorClasses("#Email");
            AddErrorMessage("#Email", 'Email address is not in correct format');
        }
        else {
            var last = emailAddress.substr((emailAddress.lastIndexOf('.') + 1));
            if (last.toLowerCase() != 'com') {
                AddErrorClasses("#Email");
                AddErrorMessage("#Email", 'Email address is not in correct format');
                test = false;
            }
        }
        return test;
    }
    return true;
};
//step2 ends

//step3 starts
function PermanentProvinceIdValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "false") {
        var valid = ElementValidation('PermanentProvinceId', 'required');
        return valid;
    }
    return true;
}
function PermanentDistrictIdValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "false") {
        var valid = ElementValidation('PermanentDistrictId', 'required');
        return valid;
    }
    return true;
}
function PermanentMunicipalityTypeIdValidation() {
    debugger;
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "false") {
        var valid = ElementValidation('PermanentMunicipalityTypeId', 'required');
        return valid;
    }
    return true;
}
function PermanentMunicipalityValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "false") {
        var valid = ElementValidation('PermanentMunicipality', 'required');
        return valid;
    }
    return true;
}
function PermanentWardNumberValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "false") {
        var valid = ElementValidation('PermanentWardNumber', 'required', 'maxlength', 4);
        return valid;
    }
    return true;
}
function PermanentToleNameValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "false") {
        var valid = ElementValidation('PermanentToleName', 'required');
        return valid;
    }
    return true;
}
function PermanentCountryIdValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "true") {
        var valid = ElementValidation('PermanentCountryId', 'required');
        return valid;
    }
    return true;
}
function PermanentAddressValidation() {
    var permanentRadio = $("input[name='PermanentIsOutsideNepal']:checked").val();
    if (permanentRadio == "true") {
        var valid = ElementValidation('PermanentAddress', 'required');
        return valid;
    }
    return true;
}
function TemporaryProvinceIdValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "false") {
        var valid = ElementValidation('TemporaryProvinceId', 'required');
        return valid;
    }
    return true;
}
function TemporaryDistrictIdValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "false") {
        var valid = ElementValidation('TemporaryDistrictId', 'required');
        return valid;
    }
    return true;
}
function TemporaryMunicipalityTypeIdValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "false") {
        var valid = ElementValidation('TemporaryMunicipalityTypeId', 'required');
        return valid;
    }
    return true;
}
function TemporaryMunicipalityValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "false") {
        var valid = ElementValidation('TemporaryMunicipality', 'required');
        return valid;
    }
    return true;
}
function TemporaryWardNumberValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "false") {
        var valid = ElementValidation('TemporaryWardNumber', 'required', 'maxlength', 4);
        return valid;
    }
    return true;
}
function TemporaryToleNameValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "false") {
        var valid = ElementValidation('TemporaryToleName', 'required');
        return valid;
    }
    return true;
}

function TemporaryCountryIdValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "true") {
        var valid = ElementValidation('TemporaryCountryId', 'required');
        return valid;
    }
    return true;
}
function TemporaryAddressValidation() {
    var temporaryRadio = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (temporaryRadio == "true") {
        var valid = ElementValidation('TemporaryAddress', 'required');
        return valid;
    }
    return true;
}
//step3 ends

//step4 starts
function OccupationIdValidation() {
    var valid = ElementValidation('OccupationId', 'required');
    return valid;
}
function OtherOccupationRemarksValidation() {
    var data = $("#OccupationId option:selected").text();
    if (data.toLowerCase() == "other") {
        var valid = ElementValidation('OtherOccupationRemarks', 'required', 'maxlength', 150);
        return valid;
    }
    return true;
}
function MemberFieldIdValidation() {
    var valid = ElementValidation('MemberFieldId', 'required');
    return valid;
}
//step4 ends

//step5 starts
function MemberImageValidation() {
    var memberPhoto = $("#MemberPhoto").val();
    var MemberPhotoString = $("#MemberPhotoString").val();
    if (memberPhoto.length == 0 && MemberPhotoString.length==0) {
        var valid = ElementValidation('MemberImage', 'required');
        return valid;
    }
    var valid = FileHandling('MemberImage');
    if (valid == false)
        return false;
    return true;
}
function CitizenshipFrontImageValidation() {
    var citizenshipFront = $("#CitizenshipFront").val();
    var CitizenshipFrontImageString = $("#CitizenshipFrontImageString").val();
    if (citizenshipFront.length == 0 && CitizenshipFrontImageString.length==0) {
        var valid = ElementValidation('CitizenshipFront', 'required');
        return valid;
    }
    var valid = FileHandling('CitizenshipFrontImage');
    if (valid == false)
        return false;
    return true;
}
function CitizenshipBackImageValidation() {
    var citizenshipBack = $("#CitizenshipBack").val();
    var CitizenshipBackImageString = $("#CitizenshipBackImageString").val();
    if (citizenshipBack.length == 0 && CitizenshipBackImageString.length==0) {
        var valid = ElementValidation('CitizenshipBackImage', 'required');
        return valid;
    }
    var valid = FileHandling('CitizenshipBackImage');
    if (valid == false)
        return false;
    return true;
}
//step5 ends

//step6 starts
function AmountValidation() {
    var valid = ElementValidation('Amount', 'required', 'maxlength', 20);
    return valid;
}
function VoucherImageFileValidation() {
    var voucherImageFile = $("#VoucherImage").val();
    var VoucherImageString = $("#VoucherImageString").val();
    if (voucherImageFile.length == 0 && VoucherImageString.length==0) {
        var valid = ElementValidation('VoucherImageFile', 'required');
        return valid;
    }
    var valid = FileHandling('VoucherImageFile');
    if (valid == false)
        return false;
    return valid;
}

function ReferalCodeValidation() {
    var valid = ElementValidation('ReferalCode', 'required', 'maxlength', 30);
    return valid;
}
//step6 ends


//form validation on events
$(this).keyup(function (event) {
    var name = $(event.target).attr("id");
    var functionName = name + 'Validation';
    var fn = window[functionName];
    if ($.isFunction(fn)) {
        window[functionName]();
    }
})
$(this).change(function (event) {
    var name = $(event.target).attr("id");
    var functionName = name + 'Validation';
    var fn = window[functionName];
    if ($.isFunction(fn)) {
        window[functionName]();
    }
})