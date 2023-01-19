// drop-down-block
jQuery('.dc-drop-down-block').dcTpl(function ($, Export) {
    var $self = $(this);
    var deg = -180;
    $self.on('click', '.dcj-toggle-btn', function (e) {
        e.preventDefault();
        $self.find(".dc-drop-down-block__content").stop().animate({ "height": "toggle" });
        if (deg === 0) {
            deg -= 180;
            $self.find(".dc-drop-down-block__arrow").css({
                transform: 'rotate(' + deg + 'deg)',
                
            })
        } else {
            deg = 0;
            $self.find(".dc-drop-down-block__arrow").css({
                transform: 'rotate(' + deg + 'deg)'
            })
        }
    })


});
// /drop-down-block
//--------------------------------------------
