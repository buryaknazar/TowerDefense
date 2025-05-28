using UnityEngine;

namespace UI
{
    public abstract class UIScreen : MonoBehaviour
    {
        protected virtual void ChangeScreenState(CanvasGroup canvasGroup, float alpha, bool blockRaycast, bool interactable)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = blockRaycast;
            canvasGroup.interactable = interactable;
        }
    }
}