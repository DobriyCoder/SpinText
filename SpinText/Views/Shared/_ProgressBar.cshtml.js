// progress-bar
jQuery('.dc-progress-bar').dcTpl(function ($, Export) {
    var $self = $(this);
    var $pointer = $self.find('.dc-progress-bar__item');

    var _max = 0;
    var _position = 0;

    function refresh() {
        var percent = _max == 0 ? 0 : 100 / _max * _position;
        $pointer.css('width', percent + '%');
    }
    refresh();

    Export.setMax = function (max) {
        _max = max;
        refresh();
    };

    Export.setPosition = function (position) {
        _position = position;
        refresh();
    };

    Export.reset = function () {
        Export.setPosition(0);
        Export.setMax(0);
    }
});
// /progress-bar
//--------------------------------------------
