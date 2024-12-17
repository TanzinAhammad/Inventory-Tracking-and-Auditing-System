$('#minPrice, #maxPrice').on('keyup change', function () {
    var minPrice = parseFloat($('#minPrice').val()) || 0; // Minimum price input, defaults to 0
    var maxPrice = parseFloat($('#maxPrice').val()) || Number.MAX_VALUE; // Max price input, defaults to large value

    $('tbody tr').each(function () {
        // Target the 5th column (Price) for filtering (index 4 because of 0-based indexing)
        var priceText = $(this).find('td:nth-child(5)').text().trim(); // Get the price text from the 5th column

        // Remove non-numeric characters (like $ signs) and convert the price to a float
        var price = parseFloat(priceText.replace(/[^0-9.-]+/g, "")) || 0;

        // Show or hide the row based on price range
        if (price >= minPrice && price <= maxPrice) {
            $(this).show(); // Show the row if price is within range
        } else {
            $(this).hide(); // Hide the row otherwise
        }
    });
});
