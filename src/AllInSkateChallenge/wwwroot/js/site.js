// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".js-delete-mileage-entry").click(function (event) {
        $(".js-confirm-mileage-entry").data("entry-id", ($(event.target).data("entry-id")));
        $("#deleteEntryModal").modal({ keyboard: true, focus: true });
    });

    $(".js-confirm-mileage-entry").click(function (event) {
        $.post("/skater/skate-log/delete", { "mileageEntryId": $(event.target).data("entry-id") })
            .done(function () { })
            .fail(function () { });
    });
});