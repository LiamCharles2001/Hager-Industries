//var limit = 2;
var companyList = { "primary": 0, "merge": 0 }
var resetList = { "primary": 0, "merge": 0 }

//$('input.single-checkbox').on('change', function (evt) {
//    if (companyList.length < limit)
//        companyList.push($(this).val);

//    else {
//        this.checked = false;
//        alert('Only Select ' + limit +' Company to merge')
//    }
//    //if ($(this).siblings(':checked').length >= limit) {
//    //    this.checked = false;
//    //}
//});
$('.btnMerge').on('click', function (evt) {
    var allowed = $(this).attr('data-id');
    var mergeWith = $(this).attr('data-name');
    companyList = resetList;
    companyList.primary = allowed;
    $('#ddlCompany option').attr('disabled', 'disabled');
    $("#lblMergeWith").text(mergeWith);
    $("#lblMergeWithSecond").text(mergeWith);

    $('#ddlCompany option').each(function () {
        if ($(this).val() != allowed) {
            $(this).removeAttr('disabled');
        }
    });
});

$('#ddlCompany').change(function () {
    $("#lblMerge").text(this.selectedOptions[0].text);
    companyList.merge = this.selectedOptions[0].value;
    if (this.selectedOptions[0].value > 0)
        $('#mergeMSG').removeClass('hidden');
    else
        $('#mergeMSG').addClass('hidden');

    $('#lblerror').addClass('hidden');
});

$('.continue').click(function () {
    $('#dvUserdetails').html('');
    

    if (companyList.merge > 0) {
        /* Request the partial view with .get request. */
        var url = '/Companies/LoadMergeData?Primary=' + companyList.primary + '&Merger=' + companyList.merge;

        $.get(url, function (data) {
            debugger
            /* data is the pure html returned from action method, load it to your page */
            $('#dvUserdetails').html(data);
        /* little fade in effect */
            
            $('#dvUserdetails').fadeIn('fast');
            $('#selectAllPrimary').trigger('click');

        });

        $('.nav-tabs > .active').next('li').find('a').trigger('click');
        $('#lblerror').addClass('hidden');
        $('#btnsavechanges').removeClass('hidden');
        $('#btncontinue').addClass('hidden');
    }
    else
        $('#lblerror').removeClass('hidden');
});
$('.back').click(function () {
    $('.nav-tabs > .active').prev('li').find('a').trigger('click');
});

//$('.selectAllPrimary').change(function () {
//    checkUncheckAll('chkPrimary',this.checked)
//});
//$('.selectAllMerge').change(function () {
//    checkUncheckAll('chkMerge', this.checked)
//});
//// Select all check boxes : Setting the checked property to true in checkAll() function
//function checkUncheckAll(clsName,isCheck) {
//    var items = document.getElementsByClassName(clsName);
//    for (var i = 0; i < items.length; i++) {
//        if (items[i].type == 'radio')
//            items[i].checked = isCheck;
//    }
//}
//$(document).on('click', 'td', function () {
//    $(this).find('input [type="radio"]').prop('checked', true)
//});
//$(document).ready(function () {
//    $("#selectAllPrimary").click(function () {
//        $('.chkPrimary').attr('checked', this.checked);
//    });
//    // if all checkbox are selected, check the selectall checkbox  also        

//    $(".chkPrimary").click(function () {
//        if ($(".chkPrimary").length == $(".chkPrimary:checked").length) {
//            $("#selectAllPrimary").attr("checked", "checked");
//        }
//        else {
//            $("#selectAllPrimary").removeAttr("checked");
//        }
//    });

//    $("#selectAllMerge").click(function () {
//        $('.chkPrimary').attr('checked', this.checked);
//    });
//    // if all checkbox are selected, check the selectall checkbox  also        

//    $(".chkMerge").click(function () {
//        if ($(".chkMerge").length == $(".chkMerge:checked").length) {
//            $("#selectAllMerge").attr("checked", "checked");
//        }
//        else {
//            $("#selectAllMerge").removeAttr("checked");
//        }
//    });

//});
$(document).on('click', '.selectAllPrimary', function () {
    if (this.checked) {
        $(".chkPrimary").prop("checked", true);
        $(".chkMerge").prop("checked", false);
        $(".selectAllMerge").prop("checked", false);

    } else {
        $(".chkPrimary").prop("checked", false);
    }

});
$(document).on('click', '.chkPrimary', function () {
    if (this.checked)
        $(".selectAllMerge").prop("checked", false);

    var numberOfCheckboxes = $(".chkPrimary").length;
    var numberOfCheckboxesChecked = $('.chkPrimary:checked').length;
    if (numberOfCheckboxes == numberOfCheckboxesChecked) {
        $(".selectAllPrimary").prop("checked", true);
        $(".selectAllMerge").prop("checked", false);

    } else {
        $(".selectAllPrimary").prop("checked", false);
    }

    var chkfor = $(this).attr('data-chkfor');

    var group = $("input:checkbox[name='" + $(this).attr("name") + "']");

    for (var i = 0; i < group.length; i++) {
        if ((chkfor != group[i].attributes[('data-chkfor')].value && this.checked))
            group[i].checked = false;
    }
});
$(document).on('click', '.selectAllMerge', function () {
    if (this.checked) {
        $(".chkMerge").prop("checked", true);
        $(".chkPrimary").prop("checked", false);
        $(".selectAllPrimary").prop("checked", false);

    } else {
        $(".chkMerge").prop("checked", false);
    }
});
$(document).on('click', '.chkMerge', function () {
    if (this.checked)
        $(".selectAllPrimary").prop("checked", false);

    var numberOfCheckboxes = $(".chkMerge").length;
    var numberOfCheckboxesChecked = $('.chkMerge:checked').length;
    if (numberOfCheckboxes == numberOfCheckboxesChecked) {
        $(".selectAllMerge").prop("checked", true);
        $(".selectAllPrimary").prop("checked", false);

    } else {
        $(".selectAllMerge").prop("checked", false);
    }
    var chkfor = $(this).attr('data-chkfor');

    var group = $("input:checkbox[name='" + $(this).attr("name") + "']");

    for (var i = 0; i < group.length; i++) {
        if ((chkfor != group[i].attributes[('data-chkfor')].value && this.checked))
            group[i].checked = false;
    }
});

$('#mergeCompanyModal').on('hide.bs.modal', function () {
    location.reload();
});
var final = new Array();;
$(document).on('click', '#btnsavechanges', function () {
    if ($("#selectAllMerge")[0].checked)
        return;
    var allcheck = $(".allcheck");
    for (var i = 0; i < allcheck.length; i++) {
        var group = $("input:checkbox[name='" + allcheck[i].name + "']");
        for (var j = 0; j < group.length; j++) {
            var data = {};

            if (group[j].checked) {
                data.field = allcheck[i].name;
                data.value = group[j].attributes[('data-chkfor')].value;
                var found = false;
                $(final).each(function () {
                    if (this.field == allcheck[i].name) {
                        found = true;
                    }
                });
                if (!found || data.field =='Contacts')
                    final.push(data);
            }
        }
    }

    things = JSON.stringify({ 'final': final });
    $.post('/Companies/MergeCompanies',
        {
            final: final, Primary: companyList.primary, Merger: companyList.merge
        },
        function () {
            alert('Merge Success');
            $('#closeModal').trigger('click');
            var url = "/Companies/Index";
            window.location.href = url; 

        });


});
//$("input:checkbox").click(function () {
//    var group = "input:checkbox[name='" + $(this).attr("name") + "']";
//    $(group).attr("checked", false);
//    $(this).attr("checked", true);
//});
//function checkFluency() {
//    var checkbox = document.getElementById('selectAllMerge');
//    if (checkbox.checked) {
//        document.getElementsByClassName('chkPrimary').prop("checked", true);
//    } else {
//        document.getElementsByClassName('chkPrimary').prop("checked", false);
//    }
//}
//function CheckOne() {
//    var numberOfCheckboxes = document.getElementsByClassName("chkMerge").length;
//    var numberOfCheckboxesChecked = document.getElementsByClassName('chkMerge:checked').length;
//    if (numberOfCheckboxes == numberOfCheckboxesChecked) {
//        document.getElementById('selectAllMerge').prop("checked", true);
//    } else {
//        document.getElementById('selectAllMerge').prop("checked", false);
//    }
//}
//$(function () {
//    $('.selectAllMerge').click(function () {
//        if (this.checked) {
//            $(".chkMerge").prop("checked", true);
//        } else {
//            $(".chkMerge").prop("checked", false);
//        }
//    });

//    $(".chkMerge").click(function () {
//        var numberOfCheckboxes = $(".chkMerge").length;
//        var numberOfCheckboxesChecked = $('.chkMerge:checked').length;
//        if (numberOfCheckboxes == numberOfCheckboxesChecked) {
//            $(".selectAllMerge").prop("checked", true);
//        } else {
//            $(".selectAllMerge").prop("checked", false);
//        }
//    });
//});