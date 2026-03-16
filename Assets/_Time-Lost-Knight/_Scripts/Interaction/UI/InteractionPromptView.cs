using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.UI
{
    [MovedFrom("")]
    public class InteractionPromptView : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
