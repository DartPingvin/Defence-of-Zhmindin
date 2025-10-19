using System;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuntityOfWheat : MonoBehaviour
{
    [SerializeField] private Button BuyPeasant;
    [SerializeField] private Button BuyKnight;
    [SerializeField] private Button StartNewWave;
    [SerializeField] private TextMeshProUGUI WheatMesh;
    [SerializeField] private int StartWheat;
    [SerializeField] private TextMeshProUGUI PeasantNum;
    [SerializeField] private int StartPeasant;
    [SerializeField] private int PeasantCost;
    [SerializeField] private TextMeshProUGUI KnightNum;
    [SerializeField] private int StartKnight;
    [SerializeField] private int KnightCost;
    [SerializeField] private TextMeshProUGUI HarvestCount;
    [SerializeField] private float TimerStartHarvest;
    [SerializeField] private TextMeshProUGUI ConsumptionCount;
    [SerializeField] private float TimerStartConsumption;
    [SerializeField] private TextMeshProUGUI ConsumptionText;
    [SerializeField] private TextMeshProUGUI ProductionText;
    [SerializeField] private TextMeshProUGUI PopulationText;
    [SerializeField] private TextMeshProUGUI TimeBeforeWave;
    [SerializeField] private int WaveCount;
    [SerializeField] private TextMeshProUGUI WaveNum;
    [SerializeField] private int RewardNum;
    [SerializeField] private TextMeshProUGUI Reward;
    [SerializeField] private TextMeshProUGUI WaveEnemies;
    [SerializeField] private int Enemies;
    


    void Awake()
    {
        UpdateUI();
        BuyPeasant.onClick.AddListener(Peasant);
        BuyKnight.onClick.AddListener(Knight);
        StartNewWave.onClick.AddListener(Wave);
    }

    void Update() //Prodaction-ConsumptionTimers
    {
        int WheatNum = StartWheat;
        float HarvestTime = TimerStartHarvest - Time.time;
        HarvestCount.text = Mathf.Round(HarvestTime).ToString();
        if (HarvestTime <= 0)
        {
            WheatNum = WheatNum + StartPeasant * 2;
            TimerStartHarvest = 15 + Time.time;
            StartWheat = WheatNum;
        }
        float ConsumptionTime = TimerStartConsumption - Time.time;
        ConsumptionCount.text = Mathf.Round(ConsumptionTime).ToString();
        if (ConsumptionTime <= 0)
        {
            WheatNum = WheatNum - (StartPeasant + StartKnight*3);
            TimerStartConsumption = 20 + Time.time;
            
            if (WheatNum < 0)
            {
                StartKnight = StartKnight + Math.DivRem(WheatNum, 3, out int result);
                StartPeasant = StartPeasant + result;
                if (StartKnight < 0)
                {
                    StartPeasant = StartPeasant + StartKnight * 3;
                    WheatNum = 0;
                    StartKnight = 0;
                }
                else
                {
                    WheatNum = 0;
                }
            }
            StartWheat = WheatNum;
        }
        WheatMesh.text = WheatNum.ToString();
        UpdateUI();

    }
    private void Peasant()
    {
        if (StartWheat >= PeasantCost)
        {
            int WheatNum = StartWheat;
            int Peasantnum = StartPeasant;
            int Cost = PeasantCost;
            WheatNum = WheatNum - Cost;
            Peasantnum = Peasantnum + 1;
            StartWheat = WheatNum;
            StartPeasant = Peasantnum;
            WheatMesh.text = StartWheat.ToString();
            PeasantNum.text = StartPeasant.ToString();
        }
        
    }
    private void Knight()
    {
        if (StartWheat >= KnightCost)
        {
            int WheatNum = StartWheat;
            int Knightnum = StartKnight;
            int Cost = KnightCost;
            WheatNum = WheatNum - Cost;
            Knightnum = Knightnum + 1;
            StartWheat = WheatNum;
            StartKnight = Knightnum;
            WheatMesh.text = StartWheat.ToString();
            KnightNum.text = StartKnight.ToString();
            UpdateUI();
        }

    }
    void UpdateUI()
    {
        WheatMesh.text = StartWheat.ToString();
        PeasantNum.text = StartPeasant.ToString();
        KnightNum.text = StartKnight.ToString();
        int Consumption = (StartPeasant + StartKnight * 3);
        ConsumptionText.text = Consumption.ToString();
        int Production = (StartPeasant * 2);
        ProductionText.text = Production.ToString();
        int Population = (StartPeasant + StartKnight);
        PopulationText.text = Population.ToString();
        

    }
    private void Wave()
    {
        
    }
}
