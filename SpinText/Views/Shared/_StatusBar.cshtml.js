// status-bar
jQuery('.dc-status-bar').dcTpl(function ($, Export) {
    var $self = $(this);
    var $value = $self.find('.dc-status-bar__value');

    var _max = 0;
    var _value = 0;

    function refresh() {
        $value.html(_value + '/' + _max);
    }

    Export.setMax = function (max) {
        _max = max;
        refresh();
    };

    Export.setValue = function (value) {
        _value = value;
        refresh();
    };

    Export.clear = function () {
        _value = 0;
        _max = 0;
        refresh();
    };
});
// /status-bar
//--------------------------------------------
