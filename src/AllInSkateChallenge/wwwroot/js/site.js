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