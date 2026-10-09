
function AddCertification() {
    debugger

    var count = 0;
    count = $("div[class*='Rdcertifn']").length;

    var template = $('#certification_div_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);


        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
              onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
        /*    onchange = onchange.replace(/\,0\)/g, ',' + count + ')');*/
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('.reneweldate'), function () {

        $(this).attr("id", "reneweldate_" + count);
    });
  
    $.each(template.find('.rd_Cer_Span_Certi'), function () {

        $(this).attr("id", "Certificate_" + count);
    });
    $.each(template.find('.rd_Cer_Span_Certitype'), function () {

        $(this).attr("id", "CertificateType_" + count);
    });
    $.each(template.find('.rd_Cer_Span_issue'), function () {

        $(this).attr("id", "IssuedBy_" + count);
    });
    $.each(template.find('.rd_Cer_Span_issueDate'), function () {

        $(this).attr("id", "IssuedDate_" + count);
    });
    $.each(template.find('.rd_Cer_Span_mode'), function () {

        $(this).attr("id", "CertificateMode_" + count);
    });


    $('.cerfication_outer').append('<div  class ="Rdcertifn" id="certification_div_' + count + '">' + template.html() + '  </div>');
    /*    $('<div  class ="step-left Rdcertifn" id="certification_div_' + count + '">' + template.html() + '  </div>').insertBefore('.cerfication_outer');*/
    //------------to clear when new row---------
    $('.kt_datepicker_2, .kt_datepicker_2_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",


        format: "dd-M-yyyy",
        orientation: "auto",

        clearBtn: true
    });
    $('#certification_div_' + count).find("input[type=text]").val('');
    $('#certification_div_' + count).find("textarea").val('');
    $('#certification_div_' + count).find("select").val('');

}

function AddSkill() {
    debugger
    var count = 0;
    count = $("div[class*='Rdskill']").length;

    var template = $('#skill_div_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);


        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
      /*      onchange = onchange.replace(/\(0\)/g, '(' + count + ')');*/
                onchange = onchange.replace(/\,0\)/g, ',' + count + ')');
     
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('.rd_Skill_Span_skType'), function () {

        $(this).attr("id", "skilType_" + count);
    });
    $.each(template.find('.rd_Skill_Span_sk'), function () {

        $(this).attr("id", "skill_" + count);

    });
    $.each(template.find('.rd_Skill_Span_skprof'), function () {

        $(this).attr("id", "skilProf_" + count);

    });

    $('.skill_Outer').append('<div  class ="Rdskill" id="skill_div_' + count + '">' + template.html() + '  </div>');

    //------------to clear when new row---------
    $('#skill_div_' + count).find("input[type=text]").val('');
    $('#skill_div_' + count).find("textarea").val('');
    $('#skill_div_' + count).find("select").val('');

}
function AddExp() {

    debugger
    var count = 0;
    count = $("div[class*='RdExp']").length;
    
        var template = $('#experience_div_0').clone();// clone the row
        $.each(template.find('input, textarea'), function () {
            var name = $(this).attr('name');
            if (typeof name != 'undefined') {
                name = name.replace(/\[0\]./g, '[' + count + '].');
                $(this).attr('name', name);
            }

            var id = $(this).attr('id');
            if (typeof id != 'undefined') {
                id = id.replace(/_0__/g, '_' + count + '__');
                $(this).attr('id', id);
            }
            //---------------------------------For row Increment When onkeyup
            var onkeyup = $(this).attr('onkeyup');
            if (onkeyup !== undefined) {
                onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
                $(this).attr('onkeyup', onkeyup);
            }

            //---------------------------------------For row Increment When onchange
            var onchange = $(this).attr('onchange');
            debugger
            if (onchange !== undefined) {
                /*onchange = onchange.replace(/\(0\)/g, '(' + count + ')');*/
                      onchange = onchange.replace(/\,0\)/g, ','+count+')'); 
             
                $(this).attr('onchange', onchange);
            }
            //---------------------------------------
        });
        $.each(template.find('select'), function () {
            var name = $(this).attr('name');
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);

            var id = $(this).attr('id');
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);


            //---------------------------------------For row Increment When onchange
            var onchange = $(this).attr('onchange');
            if (onchange !== undefined) {
                onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                $(this).attr('onchange', onchange);
            }
            //---------------------------------------
        });

        $.each(template.find('a'), function () {
            var onclick = $(this).attr('onclick');
            onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onclick', onclick);
        });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });    
     
    $.each(template.find('.rd_Exp_Span_cmpany'), function () {

        $(this).attr("id", "rd_CompanyName_" + count);
    });
    $.each(template.find('.rd_Exp_Span_Loc'), function () {

        $(this).attr("id", "rd_Location_" + count);

    });
    $.each(template.find('.rd_Exp_Span_Des'), function () {

        $(this).attr("id", "rd_Designation_" + count);

    });
    $.each(template.find('.rd_Exp_Span_fr'), function () {

        $(this).attr("id", "rd_From_" + count);

    });
    $.each(template.find('.rd_Exp_Span_to'), function () {

        $(this).attr("id", "rd_To_" + count);

    });


        $('.experience_Outer').append('<div  class ="RdExp" id="experience_div_' + count + '">' + template.html() + '  </div>');

        //------------to clear when new row---------
        $('.kt_datepicker_2, .kt_datepicker_2_validate').datepicker({
            rtl: KTUtil.isRTL(),
            todayHighlight: true,
            orientation: "bottom left",


            format: "dd-M-yyyy",
            orientation: "auto",
            clearBtn: true
        });
        $('#experience_div_' + count).find("input[type=text]").val('');
        $('#experience_div_' + count).find("textarea").val('');
        $('#experience_div_' + count).find("select").val('');  
}


function AddEDucation() {
    debugger
    var count = 0;
    count = $("div[class*='Rdeducation']").length;

    var template = $('#education_div_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
        /*    onchange = onchange.replace(/\(0\)/g, '(' + count + ')');*/
            onchange = onchange.replace(/\,0\)/g, ',' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);


        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            debugger
       /*     onchange = onchange.replace(/\(0\)/g, '(' + count + ')');*/
                onchange = onchange.replace(/\,0\)/g, ',' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });
    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('.rd_Edu_Span_Qual'), function () {

        $(this).attr("id", "rd_Qualificn_" + count);
    });
    $.each(template.find('.rd_Edu_Span_Qualtype'), function () {

        $(this).attr("id", "rd_Qualtype_" + count);

    });
    $.each(template.find('.rd_Edu_Span_spec'), function () {

        $(this).attr("id", "rd_Spec_" + count);

    });
    $.each(template.find('.rd_Edu_Span_yr'), function () {

        $(this).attr("id", "rd_Yr_" + count);

    });   
    $('.education_Outer').append('<div  class ="Rdeducation" id="education_div_' + count + '">' + template.html() + '  </div>');

    //------------to clear when new row-----

    $(".YrCompltn").datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years",
        autoclose: true
    });

    $('#education_div_' + count).find("input[type=text]").val('');
    $('#education_div_' + count).find("textarea").val('');
    $('#education_div_' + count).find("select").val('');
    // $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
    //$('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
    //$('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');


}

function DeleteRwSkill(index) {
    debugger;
 
    if (confirm("Do you want to delete..?")) {
        var count = 0;
        count = $("div[class*='Rdskill']").length;
        if (count > 1) {
            $("#skill_div_" + index).remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#skill_div_'+ j).clone(); // clone the row
                var skill = $("#Skill_" + j + "__Skill_Code").val();
                
                var type = $('#Skill_' + j + '__SkillType_Code').val();
                
                var prof = $('#Skill_' + j + '__SkillProficiency_Code').val();

                $.each(template.find('input, textarea'), function () {
                    var name = $(this).attr('name');
                    if (typeof name != 'undefined') {
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                    }
                    var id = $(this).attr('id');
                    if (typeof id != 'undefined') {
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                    }
                    //---------------------------------For row Increment When onkeyup
                    var onkeyup = $(this).attr('onkeyup');
                    if (onkeyup !== undefined) {
                        onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onkeyup', onkeyup);
                    }
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('select'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('name', name);
                    var id = $(this).attr('id');
                    id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('id', id);
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('input[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                    $(this).attr('onclick', onclick);
                });
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});
              



                $("#skill_div_" + j).html(template.html());
                $('#skill_div_' + j).attr('id', 'skill_div_' + (j - 1));

                $("#Skill_" + (j-1) + "__Skill_Code").val(skill);

                $('#Skill_' + (j - 1) + '__SkillType_Code').val(type);

                $('#Skill_' + (j - 1)  + '__SkillProficiency_Code').val(prof);

            }
        }
        else {
            $("#Skill_0__Skill_Code").val(skill);

            $('#Skill_0__SkillType_Code').val(type);

            $('#Skill_0__SkillProficiency_Code').val(prof);
        }
    }
}

function DeleteRwExp(index)
{
    debugger

    if (confirm("Do you want to delete..?")) {
        var count = 0;
        count = $("div[class*='RdExp']").length;
        if (count > 1) {
            $("#experience_div_" + index).remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#experience_div_' + j).clone(); // clone the row
                var comp = $("#Experience_" + j + "__CompanyName").val();

                var loc = $('#Experience_' + j + '__Location').val();

                var desg = $('#Experience_' + j + '__Designation').val();
                var from = $('#Experience_' + j + '__From').val();
                var to = $('#Experience_' + j + '__To').val();
                var exp = $('#Experience_' + j + '__Experience').val();

                $.each(template.find('input, textarea'), function () {
                    var name = $(this).attr('name');
                    if (typeof name != 'undefined') {
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                    }
                    var id = $(this).attr('id');
                    if (typeof id != 'undefined') {
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                    }
                    //---------------------------------For row Increment When onkeyup
                    var onkeyup = $(this).attr('onkeyup');
                    if (onkeyup !== undefined) {
                        onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onkeyup', onkeyup);
                    }
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        //onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        onchange = onchange.replace(',' + (j) , ',' + (j - 1));
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('select'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('name', name);
                    var id = $(this).attr('id');
                    id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('id', id);
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('a[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                    $(this).attr('onclick', onclick);
                });
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});




                $("#experience_div_" + j).html(template.html());
                $('#experience_div_' + j).attr('id', 'experience_div_' + (j - 1));
              
               
             

                $("#Experience_" + (j-1) + "__CompanyName").val(comp);

                $('#Experience_' + (j - 1) + '__Location').val(loc);
                $('#Experience_' + (j - 1) + '__Designation').val(desg);
                $('#Experience_' + (j - 1) + '__From').val(from);
                $('#Experience_' + (j - 1) + '__To').val(to);
                $('#Experience_' + (j - 1)  + '__Experience').val(exp);
            }
        }
        else {
            $("#Experience_0__CompanyName").val(comp);
            $('#Experience_0__Location').val(loc);
            $('#Experience_0__Designation').val(desg);
            $('#Experience_0__From').val(from);
            $('#Experience_0__To').val(to);
            $('#Experience_0__Experience').val(exp);
        }
    }
}
function DeleteRwCertfn(index)
{
    debugger
    if (confirm("Do you want to delete..?")) {
        var count = 0;
        count = $("div[class*='Rdcertifn']").length;
        if (count > 1) {
            $("#certification_div_" + index).remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#certification_div_' + j).clone(); // clone the row
                var cert = $("#Certifications_" + j + "__Certificate").val();
                var type = $('#Certifications_' + j + '__CertificateType_Code').val();
                var issuedby = $('#Certifications_' + j + '__IssuedBy').val();
                var date = $('#Certifications_' + j + '__IssuedDate').val();
                var mode = $('#Certifications_' + j + '__CertificateMode_Code').val();
                var valid = $('#Certifications_' + j + '__CertificateValidity_Code').val();
                var renewel = $('#Certifications_' + j + '__RenewalDate').val();
                
      


                $.each(template.find('input, textarea'), function () {
                    var name = $(this).attr('name');
                    if (typeof name != 'undefined') {
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                    }
                    var id = $(this).attr('id');
                    if (typeof id != 'undefined') {
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                    }
                    //---------------------------------For row Increment When onkeyup
                    var onkeyup = $(this).attr('onkeyup');
                    if (onkeyup !== undefined) {
                        onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onkeyup', onkeyup);
                    }
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('select'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('name', name);
                    var id = $(this).attr('id');
                    id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('id', id);
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('input[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                    $(this).attr('onclick', onclick);
                });
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});




                $("#certification_div_" + j).html(template.html());
                $('#certification_div_' + j).attr('id', 'certification_div_' + (j - 1));




                $("#Certifications_" + (j - 1) + "__Certificate").val(cert);
                $('#Certifications_' + (j - 1) + '__CertificateType_Code').val(type);
                $('#Certifications_' + (j - 1) + '__IssuedBy').val(issuedby);
                $('#Certifications_' + (j - 1) + '__IssuedDate').val(date);
                $('#Certifications_' + (j - 1) + '__CertificateMode_Code').val(mode);
                $('#Certifications_' + (j - 1) + '__CertificateValidity_Code').val(valid);
                $('#Certifications_' + (j - 1) + '__RenewalDate').val(renewel);

            }
        }
        else {
            $("#Certifications_0__Certificate").val(cert);
            $('#Certifications_0__CertificateType_Code').val(type);
            $('#Certifications_0__IssuedBy').val(issuedby);
            $('#Certifications_0__IssuedDate').val(date);
            $('#Certifications_0__CertificateMode_Code').val(mode);
            $('#Certifications_0__CertificateValidity_Code').val(valid);
            $('#Certifications_0__RenewalDate').val(renewel);


        }
    }
}

function DeleteRwEdu(index) {
    debugger
    if (confirm("Do you want to delete..?")) {
        var count = 0;
        count = $("div[class*='Rdeducation']").length;
        if (count > 1) {
            $("#education_div_" + index).remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#education_div_' + j).clone(); // clone the row
                var comp = $("#Education_" + j + "__QualificationType_Code").val();
                var loc = $('#Education_' + j + '__Qualification_Code').val();
                var desg = $('#Education_' + j + '__SpecializationOfBranch_Code').val();
                var from = $('#Education_' + j + '__YearOfCompletion').val();
               

                $.each(template.find('input, textarea'), function () {
                    var name = $(this).attr('name');
                    if (typeof name != 'undefined') {
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                    }
                    var id = $(this).attr('id');
                    if (typeof id != 'undefined') {
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                    }
                    //---------------------------------For row Increment When onkeyup
                    var onkeyup = $(this).attr('onkeyup');
                    if (onkeyup !== undefined) {
                        onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onkeyup', onkeyup);
                    }
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('select'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('name', name);
                    var id = $(this).attr('id');
                    id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('id', id);
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('input[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                    $(this).attr('onclick', onclick);
                });
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});




                $("#education_div_" + j).html(template.html());
                $('#education_div_' + j).attr('id', 'education_div_' + (j - 1));




                $("#Education_" + (j - 1) + "__QualificationType_Code").val(comp);
                $('#Education_' + (j - 1) + '__Qualification_Code').val(loc);
                $('#Education_' + (j - 1) + '__SpecializationOfBranch_Code').val(desg);
                $('#Education_' + (j - 1) + '__YearOfCompletion').val(from);
        
            }
        }
        else {
            $("#Education_0__QualificationType_Code").val(comp);
            $('#Education_0__Qualification_Code').val(loc);
            $('#Education_0__SpecializationOfBranch_Code').val(desg);
            $('#Education_0__YearOfCompletion').val(from);
        
        }
    }
}


