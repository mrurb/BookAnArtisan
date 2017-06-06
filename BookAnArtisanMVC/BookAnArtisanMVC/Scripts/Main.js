
function autocVal(textInput, hiddenInput, ajaxUrl, form) {
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    textInput.autocomplete({
        source: function (request, response) {
            var datat;
            $.ajax({
                url: ajaxUrl,
                data: {
                    name: request.term,
                    __RequestVerificationToken: token
                },
                dataType: "json",
                type: "POST",
                success: function (data) { 
                    response($.map(data, function (item) {
                        return {
                            label: item.UserName,
                            id: item.Id
                        };
                    }));
                }
            });
        }, select: function (event, ui){
            textInput.val(ui.item.label);
            hiddenInput.val(ui.item.id);
        }
    });
}

function autoc(textInput, hiddenInput, ajaxUrl) {
    textInput.autocomplete({
        source: function (request, response) {
            var datat;
            $.ajax({
                url: ajaxUrl,
                data: { name: request.term },
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.UserName,
                            id: item.Id
                        };
                    }));
                }
            });
        }, select: function (event, ui) {
            textInput.val(ui.item.label);
            hiddenInput.val(ui.item.id);
        }
    });
}

function autocMaterial(textInput, hiddenInput, ajaxUrl) {
    textInput.autocomplete({
        source: function (request, response) {
            var datat;
            $.ajax({
                url: ajaxUrl,
                data: { name: request.term },
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Name,
                            id: item.Id
                        };
                    }));
                }
            });
        }, select: function (event, ui) {
            textInput.val(ui.item.label);
            hiddenInput.val(ui.item.id);
        }
    });
}