using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuntityOfWheat : MonoBehaviour
{
    [SerializeField] private Button BuyPeasant;
    [SerializeField] private Button BuyKnight;
    [SerializeField] private TextMeshProUGUI WheatMesh;
    [SerializeField, TextArea(3, 10)] private string StartWheat;
    [SerializeField] private TextMeshProUGUI PeasantNum;
    [SerializeField, TextArea(3, 10)] private string PeasantCost;
    [SerializeField] private TextMeshProUGUI KnightNum;
    [SerializeField, TextArea(3, 10)] private string KnightCost;


    private void Start()
    {
        int WheatNum =Int32.Parse(StartWheat);
        int knightcost = Int32.Parse(KnightCost);
        int peasantcost = Int32.Parse(PeasantCost);
    }
    
    private void Awake()
    {
        UpdateUI();
        BuyPeasant.onClick.AddListener(Peasant);
        BuyKnight.onClick.AddListener(Knight);
    }

    private void Peasant()
    {
        UpdateUI();
        
    }

    private void Knight()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        
    }
}
