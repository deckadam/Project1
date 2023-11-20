using System;
using Grid;
using TMPro;
using UnityEngine;
using Zenject;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _matchCountField;
    [SerializeField] private int _defaultGridSize;
    private GridSystem _gridSystem;
    private int _currentMatchCount;

    [Inject]
    private void Inject(GridSystem gridSystem)
    {
        _gridSystem = gridSystem;
    }

    private void OnEnable()
    {
        _inputField.text = _defaultGridSize.ToString();
        _gridSystem.OnMatchEvent += OnMatch;
    }

    private void OnDisable()
    {
        _gridSystem.OnMatchEvent -= OnMatch;
    }

    private void OnMatch()
    {
        _currentMatchCount++;
        UpdateMatchCounter();
    }

    private void UpdateMatchCounter()
    {
        _matchCountField.text = _currentMatchCount.ToString();
    }

    public void OnGenerateClicked()
    {
        var inputValue = Convert.ToInt32(_inputField.text);
        _gridSystem.Initialize(inputValue);
        _currentMatchCount = 0;
        UpdateMatchCounter();
    }
}