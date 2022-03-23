using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    private TMP_Text _text;
    private Storage _storage;
    private int _moneyCount;

    void Awake()
    {
        _storage = FindObjectOfType<Storage>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _storage.onPackReceived.AddListener(UpdateValue);
    }

    private void OnDisable()
    {
        _storage.onPackReceived.AddListener(UpdateValue);
    }

    private void UpdateValue(int value)
    {
        _moneyCount += value;
        _text.text = _moneyCount.ToString();
    }
}
