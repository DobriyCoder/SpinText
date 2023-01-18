jQuery(function ($) {
    //--------------------------------------
    // UI
    (function () {
        $('select').selectmenu({
            change: function (event, ui) {
                $(this).change();
            }
        });

        $('input[type="number"').spinner();

        $('input[type="checkbox"], input[type="radio"]').checkboxradio();
    })();
    //--------------------------------------
});