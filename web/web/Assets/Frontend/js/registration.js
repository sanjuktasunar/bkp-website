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
    var $_data = formDataValidation_GetUrlDatas(i+1);
    if (!$_data.valid)
        return false;

    $.ajax({
        type: "post",
        url: $_data.url,
        data: $_data.data,
        success: function (resp) {
            if (resp.messageType == 'success') {
                allNextBtn_Click(i);
                i++;
            }
            else {
                showMessage_Only(resp);
            }
        },
        error: function (resp) {
            showMessage_Only(resp);
        }
    })
    //allNextBtn_Click(i);
    //i++;
})

$('.prev-btn').on('click', function (event) {
    event.preventDefault();
    i--;
    allPrevBtn_Click(i);
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
        var valid2 = elementValidation($("#MiddleName"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        var valid3 = elementValidation($("#LastName"), { isRequired: true, isMaxLength: true, maxLength: 40 });
        var valid4 = elementValidation($("#DateOfBirthBS"), { isRequired: true, isMaxLength: true, maxLength: 10 });
        var valid5 = elementValidation($("#GenderId"), { isRequired: true });
        var valid6 = elementValidation($("#MobileNumber"), { isRequired: true, isMaxLength: true, maxLength: 15 });
        var valid7 = elementValidation($("#Email"), { isRequired: true, isMaxLength: true, maxLength: 150 });
        var valid8 = elementValidation($("#MaritalStatusId"), { isRequired: true });

        if (valid1 && valid2 && valid3 && valid4 && valid5 && valid6 && valid7 && valid8)
            valid = true;
        url = "/Register/SaveStep2Data";
        data = $("#form_step_2").serialize();
        //valid = true;
    }
    //else if (index == 3) {
    //    valid = true;
    //}
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
        AddErrorMessage(element,
            $el.attr('name') + ' is required ')
        return false;
    }
    return true;
}
function maxlengthHandling(element, length) {
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
function errorMessageHandling() {
    $('input').on('keyup', function () {
        var $parent = $(this).parent();
        var element = $(this);
        if ($parent.attr('class').includes('error')) {
            if (this.value.length > 0) {
                removeErrorClasses(element);
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

$(document).ready(function () {
    errorMessageHandling();
})