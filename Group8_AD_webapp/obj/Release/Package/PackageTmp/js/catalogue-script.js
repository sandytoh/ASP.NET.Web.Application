

$(document).on('keyup', '.txtSearch', function () {
    $(".txtSearch").blur();
    $(".txtSearch").focus();
});

$(document).on("click", function (event) {
    if (!($(event.target).hasClass('showsearch'))) {
        $('.ddlsearchcontent').hide();
    }
});

$(function () {
    $(".clean").hide();
    $(".btnList").click(function () {
        $(this).addClass("active");
        $(".btnGrid").removeClass("active");
    });
    $(".btnGrid").click(function () {
        $(this).addClass("active");
        $(".btnList").removeClass("active");
    });
});

$(".btnClean").click(function () {
    if ($('#IsClean').val() == "false") {
        $("#side").hide("slow");
        $(".navbar").hide("slow");
        $(".clean").show("slow");
        $(".sidepanelarea").hide("slow");
        $('#IsClean').val("true");
    }
    else {
        $("#side").show("slow");
        $(".navbar").show("slow");
        $(".clean").hide("slow");
        $(".sidepanelarea").show("slow");
        $('#IsClean').val("false");
    }
});