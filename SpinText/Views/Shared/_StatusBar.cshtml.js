// status-bar
jQuery('.dc-status-bar').dcTpl(function ($, Export) {
    var $self = $(this);
    var $value = $self.find('.dcj-generated-value');
    var $count = $self.find('.dcj-count-value');

    var _max = 0;
    var _value = 0;
    var _count = 0;

    function refresh() {
        $value.html(_value + '/' + _max);
        $count.html(_count);
    }

    Export.setMax = function (max) {
        _max = max;
        refresh();
    };

    Export.setValue = function (value) {
        _value = value;
        refresh();
    };

    Export.setCount = function (count) {
        _count = count;
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
