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
