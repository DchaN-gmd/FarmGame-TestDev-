using UnityEngine;
using TMPro;

public class PacketsCounter : MonoBehaviour
{
    private TMP_Text _text;
    private Bag _bag;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _bag = FindObjectOfType<Bag>();
        UpdateValues();
    }

    private void OnEnable()
    {
        _bag.packsCountChanged.AddListener(UpdateValues);
    }

    private void OnDisable()
    {
        _bag.packsCountChanged.RemoveListener(UpdateValues);
    }

    private void UpdateValues()
    {
        _text.text = $"{_bag.PacksCount} / {_bag.MaxPackCount}";
    }
}
