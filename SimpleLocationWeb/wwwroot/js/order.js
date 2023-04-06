var dataTable;
$(document).ready(function () {
   dataTable= $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/order",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "pickupName", "width": "15%" },
            { "data": "user.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpDate", "width": "15%" },
            { "data": "pickUpTime", "width": "15%" },
            { "data": "pickDateOfReturn", "width": "15%" },
            { "data": "pickTimeOfReturn", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return  `<div class="w-75 btn-group">
                             <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2"> <i class="bi bi-pencil-square"></i></a>
                             </div>`
                },

                "width": "15%"
            }
        ],
        "width":"100%"
    });
});

function Delete(url) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                        
                    }
                    else {
                        //failsure notification
                        toastr.error(data.message);
                    }
                }

            })
        }
    })
}