var ICON_START_POINT = "Content/images/pin-A-drag.png";
var ICON_END_POINT = "Content/images/pin-B.png";
// Class to represent a route
function Route(data) {
    var self = this;
    self.route = data;
    self.name = data.Name;
    self.selected = function () {
        return viewModel.isCurrentRoute(self.name);
    };
}

// Overall viewmodel for this screen, along with initial state
function MakeRouteViewModel() {
    var self = this;
    self.viewModel = ko.observableArray([]);
    self.currentRoute = ko.observable();
    self.priority = ko.observable("Time");

    self.routeListShown = ko.observable(false);

    self.showLoadingBar = function () {
        viewModel.startPoint.marker.setDraggable(false);
        viewModel.endPoint.marker.setDraggable(false);
        $(controls.settings.selectors.loadingBarBlock).fadeIn(2000, function () { });
    };
    self.hideLoadingBar = function () {
        // Enable the property "draggable" for both markers
        viewModel.startPoint.marker.setDraggable(true);
        viewModel.endPoint.marker.setDraggable(true);
    };
    //    self.load_feedback = function () {
    //        $('#feedback').modal('toggle');
    //    };
    // Load initial state from server, convert it to Route instances, then populate self.routeList
    self.load = function (allRoutes) {
        var routes = [],
            length = allRoutes.length;

        // hide the loading bar
        $(controls.settings.selectors.loadingBarBlock).fadeOut(
                1400,
                function () { self.hideLoadingBar(); }
        );

        for (var i = 0; i < length; i++) {
            var route = allRoutes[i];
            route.Number = route.Name.substring(route.Name.indexOf('№'));
            routes.push(route);
        }

        self.viewModel($.map(routes, function (item) { return new Route(item); }));

        switch (self.priority()) {
            case "Time":
                self.sortByTime();
                break;
            case "Money":
                self.sortByMoney();
                break;
        }
        if (length) {
            self.routeListShown(true);
            self.setCurrentRoute(self.viewModel()[0]);
        } else {
            ///TODO 
            window.alert('Sorry, nothing!');
        }
    };

    self.isCurrentRoute = function (routeName) {
        if (self.currentRoute() == undefined) return false;
        return routeName == self.currentRoute().name;
    };

    self.setCurrentRoute = function (route) {
        path.render(route);
        self.currentRoute(route);
    };

    self.routeType = ko.computed(function () {
        if (self.currentRoute() != undefined) {
            return self.currentRoute().route.Type.Type;
        } else {
            return '';
        }
    });

    self.routeListClear = function () {
        self.viewModel.splice(0, self.viewModel().length);
    };

    self.priority.subscribe(function (newValue) {
        self.priority(newValue);
        switch (newValue) {
            case "Time":
                self.sortByTime();
                break;
            case "Money":
                self.sortByMoney();
                break;
        }

        if (self.viewModel().length > 0) {
            self.setCurrentRoute(self.viewModel()[0]);
        }
    });

    self.sortByMoney = function () {
        self.viewModel.sort(function (a, b) {
            return a.route.Cost < b.route.Cost ? -1 : 1;
        });
    };

    self.sortByTime = function () {
        self.viewModel.sort(function (a, b) {
            return a.route.TotalMinutes < b.route.TotalMinutes ? -1 : 1;
        });
    };

    self.getWalkingRouteLength = function (route, index) {
        if (route.WalkingRoutes != null) {
            return route.WalkingRoutes[index].Length;
        } else {
            return "0";
        }
    };

    //ViewModel for Controls
    self.startPoint = {
        name: ko.observable(""),
        isSelected: ko.observable(false),
        isValid: ko.observable(false),
        isCorrect: ko.observable(true),
        marker: new google.maps.Marker({
            map: null,
            draggable: true,
            icon: helpers.GetPath(ICON_START_POINT)
        })
    };

    self.endPoint = {
        name: ko.observable(""),
        isSelected: ko.observable(false),
        isValid: ko.observable(false),
        isCorrect: ko.observable(true),
        marker: new google.maps.Marker({
            map: null,
            draggable: true,
            icon: helpers.GetPath(ICON_END_POINT)
        })
    };

    self["endPoint"].isCorrect.subscribe(function (newValue) {
        if (newValue == false) $(controls.settings.selectors.endAddressSelector).tooltip('show');
        if (newValue == true) $(controls.settings.selectors.endAddressSelector).tooltip('hide');
    });

    self["startPoint"].isCorrect.subscribe(function (newValue) {
        if (newValue == false) $(controls.settings.selectors.startAddressSelector).tooltip('show');
        if (newValue == true) $(controls.settings.selectors.startAddressSelector).tooltip('hide');
    });


    self.isPathFined = ko.observable(false);

    self.isPathPossible = ko.computed(function () {
        return self["startPoint"].isValid() && self["endPoint"].isValid()
            && self["startPoint"].isCorrect() && self["endPoint"].isCorrect()
            && self["startPoint"].name() != "" && self["endPoint"].name() != ""
            && !self.isPathFined();
    });

    self["startPoint"].isSelected.subscribe(function (newValue) {
        if (newValue == false) {
            map.resolveAddress(self["startPoint"].name(), path, "startPoint", function () { });
            if (self.textVersion.shown() === true) {
                self.textVersion.show();
            }
        }
    });

    self["endPoint"].isSelected.subscribe(function (newValue) {
        if (newValue == false) {
            map.resolveAddress(self["endPoint"].name(), path, "endPoint", function () { });
            if (self.textVersion.shown() === true) {
                self.textVersion.show();
            }
        }
    });

    self["startPoint"].name.subscribe(function (newValue) { self.isPathFined(false); });

    self["endPoint"].name.subscribe(function (newValue) { self.isPathFined(false); });

    self.isPathFined.subscribe(function (newValue) {
        if (newValue == false) {
            path.clear();
            viewModel.routeListClear();
            $(legend.settings.selector).hide();
        }
    });

    self.swap = function () {
        var tempName = self.startPoint.name();
        var tempLoc = self.startPoint.marker.position;
        var tempCorrect = self.startPoint.isCorrect();
        var tempValid = self.startPoint.isValid();
        
        self.startPoint.name(self.endPoint.name());
        self.endPoint.name(tempName);

        self.endPoint.marker.setVisible(true);
        self.startPoint.marker.setVisible(true);

        self.startPoint.isCorrect(self.endPoint.isCorrect());
        self.endPoint.isCorrect(tempCorrect);

        self.startPoint.isValid(self.endPoint.isValid());
        self.endPoint.isValid(tempValid);

        if (self.endPoint.name() == "") {
            self.startPoint.marker.setPosition(self.endPoint.marker.position);
            self.endPoint.marker.setVisible(false);
        } else {
            if (self.startPoint.name() == "") {
                self.endPoint.marker.setPosition(self.startPoint.marker.position);
                self.startPoint.marker.setVisible(false);
            } else {
                self.startPoint.marker.setPosition(self.endPoint.marker.position);
                self.endPoint.marker.setPosition(tempLoc);
            }
        }
        self.isPathFined(false);
    };

    // find path, if it's possible
    self.findPath = function () {
        if (self.isPathPossible()) {
            if (self.startPoint.name() === self.endPoint.name()) {
                showStickyNoticeToast();
            } 
            else {
                self.getRoutes();
                self.showLoadingBar();
                self.isPathFined(true);
            }
            
        }
    };

    self.getRoutes = function () {
        var postData = {
            AddressPointA: viewModel.startPoint.name,
            AddressPointB: viewModel.endPoint.name,
            StartPointLongitude: viewModel.startPoint.marker.position.lat(),
            StartPointLatitude: viewModel.startPoint.marker.position.lng(),
            EndPointLongitude: viewModel.endPoint.marker.position.lat(),
            EndPointLatitude: viewModel.endPoint.marker.position.lng()
        };

        var jData = JSON.stringify(postData);
        $.ajax({
            url: helpers.GetPath("makeroute/index"),
            type: "POST",
            dataType: 'json',
            data: jData,
            contentType: 'application/json; charset=utf-8',
            success: function (returnedData) {
                viewModel.load(returnedData);
            }
        });
    };



    self.textVersion = {
        string: '',

        shown: ko.observable(false),

        show: function () {
            self.textVersion.shown() ? self.textVersion.shown(false) : self.textVersion.shown(true);
            self.routeListShown() ? self.routeListShown(false) : self.routeListShown(true);
        },

        removeBadSigns: function (str, filter) {
            var strArr = str.split(filter);

            if (strArr.length != 0) {

                str = "";
                for (var i = 0; i < strArr.length; i++) {
                    str += strArr[i];
                }
            }

            return str;
        },

        Steps: ko.computed(function () {
            var result = "";
            if (self.currentRoute() != undefined && self.routeType() == "Walking") {
                result = self.textVersion.walkingSteps(self.currentRoute().route);
            }
            if (self.currentRoute() != undefined && self.routeType() != "Walking") {
                result = self.textVersion.transportSteps(self.currentRoute().route);
            }

            return result;
        }),

        showStepsToLand: function (route) {

            var transportSteps = "";
            var steps = route.WalkingRoutes[0].Steps;
            var firstStop = route.Stops[0].Name;
            transportSteps = "<ol>";

            for (var i = 0; i < steps.length; i++) {
                transportSteps += " " + '<li style="margin-top:1em;" >' + " " + steps[i].Instruction + self.textVersion.addCSSStyles(steps[i].Time, steps[i].Length) + '</li>';
            }
            transportSteps += " " + '<li style="margin-top:1em">' + localizedMessages["Sit"] + " " + '<b><span class="text-transport">' + route.Name.replace(route.Name.slice(0, route.Name.lastIndexOf('а') + 1), "маршрутку") + " " +
                '</span></b>' + localizedMessages["OnStop"] + " " + '<b>' + firstStop + '</b>'
                + self.textVersion.addCSSStyles(route.WaitingTime, route.Price) + '</li>';


            return transportSteps;

        },
        showStepsFromLand: function (route) {
            var transportSteps = "";
            var steps = route.WalkingRoutes[1].Steps;
            var lastStop = route.Stops[route.Stops.length - 1].Name;
            transportSteps += " " + '<li style="margin-top:1em;">' + localizedMessages["RideUntil"] + " " + '<b>' + lastStop + '</b>' + " " + self.textVersion.addCSSStyles(route.Time, route.BusLength) + '</li>';

            for (var i = 0; i < steps.length; i++) {
                transportSteps += " " + '<li style="margin-top:1em">' + ' ' + steps[i].Instruction + self.textVersion.addCSSStyles(steps[i].Time, steps[i].Length) + '</li>';
            }

            transportSteps += '<br />';
            transportSteps += self.textVersion.addTransportSummaryCSSStyles(route.Price, self.currentRoute().route.Time, self.currentRoute().route.AllLength); // get summary parameters of the route

            transportSteps += " " + '</ol>';

            return transportSteps;
        },

        transportSteps: function (route) {
            var transportStepsToLand = self.textVersion.showStepsToLand(route);
            var transportStepsFromLand = self.textVersion.showStepsFromLand(route);
            var result = transportStepsToLand + transportStepsFromLand;

            return result;

        },
        walkingSteps: function (route) {
            var stringSteps = "";
            var steps = route.Steps;
            stringSteps = "<ol>";
            for (var i = 0; i < steps.length; i++) {
                stringSteps += " " + '<li style="margin-top:1em";>' + steps[i].Instruction + self.textVersion.addCSSStyles(steps[i].Time, steps[i].Length) + '</li>';
            }
            stringSteps += "</ol><br>";
            stringSteps += self.textVersion.addWalkSummaryCSSStyles(route.AllLength, route.Time);
            return stringSteps;
        },
        
        addCSSStyles: function (firstValue, secondValue) {
            var result = firstValue != null && secondValue
                ? '<div style="display:inline-block;float:right"><span class="label label-success" style="margin-right:5px;min-width:35px;display:inline-block;" >'
                    + firstValue + '</span>' + '<span class="label label-info" style="display:inline-block;min-width:35px;" >' + secondValue + '</span></div>'
                    : "";
            return result;
        },
        addTransportSummaryCSSStyles: function (distance, time, money) {
            var result = distance != null && time && money != null
                ? '<div style="text-align:center;">' +
                    '<span class="label label-success" style="margin-right:5px;min-width:40px;display:inline-block;" >' + distance + '</span>' + 
                    '<span class="label label-info" style="min-width:40px;display:inline-block;" >' + time + '</span>' +
                    '<span class="label label-warning" style="margin-left:5px;min-width:40px;display:inline-block;" >' + money + '</span></div>'
                : "";

            return result;
        },
        addWalkSummaryCSSStyles: function (distance, time) {
            var result = distance != null && time
                ? '<div style="text-align:center">' +
                    '<span class="label label-success" style="margin-right:5px;min-width:40px;display:inline-block;" >' + distance + '</span>' +
                    '<span class="label label-info" style="margin-left:5px;min-width:40px;display:inline-block;" >' + time + '</span></div>'
                : "";

            return result;
        }
    };
}

ko.bindingHandlers.slideVisibleLeft = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(), allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        var duration = allBindings.slideDuration || 400;

        if (valueUnwrapped == true) {
            $(element).show();
            $(element).animate({ left: '+=400' }, duration);
        } else {
            $(element).animate({ left: '-=400' }, duration, function () { $(element).hide(); });
        }
    }
};

ko.bindingHandlers.slideVisibleRight = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(), allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        var duration = allBindings.slideDuration || 400;

        if (valueUnwrapped == true) {
            $(element).show();
            $(element).animate({ left: '-=400' }, duration);
        } else {
            $(element).animate({ left: '+=400' }, duration, function () { $(element).hide(); });
        }
    }
};