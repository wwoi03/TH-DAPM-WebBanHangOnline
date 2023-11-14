document.addEventListener('DOMContentLoaded', function () {

    main();

    function main() {
        openFormCreate();
        updateCategory();
    }

    // hiển thị form thêm
    function openFormCreate() {
        var openFormCreate = document.getElementById('open-form-create');

        if (openFormCreate != null) {
            openFormCreate.addEventListener('click', function () {
                $.ajax({
                    url: '/CategoriesAdmin/Create',
                    type: 'get',
                    success: function (data) {
                        $('#form-container').html(data);

                        createCategory();
                    }
                })
            });
        }

    }

    // tạo dịch vụ
    function createCategory() {
        var formAddBtn = document.querySelector('#form-add-btn');

        if (formAddBtn != null) {
            formAddBtn.addEventListener('click', function () {

                var dataViewModel = {
                    Name: $('#category-name').val()
                }

                $.ajax({
                    url: '/CategoriesAdmin/Create',
                    type: 'post',
                    data: dataViewModel,
                    success: function (data) {
                        $('#form-container').html(data);
                    }
                })
            });
        }
    }

    // sửa dịch vụ
    function updateCategory() {
        var formEditBtn = document.querySelector('#form-edit-btn');

        if (formEditBtn != null) {
            formEditBtn.addEventListener('click', function () {
                document.querySelector('#form-edit').submit();
            });
        }
    }
});
