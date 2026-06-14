// blazor-interop.js
// Tái khởi tạo tất cả jQuery plugins sau mỗi lần Blazor navigate

window.reinitPage = function () {

    // ---- Destroy existing owlCarousel instances trước khi init lại ----
    var $mainCarousel = $(".carousel .owl-carousel");
    if ($mainCarousel.hasClass("owl-loaded")) {
        $mainCarousel.owlCarousel("destroy");
    }

    var $testimonialsCarousel = $(".testimonials-carousel");
    if ($testimonialsCarousel.hasClass("owl-loaded")) {
        $testimonialsCarousel.owlCarousel("destroy");
    }

    var $relatedSlider = $(".related-slider");
    if ($relatedSlider.hasClass("owl-loaded")) {
        $relatedSlider.owlCarousel("destroy");
    }

    // ---- Khởi tạo lại Main Carousel ----
    if ($mainCarousel.length) {
        $mainCarousel.owlCarousel({
            autoplay: true,
            animateOut: 'fadeOut',
            animateIn: 'fadeIn',
            items: 1,
            smartSpeed: 300,
            dots: false,
            loop: true,
            nav: false
        });
    }

    // ---- Khởi tạo lại Testimonials Carousel ----
    if ($testimonialsCarousel.length) {
        $testimonialsCarousel.owlCarousel({
            center: true,
            autoplay: true,
            dots: true,
            loop: true,
            responsive: {
                0: { items: 1 },
                576: { items: 1 },
                768: { items: 2 },
                992: { items: 3 }
            }
        });
    }

    // ---- Khởi tạo lại Related Slider ----
    if ($relatedSlider.length) {
        $relatedSlider.owlCarousel({
            autoplay: true,
            dots: false,
            loop: true,
            nav: true,
            navText: [
                '<i class="fa fa-angle-left" aria-hidden="true"></i>',
                '<i class="fa fa-angle-right" aria-hidden="true"></i>'
            ],
            responsive: {
                0: { items: 1 },
                576: { items: 1 },
                768: { items: 2 }
            }
        });
    }

    // ---- Date/Time Picker ----
    var $date = $('#date, #date2');
    $date.each(function () {
        var $el = $(this);
        if ($el.data('DateTimePicker')) {
            $el.data('DateTimePicker').destroy();
        }
        $el.datetimepicker({ format: 'L' });
    });

    var $time = $('#time, #time2');
    $time.each(function () {
        var $el = $(this);
        if ($el.data('DateTimePicker')) {
            $el.data('DateTimePicker').destroy();
        }
        $el.datetimepicker({ format: 'LT' });
    });

    // ---- Modal Video ----
    var $videoSrc;
    $(document).off('click', '.btn-play').on('click', '.btn-play', function () {
        $videoSrc = $(this).data("src");
    });
    $('#videoModal').off('shown.bs.modal').on('shown.bs.modal', function () {
        $("#video").attr('src', $videoSrc + "?autoplay=1&modestbranding=1&showinfo=0");
    });
    $('#videoModal').off('hide.bs.modal').on('hide.bs.modal', function () {
        $("#video").attr('src', $videoSrc);
    });

    // ---- Sticky Navbar ----
    $(window).off('scroll.navbar').on('scroll.navbar', function () {
        if ($(this).scrollTop() > 0) {
            $('.navbar').addClass('nav-sticky');
        } else {
            $('.navbar').removeClass('nav-sticky');
        }
    });

    // ---- Back to Top ----
    $(window).off('scroll.backtop').on('scroll.backtop', function () {
        if ($(this).scrollTop() > 200) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $(document).off('click', '.back-to-top').on('click', '.back-to-top', function () {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
        return false;
    });
};

// Smooth scroll đến element theo id, có offset để tránh bị navbar che
window.scrollToElement = function (elementId, offsetPx) {
    var offset = offsetPx || 80;
    var $el = $('#' + elementId);
    if ($el.length) {
        $('html, body').animate(
            { scrollTop: $el.offset().top - offset },
            600,
            'easeInOutExpo'
        );
    }
};

