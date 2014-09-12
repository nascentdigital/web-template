/**
 * HomeController
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
        app.controller('HomeController',
            function ($state, $scope, appModel)
            {
                console.log('home controller');
            });
    });
