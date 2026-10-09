function validateLabourRpt() {

    $.ajaxSetup({
        async: false
    });
    var i = 0;
    if (!validateById('#PayrollMonth_Code', 'pay_month')) {
        i++;
    }

    if (i == 0) {

        return true;
    }
    else
        return false;
}