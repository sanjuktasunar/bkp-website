
function LoadBranchList(id_name, id, IsAdminAccess = null, IsSelectValue = null) {
    if (id == undefined || id == null || id == '')
        id = 0;

    $.ajax({
        type: "get",
        url: "/Ajax/Index?type=branch_List&AdminAccess=" + IsAdminAccess,
        success: function (resp) {
            $(id_name).empty();
            if (IsSelectValue != undefined && IsSelectValue == 1) {
                $(id_name).append("<option value=''> Select Branch </option>")
            }
            $.each(resp, function (i, r) {
                if (r.Key == parseInt(id)) {
                    $(id_name).append("<option value='" + r.Key + "' selected='selected'>" + r.Value + "</option>")
                }
                else {
                    $(id_name).append("<option value='" + r.Key + "'>" + r.Value + "</option>")
                }
            })
        }
    })
}

function LoadBranchList_CheckAccess(isAdmin, id_name, BranchId, UserBranchId) {
    $.ajax({
        type: "get",
        url: "/Ajax/Index?type=branch_List",
        success: function (resp) {
            $(id_name).empty();
            $.each(resp, function (i, r) {
                if (isAdmin == 'True') {
                    if (BranchId == null || BranchId == 0)
                        BranchId = UserBranchId;

                    if (r.Key == parseInt(BranchId)) {
                        $(id_name).append("<option value='" + r.Key + "' selected='selected'>" + r.Value + "</option>")
                    }
                    else {
                        $(id_name).append("<option value='" + r.Key + "'>" + r.Value + "</option>")
                    }
                }
                else {
                    if (r.Key == parseInt(UserBranchId)) {
                        $(id_name).append("<option value='" + r.Key + "' selected='selected'>" + r.Value + "</option>")
                    }
                }
            })
        }
    })
}

function LoadUnitList(id_name, id) {
    LoadAjax('unit_List', id_name, id)
}

function LoadFiscalYearList(id_name, id) {
    LoadAjax('fiscalYear_List', id_name, id, true)
}

function LoadProductList(id_name, id) {
    LoadAjax('product_List', id_name, id)
}

function LoadSupplierList(id_name, id) {
    LoadAjax('supplier_List', id_name, id)
}

function LoadFormStatusList(id_name, id) {
    LoadAjax('form_status_List', id_name, id)
}

function LoadUserStatusList(id_name, id) {
    LoadAjax('user_status_List', id_name, id)
}

function LoadRoleList(id_name, id) {
    LoadAjax('role_List', id_name, id)
}

function LoadMenuList(id_name, id) {
    LoadAjax('menu_List', id_name, id)
}

function LoadParentCategoryList(id_name, id) {
    LoadAjax('parent_Category_List', id_name, id)
}

function LoadAccountHeadList(id_name, id) {
    LoadAjax('account_Head_List', id_name, id)
}

function LoadChequeIssueCategoryList(id_name, id) {
    LoadAjax('cheque_Issue_Category_List', id_name, id);
}

function LoadChequeReceiveCategoryList(id_name, id) {
    LoadAjax('cheque_Receive_Category_List', id_name, id);
}

function LoadNepaliMonthList(id_name, id) {
    LoadAjax('nepali_Month_List', id_name, id);
}

function LoadAllBranchList(id_name, id) {
    LoadAjax('all_Branch_List', id_name, id);
}

function LoadAjax(type, element_name, id,notShow_FirstIndex=null) {
    if (id == undefined || id == null || id == '')
        id = 0;

    $.ajax({
        type: "get",
        url: "/Ajax/Index?type=" + type,
        success: function (resp) {
            $(element_name).empty();
            if (notShow_FirstIndex != true) {
                $(element_name).append("<option value=''>Select Value</option>");
            }
            $.each(resp, function (i, r) {
                if (r.Key == parseInt(id)) {
                    $(element_name).append("<option value='" + r.Key + "' selected='selected'>" + r.Value + "</option>")
                }
                else {
                    $(element_name).append("<option value='" + r.Key + "'>" + r.Value + "</option>")
                }

            })
        }
    })
}

function LoadCustomerList(element_name, id,branchId) {
    if (id == undefined || id == null || id == '')
        id = 0;

    $.ajax({
        type: "get",
        url: "/Ajax/LoadCustomerList",
        data: { BranchId: branchId },
        success: function (resp) {
            $(element_name).empty();
            $(element_name).append("<option value=''>Select Customer</option>");
            $.each(resp, function (i, r) {
                if (r.Key == parseInt(id)) {
                    $(element_name).append("<option value='" + r.Key + "' selected='selected'>" + r.Value + "</option>")
                }
                else {
                    $(element_name).append("<option value='" + r.Key + "'>" + r.Value + "</option>")
                }

            })
        }
    })
}
