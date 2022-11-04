// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function generateId(elementId) {
    var random = 'xxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });

    $("#" + elementId).val(random);
}

var path_to_delete;

$(".deleteItem").click(function (e) {
    path_to_delete = $(this).data('path');
});

$('#btnContinueDelete').click(function () {
    window.location = path_to_delete;
});