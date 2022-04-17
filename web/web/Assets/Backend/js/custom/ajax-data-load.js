

function loadProvince(provinceId, idName) {
    var id = "#" + idName;
    $(id).empty();
    $.ajax({
        url: '/Ajax/GetProvince',
        type: 'get',
        async: true,
        cache: false,
        processData: false,
        contentType: 'application/json',
        success: function (response) {
            $(id).append('<option value="">Select Province</option>');
            $.each(response, function (index, row) {
                if (row.Id == provinceId) {
                    $(id).append('<option value=' + row.Id + ' selected="selected">' + row.Value + '</option>');
                }
                else {
                    $(id).append('<option value=' + row.Id + '>' + row.Value + '</option>');
                }
            });
        },
        error: function (error) {
            alert('something went wrong');
        }
    });
}

function loadDistrict(provinceId, districtId, idName) {
    if (parseInt(provinceId) > 0) {
        var id = "#" + idName;
        $(id).empty();
        $.ajax({
            url: '/Ajax/GetDistrictByProvinceId',
            data: { provinceId: provinceId },
            type: 'get',
            success: function (response) {
                $(id).append('<option value="">Select District</option>');
                $.each(response, function (index, row) {
                    if (row.Id == parseInt(districtId)) {
                        $(id).append('<option value=' + row.Id + ' selected="selected">' + row.Value + '</option>');
                    }
                    else {
                        $(id).append('<option value=' + row.Id + '>' + row.Value + '</option>')
                    }
                });
            },
            error: function (error) {
                alert(error.statusText);
            }
        });
    }
    
}

function loadAbroadCountryList() {
    $.ajax({
        url: '/Ajax/GetOutsideCountry',
        type: 'get',
        async: true,
        cache: false,
        processData: false,
        contentType: 'application/json',
        success: function (response) {
            $('#CountryId').empty();
            $('#CountryId').append('<option value="">Select Country</option>');
            $.each(response, function (index, row) {
                if (row.Id != 1) {
                    $('#CountryId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
                }
            });
        },
        error: function (error) {
            //alert(error.statusText);
        }
    });

}

function loadGenderList() {
    var gender = $("#GenderId").val();
    $.ajax({
        url: '/Ajax/GetGender',
        type: 'get',
        async: true,
        cache: false,
        processData: false,
        contentType: 'application/json',
        success: function (response) {
            $('#GenderId').empty();
            $('#GenderId').append('<option value="">Select Gender</option>');
            $.each(response, function (index, row) {
                $('#GenderId').append('<option value=' + row.Id + '>' + row.Value + '</option>')
            });
        },
        error: function (error) {
        }
    });

}