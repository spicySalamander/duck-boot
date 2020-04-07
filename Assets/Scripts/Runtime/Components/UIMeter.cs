using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeter : MonoBehaviour
{
    private float startFill = 1;
    public float percent = 0f;
    public float increment = 5;

    public bool full = false;
    public bool empty = false;

    float prev = 0f;
    bool update = false;
    float lerpPercent = 0f;

    public Image meter;
    public RectTransform rect;

    private void Start()
    {
        meter = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        Add(startFill);
    }

    private void Update()
    {
        if (lerpPercent < 1 && update == true)
        {
            lerpPercent += 1 / 10f;
        }
        else
        {
            update = false;
        }

        UpdateDisplay();
    }

    protected virtual void UpdateDisplay()
    {
        meter.fillAmount = Mathf.Lerp(prev, percent, lerpPercent);
        //10, 221, 0
        //133, 1, 23
        meter.color = new Color(Mathf.Lerp(133f/255, 10f / 255,percent),Mathf.Lerp( 1f/255, 221f / 255, percent),Mathf.Lerp(23f/255, 0f, percent));
        rect.position = Camera.main.WorldToScreenPoint(transform.parent.parent.position + (Vector3.up * 1.5f));
    }

    public virtual void Add(float magnitude)
    {
        prev = percent;
        percent += increment / 100f * (magnitude * 20);
        lerpPercent = 0f;
        update = true;

        if (percent > 1)
        {
            percent = 1;
        }
    }
    public virtual void Sub(float total, float dmg)
    {
        prev = percent;
        percent -= 1/total*dmg;
        lerpPercent = 0f;
        update = true;

        if (percent < 0.0001)
        {
            percent = 0;
        }
    }

}
