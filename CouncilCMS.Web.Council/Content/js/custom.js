$(document).ready(function() {
    $(".how_this_work_slider").owlCarousel({
    	items:1,
    	autoplay: true,
        loop:true,
        nav: true, 
        dots:true,
        navText:['<img src="img/right_black.png">','<img src="img/right_black.png">'],
    });
    $('.cancel_icon').click(function(){
    	$('.alert_popup').remove();
    });
    $('.mobile_sandvich').click(function(){
        $('.mobile_collapsed').show();
    });
     $('.close_mobile_collapsed').click(function(){
        $('.mobile_collapsed').hide();
        return false;
    });
     $('select').chosen({
        disable_search:true,
        width:"100%" ,
     });
     if (($("#add_photo").length > 0)){
        $("#add_photo").dropzone({ url: "php/index.php" , clickable: "#add_photo .imit_file", previewsContainer: ".previewsContainer", previewTemplate: "<div class='added_file_row'><p data-dz-name></p><a href='#' data-dz-remove >Cкасувати</a></div>"});
    }
    $('body').on('submit', 'form',function(){
        $('.required input').each(function(){

            if($(this).val()=='')
            {
                $(this).addClass('required_input').siblings('.required_message').show();
                return false;
            }
        });
    });    
   
});
	