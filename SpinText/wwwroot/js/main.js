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
            $(this).find('.dcg-h2').html('Block ' + (i + 1));
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
        var $parent = $self.closest('.dc-blocks-main').dcTpl();
        $self.remove();
        $parent.actualizeTemplatesNames();
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

// form-add-coins
jQuery('.dc-form-add-coins').dcTpl(function ($, Export) {
   var $self = $(this);

    function disabled() {
        $self.find('input, button').attr('disabled', 'disabled');
        $self.find('.dcj-loading-icon').show();
    }
    function enabled() {
        $self.find('input, button').removeAttr('disabled');
        $self.find('.dcj-loading-icon').hide();
    }

    $self.on('submit', 'form', function (e) {
        e.preventDefault();
        let $this = $(this);
        let url = $this.attr('action');

        var data = new FormData();
        var file = $this.find('.dcj-file-input')[0].files[0];
        if (!file) return;
        data.append('data', file);

        disabled();

        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            dataType: 'json',
            success: function (msg) {
                enabled();
            }
        });
    });
});
// /form-add-coins
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
    var get_status_url = '/home/GetHTGeneratingStatus';
    var get_count_url = '/home/GetHTCount';
    var delay = 3000;

    var $pairs_input = $self.find('textarea');
    var $btns = $self.find('button, input[type="submit"], input[type="button"]')
    var $link_btns = $self.find('.dcg-btn');
    var $progress_bar = $self.find('.dc-progress-bar').dcTpl();
    var $status_bar = $self.find('.dc-status-bar').dcTpl();

    function listingStatus() {
        setTimeout(function () {
            $.post(get_status_url, function (msg) {
                changeData(msg);

                if (msg.isCompleted)
                    finishParsing();
                else
                    listingStatus();
            });
        }, delay);
    }
    function changeData(data) {
        $progress_bar.setPosition(data.progress.position);
        $status_bar.setValue(data.progress.position);
        $status_bar.setCount(data.count);
    }
    function finishParsing() {
        enabledForm();
        $self.find('.dcj-stop-btn').addClass('dcg-btn_disabled');
    }
    function startParsing(data) {
        
        $progress_bar.setMax(data.generatingCount);
        $progress_bar.setPosition(0);
        $status_bar.setMax(data.generatingCount);
        $status_bar.setValue(0);
        $self.find('.dcj-stop-btn').removeClass('dcg-btn_disabled');
    }
    function enabledForm() {
        $pairs_input.removeAttr('disabled');
        $btns.removeAttr('disabled');
        $link_btns.removeClass('dcg-btn_disabled');
    }
    function disabledForm() {
        $pairs_input.attr('disabled', 'disabled');
        $btns.attr('disabled', 'disabled');
        $link_btns.addClass('dcg-btn_disabled');
    }
    function setCount() {
        $.post(get_count_url, function (msg) {
            $status_bar.setCount(msg.count);
        });
    }

    (function () {
        $.post(get_status_url, function (msg) {
            if (!msg || msg.isCompleted) {
                finishParsing();
                setCount();
            }
            else {
                disabledForm();
                startParsing(msg);
                changeData(msg);
                listingStatus();
            }
        });
    })();

    $self.on('click', '.dcj-stop-btn', function (e) {
        e.preventDefault();
        $.get($(this).attr('href'));
    });

    $self.on('submit', 'form', function (e) {
        e.preventDefault();

        $form = $(this);
        var data = $form.serialize();
        var url = $form.attr('action');

        disabledForm();

        $.post($form.attr('action'), data, function (msg) {
            if (!msg) return;
            startParsing(msg);
            changeData(msg);
            listingStatus();
        });
    });

    $self.on('click', '.dcj-clear-btn', function (e) {
        e.preventDefault();
        $this = $(this);

        $this.addClass('dcg-btn_disabled');

        $.get($this.attr('href'), function () {
            $this.removeClass('dcg-btn_disabled');
            setCount();
        });
    })
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
    var $pointer = $self.find('.dc-progress-bar__item');

    var _max = 0;
    var _position = 0;

    function refresh() {
        var percent = _max == 0 ? 0 : 100 / _max * _position;
        $pointer.css('width', percent + '%');
    }
    refresh();

    Export.setMax = function (max) {
        _max = max;
        refresh();
    };

    Export.setPosition = function (position) {
        _position = position;
        refresh();
    };

    Export.reset = function () {
        Export.setPosition(0);
        Export.setMax(0);
    }
});
// /progress-bar
//--------------------------------------------

// status-bar
jQuery('.dc-status-bar').dcTpl(function ($, Export) {
    var $self = $(this);
    var $value = $self.find('.dcj-generated-value');
    var $count = $self.find('.dcj-count-value');

    var _max = 0;
    var _value = 0;
    var _count = 0;

    function refresh() {
        $value.html(_value + '/' + _max);
        $count.html(_count);
    }

    Export.setMax = function (max) {
        _max = max;
        refresh();
    };

    Export.setValue = function (value) {
        _value = value;
        refresh();
    };

    Export.setCount = function (count) {
        _count = count;
        refresh();
    };

    Export.clear = function () {
        _value = 0;
        _max = 0;
        refresh();
    };
});
// /status-bar
//--------------------------------------------

// types-menu
jQuery('.dc-types-menu').dcTpl(function ($, Export) {
   var $self = $(this);
});
// /types-menu
//--------------------------------------------
