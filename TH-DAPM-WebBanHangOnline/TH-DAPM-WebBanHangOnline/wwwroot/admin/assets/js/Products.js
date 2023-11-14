function ViewDetails(id) {
    $.ajax({
        url: '../../ProducerAdmin/ViewDetails/',
        type: 'get',
        data: { id: id },

        success: function (data) {

            $('#Open-Form').html(data);
        }
    })
}
function ViewEdit(id) {
    $.ajax({
        url: '../../ProducerAdmin/ViewDetails/',
        type: 'get',
        data: { id: id },

        success: function (data) {

            $('#Open-Form').html(data);
        }
    })
}
function Create() {

    $.ajax({
        url: '../../ProductAdmin/Create/',
        type: 'get',
        success: function (data) {
            console.log(data);
            $('#Open-Form').html(data);
        }
    })
}
