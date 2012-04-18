/// <reference path="bootstrap-typeahead.js" />
//object that incapsulates all controls work
var controls = {
    //controls settings
    settings: {
        selectors: {
            startAddressSelector: "#StartPointAddress",
            startAddressErrorSelector: "#startAddressError",
            endAddressSelector: "#EndPointAddress",
            endAddressErrorSelector: "#endAddressError",
            makeRouteButtonSelector: "#makeRouteBtn",
            textVersionButtonSelector: "#textVersionBtn",
            printTextVersionButtonSelector: "#printBtn",
            loadingBarBlock: ".loadingBar"
        },
        labels: {
            addressPlaceholderText: ""
        }
    },

    makeRoute: function () {

        var postData = {
            StartPointLongitude: path.startPoint.location.lat(),
            StartPointLatitude: path.startPoint.location.lng(),
            EndPointLongitude: path.endPoint.location.lat(),
            EndPointLatitude: path.endPoint.location.lng()
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
    },

    //init function
    init: function () {
        var settings = this.settings, selectors = settings.selectors;

        //style buttons
        $('.btn').button();

        //TODO remove hardcoding
        $('.nav-collapse').on('shown', function () {
            $('.main-container').css('top', '186px');
        });

        $('.nav-collapse').on('hide', function () {
            $('.main-container').css('top', '40px');
        });

        $('#text-version-wrapper').hide();

        //add "address" class to text-boxes
        $(selectors.startAddressSelector).addClass("address");
        $(selectors.endAddressSelector).addClass("address");

        $(selectors.startAddressSelector).tooltip({
            placement: 'right',
            title: localizedMessages["AdressError"],
            trigger: 'manual'
        });
        $(selectors.endAddressSelector).tooltip({
            placement: 'right',
            title: localizedMessages["AdressError"],
            trigger: 'manual'
        });


        //set text-box placeholders for non-ie browsers
        if (!$.browser.msie) {
            //placeholder label
            $(".address").attr("placeholder", localizedMessages["EnterLocation"]);

            //on focus remove it
            $(".address").focus(function () {
                $(this).attr("placeholder", "");
            });

            //on blur restore it if needed
            $(".address").blur(function () {
                if ($(this).val() == '') {
                    $(this).attr("placeholder", localizedMessages["EnterLocation"]);
                };
            });
        }
        //placeholders for IE
        else {
            setTimeout(function () {
                $('.address').focus(function () {
                    if ($(this).attr("placeholder") == localizedMessages["EnterLocation"]) {
                        $(this).attr("placeholder", "");
                        $(this).val('');
                        $(this).css("color", '#000');
                    }
                });
            }, 100);

            //placeholder label
            $(".address").val(localizedMessages["EnterLocation"]);
            $(".address").css("color", '#ccc');

            //on blur restore it if needed
            $(".address").blur(function () {
                if ($(this).val() == '') {
                    $(this).attr("placeholder", localizedMessages["EnterLocation"]);
                    $(this).val(localizedMessages["EnterLocation"]);
                    $(this).css("color", '#ccc');
                };
            });
        }

        //close context menu
        $(selectors.startAddressSelector).focusin(function () {
            map.menu.closeMenu();
        });

        $(selectors.endAddressSelector).focusin(function () {
            map.menu.closeMenu();
        });
    }
};