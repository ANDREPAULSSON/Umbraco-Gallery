var contactForm = contactForm ||
{
    init: function() {
        this.listeners();
    },
    listeners: function () {
        $(document).on('click',
            '.contact-submit',
            function() {
                e.preventDefault();
                var form = $('#contact-form');
                form.submit();
            });
    },
    showResult: function() {
        $('#contact-form').hide('slow');
        $('#form-result').show('slow');
        console.log("funkar")
    }
}