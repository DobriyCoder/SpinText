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
        $self.find('.dcj-stop-btn').addClass('dcg-btn_disabled');
    }
    function startParsing(data) {
        
        $progress_bar.setMax(data.generatingCount);
        $progress_bar.setPosition(0);
        $status_bar.setMax(data.generatingCount);
        $status_bar.setValue(0);
        $self.find('.dcj-stop-btn').removeClass('dcg-btn_disabled');
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

    (function () {

        $.post(get_status_url, function (msg) {
            if (!msg || msg.isCompleted) {
                finishParsing();
            }
            else {
                disabledForm();
                startParsing(msg);
                changeData(msg);
                listingStatus();
            }
        });
    })();

    $self.on('click', '.dcj-stop-btn', function (e) {
        e.preventDefault();
        $.get($(this).attr('href'));
    });

    $self.on('submit', 'form', function (e) {
        e.preventDefault();

        $form = $(this);
        var data = $form.serialize();
        var url = $form.attr('action');

        disabledForm();

        $.post($form.attr('action'), data, function (msg) {
            if (!msg) return;
            startParsing(msg);
            changeData(msg);
            listingStatus();
        });
    });

    $self.on('click', '.dcj-clear-btn', function (e) {
        e.preventDefault();
        $this = $(this);

        $this.addClass('dcg-btn_disabled');

        $.get($this.attr('href'), function () {
            $this.removeClass('dcg-btn_disabled');
        });
    })
});
// /generatingform
//--------------------------------------------
