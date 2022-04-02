// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// LF

$(document).ready(function () {


    let collapsed = [];

    // If the cookie is storing any collapsed values, store them in an
    // array. Then, collapse the categories and style the chevrons.
    if (getCookie("collapsed_custom").length > 0) {
        for (category of getCookie("collapsed_custom").split(",")) {
            collapsed.push(category);

            $("#" + category).siblings("ul").addClass("collapsed_custom");
            $("#" + category).children(".bi-chevron-down").css("transform", "rotate(-90deg)");
        }
    }

    if (getCookie("menu-hidden") == "0") {
        $("#nav").removeClass("hidden");
    } else ($("#nav").addClass("hidden"));

    // console.log("collapsed_custom categories: " + collapsed);

    // Collapse and uncollapse side items, while keeping track of
    // them in an array, for saving across multiple pages.
    $(".collapsible").click(function () {
        let id = $(this).attr("id");

        // if it is not collapsed
        if (!$(this).siblings("ul").hasClass("collapsed_custom")) {

            collapsed.push(id);

            // collapse
            $(this).siblings("ul").addClass("collapsed_custom");
            $(this).children(".bi-chevron-down").css("transform", "rotate(-90deg)");

        } else { // if it is already collapsed

            // remove from array
            collapsed = $.grep(collapsed, function (val) {
                return val != id;
            });

            // uncollapse
            $(this).siblings("ul").removeClass("collapsed_custom");
            $(this).children(".bi-chevron-down").css("transform", "rotate(360deg)");
        }
        setCookie("collapsed_custom", collapsed.toString(), 30);
    });

    // Back button on view pages.
    $("#back-button").hover(function () {
        $(this).toggleClass("bi-arrow-left-circle");
        $(this).toggleClass("bi-arrow-left-circle-fill");
    });

    // Set menu visibility & cookie
    $("#nav-toggle").click(function () {
        $("#nav").toggleClass("hidden");
        if ($("#nav").hasClass("hidden"))
            setCookie("menu-hidden", 1);
        else setCookie("menu-hidden", 0);
    });


    // Auto-hide sidebar when window becomes small
    $(window).on('resize', function () {
        if ($(window).width() < 1514) {
            if (!($("#nav").hasClass("hidden"))) {
                $("#nav").addClass("hidden");
            }
        }

        if ($("#nav").hasClass("hidden"))
            setCookie("menu-hidden", 1);
        else setCookie("menu-hidden", 0);
    });

    $("#hager-logo").click(function () {
        document.location.href = '/';
    });

    //Country province
    $("#CountryID").on("change", function () {
        loadProvince(false);
        if ($("#CountryID").val() > 0) {
            if ($("#ProvinceID") != undefined)
                $("#ProvinceID").prop('disabled', false);
            if ($("#nowAddProvince") != undefined)
                $("#nowAddProvince").prop('disabled', false);

        }

    });

    $("#ShippingCountryID").on("change", function () {
        loadProvince(false, 'Shipping');
    });

    $("#BillingCountryID").on("change", function () {
        loadProvince(false, 'Billing');
    });
    $("#ShippingProvinceID").on("change", function () {
        loadCities(false, 'Shipping');
    });

    $("#BillingProvinceID").on("change", function () {
        loadCities(false, 'Billing');
    });
});

function loadProvince(isPartial, idFor, countryIDPass, listToUpdate, isLoadDefault) {
    $list = $("#ProvinceID");
    var countryID = $("#CountryID").val();
    if (isPartial)
        $list = $("#ProvincesID");
    if (idFor != undefined) {
        $list = $("#" + idFor + "ProvinceID");
        countryID = $("#" + idFor+"CountryID").val();
    };
    if (countryID == undefined)
        countryID = countryIDPass;
    if (listToUpdate != undefined)
        $list = listToUpdate;
    $.ajax({
        url: "/provinces/getProvincesByCountry",
        type: "GET",
        data: { id: countryID }, //id of the country which is used to extract cities
        traditional: true,
        success: function (result) {
            $list.empty();
            if (!isPartial || isLoadDefault == true)
                $list.append('<option value="">--Select Province--</option>');
            if (result.length == 0 && isPartial) {
                $list.append('<option value="">--No Record Found--</option>');
                $list.prop('disabled', true);
            }
            if (result.length > 0 && isPartial) {
                $list.prop('disabled', false);
            }
            $.each(result, function (i, item) {
                $list.append('<option value="' + item.value + '"> ' + item.text + ' </option>');
            });

        },
        error: function () {
            alert("Something went wrong ");
        }
    });

    
}

function loadCities(isPartial, idFor, provinceIDPass, listToUpdate,isLoadDefault) {
    $list = $("#CityID");
    var provinceID = $("#ProvincesID").val();
    if (isPartial)
        $list = $("#CitiesID");
    if (idFor != undefined) {
        $list = $("#" + idFor + "CityID");
        provinceID = $("#" + idFor + "ProvinceID").val();
    };

    if (provinceID == undefined)
        provinceID = provinceIDPass;
    $.ajax({
        url: "/cities/getCitiesByProvince",
        type: "GET",
        data: { id: provinceID }, //id of the province which is used to extract cities
        traditional: true,
        success: function (result) {
            $list.empty();
            if (!isPartial || isLoadDefault == true)
                $list.append('<option value="">--Select City--</option>');
            if (result.length == 0 && isPartial) {
                $list.append('<option value="">--No Record Found--</option>');
                $list.prop('disabled', true);
            }
            if (result.length > 0 && isPartial) {
                $list.prop('disabled', false);
            }
            $.each(result, function (i, item) {
                $list.append('<option value="' + item.value + '"> ' + item.text + ' </option>');
            });

        },
        error: function () {
            alert("Something went wrong ");
        }
    });


}

// W3Schools

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}