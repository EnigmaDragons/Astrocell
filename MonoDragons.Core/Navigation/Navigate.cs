using MonoDragons.Core.Scenes;

namespace MonoDragons.Core.Navigation
{
    public static class Navigate
    {
        private static INavigation _navigation;
        private static SceneFactory _sceneFactory;

        public static void Init(SceneFactory sceneFactory)
        {
            _sceneFactory = sceneFactory;
            _navigation = new DisposingNavigator(sceneFactory);
        }

        public static bool SceneExists(string sceneName)
        {
            return _sceneFactory.Exists(sceneName);
        }

        public static void To(IScene scene)
        {
            _navigation.NavigateTo(scene);
        }

        public static void To(string sceneName)
        {
            _navigation.NavigateTo(sceneName);
        }

        private class DisposingNavigator : INavigation
        {
            private readonly SceneFactory _factory;

            private IScene CurrentScene { get; set; }

            public DisposingNavigator(SceneFactory factory)
            {
                _factory = factory;
            }

            public void NavigateTo(string sceneName)
            {
                NavigateTo(_factory.Create(sceneName));
            }

            public void NavigateTo(IScene scene)
            {
                CurrentScene?.Dispose();
                scene.Init();
                CurrentScene = scene;
            }
        }
    }
}
