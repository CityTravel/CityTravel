function feedbackSuccess() {
    setTimeout(function () {
        $('#feedback').modal('toggle');
        $.ajax({
            url: helpers.GetPath("feedback/feedbackform"),
            dataType: "html",
            type: "GET",
            success: function (data) {
                $(".feedbackform").html(data);
            }
        });
    }, 3000);
};

$(".feedback-open-button").click(function () {
    $('#feedback').modal('toggle');
});