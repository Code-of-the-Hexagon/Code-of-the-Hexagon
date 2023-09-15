using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCellText : MonoBehaviour
{
    [SerializeField] 
    private GameObject _selectedCellPanel;

    [SerializeField] 
    private TextMeshProUGUI _labelText;

    private int _selectedCount = 0;
    private int _updateCount = 0;
    private readonly float _labelTurnOffTime = 0.5f;


    public void Start()
    {
        _selectedCellPanel.SetActive(false);
    }

    public void DisplayText(string text)
    {
        _labelText.text = text;
    }

    public void ChangeSelectedCount(int value)
    {
        _selectedCount += value;
        switch (_selectedCount)
        {
            case > 0:
                _updateCount++;
                _selectedCellPanel.SetActive(true);
                break;
            case 0:
                StartCoroutine(ExecuteAfterTime(() =>
                {
                    _selectedCellPanel.SetActive(false);
                }, _labelTurnOffTime));
                break;
            default:
                throw new Exception($"SelectCount must be greater than -1, but found: {_selectedCount}");
        }
    }

    public IEnumerator ExecuteAfterTime(Action task, float delaySeconds)
    {
        var updates = _updateCount;
        yield return new WaitForSeconds(delaySeconds);
        if (updates == _updateCount)
        {
            task.Invoke();
        }
    }
}
