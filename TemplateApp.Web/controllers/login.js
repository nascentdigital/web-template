/**
 * LoginController
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
        app.controller('LoginController',
        
            function ($state, $scope, appModel, userApi)
            {
                // define properties
                $scope.username = 'user@nascentdigital.com';
                $scope.password = 'Password!';

                console.log("login");

                // define actions
                $scope.login = function()
                {
                    userApi.login(
                    {
                        username: $scope.username,
                        password: $scope.password
                    },
                    function (response)
                    {
                        console.info('login success');
                        appModel.user = response;
                    },
                    function (e)
                    {
                        console.error('login error: ' + e);
                    });
                };
            });
    });