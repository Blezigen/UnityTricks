using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GUIScrollBar : MonoBehaviour
{

    public float minValue = 0.1f;
    public float maxValue = 100f;
    public float Value = 1f;


    public Image FillImage;
    public Text TextObject;

    public Boolean ShowCaption = true;
    public bool Animated = false;

    public float toValue = 0f;
    private float Timer = 0.1f;
    private float eps = 0.01f;
    public float speedProgress = 1;

    public float sizeW = 0;

    void Start()
    {
        var theBarRectTransform = FillImage.transform as RectTransform;
        sizeW = theBarRectTransform.sizeDelta.x;
    }

    public float GetValue()
    {
        return Value;
    }

    public void SetValue(float val)
    {
        toValue = val;
    }


    void Update()
    {

        TextObject.enabled = ShowCaption;

        var theBarRectTransform = FillImage.transform as RectTransform;



        Value = Mathf.Lerp(Value, toValue, speedProgress * Time.deltaTime);

        if (Value > maxValue)
            Value = maxValue;
        if (Value < minValue)
            Value = minValue;

        if (Value < 1)
        {
            theBarRectTransform.sizeDelta = new Vector2(0, theBarRectTransform.sizeDelta.y);
            if (ShowCaption)
            {
                TextObject.text = String.Format("{0} / {1}", 0, Mathf.Floor(maxValue));
            }
        }
        else if (Value >= toValue)
        {
            theBarRectTransform.sizeDelta = new Vector2(Mathf.CeilToInt(sizeW * Value / (maxValue - minValue)),
                theBarRectTransform.sizeDelta.y);
            if (ShowCaption)
            {
                TextObject.text = String.Format("{0} / {1}", Mathf.Floor(Value), Mathf.Floor(maxValue));
            }
        }
        else
        {
            theBarRectTransform.sizeDelta = new Vector2(Mathf.CeilToInt(sizeW * Value / (maxValue - minValue)),
                theBarRectTransform.sizeDelta.y);
            if (ShowCaption)
            {
                TextObject.text = String.Format("{0} / {1}", Mathf.CeilToInt(Value), Mathf.Floor(maxValue));
            }
        }

    }

}
