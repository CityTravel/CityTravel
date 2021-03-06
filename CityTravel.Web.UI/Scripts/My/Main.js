﻿
//Transport types enum
var TransportType = { "Walking": 0, "Bus": 1, "Subway": 2, "Trolleybus": 3, "Tram": 4 };

var printLegendBar = []; // тип и линия маршрута в легенде 
var printLegendText = []; // текст в легенде
var flag = false;
var flagMake = false;

//map point constructor
function MapPoint(location) {
    this.location = location;
    this.marker = null;
    this.name = null;
}

viewModel = new MakeRouteViewModel();
var path = new Path();

//DOM loaded event
$(document).ready(function () {
    controls.init();
    autocomplete.init();
    map.init();
    $(controls.settings.selectors.printTextVersionButtonSelector).click(function () {
        printBlockRoute();
        map.menu.closeMenu();
    });
    $(legend.settings.selector).hide();
    $(controls.settings.selectors.loadingBarBlock).hide(); // hide the loading bar
    ko.applyBindings(viewModel);
    
});




