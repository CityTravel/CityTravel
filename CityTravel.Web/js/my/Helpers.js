//functions, which uses in different objects
var helpers = {
    //resolves full path for url's. Url must be without first slash: GetPath("makeroute/index").
    GetPath: function (url) {
        var loc = location.href;
        if (loc.charAt(loc.length - 1) != '/')
            loc += '/';
        return loc + url;
    }

};

ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        var value = valueAccessor();
        $(element).toggle(ko.utils.unwrapObservable(value));
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        ko.utils.unwrapObservable(value) ? $(element).fadeIn("slow") : $(element).fadeOut("slow");
    }
}