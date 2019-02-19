using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{

    public static Image valueBar;

    public void UpdateValue(float currentValue, float totalValue)
    {
        valueBar = GetComponent<Image>();
        valueBar.fillAmount = (currentValue / totalValue);
    }
}
