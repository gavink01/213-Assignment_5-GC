// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#MusicGenre").change(function () {
        var genre = $(this).val();
        $.ajax({
            type: "GET",
            url: "/Musics/GetPerformersByGenre",
            data: { musicGenre: genre },
            success: function (data) {
                var performersDropdown = $("#MusicPerformers");
                performersDropdown.empty();
                performersDropdown.append($('<option>', {
                    value: "",
                    text: "All"
                }));
                $.each(data, function (index, item) {
                    performersDropdown.append($('<option>', {
                        value: item,
                        text: item
                    }));
                });
            }
        });
    });
});