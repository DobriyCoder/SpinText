// generatingform
jQuery('.dc-generatingform').dcTpl(function ($, Export) {
    var $self = $(this);
    var get_status_url = '/home/GetHTGeneratingStatus';
    var delay = 1000;

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
    }
    function finishParsing() {
        enabledForm();
    }
    function startParsing(data) {
        
        $progress_bar.setMax(data.generatingCount);
        $progress_bar.setPosition(0);
        $status_bar.setMax(data.generatingCount);
        $status_bar.setValue(0);
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

    $self.on('submit', 'form', function (e) {
        e.preventDefault();

        $form = $(this);
        var data = new FormData();
        data.append('data', $form.find('[type="file"]')[0].files[0]);
        var url = $form.attr('action');

        disabledForm();

        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            dataType: 'json',
            success: function (msg) {
                startParsing(msg);
                changeData(msg);
                listingStatus();
            }
        });

        /*$.post(url, data, function (msg) {
            startParsing(msg);
            changeData(msg);
            listingStatus();
        });*/
    });
});
// /generatingform
//--------------------------------------------
