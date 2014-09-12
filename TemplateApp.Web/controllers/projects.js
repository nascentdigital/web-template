/**
 * ProjectsController
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
        app.controller('ProjectsController',
            function ($state, $scope, appModel, projects)
            {
                console.log('projects controller');

                // bind scope
                $scope.projects = projects;

            });
    });
