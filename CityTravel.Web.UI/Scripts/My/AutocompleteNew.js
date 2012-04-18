var autocomplete = {
    correct: function (data) {
        if (data == "" || data.Predictions == null) return "";
        return $.map(data.Predictions, function (item) {
            if ((item.description.replace(" ", "") != "") &&
                (item.description.indexOf("??") == -1) &&
                    (item.description.indexOf("��") == -1))
                return item.description.replace(",", "");
            else {
                return null;
            }
        });
    },

    init: function () {
        var selectors = controls.settings.selectors;

        $(selectors.startAddressSelector).typeahead({
            source: function (typeahead, query) {
                return $.get(helpers.GetPath('Autocomplete/GetPredictionsForStart/'), { startPointAddress: query }, function (data) {
                    return typeahead.process(autocomplete.correct(data));
                });
            },
            items: 5,
            onselect: function (obj) {
                $(selectors.startAddressSelector).val(obj);
                $(selectors.startAddressSelector).blur();
                viewModel.startPoint.name(obj);
                viewModel.startPoint.isSelected(true);
                viewModel.startPoint.isSelected(false);
            }
        });

        $(selectors.endAddressSelector).typeahead({
            source: function (typeahead, query) {
                return $.get(helpers.GetPath('Autocomplete/GetPredictionsForEnd/'), { endPointAddress: query }, function (data) {
                    return typeahead.process(autocomplete.correct(data));
                });
            },
            items: 5,
            onselect: function (obj) {
                $(selectors.endAddressSelector).val(obj);
                $(selectors.endAddressSelector).blur();
                viewModel.endPoint.name(obj);
                viewModel.endPoint.isSelected(true);
                viewModel.endPoint.isSelected(false);
            }
        });
    }
};