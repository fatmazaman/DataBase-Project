
jQuery(document).ready(function() {
	
	/*
    	Background slideshow
	*/
	$.backstretch([
	  "assets/img/backgrounds/1.jpg", 
	  "assets/img/backgrounds/2.jpg", 
	  "assets/img/backgrounds/3.jpg"
	  ], {duration: 3000, fade: 750});
	
	/*
	    Contact form
	*/
	$('.contact-us').submit(function(e) {
	
	    e.preventDefault();
	    $('.error').remove();
	
	    var postdata = $('.contact-us').serialize();
	    $.ajax({
	        type: 'POST',
	        url: 'assets/contact.php',
	        data: postdata,
	        dataType: 'json',
	        success: function(returned_data) {
	
	            var j = 0;
	            var submit_ok = true;
	
	            $.each(returned_data, function(key, value) {
	                if(value != '') {
	                    $('.contact-us .' + key).focus();
	                    $('.contact-us').append('<div class="error"><span>+</span></div>');
	                    $('.error').css('top', (27 + j*69) + 'px');
	                    $('.error').fadeIn('slow');
	                    submit_ok = false;
	                    return false;
	                }
	                j++;
	            });
	
	            if(submit_ok) {
	                $('.contact-us').append('<p style="display: none;">Thanks for contacting us! We will get back to you very soon.</p>');
	                $('.contact-us input, .contact-us textarea, .contact-us button').remove();
	                $('.contact-us p').fadeIn('slow');
	            }
	        }
	    });
	});

});
