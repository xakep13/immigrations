function coreUtils() { };

coreUtils.googleMapApiLoaded = false;

coreUtils.randomId = function (length, selector, attribute)
{
    var abc = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    var id = '';
    
    attribute = attribute || "id";
    length = length || 20;

    for (var i = 0; i < length; i++) {
        id += abc.charAt(Math.floor(Math.random() * abc.length));
    }

    if (selector) {
        while ($(selector + "[" + attribute + "=" + id + "]").length > 0)
            id = coreUtils.randomId(length, selector, attribute);
    }

    return id;
}

coreUtils.isMobileBrowser = function () {
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
}

coreUtils.getUrlParam = function (paramName) {
    var result = null, param = [];
    location.search.substr(1).split("&")
        .forEach(function (item) {
            param = item.split("=");
            if (param[0] === paramName)
                result = decodeURIComponent(param[1]);
        });

    return result;
}

coreUtils.paramsToObject = function (params, dest) {
    if (params) {
        var param = [];
        dest = dest || {};

        params.split("&").forEach(function (item) {
            param = item.split("=");
            dest[param[0].toLowerCase()] = decodeURIComponent(param[1]);
        });

        return dest;
    }
    else
        return null;
}

coreUtils.objectToParams = function (obj) {
    var result = "";
   
    for (var prop in obj) {
        if (obj.hasOwnProperty(prop)) {
            result += prop.toLowerCase() + "=" + obj[prop] + "&";
        }
    }

    if (result.length > 0)
        return result.substring(0, result.length - 1);
    else
        return null;
}

coreUtils.formToParams = function (elem, input) {
    if (elem) {
        var result = {};

        elem.find(input).each(function () {
            var t = $(this);

            if (t.val()) {
                result[t.attr("name").toLowerCase()] = t.val();
            }
        });

        return result;
    }

    return null;
}

coreUtils.initMapApi = function () {
    coreUtils.googleMapApiLoaded = true;
}

coreUtils.createForm = function (elem) {
    var form = { maps: [] };
    elem = elem || $("form");

    var formSelect = elem.find(".bs-select");
    var formDatepicker = elem.find(".bs-datepicker");
    var formDaterangepicker = elem.find(".bs-daterangepicker");
    var formColorpicker = elem.find(".bs-colorpicker");
    var formUploader = elem.find(".upload-panel");
    var formMobileMask = elem.find(".bs-mobile");
    var formCkeditor = elem.find(".bs-ckeditor");
    var formAutocomlete = elem.find(".bs-autocomplete");
    var formAutocomleteSelect = elem.find(".bs-autocompleteselect");
    var formMask = elem.find(".bs-mask");
    var formMap = elem.find(".bs-map");

    elem.find(".help-tooltip").popover();

    if (formSelect && formSelect.length > 0) {
        formSelect.each(function () {
            coreUtils.createSelect($(this));
        });
    }

    if (formDatepicker && formDatepicker.length > 0) {
        formDatepicker.each(function () {
            coreUtils.createDatepicker($(this));
        });
    }

    if (formDaterangepicker && formDaterangepicker.length > 0) {
        formDaterangepicker.each(function () {
            coreUtils.createDaterangepicker($(this));
        });
    }

    if (formColorpicker && formColorpicker.length > 0) {
        formColorpicker.each(function () {
            coreUtils.createColorpicker($(this));
        });
    }

    if (formUploader && formUploader.length > 0) {
        formUploader.each(function () {
            coreUtils.createUploader($(this));
        });
    }

    if (formCkeditor && formCkeditor.length > 0) {
        formCkeditor.each(function () {
            coreUtils.createCkeditor($(this));
        });
    }

    if (formAutocomlete && formAutocomlete.length > 0) {
        formAutocomlete.each(function () {
            coreUtils.createAutocomplete($(this));
        });
    }

    if (formAutocomleteSelect && formAutocomleteSelect.length > 0) {
        formAutocomleteSelect.each(function () {
            coreUtils.createAutocompleteSelect($(this));
        });
    }

    if (formMask && formMask.length > 0) {
        formMask.each(function () {
            coreUtils.createMask($(this));
        });
    }

    if (formMap && formMap.length > 0) {
        formMap.each(function (i) {
            form.maps[i] = coreUtils.createPlaceMap($(this));
        });
    }
    
    coreUtils.revalidateForm(elem);
}

coreUtils.revalidateForm = function (elem) {
    elem = elem || $("form");

    elem.removeData('validator');
    elem.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse('form');
}

coreUtils.resetForm = function (elem) {
    $(elem).find("input,textarea,select").each(function () {
        var tinput = $(this);

        if (tinput.data("default-val"))
            tinput.val(tinput.data("default-val"));
        else
            tinput.val("");

        if (tinput.hasClass("bs-select"))
            tinput.selectpicker("refresh");

    });
}

coreUtils.geocodeMarker = function (place, geocoder, map, marker) {
    geocoder.geocode({ 'address': place }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            var coords = results[0].geometry.location;
            marker.setPosition(coords);
            map.setCenter(coords);
            new google.maps.event.trigger(marker, 'dragend');
        }
    });
}

coreUtils.createPlaceMap = function (elem) {
    if (coreUtils.googleMapApiLoaded) {
        var mapInst = {}

        var mapContainer = elem.find(".map-container");
        var latInput = elem.find(".input-lat");
        var lngInput = elem.find(".input-lng");
        var searchInput = elem.find(".map-search");
        var center = new google.maps.LatLng(latInput.val().replace(",", "."), lngInput.val().replace(",", "."));
        
        var map = new google.maps.Map(mapContainer[0], {
            zoom: mapContainer.data("zoom"),
            center: center,
            zoomControl: true,
            mapTypeControl: false,
            scaleControl: false,
            streetViewControl: false,
            rotateControl: false
        });

        var marker = new google.maps.Marker({
            position: center,
            map: map,
            draggable: true,
        });

        var geocoder = new google.maps.Geocoder();
        var searchBox = new google.maps.places.SearchBox(searchInput[0]);
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(searchInput[0]);

        marker.addListener('dragend', function () {
            var coords = marker.getPosition();
            map.setCenter(coords);
            latInput.val(coords.lat().toString().replace(".", ","));
            lngInput.val(coords.lng().toString().replace(".", ","));
        });

        google.maps.event.addListener(searchBox, 'places_changed', function () {
            var place = searchBox.getPlaces()[0];
            var coords = place.geometry.location;
            marker.setPosition(coords);
            map.setCenter(coords);
            latInput.val(coords.lat().toString().replace(".", ","));
            lngInput.val(coords.lng().toString().replace(".", ","));
        });

        mapInst.map = map;
        mapInst.marker = marker;
        mapInst.geocoder = geocoder;
        mapInst.latInput = latInput;
        mapInst.lngInput = lngInput;

        return mapInst;
    }
    else {
        setTimeout(500, function () { coreUtils.createPlaceMap(elem); });
    }

    
}


coreUtils.createCkeditor = function (elem) {
    var height = elem.data("height") || 300;
    var bodyClass = elem.data("body-class") || "text";
    var toolbar = elem.data("toolbar") || "mini";

    elem.ckeditor(function () { }, { height: height + 'px', enterMode: CKEDITOR.ENTER_P, bodyClass: bodyClass, toolbar: toolbar });
}

coreUtils.createColorpicker = function (elem) {
    elem.find("option").each(function () {
        var t = $(this);
        t.data("content", "<i class ='fa fa fa-fw fa-square' style='color:" + t.val() + "'></i>&nbsp;" + t.text());
    });

    elem.selectpicker({
        size: 10,
    });
}

coreUtils.createSelect = function (elem) {
    var isLiveSearch = elem.data("live-search") || (elem.find("option").length > 20 ? true : false);

    elem.selectpicker({
        liveSearch: isLiveSearch,
        mobile: coreUtils.isMobileBrowser(),
        size: 7,
        iconBase: 'fa',
        tickIcon: 'fa-check',
        container: 'body'
    });
}

coreUtils.createMask = function (elem) {
    var mask = elem.data("mask") || "+38 (999) 999-99-99";
    var placeholder = elem.attr("placeholder") || "+38 (ХХХ) ХХХ-ХХ-ХХ"

    elem.mask(mask, { placeholder: placeholder });
}

coreUtils.refreshAllSelects = function () {
    $(".bs-select").each(function () {
        $(this).selectpicker("destroy");
        coreUtils.createSelect($(this));
    });
}

coreUtils.refreshSelect = function (elem) {
    elem.selectpicker("destroy");
    coreUtils.createSelect(elem);
}

coreUtils.createDatepicker = function (elem, locale, format) {
    try { elem.data("DateTimePicker").destroy(); }
    catch (ex) { }

    format = format || elem.data("format") || "DD.MM.YYYY";

    elem.prop("readonly", true);

    elem.datetimepicker({
        format: format,
        locale: locale || "uk",
        ignoreReadonly: true,
        showClear: true,
        useCurrent: false,
        defaultDate: false,
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down",
            previous: 'fa fa-arrow-left',
            next: 'fa fa-arrow-right',
            clear: 'fa fa-trash',
            close: 'fa fa-times',
        },
    });

    if (elem.data("to-element")) {
        elem.on("dp.change", function (e) {
            $(elem.data("to-element")).data("DateTimePicker").minDate(e.date);
        });
    }

    if (elem.data("from-element")) {
        elem.on("dp.change", function (e) {
            $(elem.data("to-element")).data("DateTimePicker").maxDate(e.date);
        });
    }
}

coreUtils.createTimepicker = function (elem, locale, format) {
    try { elem.data("DateTimePicker").destroy(); }
    catch (ex) { }

    format = format || elem.data("format") || "HH:mm";

    elem.prop("readonly", true);

    elem.datetimepicker({
        format: format,
        locale: locale || "uk",
        ignoreReadonly: true,
        showClear: true,
        useCurrent: false,
        defaultDate: false,
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down",
            previous: 'fa fa-arrow-left',
            next: 'fa fa-arrow-right',
            clear: 'fa fa-trash',
            close: 'fa fa-times',
        },
    });

    if (elem.data("to-element")) {
        elem.on("dp.change", function (e) {
            $(elem.data("to-element")).data("DateTimePicker").minDate(e.date);
        });
    }

    if (elem.data("from-element")) {
        elem.on("dp.change", function (e) {
            $(elem.data("to-element")).data("DateTimePicker").maxDate(e.date);
        });
    }
}

coreUtils.createDaterangepicker = function (elem, format) {
    var format = format || "DD.MM.YYYY";

    elem.daterangepicker({
        "autoApply": true,
        "ranges": {
            'Сьогодні': [moment(), moment()],
            'Вчора': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            '7 днів': [moment().subtract(6, 'days'), moment()],
            '30 днів': [moment().subtract(29, 'days'), moment()],
            'Цей місяь': [moment().startOf('month'), moment()],
            'Цей рік': [moment().startOf('year'), moment()]
        },
        "locale": {
            "format": format || "DD.MM.YYYY",
            "separator": " - ",            
            "fromLabel": "From",
            "toLabel": "To",
            cancelLabel: 'Clear',
            //"daysOfWeek": [
            //    "Su",
            //    "Mo",
            //    "Tu",
            //    "We",
            //    "Th",
            //    "Fr",
            //    "Sa"
            //],
            "monthNames": [
               "Січень",
               "Лютий",
               "Березень",
               "Квітень",
               "Травень",
               "Червень",
               "Липень",
               "Серпень",
               "Вересень",
               "Жовтень",
               "Листопад",
               "Грудень"
            ],
            "firstDay": 1
        },
        "showCustomRangeLabel": false,
        "alwaysShowCalendars": true,
        "opens": "left",
        "autoUpdateInput": false,
        "showDropdowns": true,
    });

    elem.on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format(format) + ' - ' + picker.endDate.format(format));
    });

    elem.on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}

coreUtils.createUploader = function (elem, link) {
    console.log("create uploader");
    
    var tImg = elem.find(".img-upload");
    var tInp = elem.find(".form-control");
    var tUpl = elem.find(".upload-area");
    var tModal = elem.find(".upload-remodal");
    var btnDelete = elem.find(".btn-delete");
    var btnEdit = elem.find(".btn-edit");
    var btnView = elem.find(".btn-view");
    var cWidth = elem.data("crop-width") || 0;
    var cHeight = elem.data("crop-height") || 0;
    var oldFile = tInp.val();
    var upl;

    link = link || elem.data("upload-link") || "/api/upload/uploadfile";

    if (elem.data("crop-width") > 0 && elem.data("crop-height") > 0) {
        link = link + (link.indexOf('?') > 0 ? "&" : "?") + "cropwidth=" + elem.data("crop-width") + "&cropheight=" + elem.data("crop-height") + "&saveoriginal=true&crop=true";
    }

    if (elem.data("max-width") > 0) {
        link = link + (link.indexOf('?') > 0 ? "&" : "?") + "limitWidth=" + elem.data("max-width");
    } 

    if (elem.data("max-height") > 0) {
        link = link + (link.indexOf('?') > 0 ? "&" : "?") + "limitHeight=" + elem.data("max-height");
    }
    
    upl = elem.uploader({
        url: link,
        chunk: "100kb",
        onAdd: function (u, f) {
            oldFile = tInp.val();
            tImg.attr("src", tImg.data("uploading-img"));
            upl.settings.url = link + (link.indexOf('?') > 0 ? "&" : "?") + "oldFile=" + encodeURI(oldFile);
            upl.start();
        },
        onChunkUpload: function (u, f, i)
        {
            tInp.val(Math.round(parseFloat(i.offset / i.total) * 100) + "%");
        },
        onUpload: function (u, f, r) {
            if (r && r.status == 200) {
                var res = $.parseJSON(r.response);
                tUpl.addClass("uploaded");
                tImg.attr('src', res.preview).data("original", res.original);
                tInp.val(res.path);
                btnDelete.show();
                btnEdit.show();
                btnView.show().attr("href", res.path);

                //if (res.crop) {
                //    coreUtils.showCropModal(res.original, cWidth, cHeight, true, function (cropRes) {
                //        tUpl.addClass("uploaded");
                //        tImg.attr('src', cropRes.preview);
                //        tInp.val(cropRes.path);
                //        btnDelete.show();
                //        btnEdit.show();
                //        btnView.show().attr("href", cropRes.path);
                //    });
                //}
            } else {
                tImg.attr("src", tImg.data("nopic-img")).data("original", "");
                tInp.val("");
                tUpl.removeClass("uploaded");
                btnDelete.hide();
                btnEdit.hide();
                btnView.hide().attr("href", "");
                return alert('File upload error');
            }

            upl.removeFile(f);
            upl.refresh();
        },
        onError: function () {
            tImg.attr("src", tImg.data("nopic-img")).data("original", "");
            tInp.val("");
            tUpl.removeClass("uploaded");

            upl.splice();
            upl.refresh();

            return alert('File upload error');
        }
    })[0];

    btnDelete.click(function () {
        btnDelete.hide();
        btnEdit.hide();
        btnView.hide().attr("href", "");
        tImg.attr("src", tImg.data("nopic-img")).data("original", "");
        tInp.val("");
        tUpl.removeClass("uploaded");
    });

    if (btnEdit) {
        btnEdit.click(function () {
            coreUtils.showCropModal(tImg.data("original"), cWidth, cHeight, (cWidth > 0 && cHeight > 0), function (r) {
                tUpl.addClass("uploaded");
                tImg.attr('src', r.preview).data("original", r.original);
                tInp.val(r.path);
                btnDelete.show();
                btnEdit.show();
                btnView.show().attr("href", r.path);
            });
        });
    }
    
    return upl;
}

coreUtils.createAutocomplete = function (elem) {
    if (elem.val()) {
        elem.prop("readonly", true);
    } else {
        elem.prop("readonly", false);
        elem.autocomplete({
            serviceUrl: elem.data("remote"),
            paramName: 'query',
            minChars: 3,
            deferRequestBy: 300,
            orientation: "auto",
            showNoSuggestionNotice: true,
            noSuggestionNotice: LOCALS.noResults,
            appendTo: elem.parents(".form-group"),
            forceFixPosition: true,
            autoSelectFirst: true,
            preventBadQueries: false,
            transformResult: function (response) {
                return {
                    suggestions: $.map($.parseJSON(response), function (dataItem) {
                        return { value: dataItem.DisplayText, data: dataItem.Value };
                    })
                };
            },
            onSearchStart: function () {
                elem.parent().find(".input-clear > .fa").removeClass("fa-times").addClass("fa-spin fa-refresh");
            },
            onSearchComplete: function () {
                elem.parent().find(".input-clear > .fa").removeClass("fa-spin fa-refresh").addClass("fa-times");
            },
            onSelect: function (suggestion) {
                elem.prop("readonly", true);
                elem.autocomplete().dispose();

                var relElem = elem.data("related-element");
                var onSelect = elem.data("on-select");

                if (relElem)
                    $(relElem).val(suggestion.data);

                if (onSelect)
                    eval(onSelect);
            }
        });
    }
}

coreUtils.createAutocompleteSelect = function (elem) {
    elem.prop("readonly", true);
    console.log("from cdn");
    var acInput;
    
    if (!elem.hasClass("ac-value")) {
        acInput = $("<input/>");

        acInput.attr("class", elem.attr("class") + " ac-text").attr("name", "ac" + elem.attr("name")).attr("placeholder", elem.attr("placeholder") ? elem.attr("placeholder") : "").val(elem.data("text"));
        acInput.on("blur", function () { elem.trigger("blur"); });
        acInput.insertBefore(elem);
        elem.attr("placeholder", "");
        elem.addClass("ac-value");
    } else {
        acInput = elem.parent().find(".ac-text");
    }

    if (elem.val()) {
        acInput.prop("readonly", true);
    } else {
        acInput.prop("readonly", false);
        acInput.autocomplete({
            serviceUrl: elem.data("remote"),
            paramName: 'query',
            minChars: 3,
            deferRequestBy: 300,
            orientation: "auto",
            showNoSuggestionNotice: true,
            noSuggestionNotice: LOCALS.noResults,
            appendTo: elem.parents(".form-group"),
            forceFixPosition: true,
            autoSelectFirst: true,
            preventBadQueries: false,
            transformResult: function (response) {
                return {
                    suggestions: $.map($.parseJSON(response), function (dataItem) {
                        return { value: dataItem.DisplayText, data: dataItem.Value };
                    })
                };
            },
            onSearchStart: function () {
                elem.parent().find(".input-clear > .fa").removeClass("fa-times").addClass("fa-spin fa-refresh");
            },
            onSearchComplete: function () {
                elem.parent().find(".input-clear > .fa").removeClass("fa-spin fa-refresh").addClass("fa-times");
            },
            onSelect: function (suggestion) {
                elem.val(suggestion.data);
                elem.trigger("blur");

                acInput.prop("readonly", true);
                acInput.autocomplete().dispose();

                var onSelect = elem.data("on-select");

                if (onSelect)
                    eval(onSelect);
            }
        });
    }
}

coreUtils.showCropModal = function (src, cropWidth, cropHeight, fixedCrop, onCrop) { 
    try {
        $("#divCrop").empty().remove();
    } catch (ex) { }
    
    var modal =
        "<div id='divCrop' class='modal modal-800 fade' tabindex='-1' data-width='800' style='display:none;'>" +
            "<div class='modal-body'><div class='row'>" +
                "<div class='col-sm-7'><div class='cropper-bg'><div style='background-color:rgba(0,0,0,.5);height:450px;'><div class='crop-form form-group' style='position:relative; max-width:420px; height:450px;' style='display:none;'>" +
                    "<img style='width:100%;' src='" + src + "' />" +
                 "</div></div></div></div>" +
                 "<div class='col-sm-5'>" +
                    "<div class='well'>" +
                        "<label class='control-label'>Співвідношення сторін</label>" +
                        "<div class='row ac-btns'>" +
                            "<div class='col-sm-8'><div class='form-group'><button type='button' data-width='0' data-height='0' class='btn btn-block btn-default " + (cropWidth == 0 && cropHeight == 0 ? "active" : "") + " btn-crop-ac-free'" + (fixedCrop ? " disabled" : "") + ">довільно</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='400' data-height='400' class='btn btn-block btn-default " + (cropWidth / cropHeight == 1 / 1 ? "active" : "") + " btn-crop-ac-1-1'" + (fixedCrop ? " disabled" : "") + ">1:1</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='400' data-height='267' class='btn btn-block btn-default " + (cropWidth / cropHeight == 3 / 2 ? "active" : "") + " btn-crop-ac-3-2'" + (fixedCrop ? " disabled" : "") + ">3:2</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='267' data-height='400' class='btn btn-block btn-default " + (cropWidth / cropHeight == 2 / 3 ? "active" : "") + " btn-crop-ac-2-3'" + (fixedCrop ? " disabled" : "") + ">2:3</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='400' data-height='300' class='btn btn-block btn-default " + (cropWidth / cropHeight == 4 / 3 ? "active" : "") + " btn-crop-ac-4-3'" + (fixedCrop ? " disabled" : "") + ">4:3</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='300' data-height='400' class='btn btn-block btn-default " + (cropWidth / cropHeight == 3 / 4 ? "active" : "") + " btn-crop-ac-3-4'" + (fixedCrop ? " disabled" : "") + ">3:4</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='400' data-height='225' class='btn btn-block btn-default " + (cropWidth / cropHeight == 16 / 9 ? "active" : "") + " btn-crop-ac-16-9'" + (fixedCrop ? " disabled" : "") + ">16:9</button></div></div>" +
                            "<div class='col-sm-4'><div class='form-group'><button type='button' data-width='225' data-height='400' class='btn btn-block btn-default " + (cropWidth / cropHeight == 9 / 16 ? "active" : "") + " btn-crop-ac-9-16'" + (fixedCrop ? " disabled" : "") + ">9:16</button></div></div>" +
                        "</div><hr />" +
                        "<div class='form-group'>" +
                            "<label class='control-label'>Розмір вихідного зображення</label>" +
                            "<div class='form-group'>" +
                                "<div class='radio'><label><input type='radio' " + (fixedCrop ? "disabled" : "checked") + " name='cropSizeType' class='crop-size-type' value='auto'/><span>Відповідно обраній області</span></label></div>" +
                                "<div class='radio'><label><input type='radio' " + (fixedCrop ? "checked disabled" : "") + " name='cropSizeType' class='crop-size-type' value='manual'/><span>Вказати вручну</span></label></div>" +
                            "</div>" +
                        "</div>" +
                        "<div>" +
                            "<div class='form-group'><div class='input-group'><span class='input-group-addon' style='width:80px;'>Ширина:</span><input type='text' " + (fixedCrop ? "disabled" : "readonly") + " class='form-control crop-width' name='tbW' data-ac-width='" + cropWidth + "' value='" + cropWidth + "' /><span class='input-group-addon'>px</span></div></div>" +
                            "<div><div class='input-group'><span class='input-group-addon' style='width:80px;'>Висота:</span><input type='text' " + (fixedCrop ? "disabled" : "readonly") + " class='form-control crop-height' name='tbH' data-ac-height='" + cropHeight + "' value='" + cropHeight + "' /><span class='input-group-addon'>px</span></div></div>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
            "</div>" +
            "<div class='modal-footer'>" +
                "<button type='button' class='btn btn-default' data-dismiss='modal'>Закрити</button>" +
                "<button type='button' class='btn btn-primary btn-crop'>Зберегти</button>" +
            "</div></div>" +
        "</div>";

    $("body").append(modal);
   
    $("#divCrop").modal({ backdrop: 'static', keyboard: false });

    var data = coreUtils.createCropBox(cropWidth, cropHeight, fixedCrop);

    $("#divCrop").find(".modal-footer .btn-crop").on("click", function () {
        var postUrl = "/api/upload/ResizeImage?path=" + src + "&width=" + data.w + "&height=" + data.h + "&x=" + data.x + "&y=" + data.y;

        if ($(".crop-size-type:checked").val() == "manual")
            postUrl += "&resizeWidth=" + $(".crop-width").val() + "&resizeHeight=" + $(".crop-height").val();

        $.post(postUrl, function (res) {
            onCrop(res);
            $("#divCrop").modal('hide');
        });
    });

    $(".ac-btns button").click(function () {
        $(".ac-btns .active").removeClass("active");
        $(this).addClass("active");

        data = coreUtils.createCropBox($(this).data("width"), $(this).data("height"), fixedCrop);
    });
    
    $(".crop-size-type").on("change", function () {
        data = coreUtils.createCropBox(data.cW, data.cH, fixedCrop);
    });
}

coreUtils.createCropBox = function (cropWidth, cropHeight, disableButtons) {
    var x = 0;
    var y = 0;
    var w = cropWidth;
    var h = cropHeight;
    var canvasData, cropBoxData;
    var cropForm = $("#divCrop").find(".crop-form");
    var cropImg = $("#divCrop").find("img");
    var cType = $("#divCrop").find(".crop-size-type:checked").val();
    var cwInput = $("#divCrop").find(".crop-width");
    var chInput = $("#divCrop").find(".crop-height");
    var cropData = { x: 0, y: 0, w: cropWidth, h: cropHeight, cW: cropWidth, cH: cropHeight };

    cropForm.fadeOut(100, function () {
        try {
            cropImg.cropper("destroy");
        } catch (ex) { }

        cropImg.one("load", function () {
            cropImg.cropper({
                viewMode: 1,
                aspectRatio: (cropHeight > 0 && cropWidth > 0) ? (cropWidth / cropHeight) : NaN,
                movable: false,
                zoomable: false,
                rotatable: false,
                scalable: false,
                minContainerWidth: 420,
                minContainerHeight: 450,
                maxContainerHeight: 450,
                ready: function () {
                    cropImg.cropper('setCanvasData', canvasData);
                    cropImg.cropper('setCropBoxData', cropBoxData);
                    cropForm.fadeIn(100);
                },
                crop: function (e) {
                    cropData.x = Math.round(e.x) > 0 ? Math.round(e.x) : 0;
                    cropData.y = Math.round(e.y) > 0 ? Math.round(e.y) : 0;
                    cropData.w = Math.round(e.width) - 1;
                    cropData.h = Math.round(e.height) - 1;

                    if (cType == "auto") {
                        cwInput.val(Math.round(e.width));
                        chInput.val(Math.round(e.height));
                    }
                }
            });
        }).each(function () {
            if (this.complete) $(this).load();
        });

        if (cType == "manual") {
            cwInput.val(cropWidth).data("ac-width", cropWidth).prop("readonly", false);
            chInput.val(cropHeight).data("ac-height", cropHeight).prop("readonly", false);

            if (cropWidth > 0 && cropHeight > 0) {
                cwInput.on("change", function () {
                    chInput.val(Math.round((parseInt(cwInput.val()) * cropHeight) / cropWidth));
                });

                chInput.on("change", function () {
                    cwInput.val(Math.round((parseInt(chInput.val()) * cropWidth) / cropHeight));
                });
            }
        } else {
            cwInput.data("ac-width", cropWidth).prop("readonly", true).unbind("change");
            chInput.data("ac-height", cropHeight).prop("readonly", true).unbind("change");
        }
    });

    return cropData;
}

coreUtils.refreshListIndexes = function (elem, parentPrefix) {
    parentPrefix = parentPrefix || "";

    var prefix = parentPrefix + elem.data("prefix");

    elem.children(".item").each(function (i) {
        var t = $(this);
        var childPrefix = prefix + "[" + i + "].";

        t.find(".input").each(function () {
            if (!$(this).attr("data-generic-name")) {
                $(this).attr("data-generic-name", $(this).attr("name"));
            }

            $(this).attr("name", prefix + "[" + i + "]." + $(this).attr("data-generic-name"));
        });

        t.find("div[data-prefix]").each(function () {
           coreUtils.refreshListIndexes($(this), childPrefix);
        });
    });
}

coreUtils.disableButton = function (elem) {
    elem.prop("disabled", true).bind('click', false).addClass('disabled');
    elem.find(".fa").hide();
    elem.prepend("<span class='ld-spin'><i class='fa fa-fw fa-circle-o-notch fa-spin'></i>&nbsp;</span>");
}

coreUtils.enableButton = function (elem) {
    elem.prop("disabled", false).unbind('click', false).removeClass('disabled');
    elem.find(".ld-spin").remove();
    elem.find(".fa").show();
}

coreUtils.createPage = function (elem) {
    elem.find(".slide-gallery").fotorama({ nav: 'thumbs' });
    coreUtils.createPageMaps(elem);
    elem.find(".content-file").find("a").removeAttr('download').attr('target', '_blank');
}

coreUtils.createPageMaps = function (elem) {
    if (coreUtils.googleMapApiLoaded) {
        elem.find('.content-map').each(function (i) {
            coreUtils.createPageMap($(this));
        });
    }
    else {
        setTimeout(function () { coreUtils.createPageMaps(elem); }, 500);
    }
}

coreUtils.createPageMap = function (elem) {
    var map;
    var infowindow = null;
    var cont = $(elem).find(".content-map-container");
    var centerLat = parseFloat(cont.data('lat').replace(",", "."));
    var centerLng = parseFloat(cont.data('lng').replace(",", "."));
    var zoom = cont.data('zoom');
    var mapHeight = cont.data('height') || 400;

    cont.css({ height: mapHeight });

    map = new google.maps.Map(cont[0], {
        zoom: zoom,
        center: { lat: centerLat, lng: centerLng },
        rotateControl: false,
        scrollwheel: false,
    });

    elem.find(".map-point").each(function (i) {
        var tp = $(this);
        
        var marker = new google.maps.Marker({
            
            position: { lat: parseFloat(tp.data('lat').toString().replace(",", ".")), lng: parseFloat(tp.data('lng').toString().replace(",", ".")) },
            title: tp.data('title'),
            desc: tp.data('desc'),
            map: map
        });

        marker.addListener('click', function () {
            if (infowindow) {
                infowindow.close();
            }
            infowindow = new google.maps.InfoWindow();
            infowindow.setContent(this.desc);
            infowindow.open(map, this);
        });
    });
}