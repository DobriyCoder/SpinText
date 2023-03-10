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
