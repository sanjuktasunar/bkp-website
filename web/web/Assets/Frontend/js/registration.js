/*
export function multiStepFormHandling() {
    const allStepForms = document.querySelectorAll('.step');
    const allPrevBtn = document.querySelectorAll('.prev-btn');
    const allNextBtn = document.querySelectorAll('.next-btn');
    
    const stepIndicatorIcon = document.querySelectorAll('.step-indicator-icon');
    const stepIndicatorText = document.querySelectorAll('.step-indicator-text');

    allNextBtn.forEach((value,index) => {
        allNextBtn[index].addEventListener('click',(e)=>{
            e.preventDefault();
            allStepForms[index].classList.add('hidden');
            allStepForms[index + 1].classList.remove('hidden');

            stepIndicatorIcon[index].innerHTML = '&#10003;';
            stepIndicatorText[index + 1].classList.add('font-semibold')
            stepIndicatorIcon[index + 1].style.backgroundColor = '#22c55e'

            if(index > 4){
                stepIndicatorIcon[6].innerHTML = '&#10003;'; 
            }

        })

    })

    allPrevBtn.forEach((value,index) => {
        
        allPrevBtn[index].addEventListener('click',(e)=>{
            e.preventDefault();

            allStepForms.forEach((form) => form.classList.add('hidden') )
            allStepForms[index].classList.remove('hidden');
            stepIndicatorIcon[index].innerHTML = index + 1;
            stepIndicatorText[index + 1].classList.remove('font-semibold')
            stepIndicatorIcon[index + 1].removeAttribute("style");

            if(index > 4){
                stepIndicatorIcon[6].innerHTML = index + 2;
            }
        })
    })
    

   
} */

var allStepForms = document.querySelectorAll('.step');
var allPrevBtn = document.querySelectorAll('.prev-btn');
var allNextBtn = document.querySelectorAll('.next-btn');

var stepIndicatorIcon = document.querySelectorAll('.step-indicator-icon');
var stepIndicatorText = document.querySelectorAll('.step-indicator-text');

function allNextBtn_Click(index) {
    allStepForms[index].classList.add('hidden');
    allStepForms[index + 1].classList.remove('hidden');

    stepIndicatorIcon[index].innerHTML = '&#10003;';
    stepIndicatorText[index + 1].classList.add('font-semibold')
    stepIndicatorIcon[index + 1].style.backgroundColor = '#22c55e'

    if (index > 4) {
        stepIndicatorIcon[6].innerHTML = '&#10003;';
    }
}
function allPrevBtn_Click(index) {
    allStepForms.forEach((form) => form.classList.add('hidden'))
    allStepForms[index].classList.remove('hidden');
    stepIndicatorIcon[index].innerHTML = index + 1;
    stepIndicatorText[index + 1].classList.remove('font-semibold')
    stepIndicatorIcon[index + 1].removeAttribute("style");

    if (index > 4) {
        stepIndicatorIcon[6].innerHTML = index + 2;
    }
}

var i = 0;
$(".next-btn").click(function (evt) {
    evt.preventDefault();
    removeErrorClasses($("#search-inc-form"));
    var $_data = formDataValidation_GetUrlDatas(i+1);
    if (!$_data.valid)
        return false;

    var $btn = $(this);
    $btn.attr('disabled', true);
    $btn.find('span').html('Saving..');
    $.ajax({
        type: "post",
        url: $_data.url,
        data: $_data.data,
        success: function (resp) {
            if (resp.messageType == 'success') {
                allNextBtn_Click(i);
                LoadMemberData(resp, i + 1);
                i++;
            }
            else {
                showResultMessage(resp);
            }
            $btn.attr('disabled', false);
            $btn.find('span').html('Next');
        },
        error: function (resp) {
            showResultMessage(resp);
            $btn.attr('disabled', false);
            $btn.find('span').html('Next');
        }
    })
})

$('.prev-btn').on('click', function (event) {
    event.preventDefault();
    removeErrorClasses($("#search-inc-form"));
    i--;
    allPrevBtn_Click(i);
})

function LoadMemberData(resp, index) {
    var member = resp.memberDto;
    $("#MemberId").val(member.MemberId);
    if (index == 1) {
        $("#FirstName").val('');
        $("#MiddleName").val('');
        $("#LastName").val('');
        $("#DateOfBirthBS").val('');
        $("#MobileNumber").val('');
        $("#Email").val('');
        $("#GenderId").val('');
        $("#MaritalStatusId").val('');
        $("#OccupationId").val('');
        $("#OtherOccupationRemarks").prop('disabled', true);
        $("#OtherOccupationRemarks").val("");


        removeErrorClasses($("#FirstName"));
        removeErrorClasses($("#LastName"));
        removeErrorClasses($("#DateOfBirthBS"));
        removeErrorClasses($("#MobileNumber"));
        removeErrorClasses($("#Email"));
        removeErrorClasses($("#GenderId"));
        removeErrorClasses($("#MaritalStatusId"));
        if (member != null) {
            $("#FirstName").val(member.FirstName);
            $("#MiddleName").val(member.MiddleName);
            $("#LastName").val(member.LastName);
            $("#DateOfBirthBS").val(member.DateOfBirthBS);
            $("#MobileNumber").val(member.MobileNumber);
            $("#MobileNumber").val(member.MobileNumber);
            $("#Email").val(member.Email);
            if (member.OtherOccupationRemarks != null
                && member.OtherOccupationRemarks != '') {
                $("#OtherOccupationRemarks").val(member.OtherOccupationRemarks);
                $("#OtherOccupationRemarks").prop('disabled', false);
                $("#OccupationId").val(-1);
            }
            else {
                $("#OccupationId").val(member.OccupationId);
                $("#OtherOccupationRemarks").prop('disabled', true);
                $("#OtherOccupationRemarks").val("");
            }
            $("#GenderId").val(member.GenderId);
            $("#MaritalStatusId").val(member.MaritalStatusId);
        }
    }
    else if (index == 2) {
        $("#FormerDistrictId").val('').trigger('change');
        $("#FormerMunicipalityName").val('');
        $("#FormerWardNumber").val('');
        $("#PermanentDistrictId").val('').trigger('change');
        $("#PermanentMunicipality").val('');
        $("#PermanentWardNumber").val('');
        $(".inside-nepal").show();
        $(".outside-nepal").hide();
        $("input[name='TemporaryIsOutsideNepal'][value='false']").prop("checked", true);
        $("#TemporaryDistrictId").val('').trigger('change');
        $("#TemporaryMunicipality").val('');
        $("#TemporaryWardNumber").val('');
        $("#TemporaryCountryId").val('').trigger('change');
        $("#TemporaryAddress").val('');

        removeErrorClasses($("#FormerDistrictId"));
        removeErrorClasses($("#FormerMunicipalityName"));
        removeErrorClasses($("#FormerWardNumber"));
        removeErrorClasses($("#PermanentDistrictId"));
        removeErrorClasses($("#PermanentMunicipality"));
        removeErrorClasses($("#PermanentWardNumber"));
        removeErrorClasses($("#TemporaryDistrictId"));
        removeErrorClasses($("#TemporaryMunicipality"));
        removeErrorClasses($("#TemporaryWardNumber"));
        removeErrorClasses($("#TemporaryCountryId"));
        removeErrorClasses($("#TemporaryAddress"));


        $("#FormerDistrictId").val(member.FormerDistrictId).trigger('change');
        $("#FormerMunicipalityName").val(member.FormerMunicipalityName);
        $("#FormerWardNumber").val(member.FormerWardNumber);

        $("#PermanentDistrictId").val(member.PermanentDistrictId).trigger('change');
        $("#PermanentMunicipality").val(member.PermanentMunicipality);
        $("#PermanentWardNumber").val(member.PermanentWardNumber);

        if (member.TemporaryIsOutsideNepal) {
            $(".inside-nepal").hide();
            $(".outside-nepal").show();
            $("input[name='TemporaryIsOutsideNepal'][value='true']").prop("checked", true);

            $("#TemporaryCountryId").val(member.TemporaryCountryId).trigger('change');
            $("#TemporaryAddress").val(member.TemporaryAddress);
        }
        else {
            $(".inside-nepal").show();
            $(".outside-nepal").hide();
            $("input[name='TemporaryIsOutsideNepal'][value='false']").prop("checked", true);

            $("#TemporaryDistrictId").val(member.TemporaryDistrictId).trigger('change');
            $("#TemporaryMunicipality").val(member.TemporaryMunicipality);
            $("#TemporaryWardNumber").val(member.TemporaryWardNumber);
        }
    }
    else if (index == 3) {
        var document = resp.userDocumentDto;
        if (document != null) {
            if (document.Photo != null) {
                DisplayImageFromFolder("MemberPhotoString", document.Photo);
                $("#Photo").val(document.Photo)
            }
            
            if (document.CitizenshipFront != null) {
                DisplayImageFromFolder("CitizenshipFrontImageString", document.CitizenshipFront);
                $("#CitizenshipFront").val(document.CitizenshipFront)
            }

            if (document.CitizenshipBack != null) {
                DisplayImageFromFolder("CitizenshipBackImageString", document.CitizenshipBack);
                $("#CitizenshipBack").val(document.CitizenshipBack)
            }
        }
    }
    else if (index == 4) {
        $("#ReferenceReferalCode").val('');
        removeErrorClasses($("#ReferenceReferalCode"));
        if (member != null) {
            if (member.VoucherImage != null) {
                DisplayImageFromFolder("VoucherImageString", member.VoucherImage);
                $("#VoucherImage").val(member.VoucherImage);
            }
        }
        $("#ReferenceReferalCode").val(member.ReferenceReferalCode);
    }
}

$("#OccupationId").change(function () {
    var occupationId = $("#OccupationId").val();
    if (parseInt(occupationId) < 0) {
        $("#OtherOccupationRemarks").prop('disabled', false);
    }
    else {
        $("#OtherOccupationRemarks").prop('disabled', true);
    }
})

$("input[name='TemporaryIsOutsideNepal']").click(function () {
    var isOutside = $("input[name='TemporaryIsOutsideNepal']:checked").val();
    if (isOutside == "true") {
        $(".inside-nepal").hide();
        $(".outside-nepal").show();

        removeErrorClasses($("#TemporaryDistrictId"));
        removeErrorClasses($("#TemporaryMunicipality"));
        removeErrorClasses($("#TemporaryWardNumber"));
    }
    else {
        $(".inside-nepal").show();
        $(".outside-nepal").hide();

        removeErrorClasses($("#TemporaryCountryId"));
        removeErrorClasses($("#TemporaryAddress"));
    }
    //removeErrorClasses($("#FormerDistrictId"));
    //removeErrorClasses($("#FormerMunicipalityName"));
    //removeErrorClasses($("#FormerWardNumber"));
    //removeErrorClasses($("#PermanentDistrictId"));
    //removeErrorClasses($("#PermanentMunicipality"));
    //removeErrorClasses($("#PermanentWardNumber"));
    ////removeErrorClasses($("#TemporaryCountryId"));
    ////removeErrorClasses($("#TemporaryAddress"));
    ////removeErrorClasses($("#TemporaryDistrictId"));
    ////removeErrorClasses($("#TemporaryMunicipality"));
    ////removeErrorClasses($("#TemporaryWardNumber"));
})

function formDataValidation_GetUrlDatas(index) {
    var valid = false;
    var url = "";
    var data = new FormData();
    if (index == 1) {
        valid = elementValidation($("#CitizenshipNumber"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        url = "/Register/SaveStep1Data";
        data = $("#form_step_1").serialize();
    }
    else if (index == 2) {
        var valid1 = elementValidation($("#FirstName"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        var valid3 = elementValidation($("#LastName"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        var valid4 = elementValidation($("#DateOfBirthBS"), { isRequired: true, isMaxLength: true, maxLength: 10 });
        var valid5 = elementValidation($("#GenderId"), { isRequired: true });
        var valid6 = elementValidation($("#MobileNumber"), { isRequired: true, isMaxLength: true, maxLength: 15 });
        var valid7 = elementValidation($("#MaritalStatusId"), { isRequired: true });

        if (valid1 && valid3 && valid4 && valid5 && valid6 && valid7)
            valid = true;
        url = "/Register/SaveStep2Data";
        data = $("#form_step_2").serialize();
    }
    else if (index == 3) {
        var valid1 = elementValidation($("#FormerDistrictId"), { isRequired: true });
        var valid2 = elementValidation($("#FormerMunicipalityName"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        var valid3 = elementValidation($("#FormerWardNumber"), { isRequired: true, isMaxLength: true, maxLength: 2 });
        var valid4 = elementValidation($("#PermanentDistrictId"), { isRequired: true });
        var valid5 = elementValidation($("#PermanentMunicipality"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        var valid6 = elementValidation($("#PermanentWardNumber"), { isRequired: true, isMaxLength: true, maxLength: 2 });
        var isOutside = $("input[name='TemporaryIsOutsideNepal']:checked").val();

        if (valid1 && valid2 && valid3 && valid4 && valid5 && valid6)
            valid = true;

        if (isOutside == "true") {
            valid = false;
            var valid7 = elementValidation($("#TemporaryCountryId"), { isRequired: true });
            var valid8 = elementValidation($("#TemporaryAddress"), { isRequired: true, isMaxLength: true, maxLength: 80 });

            if (valid7 && valid8)
                valid = true;
        }
        else {
            valid = false;
            var valid7 = elementValidation($("#TemporaryDistrictId"), { isRequired: true });
            var valid8 = elementValidation($("#TemporaryMunicipality"), { isRequired: true, isMaxLength: true, maxLength: 40 });
            var valid9 = elementValidation($("#TemporaryWardNumber"), { isRequired: true, isMaxLength: true, maxLength: 2 });

            if (valid7 && valid8 && valid9)
                valid = true;
        }
        url = "/Register/SaveStep3Data";
        data = $("#form_step_3").serialize();
    }
    else if (index == 4) {
        valid = true;
        url = "/Register/SaveStep4Data";
        data = $("#form_step_4").serialize();
    }
    else if (index == 5) {
        valid = elementValidation($("#ReferenceReferalCode"), { isRequired: true, isMaxLength: true, maxLength: 30 });
        url = "/Register/SaveStep5Data";
        data = $("#form_step_5").serialize();
    }
    data = data + "&MemberId=" + $("#MemberId").val();
    return { valid, url, data};
}

function elementValidation(elementId, validationAttr) {
    if (validationAttr.isRequired == true) {
        var isValid = requiredHandling(elementId);
        if (isValid == false) return false;
    }
    if (validationAttr.isMaxLength == true) {
        var isValid = maxlengthHandling(elementId, validationAttr.maxLength);
        if (isValid == false) return false;
    }
    return true;
}

function requiredHandling(element) {
    var $el = $(element);
    removeErrorClasses(element);
    if (!$el.val()) {
        AddErrorClasses(element)
        //AddErrorMessage(element, $el.attr('name') + ' is required ')
        AddErrorMessage(element, 'This field is required');
        return false;
    }
    return true;
}
function maxlengthHandling(element, length) {
    var $el = $(element);
    removeErrorClasses(element);
    if ($el.val().length > length) {
        AddErrorClasses(element)
        //AddErrorMessage(element, $el.attr('name') + ' must be less than ' + length);
        AddErrorMessage(element, 'This field value must be less than ' + length);
        return false;
    }
    return true;
}

function errorMessageHandling() {
    $('input').on('keyup', function () {
        var $parent = $(this).parent();
        var element = $(this);
        var className = $parent.attr('class')
        if (className != undefined) {
            if (className.includes('error')) {
                if (this.value.length > 0) {
                    removeErrorClasses(element);
                }
            }
        }
    })
    $('select').on('change', function () {
        var $parent = $(this).parent();
        var element = $(this);
        if ($parent.attr('class').includes('error')) {
            if (parseInt(this.value) > 0) {
                removeErrorClasses(element);
            }
        }
    })
}

function loadDropDownList() {
    LoadGender(0, "#GenderId");
    LoadMaritalStatus(0, "#MaritalStatusId");
    LoadOccupation(0, "#OccupationId");
    LoadDistrict(0,0, "#FormerDistrictId");
    LoadDistrict(0,0, "#PermanentDistrictId");
    LoadDistrict(0,0, "#TemporaryDistrictId");
    LoadOutsideCountry(0, "#TemporaryCountryId");

    
}

function LoadSelect2() {
    $("#FormerDistrictId").select2();
    $("#PermanentDistrictId").select2();
    $("#TemporaryDistrictId").select2();
    $("#TemporaryCountryId").select2();
}
$(document).ready(function () {
    errorMessageHandling();
    LoadGender(0, "#GenderId");
    loadDropDownList();
    LoadSelect2();
})

$("#btnMemberSearch").click(function (evt) {
    evt.preventDefault();
    var element = $("#search-inc-form");
    var query = element.val();

    removeErrorClasses(element);
    if (!query) {
        AddErrorClasses(element)
        AddErrorMessage(element, "This field is required")
        return false;
    }
    else {
        if (query.length > 50) {
            AddErrorClasses(element)
            AddErrorMessage(element,"Query must be less than 50 words"
                );
            return false;
        }
    }

    var $btn_search = $("#btnMemberSearch");
    $btn_search.attr('disabled', true);
    $btn_search.find('span').html('Searching...');
    $.ajax({
        type: "get",
        url: "/Register/GetMemberByQuery",
        data: { query },
        success: function (resp) {
            if (resp.messageType == "success") {
                i = 0;
                var member = resp.memberDto;
                $("#MemberId").val(member.MemberId);
                $("#CitizenshipNumber").val(member.CitizenshipNumber);

                allNextBtn.forEach((value, index) => {
                    allStepForms[index].classList.add('hidden');
                    stepIndicatorIcon[index].innerHTML = index + 1;
                    stepIndicatorText[index + 1].classList.remove('font-semibold')
                    stepIndicatorIcon[index + 1].removeAttribute("style");
                   
                })
                allStepForms[0].classList.remove('hidden');
                allStepForms[5].classList.add('hidden');
            }
            else {
                showResultMessage(resp);
            }
            $btn_search.attr('disabled', false);
            $btn_search.find('span').html('Search');
        },
        error: function (resp) {
            showResultMessage(resp);
            $btn.attr('disabled', false);
            $btn.find('span').html('Search');
        }
    })
})