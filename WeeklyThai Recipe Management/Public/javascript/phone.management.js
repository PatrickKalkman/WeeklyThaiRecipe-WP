/// <reference path="jquery-1.6.2-vsdoc.js" />

var toastInitializer = function () {

    var initialize = function (sendToastUrl) {

        $('#sendToastButton').click(function () {

            var toastTitle = $('#toastTitle').val();
            var toastMessage = $('#toastMessage').val();

            if (toastTitle.length > 0 && toastMessage.length > 0) {
                $.ajax({
                    url: sendToastUrl,
                    success: function (data) {
                        alert('Toast is send');
                    },
                    data: {
                        toastTitle: toastTitle,
                        toastMessage: toastMessage
                    },
                    dataType: "json",
                    type: "POST"
                });
            }
        });
    };

    return {
        initialize: initialize
    };

};

var tileInitializer = function () {

    var initialize = function (sendTileUrl) {

        $('#sendTileButton').click(function () {

            var frontTitle = $('#frontTitle').val();
            var numberToShow = $('#numberToShow').val();
            var frontImageLocation = $('#frontImageLocation').val();
            var backTitle = $('#backTitle').val();
            var backImageLocation = $('#backImageLocation').val();
            var backContent = $('#backContent').val();

            if (frontTitle.length > 0 && numberToShow.length > 0 && frontImageLocation.length > 0 && backTitle.length > 0 && backImageLocation.length > 0 && backContent.length > 0) {
                $.ajax({
                    url: sendTileUrl,
                    success: function (data) {
                        alert('Tile is send');
                    },
                    data: {
                        frontTitle: frontTitle,
                        numberToShow: numberToShow,
                        frontImageLocation: frontImageLocation,
                        backTitle: backTitle,
                        backImageLocation: backImageLocation,
                        backContent: backContent
                    },
                    dataType: "json",
                    type: "POST"
                });
            }
        });
    };

    return {
        initialize: initialize
    };
};

var recipeSaver = function () {

    var initalize = function (saveRecipeUrl) {

        $('#saveRecipeButton').click(function () {

            var recipe = $('#recipeXml').val();

            $.ajax({
                url: saveRecipeUrl,
                success: function (data) {
                    alert('Recipe is saved');
                },
                data: {
                    recipe: recipe
                },
                dataType: "json",
                type: "POST"
            });
        });
    };

    return {
        initalize: initalize
    };
};

var generalErrorHandler = function() {

    var initialize = function() {
        $.ajaxSetup({
            "error": function (xhr) {
                var errorMessage = $('#errorMessage');
                if (errorMessage.length > 0) {
                    errorMessage.html(xhr.responseText);
                }

                var errorDialog = $('#errorDialog');
                if (errorDialog.length > 0) {
                    errorDialog.show();
                }
            } 
        });
    };

    return {
        initialize: initialize
    };
};