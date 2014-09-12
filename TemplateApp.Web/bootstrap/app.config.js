/**
 * App module, providing all top-level application configuration and services.
 */
define(

    // load dependencies
    [
        'app.states'
    ],

    // define module
    function (states)
    {
        // define app module/dependencies
        var app = angular.module('app',
        [
            'ui.router',
            'ngResource',
            'ngAnimate'
        ]);

        // define global constants
        app.constant('apiUrl', 'http://localhost:1389/');
        app.constant('appModel', 
        {
            // set to true when the app is initialized
            initialized: false,

            // represents the currently authenticated user
            user: undefined
        });

        // configure app module
        app.config(function ($urlRouterProvider, $stateProvider,
            $locationProvider, $httpProvider,
            $resourceProvider, $controllerProvider, $filterProvider, $provide,
            appModel)
        {
            // setup app module as a service locator
            app.controller = $controllerProvider.register;
            app.resource = $resourceProvider.register;
            app.filter = $filterProvider.register;
            app.factory = $provide.factory;
            app.service = $provide.service;

            // configure location provide to be html5 compliant
            //$locationProvider.html5Mode(true);

            // configure CORS to always send w/ credentials
            $httpProvider.defaults.withCredentials = true;

            // bind explicit states
            if (states !== undefined)
            {
                // define module loader
                var loadModules = function (modules)
                {
                    // create loader promise
                    var loader =
                    [
                        '$q', '$rootScope',
                        function ($q, $rootScope)
                        {
                            // create future promise
                            var deferred = $q.defer();

                            console.info('resolving ' + modules.length
                                + ' module(s)');

                            // asynchronously load controller using require.js
                            require(modules,
                                function (result)
                                {
                                    for (var i = 0; i < modules.length; ++i)
                                    {
                                        console.log('resolved module: '
                                            + modules[i]);
                                    }

                                    deferred.resolve(result);
                                },
                                function (e)
                                {
                                    var failure = e.requireModules
                                        && e.requireModules[0];
                                    console.error('failed to resolve modules: '
                                        + failure);
                                    deferred.reject(failure);
                                });

                            // return deferral
                            return deferred.promise;
                        }
                    ];

                    // return instance
                    return loader;
                };

                // create all states
                angular.forEach(states,
                    function (state, stateName)
                    {
                        // bind any modules to resolve
                        if (angular.isArray(state.modules)
                            && state.modules.length > 0)
                        {
                            state.resolve = state.resolve || {};
                            state.resolve._modules = loadModules(state.modules);
                        }

                        // TODO: add enter/exit handling for actionbar
                        state.onEnter = function ()
                        {
                            console.info('entering state %s', stateName);
                        };
                        state.onExit = function ()
                        {
                            console.info('exiting state %s', stateName);
                        };

                        // add state to provider
                        $stateProvider.state(stateName, state);
                    });
            }
        });

        // return app module
        return app;
    });