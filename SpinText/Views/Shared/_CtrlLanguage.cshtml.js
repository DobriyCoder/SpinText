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
