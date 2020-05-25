using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : UIResource
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text amountText;

    public override void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public override void UpdateResourceValue(float value, float maxValue)
    {
        fillImage.fillAmount = value / maxValue;
        amountText.text = ((int) value).ToString();
    }
}
