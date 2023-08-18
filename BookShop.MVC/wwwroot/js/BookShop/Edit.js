$(document).ready(function () {
    $('#CreateBookShopServiceModal form').submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created bookshop service")
            },
            error: function (data) {
                toastr["error"]("Something went wrong")
            }
        })
    });
});