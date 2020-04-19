// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function updateRoutePoints() {
    $list = $("#rp_id");
    $.ajax({
        url: "/Tickets/getRoutePointsFromTrip",
        type: "GET",
        data: { id: $("#trip_id").val() },
        traditional: true,
        success: function (result) {
            var items = "<option disabled selected>--- SELECT ---</option>"
            $list.empty();
            $.each(result, function (i, item) {
                items += '<option value="' + item["rpId"] + '"> ' + item["cityName"] + ' </option>'
            });
            $list.html(items)
        },
        error: function () {
            alert("Something went wrong call the police");

                }

    });
       
}
function updateTrips() {
    $list = $("#trip_id");
    $.ajax({
        url: "/Tickets/getTripsOnDate",
        type: "GET",
        data: { date: $("#departure_date").val() },
        traditional: true,
        success: function (result) {
            var items = "<option disabled selected>--- SELECT ---</option>"
            $list.empty();
            $.each(result, function (i, item) {
                items += '<option value="' + item["value"] + '"> ' + item["text"]+ ' </option>'
            });
            $list.html(items)
        },
        error: function () {
            alert("Something went wrong call the police");

        }

    })
}

function getSeatAndPrice() {
    $seat = $("#seat");
    $price = $("#price");
    $.ajax({
        url: "/Tickets/getSeatRange",
        type: "GET",
        data: { tripId: $("#trip_id").val(), rpId: $("#rp_id").val()  },
        traditional: true,
        success: function (result) {
            $seat.empty();
            $seat.attr('min', result["seat"]);
            $seat.attr('max', result["seat"]);
            $seat.val(result["seat"]);
            $price.val(result["price"]);
        },
        error: function () {
            alert("Something went wrong call the police");

        }

    })
}


