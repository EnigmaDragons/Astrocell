
using MonoDragons.Core.Scenes;

namespace MonoDragons.Core.Navigation
{
    public interface INavigation
    {
        void NavigateTo(string sceneName);
        void NavigateTo(IScene scene);
    }
}
