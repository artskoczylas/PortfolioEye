(function () {

    window.deleteElementById = function (id) {
        var element = document.getElementById(id);
        element.parentNode.removeChild(element);
    }

})();