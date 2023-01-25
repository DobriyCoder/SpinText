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

    $self.on('click', '.dcj-add-template', function (e) {
        e.preventDefault();
        $self.trigger('add_template_click');
    });

    $self.on('click', '.dcj-clear', function (e) {
        e.preventDefault();
        $self.trigger('clear_click');
    });
});
// /block-btns
//--------------------------------------------

// blocks-main
jQuery('.dc-blocks-main').dcTpl(function ($, Export) {
    var $self = $(this);

    $self.find('.dc-ctrl-blocks-btns').on('clear_click', function () {
        $self.find('.dc-drop-down-block').each(function () {
            $(this).dcTpl().clear();
        });
    });

    $self.find('.dc-ctrl-blocks-btns').on('add_block_click', function () {
        var $wrapper = $self.find('.dcj-blocks');
        var $tmp_block = $self.find('.dc-drop-down-block').first();
        var $block = $($tmp_block[0].outerHTML);// $tmp_block.clone(false, false);
        
        $block.hide();
        $wrapper.prepend($block);
        $block.trigger('ready');
        $block.find('.dc-block-text-input').trigger('ready');
        $block.find('.dc-block-btns').trigger('ready');
        $block.dcTpl().clear();
        $block.dcTpl().removeTemplates();
        Export.actualizeTemplatesNames();
        $block.show();
    });

    Export.actualizeTemplatesNames = function () {
        $self.find('.dc-drop-down-block').each(function (i) {
            $(this).find('.dcj-tpl').attr('name', 'Blocks['+i+'][]');
        });
    };
    Export.actualizeTemplatesNames();
});
// /blocks-main
//--------------------------------------------

// block-text-input
jQuery('.dc-block-text-input').dcTpl(function ($, Export) {
    var $self = $(this);

    $self.on('click', '.dc-block-text-input__close', function (e) {
        e.preventDefault();

        Export.remove();
    });

    Export.clear = function () {
        $self.find('.dc-block-text-input__input').val("");
    };

    Export.remove = function () {
        $self.remove();
    };
});
// /block-text-input
//--------------------------------------------

// ctrl-blocks-btns
jQuery('.dc-ctrl-blocks-btns').dcTpl(function ($, Export) {
    var $self = $(this);

    $self.on('click', '.dcj-add-block', function (e) {
        e.preventDefault();
        $self.trigger('add_block_click');
    });

    $self.on('click', '.dcj-clear', function (e) {
        e.preventDefault();
        $self.trigger('clear_click');
    });
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

    $self.on('change', 'select', function () {
        var val = $(this).val();
        document.location.href = '?lang=' + val;
    });
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

    $self.on('click', '.dc-drop-down-block__close', function (e) {
        e.preventDefault();
        e.stopPropagation();

        Export.remove();
    });

    $self.find('.dc-block-btns').on('clear_click', function () {
        Export.clear();
    });

    $self.find('.dc-block-btns').on('add_template_click', function () {
        var $wrapper = $self.find('.dcj-tmp-wrapper');
        var $tmp_block = $wrapper.find('.dc-block-text-input').first();
        var $block = $($tmp_block[0].outerHTML);

        $block.hide();
        $wrapper.prepend($block);
        $block.trigger('ready');
        $block.dcTpl().clear();
        $block.show();
    });

    Export.clear = function () {
        $self.find('.dc-block-text-input').each(function () {
            $(this).dcTpl().clear();
        });
    };

    Export.remove = function () {
        $self.remove();
    };

    Export.removeTemplates = function () {
        $self.find('.dc-block-text-input').each(function (i) {
            if (i == 0) {
                $(this).dcTpl().clear();
                return;
            }

            $(this).dcTpl().remove();
        });
    };
});
// /drop-down-block
//--------------------------------------------

// form-blocks
jQuery('.dc-form-blocks').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /form-blocks
//--------------------------------------------

// generatingform
jQuery('.dc-generatingform').dcTpl(function ($, Export) {
    var $self = $(this);

    $self.on('submit', 'form', function (e) {
        e.preventDefault();

        $form = $(this);
        var data = {};
        var url = $form.attr('action');
        console.log(url);

        $.post(url, data, function (msg) {
            console.log(msg);
        });
    });
});
// /generatingform
//--------------------------------------------

// header
jQuery('.dc-header').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /header
//--------------------------------------------

// page-title
jQuery('.dc-page-title').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /page-title
//--------------------------------------------

// progress-bar
jQuery('.dc-progress-bar').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /progress-bar
//--------------------------------------------

// status-bar
jQuery('.dc-status-bar').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /status-bar
//--------------------------------------------
