using UnityEngine;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour
{
    [SerializeField] private Image _bar;

    public void ChangeValue(float value)
    {
        _bar.fillAmount = value;
    }

}
