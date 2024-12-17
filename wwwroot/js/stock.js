$('#filterStock').on('change', function () {
    var filterType = $(this).val(); // Get selected filter type (outOfStock or lowStock)
    var threshold = 10; // Define the low stock threshold

    $('tbody tr').each(function () {
        var stock = parseInt($(this).find('td:nth-child(4)').text()) || 0; // Target the 4rd column (Stock)

        if (filterType === "outOfStock" && stock === 0) {
            $(this).show(); // Show Out of Stock rows
        }
        else if (filterType === "lowStock" && stock > 0 && stock <= threshold) {
            $(this).show(); // Show Low Stock rows
        }
        else if (filterType === "all") {
            $(this).show(); // Show all rows
        }
        else {
            $(this).hide(); // Hide rows that do not match the filter
        }
    });
});
