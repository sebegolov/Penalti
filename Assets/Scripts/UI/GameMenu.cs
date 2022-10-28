using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resultText;

    [SerializeField] private GameObject _sideButtonField;
    [SerializeField] private GameObject _nextButtonField;
    [SerializeField] private GameObject _kickButtonField;

    public void SetResult(int result)
    {
        _resultText.text = "Result: " + result + "/5";
    }

    public void ShowSideButton(bool show)
    {
        _sideButtonField.SetActive(show);
    }
    
    public void ShowNextButton(bool show)
    {
        _nextButtonField.SetActive(show);
    }
    
    public void ShowKickButton(bool show)
    {
        _kickButtonField.SetActive(show);
    }
}
