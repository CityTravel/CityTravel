var selectedShape;
var polyline = null;
var polylineCoords = [];

var Engine = {

    initStop: function () {
        this.initMap();
        this.prepAddStop();
        this.addEvents();
    },

    initRoute: function () {
        //  this.initMap();
        this.prepAddRoute();
    },
    initNonRoute: function () {
        // this.initMap();
        this.predAddRoute_NonRoaded();
    },
    map: null,
    initMap: function () {
        dirDisplay = new google.maps.DirectionsRenderer();
        var startLatlng = new google.maps.LatLng(48.46306197546078, 35.04905941284187); // отобразить центр Днепропетровска
        var mapOptions =
			{
			    zoom: 14,
			    center: startLatlng,
			    mapTypeId: google.maps.MapTypeId.ROADMAP
			};
        map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
    },
    addEvents: function () {
        $("#RouteList").change(function () {
            var id = $(this).find("option:selected").val();
            var a = '(@Model)';
            var b = 1;
        });
    },
    predAddRoute_NonRoaded: function () {
        polyline = new google.maps.Polyline({
            strokeColor: "#FF0000", // Цвет
            strokeOpacity: 1.0, // Прозрачность
            strokeWeight: 1 // Ширина
        });

        google.maps.event.addListener(map, 'rightclick', function (event) {
            Engine.add_coords_to_Polyline(event.latLng);
        });
        google.maps.event.addListener(polyline, 'click', function (event) {
            Engine.setSelection_NonRoaded(polyline);
        });
    },

    add_coords_to_Polyline: function (location) {
        var latlng = new google.maps.LatLng(location.lat(), location.lng());
        polylineCoords.push(latlng);
        polyline.setPath(polylineCoords);
        polyline.setMap(map);
        this.setSelection_NonRoaded(polyline);
    },

    setSelection: function (shape) {
        Engine.clearSelection();
        selectedShape = shape;
        shape.setEditable(true);
        //хитро используем координаты от гугла для фигуры, что бы привести их к виду, который воспримет SQLGeography
        var coords = "POLYGON((" + selectedShape.getBounds().getNorthEast().lng() + " " + selectedShape.getBounds().getNorthEast().lat() + ", " + selectedShape.getBounds().getNorthEast().lng() + " " + selectedShape.getBounds().getSouthWest().lat() + ", " + selectedShape.getBounds().getSouthWest().lng() + " " + selectedShape.getBounds().getSouthWest().lat() + ", " + selectedShape.getBounds().getSouthWest().lng() + " " + selectedShape.getBounds().getNorthEast().lat() + ", " + selectedShape.getBounds().getNorthEast().lng() + " " + selectedShape.getBounds().getNorthEast().lat() + "))";
        $("#StopGeography").val(coords);
    },


    clearSelection: function () {
        if (selectedShape) {
            selectedShape.setEditable(false);
            selectedShape = null;
        }
    },

    setSelection_NonRoaded: function (shape) {
        shape.setEditable(true);
        var coords = shape.getPath().b;
        var coord_text = "";
        for (var i = 0; i < coords.length; i++) {
            coord_text += coords[i].lng() + " " + coords[i].lat() + ", ";
        }
        coord_text = coord_text.substring(0, coord_text.lastIndexOf(', '));
        $("#RouteGeography").val(coord_text);
    },

    FillMapWithStops: function (data) {
        for (var i = 0; i < data.length; i++) {
            var myLatlng = new google.maps.LatLng(parseFloat(data[i].lat), parseFloat(data[i].lng));
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data[i].name
            });
        }
    },


    FillMapWithRoutes: function (data) {
        var marker;
        this.addStopToolTip = function (shape, name) {
            google.maps.event.addListener(shape, 'mouseover', function (Event) {
                marker = new google.maps.Marker({
                    position: Event.latLng,
                    map: map,
                    title: name,
                    icon: 'https://chart.googleapis.com/chart?chst=d_fnote&chld=arrow_d|1|000000|h|' + name
                });
            });

            google.maps.event.addListener(shape, 'mouseout', function (Event) {
                marker.setMap(null);
            });
        };

        for (var i = 0; i < data.length; i++) {
            var name = null;
            name = data[i].name;
            var routeCoords = new Array();

            var coords = null;
            coords = data[i].coord;
            for (var j = 0; j < coords.length - 1; j++) {
                var latlng = null;
                latlng = new google.maps.LatLng(parseFloat(coords[j].lat), parseFloat(coords[j].lng));
                routeCoords.push(latlng);
            }
            var route = new google.maps.Polyline({
                strokeColor: '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6), // Цвет
                strokeOpacity: 1.0, // Прозрачность
                strokeWeight: 3 // Ширина
            });
            route.setPath(routeCoords);
            route.setMap(map);
            this.addStopToolTip(route, name);
        }
    },
    selectedShape: null,

    prepAddStop: function () {
        //Самая важная часть - использование рисовалки от гугла:
        var drawingManager = new google.maps.drawing.DrawingManager({
            drawingControl: true,
            drawingControlOptions: {
                position: google.maps.ControlPosition.TOP_CENTER,
                drawingModes: [google.maps.drawing.OverlayType.CIRCLE, google.maps.drawing.OverlayType.RECTANGLE] //резрешаем круг и ректангл
            },
            circleOptions: {
                fillColor: '#FC1E56',
                fillOpacity: 0.40,
                strokeWeight: 1,
                clickable: true,
                zIndex: 1,
                editable: true
            }
        });
        //после заверешния отрисовки устанавливаем обработчики, и выделяем последнюю обрисованную фигуру.
        //обработчики фактически следят, что бы фиксировалось исменение центра координат фигуры.
        google.maps.event.addListener(drawingManager, 'overlaycomplete', function (e) {
            if (e.type != google.maps.drawing.OverlayType.MARKER) {
                drawingManager.setDrawingMode(null);
                var shape = e.overlay;
                shape.type = e.type;
                google.maps.event.addListener(shape, 'click', function () {
                    Engine.setSelection(shape);
                });
                google.maps.event.addListener(shape, 'center_changed', function () {
                    Engine.setSelection(shape);
                });

                Engine.setSelection(shape);
            }
        });
        drawingManager.setMap(map);
        //отрисовываем все остановки из бд
        $.getJSON('\GetAllStops', function (data) {
            Engine.FillMapWithStops(data);
            $.getJSON('\GetAllRoutes', function (data) {
                Engine.FillMapWithRoutes(data);
            });
        });
    },

    prepAddRoute: function () {
        var rendererOptions = {
            draggable: true //делаем маркеры доступными для перетаскивания
        };
        directionsDisplay = new google.maps.DirectionsRenderer(rendererOptions);
        directionsService = new google.maps.DirectionsService();
        google.maps.event.addListener(map, 'rightclick', function (event) { //обработчик события нажатия правой кнопки мыши
            Engine.makeroute(event.latLng);
        });
        google.maps.event.addListener(directionsDisplay, 'directions_changed', function () { //обработчик собыитя изменения маршрута
            Engine.computeTotalDistance(directionsDisplay.directions);
        });
        geocoder = new google.maps.Geocoder();
        directionsDisplay.setMap(map);
        start = "";
        end = "";
        way = [];
    },

    //Функция для прокладки и отображения маршрута
    makeroute: function (location) {
        //        var marker = new google.maps.Marker({
        //                position: location,
        //                map: map
        //            });
        if (start == "" && end == "") {
            start = location; //устанавливаем начальное значение, если не заданно еще
        }
        else {
            if (end == "" && start != "") { //устанавливаем конечное значение, если оно еще не установлено и прокладываем путь.
                end = location;
                var request = {
                    origin: start,
                    waypoints: way,
                    destination: end,
                    travelMode: google.maps.TravelMode.WALKING
                };
                directionsService.route(request, function (response, status) {
                    if (status == google.maps.DirectionsStatus.OK) {
                        directionsDisplay.setDirections(response);
                    }
                });
            } else {
                if (start != null && end != null) { //устанавливаем промежуточные значения и прокладываем путь.
                    way.push({
                        location: end,
                        stopover: false
                    });
                    end = location;
                    var request = {
                        origin: start,
                        waypoints: way,
                        destination: end,
                        travelMode: google.maps.TravelMode.WALKING
                    };
                    directionsService.route(request, function (response, status) {
                        if (status == google.maps.DirectionsStatus.OK) { //отрисовываем всю эту ерунду на карте
                            directionsDisplay.setDirections(response);
                        }
                    });
                }
            }
        }
    },

    computeTotalDistance: function (res) {
        steps = res.routes[0].legs[0].steps; //массив объектов шагов нашего маршрута
        var total = 0;
        var myroute = res.routes[0];
        for (i = 0; i < myroute.legs.length; i++) {
            total += myroute.legs[i].distance.value;
        }
        total = total / 1000;
        $("#DistanceText").val(total + " км");
        text = "";
        //берем все части каждого шага и записываем их в строку, которую поймет SQLGeography
        for (var i = 0; i < steps.length; i++) {
            for (var j = 0; j < steps[i].path.length; j++) {
                text = text + steps[i].path[j].lng() + " " + steps[i].path[j].lat() + ", ";
            }

        }
        text = text.substring(0, text.lastIndexOf(', '));
        $("#RouteGeography").val(text);
    },

    setSelection_NonRoaded: function (shape) {
        shape.setEditable(true);
        var coords = shape.getPath().b;
        var coord_text = "";
        for (var i = 0; i < coords.length; i++) {
            coord_text += coords[i].lng() + " " + coords[i].lat() + ", ";
        }
        coord_text = coord_text.substring(0, coord_text.lastIndexOf(', '));
        $("#RouteGeography").val(coord_text);
    }
};

$(document).ready(function () {
    Engine.initStop();
    Engine.initRoute();
    Engine.initNonRoute();
});