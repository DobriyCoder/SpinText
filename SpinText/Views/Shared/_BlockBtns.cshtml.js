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
