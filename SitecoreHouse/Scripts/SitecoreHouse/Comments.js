
(function ($) {
    if ($('body.pagemode-edit').length > 0)
    {
        //$('body').on("click", function (event) {
        //    showSHComments();
        //});

        //function showSHComments() {
        //    debugger;
        //    log(event.target);
        //    $(".SH-comments").addClass("ninja");
        //    $(event.target).closest(".SH-comments").removeClass("ninja");
        //    log($(event.target).closest(".SH-comments"));
        //}

        $.each($(".sh-comments"), function (key, value) {
            $(this).parent().addClass('sh-comments-hover');
        });

        $('.sh-comments-hover').hover(function () {
            $(this).closest('.sh-comments').removeClass('ninja');
        }, function () {
            $(this).closest('.sh-comments').addClass('ninja');
        });
    }
})(jQuery);