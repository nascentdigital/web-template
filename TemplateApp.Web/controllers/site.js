/**
 * SiteController
 */
define(

    // load dependencies
    [
        'app'
    ],

    // define module
    function (app)
    {
        // create controller
        app.controller('SiteController',
            function ($state, $scope)
            {
                console.log('site controller');
            });
    });
