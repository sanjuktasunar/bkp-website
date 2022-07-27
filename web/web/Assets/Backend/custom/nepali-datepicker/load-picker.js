
function getTodayNepaliDate() {
    var obj = NepaliFunctions.GetCurrentBsDate();
    var nepaliMonth = obj.month.toString();
    if (nepaliMonth.length == 1)
        nepaliMonth = "0" + nepaliMonth;

    return (obj.year + '-' + nepaliMonth + '-' + obj.day);
}