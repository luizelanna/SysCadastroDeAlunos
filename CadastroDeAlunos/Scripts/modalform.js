$(function () {
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        // hide dropdown if any
        $(e.target).closest(".btn-group").children(".dropdown-toggle").dropdown("toggle");
        $("#myModalContent").load(this.href, function () {
            $("#myModal").modal({
                //backdrop: "static",
                keyboard: true
            }, "show");
            bindForm(this);
        });
        return false;
    });

});

function bindForm(dialog) {
    $("form", dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $("#myModal").modal("hide");
                    //Refresh
                    location.reload();
                    //location.load();
                } else {
                    alert("Erro");
                    $("#myModalContent").html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}

//nova parte

$(document).ready(function () {
    $("#openBtn").click(function () {
        $("#myModal").modal();
    });
    $(".modal")
        .on({
            'show.bs.modal': function () {
                var idx = $(".modal:visible").length;
                $(this).css("z-index", 1040 + (15 * idx));
            },
            'shown.bs.modal': function () {
                var idx = ($(".modal:visible").length) - 1; // raise backdrop after animation.
                $(".modal-backdrop").not(".stacked")
                    .css("z-index", 1039 + (10 * idx))
                    .addClass("stacked");
            },
            'hidden.bs.modal': function () {
                if ($(".modal:visible").length > 0) {
                    // restore the modal-open class to the body element, so that scrolling works
                    // properly after de-stacking a modal.
                    setTimeout(function () {
                        $(document.body).addClass("modal-open");
                    }, 0);
                }
            }
        });
});