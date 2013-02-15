$(document).ready(function () {
    if ($.browser.msie) {
        function interval() {
            $('.new').children().not('.secondaryText').delay(1500).animate({ marginTop: "100px" }, 1000, function () {
                $(this).delay(500).animate({ marginTop: "20px" }, 1000);
            });
        }

        setInterval(function () {
            interval();
        }, 1500);
    }

    $('#hiddenNav').click(function () {
        $('#secondaryContent').toggle();
    });
});

