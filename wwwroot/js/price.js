$('#minPrice, #maxPrice').on('keyup change', function () {
    var minPrice = parseFloat($('#minPrice').val()) || 0; // Minimum price input, defaults to 0
    var maxPrice = parseFloat($('#maxPrice').val()) || Number.MAX_VALUE; // Max price input, defaults to large value

    $('tbody tr').each(function () {
        var price = parseFloat($(this).find('td:nth-child(4)').text()) || 0; // Target the 4th column for Price

        if (price >= minPrice && price <= maxPrice) {
            $(this).show(); // Show the row if price is in range
        } else {
            $(this).hide(); // Hide the row otherwise
        }
    });
});
