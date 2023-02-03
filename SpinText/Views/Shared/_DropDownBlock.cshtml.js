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
