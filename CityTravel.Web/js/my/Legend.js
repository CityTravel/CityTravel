//object that incapsulates legend logic
var legend = {
    settings: {
        selector: "#legend",
        legendEntityBarSelector: ".legendEntityBar",
        legendEntityTextSelector: ".legendEntityText",
        transportTypeWalkingSelector: ".transportTypeWalking",
        transportTypeWalkingDefaultText: "Пеший маршрут",
        transportTypeBusSelector: ".transportTypeBus",
        transportTypeBusDefaultText: "bus №"
    },

    reset: function () {
        $(this.settings.selector).empty();
        printLegendBar.length = 0; // обнулить массивы для рачпечатки легенды 
        printLegendText.length = 0;

        $(legend.settings.selector).show();


    },

    render: function (transportType, additionalText) {
        var settings = this.settings;

        var legendEntityBar = $("<div>");
        legendEntityBar.addClass(settings.legendEntityBarSelector.replace(".", ""));

        var legendEntityText = $("<div>");
        legendEntityText.addClass(settings.legendEntityTextSelector.replace(".", ""));

        switch (transportType) {
            case TransportType.Walking:
                {
                    legendEntityBar.addClass(settings.transportTypeWalkingSelector.replace(".", ""));
                    legendEntityText.append("<text>" + settings.transportTypeWalkingDefaultText + additionalText + "</text>");

                    // Сохраняем легенды в массив для распечатки 
                    printLegendBar.push(transportType); // тип тарнспорта для маршрута в легенде 
                    printLegendText.push(settings.transportTypeWalkingDefaultText); // текст в легенде

                }
                break;
            case TransportType.Bus:
                {
                    legendEntityBar.addClass(settings.transportTypeBusSelector.replace(".", ""));
                    legendEntityText.append("<text>" + additionalText + "</text>"); // + settings.transportTypeBusDefaultText

                    // Сохраняем легенды в массив для распечатки 
                    printLegendBar.push(transportType); // тип тарнспорта для распечатки 
                    printLegendText.push(additionalText); // текст в легенде 
                }
                break;

            case TransportType.Subway:
                {

                } break;

            case TransportType.Trolleybus:
                {

                } break;

            case TransportType.Tram:
                {

                } break;

            default:
                console.log("Undefined transport type in legend render!");
                break;
        }

        $(settings.selector).append(legendEntityBar);
        $(settings.selector).append(legendEntityText);
    }
};