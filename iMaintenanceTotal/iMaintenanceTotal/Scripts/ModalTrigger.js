$(function () {
    $('.editor-container').click(function () {
        var url = "/MaintTask/Edit";
        var id = $(this).attr('data-id');
        $.get(url + '/' + id, function (data) {
            $('#editor-content-container').html(data);
            $('#editor-container').modal('show');
        });
    });
});

function success(data,status,xhr) {
    $('#editor-container').modal('hide');
    $('#editor-content-container').html("");
}

function failure(xhr,status,error) {
    $('#editor-content-container').html(xhr.responseText);
    $('#editor-container').modal('show');
}
