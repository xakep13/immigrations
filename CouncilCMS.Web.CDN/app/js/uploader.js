(function ($) {
    $.fn.uploader = function (options) {
        var uploaders = [];

        var _o = $.extend({
            url: '/api/upload/uploadimage',
            browseButton: null,
            container: '.upload-area',
            maxFileSize: '200mb',
            dropElement: '.upload-area',
            extensions: "*",
            multiSelection: true,
            chunk: 0,
            onAdd: null,
            onChunkUpload: null,
            onUpload: null,
            onError: null,
            onChange: null,
        }, options);

        function _init(selector) {
            return selector.each(function (i) {
                uploaders[i] = _createUploader($(this));
                console.log("uploader-created " + i);
            });
        }

        function _createUploader(element) {
            var upl = new plupload.Uploader({
                runtimes: 'html5,html4',
                url: _o.url,
                browse_button: _o.browseButton || element.find(".btn-upload")[0],
                container: element.find(_o.container)[0],
                drop_element: element.find(_o.dropElement)[0],
                max_file_size: _o.maxFileSize,
                resize: { width: 1920, quality: 95 },
                chunk_size: _o.chunk,
                multi_selection: _o.multiSelection,
                filters: [{ title: "all", extensions: _o.extensions }],
                unique_names: true,                
                init: {
                    StateChaged: function (up) {
                        if (_o.onChange)
                            _o.onChange(up);
                    },
                    FilesAdded: function (up, files) {
                        if (_o.onAdd)
                            _o.onAdd(up, files);
                        else
                            return up.start();
                    },
                    FileUploaded: function (up, file, r) {
                        if (_o.onUpload)
                            _o.onUpload(up, file, r);
                    },
                    ChunkUploaded: function (up, file, info) {
                        if (_o.onChunkUpload)
                            _o.onChunkUpload(up, file, info);
                    },
                    Error: function (up, err) {
                        if (_o.onError)
                            _o.onError(up, err);
                    }
                },
            });

            upl.init();

            return upl;
        }

        _init(this);

        return uploaders;
    };
}(jQuery));