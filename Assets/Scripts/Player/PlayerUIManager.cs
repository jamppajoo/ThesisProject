﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {

    /// <summary>
    /// speedboostin värin vaihto
    /// speedboostin tyhjennys kun sitä käytetään
    /// Nimen vaihto gamemanagerin mukaseksi
    /// 
    /// 
    /// </summary>

    private float colorSelectorPointerMoveMultiplier = 45;

    private Vector3 colorSelectorAreaOriginalPlace;
    private Vector3 colorSelectorOffset = new Vector3(0,2000,0);

    private GameObject colorSelectorAreaGameObject;
    private GameObject colorSelectorPointer;

    private RectTransform colorSelectorAreaRectTransform;

    private Slider speedBoostSlider;

    private Image speedBoostFill;

    void Start () {
        colorSelectorAreaGameObject = transform.Find("ColorSelectorArea").gameObject;
        colorSelectorAreaRectTransform = colorSelectorAreaGameObject.GetComponent<RectTransform>();
        colorSelectorAreaOriginalPlace = colorSelectorAreaRectTransform.position;
        colorSelectorPointer = colorSelectorAreaGameObject.transform.GetChild(0).gameObject;

        speedBoostSlider = transform.Find("SpeedBoost").GetComponent<Slider>();
        speedBoostFill = speedBoostSlider.fillRect.GetComponent<Image>();

	}

    private void Update()
    {
    }

    public void DisplayColorSelector(bool show)
    {
        if (show)
            colorSelectorAreaRectTransform.position = colorSelectorAreaOriginalPlace;
        else
            colorSelectorAreaRectTransform.position = colorSelectorAreaOriginalPlace + colorSelectorOffset;
    }
    public void MoveColorSelector(Vector3 selectorPosition)
    {
        colorSelectorPointer.GetComponent<RectTransform>().localPosition = selectorPosition ;
    }
    public void SetSpeedBoostValue(float value)
    {
        speedBoostSlider.value = value;
    }
    public void SetSpeedBoostMaxValue(float maxValue)
    {
        speedBoostSlider.maxValue = maxValue;
    }
    public void SetSpeedBoostColor(Color color)
    {
        speedBoostFill.color = color;
    }



}
