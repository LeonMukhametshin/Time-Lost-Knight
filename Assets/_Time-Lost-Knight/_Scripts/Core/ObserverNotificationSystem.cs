using Game.Observer;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core
{
    [MovedFrom("")]
    public sealed class ObserverNotificationSystem : MonoBehaviour
    {
        [SerializeField] private Subject m_subject;

        [SerializeReference][SerializeReferenceDropdown]
        [SerializeField] private IObserver[] observers;

        private void OnEnable() =>
             m_subject.AddObservers(observers);

        private void OnDisable() =>
             m_subject.RemoveObservers(observers);

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Notify();
            }
        }
    }
}
