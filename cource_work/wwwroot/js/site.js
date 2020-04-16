// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function updateRoutePoints() {
    $list = $("#rp_id");
    $.ajax({
        url: "/Tickets/getRoutePointsFromRoute",
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

function getSeatRange() {
    $target = $("#seat");
    $.ajax({
        url: "/Tickets/getSeatRange",
        type: "GET",
        data: { tripId: $("#trip_id").val() },
        traditional: true,
        success: function (result) {
            $target.empty();
            $target.attr('min', result["min"]);
            $target.attr('max', result["max"]);
        },
        error: function () {
            alert("Something went wrong call the police");

        }

    })
}


