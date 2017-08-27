function ShowAddSchemeWindow() {
    $("#main-wc").animate({
            top: "100vh"
        },
        200,
        function () {
            $("#main-wc").hide();
            $("#add-scheme-wc").show();
            $("#add-scheme-wc").animate({
                    left: "0"
                },
                200);
        });
}

$(document).ready(function () {

    $("#btn-add-scheme").click(function() {
        ShowAddSchemeWindow();
    });
});