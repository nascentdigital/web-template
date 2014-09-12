/**
 * Project API.
 */
define(

    // load dependencies
    [
        'app'
    ],

    // define module
    function (app)
    {
        // define singleton for profile service
        app.factory('projectApi', function ($resource, apiUrl)
        {
            var projectApi = $resource(apiUrl + 'project/:projectId',
                { projectId: '@Id' },
                {
                    query:
                    {
                        method: 'GET',
                        url: apiUrl + 'projects',
                        isArray: true
                    },
                    get:
                    {
                        method: 'GET'
                    }
                });
            return projectApi;
        });
    });