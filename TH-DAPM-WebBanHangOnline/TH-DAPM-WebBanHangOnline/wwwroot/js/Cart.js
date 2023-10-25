function AddToCart(producid) {
    let quantity = document.getElementById('quantity').value;
    
    $.ajax({
        url: '../../CartCustomer/AddToCart2/' + producid + '/' + quantity,
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