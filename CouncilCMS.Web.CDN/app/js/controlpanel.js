/// <reference path="core-utils.js" />

function bsAdmin() { };

bsAdmin.editMap;
bsAdmin.editMapMarkers;
bsAdmin.galleryUploader;
bsAdmin.processingTimer;
bsAdmin.divPreview;
bsAdmin.divRowEdit;
bsAdmin.divColumnEdit;
bsAdmin.divContentEdit;
bsAdmin.divProcessing;

//--CONTENT--//

//--Handlers--//

$(document).ready(function () {
    $(document).on("click", ".preview-content", function (e) {
        e.preventDefault();
        $('body').modalmanager('loading');
        var id = $(this).data("id");
        var type = $(this).data("type");

        bsAdmin.divPreview.find(".modal-body").load("/uk/controlpanel/ajax/PreviewContent/", { itemid: id, type: type }, function () {
            bsAdmin.divPreview.modal({ backdrop: 'static', keyboard: false, modalOverflow: true });
            coreUtils.createPage(bsAdmin.divPreview.find(".modal-body"));
        });
    });


    $(document).on("click", ".edit-content-row", function () {
        $('body').modalmanager('loading');
        var id = $(this).data("id");

        bsAdmin.divRowEdit.find(".modal-body").load("/uk/controlpanel/ajax/GetContentRowEditForm/", { id: id }, function () {
            bsAdmin.divRowEdit.modal({ backdrop: 'static', keyboard: false });
        });
    });

    $(document).on("click", ".edit-content-column", function () {
        $('body').modalmanager('loading');
        var id = $(this).data("id");

        bsAdmin.divColumnEdit.find(".modal-body").load("/uk/controlpanel/ajax/GetContentColumnEditForm/", { id: id }, function () {
            bsAdmin.divColumnEdit.modal({ backdrop: 'static', keyboard: false });
        });
    });

    $(document).on("click", ".add-row", function () {
        $('body').modalmanager('loading');
        var itemId = $(this).data("item-id");
        var type = $(this).data("type");

        bsAdmin.divRowEdit.find(".modal-body").load("/uk/controlpanel/ajax/GetRowSelect/", { itemId: itemId, type: type }, function () {
            bsAdmin.divRowEdit.modal({ backdrop: 'static', keyboard: false, modalOverflow: true });
        });
    });

    $(document).on("click", ".add-content", function () {
        $('body').modalmanager('loading');
        var colId = $(this).data("col-id");
        var pos = $(this).data("position");

        bsAdmin.divContentEdit.find(".modal-body").load("/uk/controlpanel/ajax/GetContentSelect/", { contentcolumnid: colId, position: pos }, function () {
            bsAdmin.divContentEdit.modal({ backdrop: 'static', keyboard: false, modalOverflow: true });
        });
    });

    $(document).on("click", ".edit-content", function () {
        $('body').modalmanager('loading');
        var id = $(this).data("id");

        bsAdmin.divContentEdit.find(".modal-body").load("/uk/controlpanel/ajax/GetContentEditForm/", { id: id }, function () {
            bsAdmin.divContentEdit.modal({ backdrop: 'static', keyboard: false, modalOverflow: true });
            bsAdmin.createEditMap();
            bsAdmin.createEditGallery();
        });
    });

    $(document).on("click", ".select-row", function () {
        var itemId = $(this).data("item-id");
        var type = $(this).data("type");
        var colType = $(this).data("col-type");

        bsAdmin.divRowEdit.modal('hide');
        bsAdmin.startProcessing();
        
        $.post("/uk/controlpanel/ajax/addcontentrow/", { itemId: itemId, type: type, colType: colType }, function (r) {
            bsAdmin.endProcessing();
            $("#divRows").append(r);                
        });        
    });

    $(document).on("click", ".select-content", function () {
        var colId = $(this).data("col-id");
        var type = $(this).data("type");

        bsAdmin.divContentEdit.find(".modal-body").load("/uk/controlpanel/ajax/GetContentEditForm/", { id: 0, contentcolumnid: colId, type: type }, function () {
            coreUtils.createForm(bsAdmin.divContentEdit.find("form[data-ajax='true']"));
            bsAdmin.divContentEdit.find(".loading-mask").remove();
            bsAdmin.createEditMap();
            bsAdmin.createEditGallery();
        });
    });

    $(document).on("click", ".close-content-edit", function () {
        bsAdmin.divContentEdit.modal('hide');
    });

    $(document).on("click", ".move-content-row", function () {
        var id = $(this).data("id");
        var direction = parseInt($(this).data("direction"));
        var item = $("#row" + id);
        var neighbour = direction > 0 ? item.next() : item.prev();

        if (neighbour && neighbour.hasClass("row-item")) {
            item.animate({ top: direction * 50  }, 200, function () {
                neighbour.animate({ top: -direction * 50 }, 200, function () {
                    $.post("/uk/controlpanel/ajax/movecontentrow", { id: id, neighbourId: neighbour.attr("id").substring(3), direction: direction > 0 ? "down" : "up" }, function () {
                        item.css('top', '0px');
                        neighbour.css('top', '0px');
                        item.insertBefore(neighbour);
                    });
                });
            });
        }
    });

    $(document).on("click", ".move-content", function () {
        var id = $(this).data("id");
        var direction = parseInt($(this).data("direction"));
        var item = $("#content" + id);
        var neighbour = direction > 0 ? item.next() : item.prev();

        if (neighbour && neighbour.hasClass("content-item")) {

            item.animate({ top: direction * 30 }, 200, function () {
                neighbour.animate({ top: -direction * 30 }, 200, function () {
                    $.post("/uk/controlpanel/ajax/movecontent", { id: id, neighbourId: neighbour.attr("id").substring(7), direction: direction > 0 ? "down" : "up" }, function () {
                        item.css('top', '0px');
                        neighbour.css('top', '0px');
                        if (direction > 0)
                            item.insertAfter(neighbour);
                        else
                            item.insertBefore(neighbour);
                    });
                });
            });
        }
    });



    $(document).on("click", ".remove-content-row", function () {
        $(this).addClass("disabled").find(".fa").removeClass("fa-trash").addClass("fa-circle-o-notch fa-spin");

        if (confirm("Увага! Весь контент цього блоку буде видалено. Ви впевнені, що хочете продовжити?")) {
            var type = $(this).parents(".content-edit").data("type");
            var id = $(this).data("id");
            var itemId = $(this).data("item-id");

            $.post("/uk/controlpanel/ajax/removecontentrow", { id: id, itemid: itemId, type: type }, function () {
                $("#row" + id).slideUp(300, function () { $(this).empty().remove(); });
            });
        } else {
            $(this).removeClass("disabled").find(".fa").addClass("fa-trash").removeClass("fa-circle-o-notch fa-spin");
        }
    });

    $(document).on("click", ".remove-content", function () {
        $(this).prop("disabled", true).addClass("disabled").find(".fa").removeClass("fa-trash").addClass("fa-circle-o-notch fa-spin");

        if (confirm("Увага! Контент буде видалено без можливості відновлення. Ви впевнені, що хочете продовжити?")) {
            var id = $(this).data("id");
            $.post("/uk/controlpanel/ajax/removecontent", { id: id }, function () {
                $("#content" + id).slideUp(300, function () { $(this).empty().remove(); });
                bsAdmin.divContentEdit.modal('hide');
            });
        } else {
            $(this).prop("disabled", false).removeClass("disabled").find(".fa").addClass("fa-trash").removeClass("fa-circle-o-notch fa-spin");
        }
    });

    //-MAP-//

    $(document).on("click", "#btnMapZoomPlus", function () {
        var zoom = parseInt($("#tbMapZoom").val()) + 1;

        if (zoom <= 20)
        {
            bsAdmin.editMap.setZoom(zoom);
            $("#tbMapZoom").val(zoom);
        }
    });

    $(document).on("click", "#btnMapZoomMinus", function () {
        var zoom = parseInt($("#tbMapZoom").val()) - 1;

        if (zoom >= 2) {
            bsAdmin.editMap.setZoom(zoom);
            $("#tbMapZoom").val(zoom);
        }
    });

    $(document).on("click", "#btnMapHeightPlus", function () {
        var height = parseInt($("#tbMapHeight").val()) + 20;

        $("#divContentMap").css({ height: height });
        $("#tbMapHeight").val(height);

        google.maps.event.trigger(bsAdmin.editMap, "resize");
    });

    $(document).on("click", "#btnMapHeightMinus", function () {
        var height = parseInt($("#tbMapHeight").val()) - 20;

        if (height >= 20) {
            $("#divContentMap").css({ height: height });
            $("#tbMapHeight").val(height);

            google.maps.event.trigger(bsAdmin.editMap, "resize");
        }
    });

    $(document).on("click", "#btnAddMarker", function () {
        bsAdmin.addEditMarker(undefined, undefined, undefined, true);
    });

    $(document).on("click", "#btnSaveMarker", function () {
        var marker;
        var id = $("#hMarkerId").val();
        var markerFind = $("#divFindMarker");
        var markerForm = $("#divSaveMarker");
        var markerItem = $("#divMapEditMarkers").find("#marker" + id);

        titleUk = $("#tbMarkerTitleUk").val();
        titleRu = $("#tbMarkerTitleRu").val();
        titleEn = $("#tbMarkerTitleEn").val();
        descUk = $("#tbMarkerDescUk").val();
        descRu = $("#tbMarkerDescRu").val();
        descEn = $("#tbMarkerDescEn").val();
       

        markerItem.find("input[name='MapPoints[" + id + "].TitleUk']").val(titleUk);
        markerItem.find("input[name='MapPoints[" + id + "].TitleRu']").val(titleRu);
        markerItem.find("input[name='MapPoints[" + id + "].TitleEn']").val(titleEn);
        markerItem.find("input[name='MapPoints[" + id + "].DescriptionUk']").val(descUk);
        markerItem.find("input[name='MapPoints[" + id + "].DescriptionRu']").val(descRu);
        markerItem.find("input[name='MapPoints[" + id + "].DescriptionEn']").val(descEn);

        for (var i = 0; i < bsAdmin.editMapMarkers.length; i++) {
            if (bsAdmin.editMapMarkers[i].id == id)
                marker = bsAdmin.editMapMarkers[i];
        }

        var infowindow = new google.maps.InfoWindow();
        infowindow.setContent(descUk);
        infowindow.open(bsAdmin.editMap, marker);
        markerFind.collapse('show');
        markerForm.collapse('hide');
    });

    //-END-MAP-//

    //-GALLERY-//

    $(document).on("click", ".gallery-edit-item-edit", function () {
        var id = $(this).data("id");
        var img = $("input[name='GalleryItems[" + id + "].Image']").val();

        coreUtils.showCropModal(img, 0, 0, false, function (r) {
            $("input[name='GalleryItems[" + id + "].Image']").val(r.path);
            $("#galleryEditItemImg" + id).attr("src", r.path);            
        });
    });

    $(document).on("click", ".gallery-edit-item-del", function () {
        var id = $(this).data("id");

        $("#galleryEditItem" + id).empty().remove();
    });

    //-END-GALLERY-//
});

//--End Handlers--//


//--CONTENT-MAP--//
bsAdmin.createEditMap = function () {
    if ($("#formMapEdit").length > 0) {
        var mapSearchBox = new google.maps.places.SearchBox($("#tbAddMarkerByPlace")[0]);

        bsAdmin.editMapMarkers = [];

        bsAdmin.editMap = new google.maps.Map(
            $("#divContentMap")[0],
            {
                zoom: parseInt($("#tbMapZoom").val()),
                center: { lat: parseFloat($("#hCenterLat").val().replace(",", ".")), lng: parseFloat($("#hCenterLng").val().replace(",", ".")) },
                zoomControl: false,
                mapTypeControl: false,
                scaleControl: false,
                streetViewControl: false,
                rotateControl: false,
                scrollwheel: false,
            });

        google.maps.event.addListener(bsAdmin.editMap, 'dragend', function () {
            var coords = bsAdmin.editMap.getCenter();

            $("#hCenterLat").val(coords.lat().toString().replace(".", ","));
            $("#hCenterLng").val(coords.lng().toString().replace(".", ","));
        });

        google.maps.event.addListener(mapSearchBox, 'places_changed', function () {
            var place = mapSearchBox.getPlaces()[0];
            var coords = place.geometry.location;
            bsAdmin.editMap.setCenter(coords);
            bsAdmin.addEditMarker(undefined, coords.lat(), coords.lng(), true);
        });

        $("#divMapEditMarkers > .map-edit-marker").each(function () {
            var id = $(this).find("input[name='MapPoints.Index']").val();
            var lat = parseFloat($(this).find("input[name='MapPoints[" + id + "].Lat']").val().replace(",", "."));
            var lng = parseFloat($(this).find("input[name='MapPoints[" + id + "].Lng']").val().replace(",", "."));
            var titleUk = $(this).find("input[name='MapPoints[" + id + "].TitleUk']").val();
            var titleRu = $(this).find("input[name='MapPoints[" + id + "].TitleRu']").val();
            var titleEn = $(this).find("input[name='MapPoints[" + id + "].TitleEn']").val();
            var descUk = $(this).find("input[name='MapPoints[" + id + "].DescriptionUk']").val();
            var descRu = $(this).find("input[name='MapPoints[" + id + "].DescriptionRu']").val();
            var descEn = $(this).find("input[name='MapPoints[" + id + "].DescriptionEn']").val();

            bsAdmin.addEditMarker(id, lat, lng, false, titleRu, titleUk, titleEn, descRu, descUk, descEn);
        });
    }
}

bsAdmin.addEditMarker = function (id, lat, lng, showForm, titleRu, titleUk, titleEn, descRu, descUk, descEn) {
    id = id || coreUtils.randomId(20, ".map-edit-marker > input", "value");
    lat = lat || bsAdmin.editMap.getCenter().lat();
    lng = lng || bsAdmin.editMap.getCenter().lng();
    showForm = showForm;
    descRu = descRu || "";
    descUk = descUk || "";
    descEn = descEn || "";
    titleRu = titleRu || "Новый маркер";
    titleUk = titleUk || "Новий маркер";
    titleEn = titleEn || "New marker";

    var marker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        title: titleUk,
        map: bsAdmin.editMap,
        draggable: true,
        id: id
    });

    if ($("#marker" + id).length == 0) {
        $("#divMapEditMarkers").append(
            "<div class='map-edit-marker' id='marker" + id + "'>" +
                "<input type='hidden' name='MapPoints.Index' value='" + id + "' />" +
                "<input type='hidden' name='MapPoints[" + id + "].Lat' value='" + lat + "' />" +
                "<input type='hidden' name='MapPoints[" + id + "].Lng' value='" + lng + "' />" +
                "<input type='hidden' name='MapPoints[" + id + "].TitleUk' value='"+ titleUk +"' />" +
                "<input type='hidden' name='MapPoints[" + id + "].TitleRu' value='"+ titleRu +"' />" +
                "<input type='hidden' name='MapPoints[" + id + "].TitleEn' value='"+ titleEn +"' />" +
                "<input type='hidden' name='MapPoints[" + id + "].DescriptionUk' value='"+ descUk +"' />" +
                "<input type='hidden' name='MapPoints[" + id + "].DescriptionRu' value='"+ descRu +"' />" +
                "<input type='hidden' name='MapPoints[" + id + "].DescriptionEn' value='"+ descEn +"' />" +
            "</div>");
    }

    marker.addListener('click', function () {
        bsAdmin.showMarkerForm(id);
    });

    marker.addListener('dragend', function () {
        var coords = marker.getPosition();

        $("input[name='MapPoints[" + id + "].Lat']").val(coords.lat().toString().replace(".", ","));
        $("input[name='MapPoints[" + id + "].Lng']").val(coords.lng().toString().replace(".", ","));
    });

    bsAdmin.editMapMarkers.push(marker);

    if (showForm)
        bsAdmin.showMarkerForm(id);
}

bsAdmin.showMarkerForm = function (id, index) {
    var markerFind = $("#divFindMarker");
    var markerSave = $("#divSaveMarker");
    var markerItem = $("#divMapEditMarkers").find("#marker" + id);
    var titleUk = markerItem.find("input[name='MapPoints[" + id + "].TitleUk']").val();
    var titleRu = markerItem.find("input[name='MapPoints[" + id + "].TitleRu']").val();
    var titleEn = markerItem.find("input[name='MapPoints[" + id + "].TitleEn']").val();
    var descUk = markerItem.find("input[name='MapPoints[" + id + "].DescriptionUk']").val();
    var descRu = markerItem.find("input[name='MapPoints[" + id + "].DescriptionRu']").val();
    var descEn = markerItem.find("input[name='MapPoints[" + id + "].DescriptionEn']").val();

    CKEDITOR.instances["tbMarkerDescUk"].setData(descUk);
    CKEDITOR.instances["tbMarkerDescRu"].setData(descRu);
    CKEDITOR.instances["tbMarkerDescEn"].setData(descEn);
    markerSave.find("#tbMarkerTitleUk").val(titleUk);
    markerSave.find("#tbMarkerTitleRu").val(titleRu);
    markerSave.find("#tbMarkerTitleEn").val(titleEn);
    markerSave.find("#hMarkerId").val(id);
    markerFind.collapse('hide');
    markerSave.collapse('show');
}
//--END-CONTENT-MAP--//

bsAdmin.createEditGallery = function () {
    if ($("#divGalleryEditItems").length > 0) {
        $("#divGalleryEditItems > .gallery-edit-item").each(function () {
            var id = $(this).find("input[name='GalleryItems.Index']").val();
            var url = $(this).find("input[name='GalleryItems[" + id + "].Image']").val();
            var titleUk = $(this).find("input[name='GalleryItems[" + id + "].TitleUk']").val();
            var titleRu = $(this).find("input[name='GalleryItems[" + id + "].TitleRu']").val();
            var titleEn = $(this).find("input[name='GalleryItems[" + id + "].TitleEn']").val();

            bsAdmin.addGalleryItem(id, url, titleUk, titleRu, titleEn);
        });

        var elem = $(".gallery-edit-upload");

        bsAdmin.galleryUploader = elem.uploader({
            url: '/api/upload/uploadfile?type=image&directory=doc_files',
            browseButton: elem.find(".btn")[0],
            container: elem.find("div")[0],
            dropElement: elem.find("div")[0],
            chunk: "200kb",
            onAdd: function (u, f) {
                coreUtils.disableButton($("#btnUploadGalleryItems"));
                u.start();
            },
            onUpload: function (u, f, r) {
                if (r && r.status == 200) {
                    var res = $.parseJSON(r.response);
                    bsAdmin.addGalleryItem(undefined, res.path);

                    u.removeFile(f);

                    if (u.files.length == 0) {
                        coreUtils.enableButton($("#btnUploadGalleryItems"));
                        u.refresh();
                    }
                } else {
                    coreUtils.enableButton($("#btnUploadGalleryItems"));
                    return alert('File upload error');
                }
            },
            onError: function () {
                return alert('File upload error');
            }
        })[0];

        Sortable.create($(".gallery-edit-items")[0], {
            handle: "img",
            animation: 150
        });
    }    
}

bsAdmin.addGalleryItem = function(id, url, titleUk, titleRu, titleEn) {
    id = id || coreUtils.randomId(20, ".gallery-item-edit > input", "value");

    titleUk = titleUk || "";
    titleRu = titleRu || "";
    titleEn = titleEn || "";

    if ($("#galleryEditItem" + id).length == 0) {
        $("#divGalleryEditItems").append(
            "<div class='gallery-edit-item card-sm' id='galleryEditItem" + id + "'>" +
                "<input type='hidden' name='GalleryItems.Index' value='" + id + "' />" +
                "<input type='hidden' name='GalleryItems[" + id + "].Image' value='" + url + "' />" +
                "<div class='div-table'>" +
                    "<div class='div-cell gallery-edit-item-img' style='width:120px;height:120px;'><img class='img' id='galleryEditItemImg" + id + "' src='" + url + "' /></div>" +
                    "<div class='div-cell'>" +
                        "<div class='form-group'>" +
                            "<div class='lang-uk'>" +
                                "<label class='control-label'>Підпис фото (ua)</label>" +
                                "<input type='text' class='form-control' placeholder='Підпис фото (ua)' name='GalleryItems[" + id + "].NameUk' value='" + titleUk + "' />" +
                            "</div>" +
                            "<div class='lang-ru'>" +
                                "<label class='control-label'>Підпис фото (ru)</label>" +
                                "<input type='text' class='form-control' placeholder='Підпис фото (ru)' name='GalleryItems[" + id + "].NameRu' value='" + titleRu + "' />" +
                            "</div>" +
                            "<div class='lang-en'>" +
                                "<label class='control-label'>Підпис фото (en)</label>" +
                                "<input type='text' class='form-control' placeholder='Підпис фото (en)' name='GalleryItems[" + id + "].NameEn' value='" + titleEn + "' />" +
                            "</div>" +
                        "</div>" +
                        "<div class='text-right'>" +
                            "<a href='" + url + "' target='_blank' class='link-btn' title='переглянути зображення'>" +
                                "<i class='fa fa-fw fa-eye'></i>" +
                            "</a>" +
                            "<span class='link-btn gallery-edit-item-edit' data-id='" + id + "' title='редагувати зображення'>" +
                                "<i class='fa fa-fw fa-pencil'></i>" +
                            "</span>" +
                            "<span class='link-btn gallery-edit-item-del' data-id='" + id + "' title='видалити зображення'>" +
                                "<i class='fa fa-fw fa-trash'></i>" +
                            "</span>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
            "</div>");
    }
}

//--END-CONTENT--//

bsAdmin.startProcessing = function () {
    clearTimeout(bsAdmin.processingTimer);
    bsAdmin.divProcessing.animate({ height: window.innerHeight }, 300);
}

bsAdmin.endProcessing = function () {
    clearTimeout(bsAdmin.processingTimer);
    bsAdmin.processingTimer = setTimeout(function () {
        bsAdmin.divProcessing.animate({ height: 0 }, 200, function () {
            $("button[type='submit']").prop('disabled', false);
        });
    }, 100);
}








$(document).ready(function () {
    $.fn.modal.defaults.spinner = $.fn.modalmanager.defaults.spinner =
    '<div class="loading-spinner" style="width: 200px; margin-left: -100px;">' +
        '<div class="progress progress-striped active">' +
            '<div class="progress-bar" style="width: 100%;"></div>' +
        '</div>' +
    '</div>';

    //---Modals
    $(document).on("closed", ".remodal-form", function () {
        $(this).find(".remodal-content").empty();
    });

    $(document).on("opened", ".remodal-form", function () {
        coreUtils.createForm($(this).find("form[data-ajax='true']"));
    });

    $(document).on("show.bs.modal", ".modal", function () {
        coreUtils.createForm($(this).find("form[data-ajax='true']"));
    });

    $(document).on("hide.bs.modal", ".modal", function () {
        $(this).find(".modal-body").empty();
    });
    //---End-Modals

    $(document).on('show.bs.dropdown', '.table-responsive', function () {
        $('.table-responsive').css("overflow", "inherit");
    });

    $(document).on('hide.bs.dropdown', '.table-responsive', function () {
        $('.table-responsive').css("overflow", "auto");
    });

    $(document).on("click", ".back-to-list", function (e) {
        window.createListState();
        window.pushState();
    });

    $(document).on("click", ".no-event", function (e) {
        e.preventDefault();
    });

    $(document).on("click", ".input-clear", function () {
        var input = $(this).parents(".input-group").find(".form-control");
        input.prop("readonly", input.hasClass("bs-select") ? true : false).val("");

        if (input.data("related-element"))
            $(input.data("related-element")).val("");

        if (input.hasClass("bs-autocomplete"))
            coreUtils.createAutocomplete(input);

        input.each(function () {
            if ($(this).hasClass("ac-value"))
                coreUtils.createAutocompleteSelect($(this));
        });
    });

    $(document).on("change", "input[type='checkbox'][data-toggle='show']", function () {
        var t = $(this).data("target");

        if ($(this).is(":checked")) {
            $(t).slideDown();
        } else {
            $(t).slideUp();
        }
    });

    $(document).on("click", ".btn-remove-relateditem", function () {
        var t = $(this);
        var tContainer = $(t.parents(".related-items"));

        t.parents(".related-item").remove();
        coreUtils.refreshListIndexes(tContainer);
    });

    $(document).on("submit", "form", function (e) {
        coreUtils.disableButton($(this).find("button[type='submit'], input[type='submit']"));
        
        $(this).validate({
            ignore: ".ignore"
        });

        try {
            if (!$(this).valid()) {
                coreUtils.enableButton($(this).find("button[type='submit'], input[type='submit']"));
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

    $(".bs-daterangepicker").each(function () {
        coreUtils.createDaterangepicker($(this));
    });

    $(".bs-colorpicker").each(function () {
        coreUtils.createColorpicker($(this));
    });

    $(".bs-autocomplete").each(function () {
        coreUtils.createAutocomplete($(this));
    });

    $(".bs-autocompleteselect").each(function () {
        coreUtils.createAutocompleteSelect($(this));
    });

    $(".bs-ckeditor").each(function () {
        coreUtils.createCkeditor($(this));
    });

    $(".bs-mask").each(function () {
        coreUtils.createMask($(this));
    });

    $(".bs-map").each(function () {
        coreUtils.createPlaceMap($(this));
    });
});
