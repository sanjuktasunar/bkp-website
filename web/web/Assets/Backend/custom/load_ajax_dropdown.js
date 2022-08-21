
function LoadShareholder(id, element = null) {
    if (element == null || element == '')
        element = "#ShareholderId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "shareholder_List"
    };
    LoadAjax($ajax_param);
}

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

function LoadRole(id, element = null) {
    if (element == null || element == '')
        element = "#RoleId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "role_list"
    };
    LoadAjax($ajax_param);
}

function LoadUserType(id, element = null) {
    if (element == null || element == '')
        element = "#UserTypeId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "user_type_list"
    };
    LoadAjax($ajax_param);
}

function LoadUserTypeForUserList(id, element = null) {
    if (element == null || element == '')
        element = "#UserTypeId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "user_type_for_user_list"
    };
    LoadAjax($ajax_param);
}

function LoadUserStatus(id, element = null) {
    if (element == null || element == '')
        element = "#UserStatusId";

    var $ajax_param = {
        element: element,
        id: id,
        type: "user_status_list"
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

function LoadShareTypesWithDetails(id, element = null,$element_price = null) {
    if (element == null || element == '')
        element = "#ShareTypeId";

    if ($element_price == null || $element_price == '')
        $element_price = "#SharePricePerKitta";

    var $ajax_param = {
        element: element,
        id: id,
        type: "share_type_with_detail_list"
    };
    $.ajax({
        type: "post",
        url: "/Ajax/Index",
        data: $ajax_param,
        success: function (resp) {
            debugger;
            $($ajax_param.element).empty();
            $.each(resp, function (index, row) {
                if (index == 0 && id == 0) {
                    $($element_price).val(parseFloat(row.Value1).toFixed(2));
                }

                if ($ajax_param.id == row.Key) {
                    $($ajax_param.element).append("<option value=" + row.Key + " selected='selected'> " + row.Value + " </option>");
                    $($element_price).val(parseFloat(row.Value1).toFixed(2));
                }
                else {
                    $($ajax_param.element).append("<option value=" + row.Key + "> " + row.Value + " </option>");
                }
                
            })
            $share_tye_list = resp;
        }
    })
}

function LoadIsPrimaryShareTypesWithDetails(id, element = null, $element_price = null) {
    if (element == null || element == '')
        element = "#ShareTypeId";

    if ($element_price == null || $element_price == '')
        $element_price = "#SharePricePerKitta";

    var $ajax_param = {
        element: element,
        id: id,
        type: "is_primary_share_type_detail_list"
    };
    $.ajax({
        type: "post",
        url: "/Ajax/Index",
        data: $ajax_param,
        success: function (resp) {
            debugger;
            $($ajax_param.element).empty();
            $.each(resp, function (index, row) {
                if (index == 0 && id == 0) {
                    $($element_price).val(parseFloat(row.Value1).toFixed(2));
                }

                if ($ajax_param.id == row.Key) {
                    $($ajax_param.element).append("<option value=" + row.Key + " selected='selected'> " + row.Value + " </option>");
                    $($element_price).val(parseFloat(row.Value1).toFixed(2));
                }
                else {
                    $($ajax_param.element).append("<option value=" + row.Key + "> " + row.Value + " </option>");
                }

            })
            $share_tye_list = resp;
        }
    })
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