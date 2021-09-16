// Use jQuery to perform partial rendering of server side content.
$(function () {
    // When a pager link within the mainContent section is clicked, load the 
    // result using AJAX.
    $('#mainContent').on('click', '.pager a', function () {
        var url = $(this).attr('href');

        $('#mainContent').load(url);

        return false;
    })
})