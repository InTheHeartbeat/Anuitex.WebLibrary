
function ShowSignForm() {
    $("#index-container").animate({
            top: "100vh"
        },
        200,
        function () {
            $("#sing-container").css("display", "block").animate({ left: "0vw" }, 200);
        });
}

function HideSignForm() {
    $("#sing-container").animate({
            left: "100vw"
        },
        200,
        function () {
            $("#sing-container").css("display", "none");
            $("#index-container").animate({
                    top: "100vh"
                },
                200);
        });
}

$(document).ready(function () {
    $("#btn-signup-li").click(function () {
        $("#btn-signup").click();
    });

    

});