function _set_combo_catalog(catalog, parameter, htmlName, isRequired, container, functionName, multiple = "false") {

    $.ajax({
        type: "POST",
        url: '/GenericComponents/GetComboByCatalog',
        dataType: "html",
        data: {
            "htmlName": htmlName,
            "isRequired": isRequired,
            "catalog": catalog + ",",
            "parameter": parameter + ",",
            "functionName": functionName,
            "multiple": multiple
        },
        success: function (data) {
            $("#" + container).html(data);
        },
        error: function (xhr, status) {
            var errmsg = xhr.responseText;
            alert(errmsg);
        }
    });
}