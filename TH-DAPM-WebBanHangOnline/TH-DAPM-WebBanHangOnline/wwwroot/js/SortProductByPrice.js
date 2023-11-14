document.addEventListener('DOMContentLoaded', function () {
    //Sort();
    Radiobuton();
    function Sort(priceid) {
        $.ajax({
            url: '/HomeCustomer/SortProByPrice/' + priceid,
            type: 'get',
            success: function (data) {
                document.getElementById('ListProSort1').style.display = 'none';
                $('#ListProSort').html(data);
            }
        })
    }
    function Radiobuton() {
        var priceFilterForm = document.getElementById("priceFilterForm");

        priceFilterForm.addEventListener("change", function () {
            var checkedRadio = document.querySelector('input[name="price"]:checked');

            if (checkedRadio) {
                var selectedPriceRange = checkedRadio.id.replace("price-", "");
                console.log("Selected Price Range: " + selectedPriceRange);
                Sort(selectedPriceRange);
            }
        });
    }

});