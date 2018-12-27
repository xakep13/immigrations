/// <reference path="core-utils.js" />

$(document).ready(function () {
    $(document).on("click", ".input-clear", function () {
        var input = $(this).parents(".input-group").find(".form-control");
        input.prop("readonly", input.hasClass("bs-select") ? true : false).val("");

        if (input.data("related-element"))
            $(input.data("related-element")).val("");

        if (input.hasClass("bs-autocomplete"))
            coreUtils.createAutocomplete(input);
    });

    $(document).on("click", ".btn-remove-relateditem", function () {
        var t = $(this);
        var tContainer = $(t.parents(".related-items"));

        t.parents(".related-item").remove();
        coreUtils.refreshListIndexes(tContainer);
    });

    $(document).on("hover", ".text-elipsis", function () {
        $(this).attr("title", $(this).text());
    });

    $(document).on("submit", "form", function (e) {
        $(this).find("button[type='submit'], input[type='submit']").prop("disabled", true);

        $(this).validate({
            ignore: ".ignore"
        });

        try {
            if (!$(this).valid()) {
                $(this).find("button[type='submit'], input[type='submit']").prop("disabled", false);
                e.preventDefault();
                return false;
            } else {
                try { return validFormBeforeSend(e); }
                catch (ex) { }
            }
        } catch (ex) { }
    });

    $("form").submit(function (event) {
        if ($(this).hasClass("disabled")) {
            event.preventDefault();
            return false;
        }
    });

    $(".bs-select").each(function () {
        coreUtils.createSelect($(this));
    });

    $(".upload-panel").each(function () {
        coreUtils.createUploader($(this));
    });

    $(".bs-datepicker").each(function () {
        coreUtils.createDatepicker($(this));
    });

    $(document).on("change", ".input[id]", function () {
        $this = $(this);
        $track = $("[data-track='#" + $(this).attr("id") + "']");

        if ($track)
            $track.text($track.data("default-text") + ": " + $this.val());
    });
});
