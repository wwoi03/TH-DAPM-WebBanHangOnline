main();

function main() {
    createCategory();
    updateCategory();
}

// tạo dịch vụ
function createCategory() {
    var formAddBtn = document.querySelector('#form-add-btn');

    if (formAddBtn != null) {
        formAddBtn.addEventListener('click', function () {
            document.querySelector('#form-add').submit();
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