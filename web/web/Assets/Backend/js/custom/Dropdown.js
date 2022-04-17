
function LoadFiscalYear(id,element = null) {
    if (element == null || element == '')
        element = "#FiscalYearId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "fiscal_year_list"
    };
    LoadAjax($ajax_param);
}

function LoadGender(id, element = null) {
    if (element == null || element == '')
        element = "#GenderId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "gender_list"
    };
    LoadAjax($ajax_param);
}

function LoadProvince(id, element = null) {
    if (element == null || element == '')
        element = "#ProvinceId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "province_list"
    };
    LoadAjax($ajax_param);
}

function LoadDistrict(id,provinceId, element = null) {
    if (element == null || element == '')
        element = "#DistrictId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "district_list",
        provinceId: provinceId
    };
    LoadAjax($ajax_param);
}

function LoadOccupation(id, element = null) {
    if (element == null || element == '')
        element = "#OccupationId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "occupation_list"
    };
    LoadAjax($ajax_param);
}

function LoadShareTypes(id, element = null) {
    if (element == null || element == '')
        element = "#ShareTypeId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "share_type_list"
    };
    LoadAjax($ajax_param);
}

function LoadAgentStatus(id, element = null) {
    if (element == null || element == '')
        element = "#AgentStatusId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "agent_Status_list"
    };
    LoadAjax($ajax_param);
}

function LoadAccountHead(id, element = null) {
    if (element == null || element == '')
        element = "#AccountHeadId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "account_Head_list"
    };
    LoadAjax($ajax_param);
}

function LoadAjax($ajax_param) {
    $.ajax({
        url: "/Ajax/Index",
        data: $ajax_param,
        type: "post",
        success: function (resp) {
            $($ajax_param.element).empty();
            $($ajax_param.element).append("<option value=''> Select Value </option>")
            $.each(resp, function (index, row) {
                if ($ajax_param.id == row.Key) {
                    $($ajax_param.element).append("<option value=" + row.Key + " selected='selected'> " + row.Value + " </option>");
                }
                else {
                    $($ajax_param.element).append("<option value=" + row.Key + "> " + row.Value + " </option>");
                }
            })
        }
    })
}