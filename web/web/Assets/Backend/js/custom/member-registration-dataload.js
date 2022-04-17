

function loadFieldData() {
    var memberId = $("#MemberId").val();
    if (parseInt(memberId) > 0) {
        $.ajax({
            type: 'get',
            url: '/MemberRegister/GetMemberById',
            data: { id: memberId },
            success: function (res) {
                setFieldValues(res);
            }
        })
    }
}

function setFieldValues(res) {
    if (res != null) {
        $("#MemberId").val(res.MemberId);
        $("#FirstName").val(res.FirstName);
        $("#MiddleName").val(res.MiddleName);
        $("#LastName").val(res.LastName);
        $("#CitizenshipNumber").val(res.CitizenshipNumber);
        $("#GenderId").val(res.GenderId);
        $("#DateOfBirthBS").val(res.DateOfBirthBS);
        $("#MobileNumber").val(res.MobileNumber);
        $("#Email").val(res.Email);

        //$("#OccupationId").val(res.OccupationId);
        //if (res.OccupationId == 4) {
        //    $("#OtherOccupationRemarks").val(res.OtherOccupationRemarks);
        //    $("#occupationDiv").show();
        //}
        //else {
        //    $("#OtherOccupationRemarks").val('');
        //    $("#occupationDiv").hide();
        //}
        //$("#MemberFieldId").val(res.MemberFieldId);
    }
}

function loadOccupation() {
    var memberId = $("#MemberId").val();
    $.ajax({
        type: 'get',
        url: '/MemberRegister/GetMemberById',
        data: { id: memberId },
        success: function (res) {
            $("#OccupationId").val(res.OccupationId);
            if (res.OccupationId == 8) {
                $("#OtherOccupationRemarks").val(res.OtherOccupationRemarks);
                $("#occupationDiv").show();
            }
            else {
                $("#OtherOccupationRemarks").val('');
                $("#occupationDiv").hide();
            }
            $("#MemberFieldId").val(res.MemberFieldId);
        }
    })
}
function loadData(current) {
    if (current != 1) {
        $("#SearchDiv").hide();
    }
    if (current == 1) {
        loadGenderList();
        $("#SearchDiv").show();
    }
    if (current == 2) {
        loadOutsideCountryList();
        loadMunicipalityTypeList();

        //loadProvinceList();
        //loadOccupationList();
        //loadMemberFieldList();
        //loadAddress();
    }
    if (current == 3) {
        loadProvinceList();
        loadOccupationList();
        loadMemberFieldList();
        loadAddress();

        //$('#occupationDiv').hide();
        //loadOccupation();
    }
    if (current == 4) {
        $('#occupationDiv').hide();
        loadOccupation();
    }
    if (current == 5) {
        loadMemberDocument();

    }
    if (current == 6) {
        loadBankDeposit();
    }
    if (current == 7) {
        
        //$(".progress").hide();
        //SendEmail();
    }
  
}
function SendEmail() {
    $.ajax({
        type: 'post',
        url: '/MemberRegister/SendConfirmEmail',
        data: { memberId: $("#MemberId").val() },
        success: function (resp) {
          
        },
        error: function (err) {

        }
    })
}
function loadMemberDocument() {
    $.ajax({
        type: 'get',
        url: '/MemberRegister/GetMemberDocuments',
        data: { memberId: $("#MemberId").val() },
        success: function (resp) {
            $("#CitizenshipFront").val(resp.CitizenshipFront);
            $("#CitizenshipBack").val(resp.CitizenshipBack);
            $("#MemberPhoto").val(resp.Photo);
            DisplayImage('MemberPhotoString', resp.Photo)
            DisplayImage('CitizenshipFrontImageString', resp.CitizenshipFront)
            DisplayImage('CitizenshipBackImageString', resp.CitizenshipBack)
        },
        error: function (err) {

        }
    })
}
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


function DisplayImage(elementName, imageName) {
    var divId = "#Div" + elementName;
    if (imageName.length > 0) {
        $(divId).show();
        $(divId).html('');
        $(divId).append('<img src="ImageStorage/' + imageName + '" />')
        //$(divId).append("<img src='/ImageStorage/'" + imageName + ">")
        //$('<img>', {
        //    src: imageString
        //}).appendTo($(divId));
    }
}
//data load for step1 starts
function loadGenderList() {
    var gender = $("#GenderId").val();
    if (gender.length == 0) {
        $.ajax({
            url: '/Ajax/GetGender',
            type: 'get',
            async: true,
            cache: false,
            processData: false,
            contentType: 'application/json',
            success: function (response) {
                $('#GenderId').empty();
                $.each(response, function (index, row) {
                    $('#GenderId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                });
            },
            error: function (error) {
            }
        });
    }

}
//data load for step1 ends

//data load for step3 starts
function loadProvinceList() {
    var permanentProvince = $("#PermanentProvinceId").val();
    var tempProvince = $("#TemporaryProvinceId").val();
    if (permanentProvince.length == 0 || tempProvince.length == 0) {
        $.ajax({
            url: '/Ajax/GetProvince',
            type: 'get',
            async: true,
            cache: false,
            processData: false,
            contentType: 'application/json',
            success: function (response) {
                if (permanentProvince.length == 0) {
                    $('#PermanentProvinceId').empty();
                    $('#PermanentProvinceId').append('<option value="">Select Province</option>');
                    $.each(response, function (index, row) {
                        $('#PermanentProvinceId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    });
                }
                if (tempProvince.length == 0) {
                    $('#TemporaryProvinceId').empty();
                    $('#TemporaryProvinceId').append('<option value="">Select Province</option>');
                    $.each(response, function (index, row) {
                        $('#TemporaryProvinceId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    });
                }
            },
            error: function (error) {
                alert(error.statusText);
            }
        });
    }
}
async function loadMunicipalityTypeList() {
    var permanentMunicipalityType = $("#PermanentMunicipalityTypeId").val();
    var tempMunicipalityType = $("#TemporaryMunicipalityTypeId").val();
    if (permanentMunicipalityType.length == 0 || tempMunicipalityType.length == 0) {
        await $.ajax({
            url: '/Ajax/GetMunicipalityType',
            type: 'get',
            async: true,
            cache: false,
            processData: false,
            contentType: 'application/json',
            success: function (response) {
                if (permanentMunicipalityType.length == 0) {
                    $('#PermanentMunicipalityTypeId').empty();
                    $('#PermanentMunicipalityTypeId').append('<option value="">Select Type</option>');
                    $.each(response, function (index, row) {
                        $('#PermanentMunicipalityTypeId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    });
                }
                if (tempMunicipalityType.length == 0) {
                    $('#TemporaryMunicipalityTypeId').empty();
                    $('#TemporaryMunicipalityTypeId').append('<option value="">Select Type</option>');
                    $.each(response, function (index, row) {
                        $('#TemporaryMunicipalityTypeId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    });
                }
            },
            error: function (error) {

            }
        });
    }
}
function loadOutsideCountryList() {
    var permanentCountry = $("#PermanentCountryId").val();
    var tempCountry = $("#TemporaryCountryId").val();
    if (permanentCountry.length == 0 || tempCountry.length == 0) {
        $.ajax({
            url: '/Ajax/GetOutsideCountry',
            type: 'get',
            async: true,
            cache: false,
            processData: false,
            contentType: 'application/json',
            success: function (response) {
                if (permanentCountry.length == 0) {
                    $('#PermanentCountryId').empty();
                    $('#PermanentCountryId').append('<option value="">Select Country</option>');
                    $.each(response, function (index, row) {
                        $('#PermanentCountryId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    });
                }
                if (tempCountry.length == 0) {
                    $('#TemporaryCountryId').empty();
                    $('#TemporaryCountryId').append('<option value="">Select Country</option>');
                    $.each(response, function (index, row) {
                        $('#TemporaryCountryId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    });
                }
            },
            error: function (error) {
                //alert(error.statusText);
            }
        });
    }

}
$('.province').change(function () {
    var provinceId = $(this).val();
    var idName = $(this).attr('id');
    loadDistrictList(provinceId, idName);
});
function loadDistrictList(provinceId, idName, PermanentDistrictIdValue = null, TemporaryDistrictIdValue = null) {
    $.ajax({
        url: '/Ajax/GetDistrictByProvinceId',
        data: { provinceId: provinceId },
        type: 'get',
        success: function (response) {
            if (idName.toLowerCase() == 'permanentprovinceid') {
                $('#PermanentDistrictId').empty();
                $('#PermanentDistrictId').append('<option value="">Select District</option>');
                $.each(response, function (index, row) {
                    console.log(row);
                    $('#PermanentDistrictId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                });
                if (PermanentDistrictIdValue != null) {
                    $('#PermanentDistrictId').val(PermanentDistrictIdValue)
                }
            }
            if (idName.toLowerCase() == 'temporaryprovinceid') {
                $('#TemporaryDistrictId').empty();
                $('#TemporaryDistrictId').append('<option value="">Select District</option>');
                $.each(response, function (index, row) {
                    console.log(row);
                    $('#TemporaryDistrictId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                });
                if (TemporaryDistrictIdValue != null) {
                    $('#TemporaryDistrictId').val(TemporaryDistrictIdValue)
                }
            }
        },
        error: function (error) {
            alert(error.statusText);
        }
    });
}
function clearPermanentInsideAddressData() {
    $("#PermanentProvinceId").val($("#PermanentProvinceId option:first").val());
    $("#PermanentProvinceId").parent().addClass('form-group').removeClass('error');

    $('#PermanentDistrictId').empty();
    $('#PermanentDistrictId').parent().addClass('form-group').removeClass('error');
    $('#PermanentDistrictId').append('<option value=""> Select District </option>')

    $("#PermanentMunicipalityTypeId").val($("#PermanentMunicipalityTypeId option:first").val());
    $("#PermanentMunicipalityTypeId").parent().addClass('form-group').removeClass('error');

    $("input[name='PermanentMunicipality']").val('');
    $("input[name='PermanentMunicipality']").parent().addClass('form-group').removeClass('error');

    $("input[name='PermanentWardNumber']").val('');
    $("input[name='PermanentWardNumber']").parent().addClass('form-group').removeClass('error');

    $("input[name='PermanentToleName']").val('');
    $("input[name='PermanentToleName']").parent().addClass('form-group').removeClass('error');
}
function clearPermanentOutsideAddressData() {
    $("#PermanentCountryId").val($("#PermanentCountryId option:first").val());
    $("#PermanentCountryId").parent().addClass('form-group').removeClass('error');

    $("input[name='PermanentAddress']").val('');
    $("input[name='PermanentAddress']").parent().addClass('form-group').removeClass('error');
}
function clearTempInsideAddressData() {
    $("#TemporaryProvinceId").val($("#TemporaryProvinceId option:first").val());
    $("#TemporaryProvinceId").parent().addClass('form-group').removeClass('error');

    $('#TemporaryDistrictId').empty();
    $('#TemporaryDistrictId').parent().addClass('form-group').removeClass('error');
    $('#TemporaryDistrictId').append('<option value=""> Select District </option>')

    $("#TemporaryMunicipalityTypeId").val($("#TemporaryMunicipalityTypeId option:first").val());
    $("#TemporaryMunicipalityTypeId").parent().addClass('form-group').removeClass('error');

    $("input[name='TemporaryMunicipality']").val('');
    $("input[name='TemporaryMunicipality']").parent().addClass('form-group').removeClass('error');

    $("input[name='TemporaryWardNumber']").val('');
    $("input[name='TemporaryWardNumber']").parent().addClass('form-group').removeClass('error');

    $("input[name='TemporaryToleName']").val('');
    $("input[name='TemporaryToleName']").parent().addClass('form-group').removeClass('error');
}
function clearTempOutsideAddressData() {
    $("#TemporaryCountryId").val($("#TemporaryCountryId option:first").val());
    $("#TemporaryCountryId").parent().addClass('form-group').removeClass('error');

    $("input[name='TemporaryAddress']").val('');
    $("input[name='TemporaryAddress']").parent().addClass('form-group').removeClass('error');
}
$("input[type='radio']").click(function () {
    var id = $(this).attr('id');
    if (id.toLowerCase() == "permanentradio") {
        var checked = $("input[name='PermanentIsOutsideNepal']:checked").val();
        if (checked == "true") {
            clearPermanentInsideAddressData();
            $("#permanentInsideNepalAddress").hide();
            $("#permanentOutsideNepalAddress").show();
            //loadOutsideCountryList();
        }
        else {
            clearPermanentOutsideAddressData();
            $("#permanentInsideNepalAddress").show();
            $("#permanentOutsideNepalAddress").hide();
        }
    }

    else if (id.toLowerCase() == "tempradio") {
        var checked = $("input[name='TemporaryIsOutsideNepal']:checked").val();
        if (checked == "true") {
            clearTempInsideAddressData();
            $("#tempInsideNepalAddress").hide();
            $("#tempOutsideNepalAddress").show();
            //loadOutsideCountryList();
        }
        else {
            clearTempOutsideAddressData();
            $("#tempInsideNepalAddress").show();
            $("#tempOutsideNepalAddress").hide();
        }
    }
})
function loadAddress() {
    var memberId = $("#MemberId").val();
    $.ajax({
        type: 'get',
        url: '/MemberRegister/GetMemberAddress',
        data: { memberId: memberId },
        success: function (response) {
            if (response != null) {
                setMemberAddress(response);
            }
        }
    })
}
function setMemberAddress(resp) {
    $("input[name=PermanentIsOutsideNepal][value=" + resp.PermanentIsOutsideNepal + "]").prop('checked', true);
    $("input[name=TemporaryIsOutsideNepal][value=" + resp.TemporaryIsOutsideNepal + "]").prop('checked', true);

    if (resp.PermanentIsOutsideNepal == true) {
        $("#permanentOutsideNepalAddress").show();
        $("#permanentInsideNepalAddress").hide();

        $("#PermanentCountryId").val(resp.PermanentCountryId);
        $("#PermanentAddress").val(resp.PermanentAddress);
    }
    else {
        $("#permanentOutsideNepalAddress").hide();
        $("#permanentInsideNepalAddress").show();

        $("#PermanentProvinceId").val(resp.PermanentProvinceId);
        loadDistrictList(resp.PermanentProvinceId, 'permanentprovinceid', resp.PermanentDistrictId);
        $("#PermanentMunicipalityTypeId").val(resp.PermanentMunicipalityTypeId);
        $("#PermanentMunicipality").val(resp.PermanentMunicipality);
        $("#PermanentWardNumber").val(resp.PermanentWardNumber);
        $("#PermanentToleName").val(resp.PermanentToleName);

    }

    if (resp.TemporaryIsOutsideNepal == true) {
        $("#tempInsideNepalAddress").hide();
        $("#tempOutsideNepalAddress").show();

        $("#TemporaryCountryId").val(resp.TemporaryCountryId);
        $("#TemporaryAddress").val(resp.TemporaryAddress);
    }
    else {
        $("#tempInsideNepalAddress").show();
        $("#tempOutsideNepalAddress").hide();

        $("#TemporaryProvinceId").val(resp.TemporaryProvinceId);
        loadDistrictList(resp.TemporaryProvinceId, 'temporaryprovinceid', null, resp.TemporaryDistrictId);

        $("#TemporaryMunicipalityTypeId").val(resp.TemporaryMunicipalityTypeId);
        $("#TemporaryMunicipality").val(resp.TemporaryMunicipality);
        $("#TemporaryWardNumber").val(resp.TemporaryWardNumber);
        $("#TemporaryToleName").val(resp.TemporaryToleName);
    }
}
//data load for step3 ends


//data load for step4 starts
function loadMemberFieldList() {
    var memberField = $("#MemberFieldId").val();
    if (memberField.length == 0) {
        $.ajax({
            url: '/Ajax/GetMemberField',
            type: 'get',
            async: true,
            cache: false,
            processData: false,
            contentType: 'application/json',
            success: function (response) {
                $('#MemberFieldId').empty();
                $('#MemberFieldId').append('<option value="">Interested Field</option>');
                $.each(response, function (index, row) {
                    $('#MemberFieldId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                });
            },
            error: function (error) {
            }
        });
    }
}
function loadOccupationList() {
    var occupation = $("#OccupationId").val();
    if (occupation.length == 0) {
        $.ajax({
            url: '/Ajax/GetOccupation',
            type: 'get',
            async: true,
            cache: false,
            processData: false,
            contentType: 'application/json',
            success: function (response) {
                debugger;
                $('#OccupationId').empty();
                $('#OccupationId').append('<option value="">Select Occupation</option>');
                $.each(response, function (index, row) {
                    console.log(row);
                    $('#OccupationId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                });
            },
            error: function (error) {

            }
        });
    }
}
$("#OccupationId").change(function () {
    var data = $("#OccupationId option:selected").text();
    if (data.toLowerCase() == "other") {
        $('#occupationDiv').show();
    }
    else {
        $('#OtherOccupationRemarks').val('');
        $('#occupationDiv').hide();
    }
})
 //data load for step4 ends

//data load for step6 starts
function loadBankDeposit() {
    $.ajax({
        type: 'get',
        url: '/MemberRegister/GetBankDeposit',
        data: { memberId: $("#MemberId").val() },
        success: function (resp) {
            $("#Amount").val(resp.Amount);
            $("#VoucherImage").val(resp.VoucherImage);
            $("#VoucherImage").val(resp.VoucherImage);
            $("#ReferalCode").val(resp.ReferenceReferalCode);

            DisplayImage('VoucherImageString', resp.VoucherImage)
        }
    })
}
//data load for step6 ends