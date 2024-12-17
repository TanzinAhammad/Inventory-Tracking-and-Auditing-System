$('#textSearch').keyup(function () {
    var typeValue = $(this).val().toLowerCase(); // Convert input to lowercase for case-insensitive comparison

    $('tbody tr').each(function () {
        var categoryText = $(this).find('td:nth-child(6)').text().toLowerCase(); // Target only the Category column (7th column)

        if (categoryText.includes(typeValue)) {
            $(this).show(); // Show row if Category matches
        } else {
            $(this).fadeOut(); // Hide row if no match
        }
    });
});
