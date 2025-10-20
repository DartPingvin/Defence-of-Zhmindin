using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    [SerializeField] int StartGold;
    [SerializeField] private TextMeshProUGUI GoldText;
    [SerializeField] int StartMiners;
    [SerializeField] private TextMeshProUGUI MinersText;
    [SerializeField] private Button SellWheat;
    [SerializeField] int GoldPrice = 100;

    private void Awake()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        GoldText.text = StartGold.ToString();
        MinersText.text = StartMiners.ToString();
    }

}
