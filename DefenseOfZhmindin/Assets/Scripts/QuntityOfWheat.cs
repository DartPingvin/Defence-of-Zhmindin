using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuntityOfWheat : MonoBehaviour
{
    [SerializeField] private Button BuyPeasant;
    [SerializeField] private Button BuyKnight;
    [SerializeField] private TextMeshProUGUI WheatMesh;
    [SerializeField] private int StartWheat;
    [SerializeField] private TextMeshProUGUI PeasantNum;
    [SerializeField] private int StartPeasant;
    [SerializeField] private int PeasantCost;
    [SerializeField] private TextMeshProUGUI KnightNum;
    [SerializeField] private int StartKnight;
    [SerializeField] private int KnightCost;
    [SerializeField] private TextMeshProUGUI TimerCount;
    [SerializeField] private float TimerStart;

    private void Awake()
    {
        UpdateUI();
        BuyPeasant.onClick.AddListener(Peasant);
        BuyKnight.onClick.AddListener(Knight);
    }

    private void Update()
    {
        int WheatNum = StartWheat;
        float CurentTime = TimerStart - Time.time;
        TimerCount.text = Mathf.Round(CurentTime).ToString();
        if (CurentTime <= 0)
        {
            WheatNum = WheatNum + StartPeasant * 2;
            CurentTime = 15;
            TimerStart = 15;
        }
        WheatMesh.text = WheatNum.ToString();

    }
    private void Peasant()
    {
        
    }

    private void Knight()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        
    }
}
