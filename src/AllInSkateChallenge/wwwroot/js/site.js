// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".js-cookie-banner--banner").click(function () {
        var expires = new Date;
        expires.setFullYear(expires.getFullYear() + 1);
        document.cookie = "cookieWarningDismissed=true;path=/;expires=" + expires.toUTCString() + ";";
        $(".js-cookie-banner").remove();
    });
});

function processStravaUpdates() {
    $("#stravaImportModal").modal("show");
    $(".js-strava-modal-close").prop("disabled", true);

    $.get("api/strava/importlatest")
        .done(function (data) {
            $(".js-strava-modal-close").prop("disabled", false);
            var report = "<p>" + data.numberImported + " activities were successfully imported.</p>";
            report += "<p>" + data.numberIgnored + " activities alerts were dismissed as ineligible for the event.</p>";

            $(".js-strava-modal-body").html(report);
            $(".js-strava-alert-banner").remove();
        })
        .fail(function () {
            $(".js-strava-modal-close").prop("disabled", false);
            $(".js-strava-modal-body").html("<p>An error occured when processing your strava events.</p>");
        });
}