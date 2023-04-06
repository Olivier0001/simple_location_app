var dataTable;
$(document).ready(function () {
   dataTable= $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/User",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "userName", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "firstName", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            
        ],
        "width":"100%"
    });
});

//function Delete(url) {

//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    if (data.success) {
//                        dataTable.ajax.reload();
//                        //success notification
//                        toastr.success(data.message);
                        
//                    }
//                    else {
//                        //failsure notification
//                        toastr.error(data.message);
//                    }
//                }

//            })
//        }
//    })
//}