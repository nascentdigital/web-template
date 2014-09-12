/**
 * ProjectController
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
        app.controller('ProjectController',
            function ($state, $scope, appModel, project)
            {
                console.log('project controller');

                // bind scope
                $scope.project = project;

            });
    });
