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
   
});
	