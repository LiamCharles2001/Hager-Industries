$('#btnRight').click(function (e) {
    var selectedOpts = $('#selectedOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnLeft').click(function (e) {
    var selectedOpts = $('#availOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

// Company Types

$(document).ready(function () {
    if ($('input[name="IsContractor"]').is(':checked')) $('#lbContractorTypes').show();
    if ($('input[name="IsVendor"]').is(':checked')) $('#lbVendorTypes').show();
    if ($('input[name="IsCustomer"]').is(':checked')) $('#lbCustomerTypes').show();
});

// Contractor

$('input[name="IsContractor"]').on('click', function () {
    if ($(this).is(':checked')) $('#lbContractorTypes').show();
    else $('#lbContractorTypes').hide();
});

$('#btnContractorRight').click(function (e) {
    var selectedContractorOpts = $('#selectedContractorOptions option:selected');
    if (selectedContractorOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availContractorOptions').append($(selectedContractorOpts).clone());
    $(selectedContractorOpts).remove();
    e.preventDefault();
});

$('#btnContractorLeft').click(function (e) {
    var selectedContractorOpts = $('#availContractorOptions option:selected');
    if (selectedContractorOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedContractorOptions').append($(selectedContractorOpts).clone());
    $(selectedContractorOpts).remove();
    e.preventDefault();
});

// Vendor

$('input[name="IsVendor"]').on('click', function () {
    if ($(this).is(':checked')) $('#lbVendorTypes').show();
    else $('#lbVendorTypes').hide();
});

$('#btnVendorRight').click(function (e) {
    var selectedVendorOpts = $('#selectedVendorOptions option:selected');
    if (selectedVendorOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availVendorOptions').append($(selectedVendorOpts).clone());
    $(selectedVendorOpts).remove();
    e.preventDefault();
});

$('#btnVendorLeft').click(function (e) {
    var selectedVendorOpts = $('#availVendorOptions option:selected');
    if (selectedVendorOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedVendorOptions').append($(selectedVendorOpts).clone());
    $(selectedVendorOpts).remove();
    e.preventDefault();
});

// Customer

$('input[name="IsCustomer"]').on('click', function () {
    if ($(this).is(':checked')) $('#lbCustomerTypes').show();
    else $('#lbCustomerTypes').hide();
});

$('#btnCustomerRight').click(function (e) {
    var selectedCustomerOpts = $('#selectedCustomerOptions option:selected');
    if (selectedCustomerOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availCustomerOptions').append($(selectedCustomerOpts).clone());
    $(selectedCustomerOpts).remove();
    e.preventDefault();
});

$('#btnCustomerLeft').click(function (e) {
    var selectedCustomerOpts = $('#availCustomerOptions option:selected');
    if (selectedCustomerOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedCustomerOptions').append($(selectedCustomerOpts).clone());
    $(selectedCustomerOpts).remove();
    e.preventDefault();
});

$('#btnSubmit').click(function (e) {
    $('#selectedOptions option').prop('selected', true);
    $('#selectedContractorOptions option').prop('selected', true);
    $('#selectedVendorOptions option').prop('selected', true);
    $('#selectedCustomerOptions option').prop('selected', true);
});