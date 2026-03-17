using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.ServiceLocatorSpace
{
    [MovedFrom("")]
    public interface IServiceLocator<T>
    {
        TP Register<TP>(TP newService) where TP : T;
        void Unregister<TP>(TP service) where TP : T;
        TP Get<TP>() where TP : T;
    }
}