CKEDITOR.plugins.add( 'cmslink', {
    requires: 'widget',
    icons: 'cmslink',
    init: function( editor ) {
        CKEDITOR.dialog.add('cmslink', this.path + 'dialogs/cmslink.js');
        editor.execCommand('cmslink');
        editor.ui.addButton('cmslink',
            {
                label: LOCALS.addDocumentLink,
                icon: this.path + 'icons/icn.png',
                command: 'cmslink',
                toolbar: 'links'
            });

        editor.widgets.add('cmslink', {
            //button: 'Вставить ссылку на документ',
            template:
            '<a class="bs-link" datahref="" href=""></a>',
            dialog: 'cmslink',

            upcast: function (element) {
                return element.name == element.hasClass('bs-link');
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