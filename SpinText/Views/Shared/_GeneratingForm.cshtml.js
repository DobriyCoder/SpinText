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
