// block-text-input
jQuery('.dc-block-text-input').dcTpl(function ($, Export) {
    var $self = $(this);

    $self.on('click', '.dc-block-text-input__close', function (e) {
        e.preventDefault();

        $self.slideUp(function () {
            $self.remove();
        });
    });

    Export.clear = function () {
        $self.find('.dc-block-text-input__input').val("");
    };
});
// /block-text-input
//--------------------------------------------
