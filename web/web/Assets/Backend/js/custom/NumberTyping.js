
function AllowNumberOnly(event) {
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}

function AllowPositiveNumber(event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}
