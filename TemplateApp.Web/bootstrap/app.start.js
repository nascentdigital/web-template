/**
 * App module, providing all top-level application configuration and services.
 */
define(

    // load dependencies
    [
        'app',
        'services/userApi',
        'services/projectApi'
    ],

    // define module
    function (app)
    {
        app.run(
            function ($rootScope, $state, $stateParams, $location, appModel, userApi)
            {
                // bind scope
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;
                $rootScope.appModel = appModel;

                // definite initialization handler
                var tryInitializeUnhooks = [];
                var stateChangeComplete = false;
                var userQueryComplete = false;
                var tryInitialize = function ()
                {
                    // ignore if wait on state change or user query
                    if (stateChangeComplete == false
                        || userQueryComplete == false)
                    {
                        console.info('waiting for state/user completion before initializing');
                        return;
                    }

                    // unregister all initialization listeners
                    if (tryInitializeUnhooks != null)
                    {
                        for (var i = 0; i < tryInitializeUnhooks.length; ++i)
                        {
                            tryInitializeUnhooks[i]();
                        }
                        tryInitializeUnhooks = null;
                    }

                    // mark initialized
                    appModel.initialized = true;
                    console.info('state: ' + $state.$current.name);

                    // route to login if logged out
                    var user = appModel.user;
                    if (user == null)
                    {
                        $state.go('login');
                    }

                    // or handle user routing
                    else if ($state.includes('site') == false)
                    {
                        $state.go('site.home');
                    }
                };

                // wait for initial state change
                tryInitializeUnhooks.push($rootScope.$on(
                    '$locationChangeSuccess',
                    function (event)
                    {
                        // skip if not for empty path
                        var path = $location.path();
                        if (path !== undefined
                            && path.trim().length > 0)
                        {
                            console.info('ignoring stateful location change: ' + path);
                            return;
                        }

                        console.info('location change success: ' + path);

                        // mark state change complete
                        stateChangeComplete = true;

                        // try to initialize
                        tryInitialize();
                    }));
                tryInitializeUnhooks.push($rootScope.$on(
                    '$stateChangeSuccess',
                    function (event, toState, toParams, fromState, fromParams)
                    {
                        console.info('state change success: ' + toState);

                        // mark state change complete
                        stateChangeComplete = true;

                        // try to initialize
                        tryInitialize();
                    }));
                tryInitializeUnhooks.push($rootScope.$on(
                    '$stateNotFound',
                    function (event, unfoundState, fromState, fromParams)
                    {
                        console.error('state change failed: ' + unfoundState);

                        // mark state change complete
                        stateChangeComplete = true;

                        // try to initialize
                        tryInitialize();
                    }));

                // observe model properties
                $rootScope.$watch('appModel.user',
                    function (user)
                    {
                        // ignore if undefined
                        if (user === undefined)
                        {
                            return;
                        }

                        console.info('user query complete');

                        // mark state change complete
                        userQueryComplete = true;

                        // try to initialize
                        tryInitialize();
                    });

                // try to fetch initial user information
                userApi.currentUser(
                    function (response)
                    {
                        appModel.user = response;
                    },
                    function (e)
                    {
                        appModel.user = null;
                    });
            });

        // return app module
        return app;
    });