﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".js-delete-mileage-entry").click(function (event) {
        $(".js-confirm-delete-mileage-entry").data("entry-id", ($(event.target).data("entry-id")));
        $("#deleteEntryModal").modal({ keyboard: true, focus: true });
    });

    $(".js-confirm-delete-mileage-entry").click(function (event) {
        var rowSelector = "tr[data-entry-id='" + $(event.target).data("entry-id") + "']";
        $.post("/skater/skate-log/delete", { "mileageEntryId": $(event.target).data("entry-id") })
            .done(function () {
                $(rowSelector).remove();
                $("#deleteEntryModal").modal('hide');
            });
    });

    $(".js-add-mileage-entry").click(function () {
        $("#addEntryModal").modal({ keyboard: true, focus: true });
    });

    $(".js-cookie-banner--banner").click(function () {
        document.cookie = "cookieWarningDismissed=true;path=/;";
        $(".js-cookie-banner").remove();
    });
});