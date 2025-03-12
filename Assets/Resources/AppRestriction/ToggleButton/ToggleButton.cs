using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ToggleButton : MonoBehaviour
{
    public float animationDuration = 1f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public Image background;
    public Sprite enableBackground;
    public Sprite disableBackground;

    public Button button;
    public TMP_Text buttonText;

    protected float value;

    public GameObject handle;

    public float leftX = 0;
    public float rightX = 155;

    public void Toggle() 
    {
        button.interactable = false;
        var nextValue = value == 0 ? 1 : 0;
        setVisuals(nextValue);
        StartCoroutine(AnimateSlider(nextValue));
    }

    private IEnumerator AnimateSlider(float nextValue)
        {
            float startValue = value;
            float endValue = nextValue;

            float time = 0;
            if (animationDuration > 0)
            {
                while (time < animationDuration)
                {
                    time += Time.deltaTime;

                    float lerpFactor = slideEase.Evaluate(time / animationDuration);
                    value = Mathf.Lerp(startValue, endValue, lerpFactor);
                    
                    moveHandle(nextValue, lerpFactor);

                    yield return null;
                }
            }

            value = endValue;
            button.interactable = true;
        }

    private void moveHandle(float nextValue, float lerpFactor)
    {
        if (nextValue == 1)
        {
            handle.transform.localPosition = new Vector3(Mathf.Lerp(leftX, rightX, lerpFactor), handle.transform.localPosition.y, handle.transform.localPosition.z);
        }
        else
        {
            handle.transform.localPosition = new Vector3(Mathf.Lerp(rightX, leftX, lerpFactor), handle.transform.localPosition.y, handle.transform.localPosition.z);
        }
    }

    private void setVisuals(float nextValue)
    {
        buttonText.text = nextValue == 0 ? "Off" : "On";
        background.sprite = nextValue == 0 ?  disableBackground : enableBackground;
    }
}
