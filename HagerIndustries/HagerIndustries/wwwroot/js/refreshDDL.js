function refreshDDL(ddl_ID, uri, addDefault, defaultText, fadeOutIn) {
    var theDDL = $("#" + ddl_ID);
    var selectedOption = theDDL.val();
    var URL = "/" + uri + "/" + selectedOption;
    $(function () {
        $.getJSON(URL, function (data) {
            if (data !== null && !jQuery.isEmptyObject(data)) {
                theDDL.empty();
                if (addDefault) {
                    theDDL.append($('<option/>', {
                        value: "",
                        text: defaultText
                    }));
                }
                $.each(data, function (index, item) {
                    //if (ddl_ID == 'ProvinceID' && $("#CountryID") != undefined) {

                    //}
                    theDDL.append($('<option/>', {
                        value: item.value,
                        text: item.text,
                        selected: item.selected
                    }));
                });
                theDDL.trigger("chosen:updated");
                if (ddl_ID == "CountryID" && $("#tempCountry") != undefined) {
                    $("#tempCountry").empty();

                    $("#tempCountry").append($('<option/>', {
                        value: "",
                        text: defaultText
                    }));
                    $.each(data, function (index, item) {
                        $("#tempCountry").append($('<option/>', {
                            value: item.value,
                            text: item.text,
                            selected: item.selected
                        }));
                    });
                    $("#tempCountry").trigger("chosen:updated");
                }
                
            }
        });
    });
    if (fadeOutIn) {
        theDDL.fadeToggle(400, function () {
            theDDL.fadeToggle(400);
        });
    }
    return;
}