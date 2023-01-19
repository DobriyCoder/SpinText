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
// api-check-box
jQuery('.dc-api-check-box').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /api-check-box
//--------------------------------------------

// block-btns
jQuery('.dc-block-btns').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /block-btns
//--------------------------------------------

// blocks-main
jQuery('.dc-blocks-main').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /blocks-main
//--------------------------------------------

// block-text-input
jQuery('.dc-block-text-input').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /block-text-input
//--------------------------------------------

// ctrl-blocks-btns
jQuery('.dc-ctrl-blocks-btns').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /ctrl-blocks-btns
//--------------------------------------------

// ctrl-btn
jQuery('.dc-ctrl-btn').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /ctrl-btn
//--------------------------------------------

// ctrl-language
jQuery('.dc-ctrl-language').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /ctrl-language
//--------------------------------------------

// currency-pairs
jQuery('.dc-currency-pairs').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /currency-pairs
//--------------------------------------------

// drop-down-block
jQuery('.dc-drop-down-block').dcTpl(function ($, Export) {
    var $self = $(this);
    var deg = 0;
    $self.on('click', '.dcj-toggle-btn', function (e) {
        e.preventDefault();
        $self.find(".dc-drop-down-block__content").stop().animate({ "height": "toggle" });
        if (deg === 0) {
            deg -= 180;
            $self.find(".dc-drop-down-block__arrow").css({
                transform: 'rotate(' + deg + 'deg)',
                
            })
        } else {
            deg = 0;
            $self.find(".dc-drop-down-block__arrow").css({
                transform: 'rotate(' + deg + 'deg)'
            })
        }
    })


});
// /drop-down-block
//--------------------------------------------

// progress-bar
jQuery('.dc-progress-bar').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /progress-bar
//--------------------------------------------
