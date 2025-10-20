using System;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuntityOfWheat : MonoBehaviour
{
    //Base
    
    [SerializeField] private TextMeshProUGUI EventText1;
    [SerializeField] private TextMeshProUGUI EventText2;
    private string EventText;
    private float HarvestTime;
    private float ConsumptionTime;
    int Consumption;
    int Production;
    int Population;
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
    //Economy
    [SerializeField] private TextMeshProUGUI HarvestCount;
    [SerializeField] private float TimerStartHarvest;
    [SerializeField] private TextMeshProUGUI ConsumptionCount;
    [SerializeField] private float TimerStartConsumption;
    [SerializeField] private TextMeshProUGUI ConsumptionText;
    [SerializeField] private TextMeshProUGUI ProductionText;
    [SerializeField] private TextMeshProUGUI PopulationText;
    //Waves
    float WavePreapare;
    [SerializeField] private TextMeshProUGUI TimeBeforeWave;
    [SerializeField] private float WavePrepareTime;
    [SerializeField] private int WaveCount;
    [SerializeField] private TextMeshProUGUI WaveNum;
    [SerializeField] private int RewardNum;
    [SerializeField] private TextMeshProUGUI Reward;
    [SerializeField] private int EnemiesNum;
    [SerializeField] private TextMeshProUGUI WaveEnemies;
    [SerializeField] private bool IsWaveActive;
    //Defeat
    [SerializeField] private bool IsPopZ;
    //Victory
    [SerializeField] private bool IsHundredWavesWin;

    private void EventDisplay()
    {
    
    }

    private void Awake() //Buttons
    {
        UpdateUI();
        BuyPeasant.onClick.AddListener(Peasant);
        BuyKnight.onClick.AddListener(Knight);
        StartNewWave.onClick.AddListener(Wave);
        HarvestTime = TimerStartHarvest;
        ConsumptionTime = TimerStartConsumption;
        IsPopZ = false;
        IsHundredWavesWin = false;
        
    }

    private void Update() //Prodaction-ConsumptionTimers
    {
        int WheatNum = StartWheat;
        HarvestTime -= Time.deltaTime;
        HarvestCount.text = Mathf.Round(HarvestTime).ToString();
        if (HarvestTime <= 0)
        {
            WheatNum = WheatNum + StartPeasant * 2;
            StartWheat = WheatNum;
            HarvestTime = TimerStartHarvest;
            EventText = ($"Peasant,s Have Collected {Production} Wheat");
        }
        ConsumptionTime -= Time.deltaTime;
        ConsumptionCount.text = Mathf.Round(ConsumptionTime).ToString();
        if (ConsumptionTime <= 0)
        {
            WheatNum = WheatNum - (StartPeasant + StartKnight*3);
            EventText = ($"Citisens Have Consumed {Consumption} Wheat");

            if (WheatNum < 0)
            {
                StartKnight = StartKnight + Math.DivRem(WheatNum, 3, out int result);
                StartPeasant = StartPeasant + result;
                EventText = ($"Citisens Have Died Because Of Hunger");
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
            ConsumptionTime = TimerStartConsumption;
        }
        WheatMesh.text = WheatNum.ToString();
        UpdateUI();
        //WaveUpdate
        if (IsWaveActive == true)
        {
            UpdateWaveTimer();
        }
        ;
        //Defeat
        if (IsPopZ == true)
        {
            Defeat();
        }
        //Victory
        if (IsHundredWavesWin == true)
        {
            Victory();
        }
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
            EventText = ("New Peasant Appeard In Zhmindin");
            UpdateUI();
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
            EventText = ("New Knight Appeard In Zhmindin");
            UpdateUI();
        }

    }
    void UpdateUI()
    {
        EventDisplay();
        WheatMesh.text = StartWheat.ToString();
        PeasantNum.text = StartPeasant.ToString();
        KnightNum.text = StartKnight.ToString();
        Consumption = (StartPeasant + StartKnight * 3);
        ConsumptionText.text = Consumption.ToString();
        Production = (StartPeasant * 2);
        ProductionText.text = Production.ToString();
        Population = (StartPeasant + StartKnight);
        PopulationText.text = Population.ToString();
        if (Population <= 0)
        {
            IsPopZ = true;
        }
    }
    private void Wave()
    {
        if (IsWaveActive == false)
        {
            EnemiesNum = StartPeasant / 4 + WaveCount * 2 + WaveCount / 2;
            WaveEnemies.text = EnemiesNum.ToString();
            RewardNum = EnemiesNum * 30 + WaveCount * 2 + WaveCount / 2;
            Reward.text = RewardNum.ToString();
            WavePrepareTime = WavePrepareTime + WaveCount;
            WavePreapare = WavePrepareTime;
            EventText = ("New Wave Is Comming");
            IsWaveActive = true;
            UpdateUI();
        }
    }
    private void UpdateWaveTimer()
    {
        WavePreapare -= Time.deltaTime;
        TimeBeforeWave.text = Mathf.Round(WavePreapare).ToString();
        if (WavePreapare <= 0)
        {
            if (StartKnight >= EnemiesNum)
            {
                StartWheat = StartWheat + RewardNum;
                EventText = ($"You Have Won {WaveCount} Wave");
                WaveCount = WaveCount + 1;
                WaveNum.text = WaveCount.ToString();
                IsWaveActive = false;
                UpdateUI();
            }
            else
            {
                StartWheat = StartWheat - (RewardNum - StartKnight / 15);
                WheatMesh.text = StartWheat.ToString();
                StartKnight = StartKnight - RewardNum/15;
                if (StartKnight < 0)
                {
                    StartKnight = 0;
                }
                KnightNum.text = StartKnight.ToString();
                IsWaveActive = false;
                EventText = ($"You Have Losted {WaveCount} Wave");
                UpdateUI();
            }
            WavePreapare = WavePrepareTime;
        }
        UpdateUI();
    }

    private void Defeat()
    {
        SceneManager.LoadScene(2);
    }

    private void Victory()
    {
        EventText = ("Victory");
    }
}
