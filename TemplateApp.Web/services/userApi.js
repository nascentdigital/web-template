/**
 * User API.
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
        app.factory('userApi', function ($resource, apiUrl)
        {
            var userApi = $resource(apiUrl + 'user/:userId',
                { userId: '@id' },
                {
                    currentUser:
                    {
                        method: 'GET',
                        url: apiUrl + 'user/current'
                    },
                    login: 
                    {
                        method: 'POST',
                        url: apiUrl + 'user/login'
                    },
                    logout:
                    {
                        method: 'GET',
                        url: apiUrl + 'user/logout'
                    }
                })
            return userApi;
        });
    });