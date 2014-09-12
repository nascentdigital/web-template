
/**
 * Defines application states for angualar-ui-router.  
 */
define(

    // load dependencies
    [
    ],

    // define module
    function ()
    {
        return {

            /**
             * Onboarding
             */
            'login':
            {
                url: '/login',
                templateUrl: 'views/onboarding/login.html',
                controller: 'LoginController',
                modules:
                [
                    'controllers/login'
                ]
            },

            /**
             * Home
             */
            'site':
            {
                abstract: true,
                templateUrl: 'views/_layout.html',
                controller: 'SiteController',
                modules:
                [
                    'controllers/site'
                ]
            },
            'site.home':
            {
                url: '/home',
                views:
                {
                    '':
                    {
                        templateUrl: 'views/home/index.html',
                        controller: 'HomeController'
                    }
                },
                modules:
                [
                    'controllers/home'
                ]
            },
            'site.projects':
            {
                url: '/projects',
                views:
                {
                    '':
                    {
                        templateUrl: 'views/projects/index.html',
                        controller: 'ProjectsController'
                    }
                },
                resolve:
                {
                    projects: function ($stateParams, projectApi)
                    {
                        var projects = projectApi.query();
                        return projects.$promise;
                    }
                },
                modules:
                [
                    'controllers/projects'
                ]
            },
            'site.projects.project':
            {
                url: '/:projectId',
                views:
                {
                    '@site':
                    {
                        templateUrl: 'views/projects/project.html',
                        controller: 'ProjectController'
                    }
                },
                resolve:
                {
                    project: function ($stateParams, projects)
                    {
                        var projectId = parseInt($stateParams.projectId);

                        var project = null;
                        var projectCount = projects.length;
                        for (var i = 0; i < projectCount; ++i)
                        {
                            var p = projects[i];
                            if (p.Id == projectId)
                            {
                                project = p;
                                break;
                            }
                        }

                        return project;
                    }
                },
                modules:
                [
                    'controllers/project'
                ]
            }
        };
    });