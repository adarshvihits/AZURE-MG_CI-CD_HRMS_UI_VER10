 $(function () {
     $('[data-bs-toggle="tooltip"]').tooltip()       
    })
    $('[data-bs-toggle="tooltip"]').tooltip({
        trigger: 'hover',
        container: '.wrapper'
    });
    //$(".tooltip-link").on("mouseover", function () {

    //    $(".tooltip-hover").fadeIn(300, function () {

    //    });

    //})
    //$(".tooltip-link").on("mouseout", function () {

    //    $(".tooltip-hover").fadeOut(300, function () {

    //    });    

    //})
$(".btn ").click(function () {
    $(".tooltip-hover").fadeOut(300, function () {
    });
 

});


