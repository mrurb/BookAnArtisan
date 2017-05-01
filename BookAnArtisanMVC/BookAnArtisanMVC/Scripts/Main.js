//$(document).ready(function () {
function autoc(TextInput, HiddenInput, AjaxUrl){
    TextInput.autocomplete({
        source: function (request, response) {
            var datat;
            $.ajax({
                url: AjaxUrl,
                data: { name: request.term },
                dataType: "json",
                type: "POST",
                success: function (data) { 
                    response($.map(data, function (item) {
                        return {
                            label: item.UserName,
                            id: item.Id
                        }
                    }));
                }
            });
        }, select: function (event, ui){
            TextInput.val(ui.item.label);
            HiddenInput.val(ui.item.id);
        }
    });
   }
//});