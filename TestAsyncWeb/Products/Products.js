

$(document).ready (function () {

    var products = {};

    var btnGetProducts = ("#btnGetProducts");
    var btnGetTime = ("#btnGetTime");

    $(btnGetProducts).on("click", function (e) {
        e.preventDefault()
        getProducts();
    });

    var getProducts = function () {

        var url = "/Products/ProductService.svc/GetProductsAsync";
        $("#products").text("");

        $.get(url, null, function (result) {

           
            //var res = JSON.parse(products);

            $("#products").text(result);
            
        });
    };

    $(btnGetTime).on("click", function (e) {
        e.preventDefault()

        var url = "/Products/ProductService.svc/GetCurrentTime";

        $.get(url, null, function (data) {

            $("#lblTime").text(data);
        });

    });


   
});