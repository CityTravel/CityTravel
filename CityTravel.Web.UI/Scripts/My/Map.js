
var KEY_WORDS_FOR_ADDRESS = ["Днепропетровск", "Днепропетровская область", "Украина", "49000"];
var KEY_WORD_FOR_REGION = "район";
var POSSIBLE_LOCATIONS = ["Днепропетровск"];
var CITY_STRING = "Днепропетровск, Днепропетровская область, Украина, 49000";
var ICON_START_POINT = "Content/images/pin-A-drag.png";
var ICON_END_POINT = "Content/images/pin-B.png";
var numMarker = { "startPoint": 0, "endPoint": 1 };

//object that incapsulates map logic
var map = {
    mapObject: null,
    directionsRenderer: null,
    geocoder: null,

    //map settings
    settings: {
        //center defaults to city center
        mapCenter: {
            Lat: 48.46306197546078,
            Lng: 35.04905941284187
        },
        mapDisplayOptions: {
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            streetViewControl: false,
            mapTypeControlOptions: {
                style: google.maps.MapTypeControlStyle.DEFAULT,
                position: google.maps.ControlPosition.TOP_CENTER
            }
        },
        //map element selector
        mapCanvasSelector: "#googleMap",
        mapContextMenuSelector: "#contextMenu",
        mapContextMenuClass: ".ContextMenuGoogle",
        mapPointMenuClass: ".ContextMenuPointGoogle",
        mapImgMenuClass: ".ContextMenuMarkerImg",

        mapMarkerX: {},
        NumMarker: { "A": 0, "B": 1 },
        MarkerA: "A",
        MarkerB: "B",
        NumMarkerRev: { 0: "A", 1: "B" }

    },

    //init function
    init: function () {
        var settings = this.settings;
        settings.mapDisplayOptions.center = new google.maps.LatLng(settings.mapCenter.Lat, settings.mapCenter.Lng);
        map.mapObject = new google.maps.Map($(settings.mapCanvasSelector)[0], settings.mapDisplayOptions);
        map.directionsRenderer = new google.maps.DirectionsRenderer();
        map.geocoder = new google.maps.Geocoder();

        // right clicking mouse button handler
        this.menu.init();
        //init markers
        map.createMarker(viewModel, "startPoint");
        map.createMarker(viewModel, "endPoint");

    },

    //fit map to start and end point
    fitToBounds: function (path) {
        var bounds = new google.maps.LatLngBounds();
        if (path.startPoint != null)
            bounds.extend(path.startPoint.location);
        if (path.endPoint != null)
            bounds.extend(path.endPoint.location);
        map.mapObject.setCenter(bounds.getCenter());
        map.mapObject.fitBounds(bounds);

    },
    fitToBounds: function (startPoint, endPoint) {
        var bounds = new google.maps.LatLngBounds();
        if (startPoint != null)
            bounds.extend(startPoint.marker.position);
        if (endPoint != null)
            bounds.extend(endPoint.marker.position);
        map.mapObject.setCenter(bounds.getCenter());
        map.mapObject.fitBounds(bounds);

    },

    //return only with name of street, number of building, name of region 
    delFromAddressUnwantedValues: function (address) {
        for (var i = 0; i < KEY_WORDS_FOR_ADDRESS.length; i++) {
            address = address.replace(", " + KEY_WORDS_FOR_ADDRESS[i], "");
        }
        return address;
    },

    //return specified address to Dnepropetrosk(it works like a filter for Dnepropetrosk) 
    addToAddressKeyWords: function (address) {
        for (var i = 0; i < KEY_WORDS_FOR_ADDRESS.length; i++) {
            if (address.toString().search(KEY_WORDS_FOR_ADDRESS[i]) == -1) {
                address = address + " " + KEY_WORDS_FOR_ADDRESS[i];
            }
        }
        return address;
    },

    //check if address is located in Dnepropetrovsk(if yes => return true)
    IsAddressValid: function (resultString) {
        if (resultString == CITY_STRING) {
            return false;
        }
        for (var i = 0; i < POSSIBLE_LOCATIONS.length; i++) {
            if (resultString.search(POSSIBLE_LOCATIONS[i] + ",") == -1) return false;
        }
        return true;
    },

    //return region of street/building/address
    RegionOfStreet: function (addressComponents) {
        for (var i = 0; i < addressComponents.length; i++) {
            if (addressComponents[i].long_name.search("KEY_WORD_FOR_REGION") != -1) return ', ' + addressComponents[i].long_name;
        }
        return "";
    },

    //set event for markers(for drag-and-drop)
    createMarker: function (viewModel, property) {
        map.menu.setEventMarker(viewModel[property].marker, property);
        viewModel[property].marker.setMap(map.mapObject);
    },

    menu:
	{
	    openMenu: function (pos) {
	        $(map.settings.mapContextMenuSelector).css("left", pos.x);
	        $(map.settings.mapContextMenuSelector).css("top", pos.y);
	        $(map.settings.mapContextMenuSelector).show();
	    },

	    closeMenu: function () {
	        $(map.settings.mapContextMenuSelector).hide();
	    },

	    init: function () {

	        $(map.settings.mapContextMenuSelector).addClass(map.settings.mapContextMenuClass.replace(".", ""));

	        var points = $("<ul>");
	        var pointA = $('<span>');
	        var pointB = $('<span>');
	        points.append(pointA, pointB);
	        pointA.addClass(map.settings.mapPointMenuClass.replace(".", ""));
	        pointA.append($("#imagePointA"));
	        pointB.addClass(map.settings.mapPointMenuClass.replace(".", ""));
	        pointB.append($("#imagePointB"));

	        $(map.settings.mapContextMenuSelector).append(pointA);
	        $(map.settings.mapContextMenuSelector).append(pointB);

	        google.maps.event.addListener(
				map.mapObject,
				"rightclick",
				function (event) {

				    // If the loading bar is active, so don't display the context menu on the map
				    if ($(controls.settings.selectors.loadingBarBlock).css('display') == 'none') {
				        var s = event.pixel;
				        map.menu.openMenu(event.pixel);
				        map.menu.mapMarkerX = event.latLng;

				    }
				});

	        google.maps.event.addListener(
				map.mapObject,
				"click",
				function () {
				    map.menu.closeMenu();
				});

	        $(pointA).click(function () {
	            map.menu.setMarker(map.menu.mapMarkerX, "startPoint");
	        });

	        $(pointB).click(function () {
	            map.menu.setMarker(map.menu.mapMarkerX, "endPoint");
	        });
	    },

	    setMarker: function (pos, property) {
	        map.geocoder.geocode({ 'latLng': pos }, function (results, status) {
	            if (status == google.maps.GeocoderStatus.OK) {
	                var curAddress = results[0].formatted_address + ', ' + results[1].address_components[0].long_name;
	                map.resolveAddress((curAddress), path, property, function () {
	                    switch (property) {
	                        case "startPoint":
	                            $(controls.settings.selectors.startAddressSelector).css("color", '#000');
	                            break;
	                        case "endPoint":
	                            $(controls.settings.selectors.endAddressSelector).css("color", '#000');
	                            break;
	                    }
	                });
	            }
	        });
	        map.menu.closeMenu();
	    },
	    setEventMarker: function (marker, property) {
	        google.maps.event.addListener(
                marker,
	            "dragend",
	            function () {
	                map.menu.setMarker(marker.position, property);
	            }
	        );
	    }
	},

    //resolve address
    resolveAddress: function (address, destination, property, success) {
        viewModel[property].isValid(false);
        

        //if no address specified simply exit
        if (!address || address === "") {
            if (viewModel[property].marker != null) {
                viewModel[property].marker.setVisible(false);
            }
            viewModel[property].isCorrect(true);
            return;
        }

        //check entered address value type
        var numberCheck = address / 1;
        if (!isNaN(numberCheck)) {
            console.log("Address consists only of numbers!");
            viewModel[property].isCorrect(false);
            return;
        }

        //variable that stores the previous location of marker
        var oldPosition = null;
        //var oldLocation = viewModel[property].marker.location
        if (viewModel[property].marker.position != undefined) {
            oldPosition = viewModel[property].marker.position;
        }
        //if no city specified add it
        address = map.addToAddressKeyWords(address);
        var curAddress;
        //send request to google maps geocoder and return MapPoint object
        map.geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                curAddress = results[0].formatted_address + map.RegionOfStreet(results[0].address_components);
                //check if address is not valid, show errot and exit
                if (!map.IsAddressValid(curAddress)) {
                    viewModel[property].isCorrect(false);
                    console.log("Address do not exist!.");
                    if (oldPosition != null) {
                        // it doesn't work correctly
                        viewModel[property].marker.setPosition(oldPosition);
                    }
                }
                //check if address ok
                else {
                    viewModel[property].marker.setPosition(results[0].geometry.location);
                    viewModel[property].name(map.delFromAddressUnwantedValues(curAddress));
                    viewModel[property].marker.setVisible(true);
                    viewModel[property].isValid(true);
                    viewModel[property].isCorrect(true);

                }
                //scale with respect to markers
                if (viewModel["startPoint"].isValid() && viewModel["endPoint"].isValid() && !viewModel.isPathFined()) {
                    map.fitToBounds(viewModel["startPoint"], viewModel["endPoint"]);
                };

                //call success function
                success();
            }
            //if address geocoding error
            else {
                //return marker to the last place if address geocoding error
                if (oldPosition != null) {
                    // it doesn't work correctly
                    viewModel[property].marker.setPosition(oldPosition);
                };
                console.log("Address do not exist!.");
                console.log("Geocoder error!");
            }
        });

    }
};