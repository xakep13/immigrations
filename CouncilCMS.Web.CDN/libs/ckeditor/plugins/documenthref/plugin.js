CKEDITOR.plugins.add( 'documenthref', {
    requires: 'widget',
    icons: 'documenthref',
    init: function( editor ) {
        CKEDITOR.dialog.add('documenthref', this.path + 'dialogs/documenthref.js');
        editor.execCommand('documenthref');
        editor.ui.addButton('documenthref',
            {
                label: LOCALS.addDocumentLink,
                icon: this.path + 'icons/documenthref.png',
                command: 'documenthref',
                toolbar: 'links'
            });

        editor.widgets.add('documenthref', {
            //button: 'Вставить ссылку на документ',
            template:
            '<a class="document_href" datahref="" href=""></a>',
            dialog: 'documenthref',

            upcast: function (element) {
                console.log('upcast');
                return element.name == element.hasClass('document_href');
            },

            data: function () {
                var hrefElem = $(this.element.$);
                if(!this.data.docHref){

                }
                hrefElem.attr('href', this.data.docHref) ;
                hrefElem.text(this.data.docText);
                hrefElem.attr('datahref',this.data.docData);

            }
        });
    }
} );