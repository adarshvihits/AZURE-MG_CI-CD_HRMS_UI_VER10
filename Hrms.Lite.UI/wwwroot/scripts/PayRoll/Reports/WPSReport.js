function validatefun() {
    debugger;

    if (validateById("#PayrollMonth_Code", "pay_month")) {     
        return true;
    }
    else {
         document.getElementById("PayrollMonth_Code").focus();    
        return false;
    }
}