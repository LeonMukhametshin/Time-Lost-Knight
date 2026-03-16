using Game.Observer;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.Objects
{
    [MovedFrom("")]
    public class Chest : Subject
    {
        //TODO some logic(((
        private bool m_isOpen = false;
        public bool isOpen => m_isOpen;

        public void Open()
        {
            if(m_isOpen)
            {
                return;
            }

            m_isOpen = true;
            Debug.Log("Chest opened");
        }
    }
}
