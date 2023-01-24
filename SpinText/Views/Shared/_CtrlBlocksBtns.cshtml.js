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
