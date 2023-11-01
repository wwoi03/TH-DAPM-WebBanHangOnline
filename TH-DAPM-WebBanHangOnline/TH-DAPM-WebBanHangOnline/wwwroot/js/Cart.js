﻿function AddToCart(producid) {
    let quantity = document.getElementById('quantity').value;
    
    $.ajax({
        url: '../../CartCustomer/AddToCartToProductDetals/' + producid + '/' + quantity,
        type: 'GET',
        success: function (data) {

            window.location.href = data.redirectUrl;
        },
        error: function (error) {
            // Hàm này sẽ được gọi khi có lỗi xảy ra trong quá trình yêu cầu
            console.log(error);
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    EditQuantityCard();


    function EditQuantityCard() {
        
        var btnMinus = document.querySelectorAll('.btn-minus');
        var btnPlus = document.querySelectorAll('.btn-plus');     
        btnMinus.forEach(function (btn) {
            btn.addEventListener('click', function () {
                // Lấy input chứa số lượng
                var inputQuantity = this.parentNode.parentNode.querySelector('input');
                console.log(inputQuantity.value);
                var cartId = this.getAttribute('data');
                let price = document.getElementById('item-price');
                // this.getElementById('total').value = price * document.getElementById(cartId);
                console.log(price);
                $.ajax({                 
                    url: "/CartCustomer/EditQuantityPro/" + cartId + "/" + inputQuantity.value,
                    type: "Get",                   
                    success: function (data) {                      
                        console.log(data);
                    }
                })
            });
        });

        btnPlus.forEach(function (btn) {
            btn.addEventListener('click', function () {
                // Lấy input chứa số lượng
                var inputQuantity = this.parentNode.parentNode.querySelector('input');
                console.log(inputQuantity.value);
                var cartId = this.getAttribute('data');
                console.log('id=' + cartId);
                $.ajax({
                    url: "/CartCustomer/EditQuantityPro/" + cartId + "/" + inputQuantity.value,
                    type: "Get",
                    success: function (data) {
                        console.log(data);
                        document.getElementById('total-' + cartId).innerHTML = data;
                    }
                })
            });
        });
    }
    
});






