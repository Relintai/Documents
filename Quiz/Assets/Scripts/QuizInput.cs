using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuizInput
{
    public Button button;
    public TextMeshProUGUI text;
    public Image correctImage;
    public Image incorrectImage;

    public void Reset()
    {
        text.text = "";
        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
    }
}
