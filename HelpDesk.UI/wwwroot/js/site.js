$(document).ready(function () {
    $('.leftmenutrigger').on('click', function (e) {
        $('.side-nav').toggleClass("open");
        e.preventDefault();
    });
    $(window).on('beforeunload', function () {
        displayBusyIndicator();
    });
    $(document).on('submit', 'form', function () {
        displayBusyIndicator();
    });
});
