CKEDITOR.dialog.add( 'cmslink', function( editor ) {
    return {
        title: LOCALS.addDocumentLink,
        minWidth: 500,
        minHeight: 200,
        contents: [
            {
                id: 'info',
                elements: [
                    {
                        id:'HrefText',
                        type:'text',
                        label: "Текст посилання",
                        setup: function(widget) {
                            this.setValue(widget.data.docText);
                        },
                        commit: function(widget) {
                            widget.setData('docText', this.getValue());
                        }
                    },
                    {
                        id: 'docData',
                        type: 'text',
                        label: "Пошук на порталі",
                        className: 'add-cms-autocomplete',
                        setup: function (widget) {
                            this.setValue(widget.data.docData);
                        },
                        commit: function (widget) {
                            widget.setData('docData', this.getValue());
                        }
                    },
                    {
                        id: 'href',
                        type: 'text',
                        label: "Посилання",
                        className: 'add-cms-link',
                        setup: function (widget) {
                            this.setValue(widget.data.docHref);
                        },
                        commit: function (widget) {
                            widget.setData('docHref', this.getValue());
                        }
                    },

                ]
            }
        ],
        buttons: [
            CKEDITOR.dialog.okButton,
            CKEDITOR.dialog.cancelButton,
        ],
        onLoad: function (widget) {
            $(".add-cms-link input").prop("readonly", true);
            $('.add-cms-autocomplete input').autocomplete({
                serviceUrl: BASE_DOMAIN + '/uk/controlpanel/ajax/findcmslink/',
                paramName: 'query',
                minChars: 3,
                orientation: "auto",
                showNoSuggestionNotice: true,
                noSuggestionNotice: LOCALS.noResults,
                appendTo: $('.add-cms-autocomplete input').parent(),
                forceFixPosition: true,
                autoSelectFirst: true,
                transformResult: function (response) {
                    return {
                        suggestions: $.map($.parseJSON(response), function (dataItem) {
                            return { value: dataItem.DisplayText, data: dataItem.Value };
                        })
                    };
                },
                onSelect: function (suggestion) {
                    $(".add-cms-link input").val(suggestion.data);
                }
            });
        }
    };
} );