
var signFormVisible = false;

function ShowSignForm() {
    if (!signFormVisible) {
        $("body").css("overflow", "hidden");
        $("#index-container").animate({
                top: "100vh"
            },
            250,
            function () {
                $("#sing-container").css("left", "100vw");
                $("#sing-container").css("top", "5em");
                $("#sing-container").css("display", "block").animate({ left: "0vw" },
                    250,
                    function() { $("body").css("overflow-x", "auto"); });
            });
        signFormVisible = true;
    } else {
        HideSignForm();
        ShowSignForm();
    }
}

function HideSignForm() {
    if (signFormVisible) {
        $("body").css("overflow", "hidden");
        $("#sing-container").animate({
                top: "100vh"
            },
            100,
            function() {
                $("#sing-container").css("display", "none");
                $("body").css("overflow-x", "auto"); 
            });
        signFormVisible = false;
    }
}

$(document).ready(function () {
    $(".entity-photo").click(function () {

        var img = $(this);
        var src = img.css("background-image");

        $("body").append("<div class='popup'>" +
            "<div class='popup_bg popup_img'></div>" +
            "</div>");
        $("body").find(".popup_img").css("background-image", src);
        $(".popup").fadeIn(400);
        $(".popup_bg").click(function () {
            $(".popup").fadeOut(400);
            setTimeout(function () {
                    $(".popup").remove();
                },
                400);
        });
    });


    $("#btn-signup-li").click(function () {
        $("#btn-signup").click();
    });

    $(".section-selector").click(function (event) {
        $(".section-selector").find(".active").removeClass("active");
        if ($(event.target).is("a") || $(event.target).is("span")) {
            $(event.target).parent().addClass("active");            
        } else {
            $(event.target).addClass("active");
            $(event.target).children()[0].click();            
        }


    });

});