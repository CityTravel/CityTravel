//object that incapsulates path finding logic
var ICON_BUS_STOP = "Content/images/circle-small.png";

function Path() {

    //    this.startPoint = null;
    //    this.endPoint = null;
    this.startPoint = viewModel.startPoint.marker;
    this.endPoint = viewModel.endPoint.marker;
    this.previousStartPosition = "";
    this.previousEndPosition = "";
    this.startMakerRouteBloker = "";
    this.endMakerRouteBloker = "";
    this.stopsMarkers = [];

    this.directionsService = new google.maps.DirectionsService();

    this.startLandingDirectionsRenderer = new google.maps.DirectionsRenderer({
        preserveViewport: true,
        routeIndex: 0,
        polylineOptions: {
            style: "dashed",
            strokeColor: "#00FF00"
        }
    });

    this.startLandingDirectionsRenderer.suppressMarkers = true;
    this.endLandingDirectionsRenderer = new google.maps.DirectionsRenderer({
        preserveViewport: true,
        routeIndex: 0,
        polylineOptions: {
            style: "dashed",
            strokeColor: "#00FF00"
        }
    });

    this.endLandingDirectionsRenderer.suppressMarkers = true;

    this.busPolyLineOptions = {
        style: "dashed",
        strokeColor: "#4585FF",
        strokeOpacity: 1.0,
        strokeWeight: 5
    };

    this.startWalkPolyLineOptions = {
        style: "dashed",
        strokeColor: "#58C56F",
        strokeOpacity: 1.0,
        strokeWeight: 5
    };

    this.endWalkPolyLineOptions = {
        style: "dashed",
        strokeColor: "#58C56F",
        strokeOpacity: 1.0,
        strokeWeight: 5
    };

    this.walkPolyLineOptions = {
        style: "dashed",
        strokeColor: "#58C56F",
        strokeOpacity: 1.0,
        strokeWeight: 5
    };


    this.busPolyLine = new google.maps.Polyline(this.busPolyLineOptions);
    this.startWalkPolyLine = new google.maps.Polyline(this.startWalkPolyLineOptions);
    this.endWalkPolyline = new google.maps.Polyline(this.endWalkPolyLineOptions);
    this.walkPolyline = new google.maps.Polyline(this.walkPolyLineOptions);


    this.render = function (data) {
        path.clear();
        legend.reset();
        var route = new Array(data.route.MapPoints.length);
        var routeBounds = new google.maps.LatLngBounds();

        for (var i = 0; i < route.length; i++) {
            route[i] = new google.maps.LatLng(data.route.MapPoints[i].Longitude, data.route.MapPoints[i].Latitude);
            routeBounds.extend(route[i]);
            PolyLinePrint = route; //  сохраняем объект "Маршрут" транспорта (для распечатки)
        }

        routeBounds.extend(this.startPoint.position);
        routeBounds.extend(this.endPoint.position);
        map.mapObject.setCenter(routeBounds.getCenter());
        map.mapObject.fitBounds(routeBounds);

        if (data.route.Type.Type !== "Walking") {

            var startWalkingStep = new Array(data.route.WalkingRoutes[0].MapPoints.length);
            var endWalkingStep = new Array(data.route.WalkingRoutes[1].MapPoints.length);

            for (var i = 0; i < startWalkingStep.length; i++) {
                startWalkingStep[i] = new google.maps.LatLng
                (data.route.WalkingRoutes[0].MapPoints[i].Longitude, data.route.WalkingRoutes[0].MapPoints[i].Latitude);
            }

            for (var i = 0; i < endWalkingStep.length; i++) {
                endWalkingStep[i] = new google.maps.LatLng
                (data.route.WalkingRoutes[1].MapPoints[i].Longitude, data.route.WalkingRoutes[1].MapPoints[i].Latitude);
            }

            this.startWalkPolyLine.setPath(startWalkingStep);
            this.startWalkPolyLine.setMap(map.mapObject);
            this.endWalkPolyline.setPath(endWalkingStep);
            this.endWalkPolyline.setMap(map.mapObject);
            this.busPolyLine.setPath(route);
            this.busPolyLine.setMap(map.mapObject);
            this.renderStops(data.route.Stops);

            //    		textVersion.showStepsToLand(data.route);
            //    		textVersion.showStepsFromLand(data.route);

            legend.render(TransportType.Walking, "");
            $(legend.settings.selector).append("<br/>");
            legend.render(TransportType.Bus, data.route.Name);

        } else {

            //    		textVersion.showWalkingSteps(data.route);
            this.walkPolyline.setPath(route);
            this.walkPolyline.setMap(map.mapObject);
            legend.render(TransportType.Walking, "");

        }
    };


    this.renderStops = function (stops) {
        var markers = this.stopsMarkers;
        if (markers != null) {
            for (i in markers) {
                markers[i].setMap(null);
            }
        }

        var markerIcon = helpers.GetPath(ICON_BUS_STOP);

        for (var i = 0; i < stops.length; i++) {
            var location = new google.maps.LatLng(stops[i].Points.Longitude, stops[i].Points.Latitude);

            var marker = new google.maps.Marker({
                position: location,
                map: map.mapObject,
                icon: markerIcon

            });

            this.addInfoWindow(marker, stops[i].Name);
            markers.push(marker);


        }
    };

    this.addInfoWindow = function (marker, stopName) {

        google.maps.event.addListener(marker, 'mouseover', function (mEvent) {
            var point = convertToPoint(mEvent.latLng, this.map);
            var map = $("#googleMap").offset();
            $("#tooltip").show().css({ left: map.left + point.x + 20, top: map.top + point.y - 60 }).html(stopName);

        });
        google.maps.event.addListener(marker, 'mouseout', function (mEvent) {
            $("#tooltip").hide();
        });
        var convertToPoint = function (latLng, map) {
            var topRight = map.getProjection().fromLatLngToPoint(map.getBounds().getNorthEast());
            var bottomLeft = map.getProjection().fromLatLngToPoint(map.getBounds().getSouthWest());
            var scale = Math.pow(2, map.getZoom());
            var worldPoint = map.getProjection().fromLatLngToPoint(latLng);
            return new google.maps.Point((worldPoint.x - bottomLeft.x) * scale, (worldPoint.y - topRight.y) * scale);
        };

    };

    this.clear = function () {
        var markers = this.stopsMarkers;
        if (markers != null) {
            for (i in markers) {
                markers[i].setMap(null);
            }
        }
        this.busPolyLine.setMap(null);
        this.walkPolyline.setMap(null);
        this.startWalkPolyLine.setMap(null);
        this.endWalkPolyline.setMap(null);

        path.walkPolyline.latLngs.b[0].length = 0;
        path.busPolyLine.latLngs.b[0].length = 0;
        path.startWalkPolyLine.latLngs.b[0].length = 0;
        path.endWalkPolyline.latLngs.b[0].length = 0;

        printLegendBar.length = 0; // тип и линия маршрута в легенде 
        printLegendText.length = 0; // текст в легенде

        //    	textVersion.string = null; // обнулить текстовую версию 
    };
};