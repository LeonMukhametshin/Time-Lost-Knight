using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Observer
{
    [MovedFrom("")]
    public static class SubjectExtensions
    {
        public static void AddObservers(this Subject subject, IReadOnlyCollection<IObserver> observers)
        {
            foreach(var observer in observers)
            {
                subject.AddObserver(observer);
            }
        }

        public static void RemoveObservers(this Subject subject, IReadOnlyCollection<IObserver> observers)
        {
            foreach (var observer in observers)
            {
                subject.RemoveObserver(observer);
            }
        }
    }
}
