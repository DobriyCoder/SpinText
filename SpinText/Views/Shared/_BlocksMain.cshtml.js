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
        $block.slideDown();
    });
});
// /blocks-main
//--------------------------------------------
