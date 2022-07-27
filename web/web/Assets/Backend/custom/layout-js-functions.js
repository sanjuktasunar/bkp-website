
$(document).ready(function () {
    PageLoad_Nav_Active_Inactive();
})

function PageLoad_Nav_Active_Inactive() {
    var nav_Name = window.location.pathname.trim()
        .replace('/', '')
        .replace(' ', '')
        .split('/')[0]
        .replace('List', '')
        .replace('Add', '')
        .replace('Modify', '')
        .replace('Details', '')
        .replace('AfterApprove', '');

    if (nav_Name == "AssignMenuAccess_Role") {
        nav_Name = "Role";
    }
    else if (nav_Name == "ChangePassword") {
        nav_Name = "User";
    }
    var $a_tag = "#" + nav_Name;
    $($a_tag).addClass('active');
    var parentDiv = $($a_tag).parents('.collapse-inner').parents('.collapse');
    $(parentDiv).addClass('show');
}

$(".ClearAllCache").click(function (event) {
    event.preventDefault();
    $.ajax({
        type: 'post',
        url: '/User/ClearAllCache',
        success: function (resp) {
            showResultMessage(resp, $(location).attr('href'))
        }
    })
});

/***********************not remove,will useful later*******************/
//function PageLoad_Nav_Active_Inactive() {
//    var pageName = window.location.pathname
//        .replace('/', '');

//    var $a_tag = "";
//    if (pageName.includes('Add') || pageName.includes('Modify')) {
//        var nav_name = pageName.replace('Add', '').replace('Modify', '');
//        pageName = nav_name + 'List';
//        alert(pageName);
//    }
    
//    var $a_tag = $("a[href$='" + pageName + "']");
//    $a_tag.addClass('active');
//    var parentDiv = $($a_tag).parents('.collapse-inner').parents('.collapse');
//    $(parentDiv).addClass('show');
//}


