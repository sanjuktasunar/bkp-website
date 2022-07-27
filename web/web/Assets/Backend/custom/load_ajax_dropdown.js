
function LoadFiscalYear(id, element = null) {
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

function LoadMaritalStatus(id, element = null) {
    if (element == null || element == '')
        element = "#MaritalStatusId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "marital_Status_List"
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

function LoadDistrict(id, provinceId, element = null) {
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

function LoadShareTypes(id, element = null, not_include = null) {
    if (element == null || element == '')
        element = "#ShareTypeId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "share_type_list"
    };
    LoadAjax($ajax_param, not_include);
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


function LoadOutsideCountry(id, element = null) {
    if (element == null || element == '')
        element = "#OutsideCountryId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "outside_country_List"
    };
    LoadAjax($ajax_param);
}

function LoadReferenceAgent(id, element = null) {
    if (element == null || element == '')
        element = "#ddlAgentId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "reference_Agent_List"
    };
    LoadAjax($ajax_param);
}

function LoadReferenceMember(id, element = null) {
    if (element == null || element == '')
        element = "#ddlMemberId";
    var $ajax_param = {
        element: element,
        id: id,
        type: "reference_Member_List"
    };
    LoadAjax($ajax_param);
}


function LoadAjax($ajax_param, not_include = null) {
    $.ajax({
        type: "post",
        url: "/Ajax/Index",
        data: $ajax_param,
        success: function (resp) {
            $($ajax_param.element).empty();
            if (not_include == null || not_include == false) {
                $($ajax_param.element).append("<option value=''> Select Value </option>");
            }



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