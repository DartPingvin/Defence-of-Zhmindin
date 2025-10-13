using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuntityOfWheat : MonoBehaviour
{
    [SerializeField] private Button BuyPeasant;
    [SerializeField] private Button BuyKnight;
    [SerializeField] private TextMeshProUGUI WheatMesh;
    [SerializeField, TextArea(3, 10)] private int StartWheat;
    [SerializeField] private TextMeshProUGUI PeasantNum;
    [SerializeField, TextArea(3, 10)] private int PeasantCost;
    [SerializeField] private TextMeshProUGUI KnightNum;
    [SerializeField, TextArea(3, 10)] private int KnightCost;
    [SerializeField] private TextMeshProUGUI TimerCount;
    [SerializeField, TextArea(3, 10)] private float TimerStart;
    
    private void Awake()
    {
        UpdateUI();
        BuyPeasant.onClick.AddListener(Peasant);
        BuyKnight.onClick.AddListener(Knight);
    }

    private void Update()
    {
        while (true)
        { 
            float CurentTime = TimerStart - Time.time;
            TimerCount.text = Mathf.Round(CurentTime).ToString();
            if (CurentTime <= 0)
            {
                TimerStart = 15;
            }
        }
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
