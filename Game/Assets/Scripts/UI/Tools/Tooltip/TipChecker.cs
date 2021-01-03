using UnityEngine;
using UnityEngine.EventSystems;

namespace DapperDino.TooltipUI
{
    public class TipChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        


        public void OnPointerEnter(PointerEventData eventData)
        {
            var info = gameObject.GetComponent<ITIP>();
            if (info != null)
                UIManager._instance.TIP.DisplayInfo(info);
            else
                Debug.LogError("Error TIP script.");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager._instance.TIP.HideInfo();
        }
    }
}
