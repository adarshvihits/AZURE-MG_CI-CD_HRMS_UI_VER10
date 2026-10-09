//----------Applicant Profile - section navigation--------------------
// The sections scroll inside #ApplicantProfileScroll (the header and the tabs stay fixed above it).

$(document).ready(function () {
    var scroller = $('#ApplicantProfileScroll');

    // tab click: scroll the section to the top of the scroll area
    $('#ApplicantProfileNav').on('click', '.apf-nav-item', function (e) {
        e.preventDefault();
        var target = $($(this).attr('href'));
        if (target.length == 0)
            return;
        var top = scroller.scrollTop() + target.position().top - 10;
        scroller.stop().animate({ scrollTop: top }, 300);
        setActiveProfileNav($(this).attr('href'));
    });

    // highlight the tab of the section currently at the top of the scroll area
    scroller.on('scroll', function () {
        var current = '#sec-personal';
        $('.apf-section').each(function () {
            if ($(this).position().top <= 40)
                current = '#' + this.id;
        });
        // at the very bottom, the last section is the one being read
        if (scroller.scrollTop() + scroller.innerHeight() >= scroller[0].scrollHeight - 5)
            current = '#' + $('.apf-section').last().attr('id');
        setActiveProfileNav(current);
    });
});

function setActiveProfileNav(href) {
    $('#ApplicantProfileNav .apf-nav-item').removeClass('active');
    $('#ApplicantProfileNav .apf-nav-item[href="' + href + '"]').addClass('active');
}
