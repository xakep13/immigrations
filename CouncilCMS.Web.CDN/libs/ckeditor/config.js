/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
        { name: 'styles' },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'colors', groups: ['colors'] },
		'/',
		{ name: 'paragraph', groups: ['align', 'list', 'indent', 'blocks', 'bidi', 'paragraph'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'links', groups: ['links'] },
		'/',
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'document', groups: ['document', 'doctools', 'mode'] },
		{ name: 'tools', groups: ['tools'] },
		'/',
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
    ];

    config.toolbar_nano = [
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'Link', 'Unlink', 'cmslink', '-', 'RemoveFormat', 'Source'] },
    ];

    config.toolbar_mini = [
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'TextColor', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'cmslink', '-', 'RemoveFormat', 'Source'] },
    ];

    config.toolbar_table = [
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'TextColor', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'cmslink', 'Image', 'Table', '-', 'RemoveFormat', 'Source'] },
    ];

    config.toolbar_simple = [
		{ name: 'basicstyles1', items: ['Font', 'FontSize', 'Format', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'RemoveFormat', '-', 'Source'] },
        { name: 'basicstyles2', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', '-', 'Bold', 'Italic', 'Underline', '-', 'TextColor', 'BGColor', '-', 'Image', 'Table', 'Iframe', '-', 'NumberedList', 'BulletedList', 'HorizontalRule', '-', 'Outdent', 'Indent', '-', 'Anchor', 'Link', 'Unlink', 'cmslink'] },
    ];

    config.format_tags = "p;h2;h3;h4";
    config.defaultLanguage = LANG;
    config.removeButtons = 'NewPage,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Language,CreateDiv,Flash,Smiley,About,ShowBlocks';
    config.extraPlugins = 'tableresize,lineutils,widgetselection,widget,filetools,button,toolbar,notificationaggregator,uploadwidget,uploadimage,image2,cmslink,nbsp,horizontalrule';
    config.filebrowserUploadUrl = "/api/upload/ckefileupload";
    config.imageUploadUrl = BASE_DOMAIN + "/api/upload/ckeimageuploadnew";    
    config.contentsCss = BASE_DOMAIN + "/content/libs/ckeditor/contents.css";
    config.stylesSet = BASE_DOMAIN + "/content/libs/ckeditor/styles.js";
};