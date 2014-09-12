  
// configure require.js
require.config(
{
    // module paths
    paths:
    {
        'jquery': 'lib/jquery',
        'angular': 'lib/angular',
        'angular-router': 'lib/angular-ui-router',
        'angular-resource': 'lib/angular-resource',
        'angular-animate': 'lib/angular-animate',
        'app.states': 'bootstrap/app.states',
        'app': 'bootstrap/app.config',
        'app.start': 'bootstrap/app.start'
    },

    // module dependencies
    shim:
    {
        'angular-router':
        {
            deps:
            [
                'angular'
            ]
        },
        'angular-resource':
        {
            deps:
            [
                'angular'
            ]
        },
        'angular-animate':
        {
            deps:
            [
                'angular'
            ]
        },
        'app':
        {
            deps:
            [
                'jquery',
                'angular',
                'angular-router',
                'angular-resource',
                'angular-animate',
                'app.states'
            ]
        },
        'app.start':
        {
            deps:
            [
                'app'
            ]
        }
    }
});

// initiate app bootstrapping
require(

    // define app dependencies
    [
        'app.start'
    ],

    // manually bootstrap document w/ angular
    function ()
    {
        angular.bootstrap(document, ['app']);
    }
);