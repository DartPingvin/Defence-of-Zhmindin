using System;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuntityOfWheat : MonoBehaviour
{
    //Base
    public GameObject PauseMenu;
    public GameObject MessegePrefab;
    public Transform MessegeParent;
    [SerializeField] private TextMeshProUGUI EventText1;
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

    public void EventDisplay()
    {
        EventText1.text = EventText;
        Instantiate(MessegePrefab, MessegeParent);
    }

    private void Awake() //Buttons
    {
        UpdateUI();
        EventDisplay();
        EventText = ("Welcome To Zhmindin!");
        EventDisplay();
        BuyPeasant.onClick.AddListener(Peasant);
        BuyKnight.onClick.AddListener(Knight);
        StartNewWave.onClick.AddListener(Wave);
        HarvestTime = TimerStartHarvest;
        ConsumptionTime = TimerStartConsumption;
        IsPopZ = false;
        IsHundredWavesWin = false;
        
    }
    private void Update() //Prodaction-ConsumptionTimers, Victory-Defeat Cheaks and WaveUpdate.
    {
        if (PauseMenu.activeInHierarchy != true)
        {
            int WheatNum = StartWheat;
            HarvestTime -= Time.deltaTime;
            HarvestCount.text = Mathf.Round(HarvestTime).ToString();
            if (HarvestTime <= 0)
            {
                WheatNum = WheatNum + StartPeasant * 3;
                StartWheat = WheatNum;
                HarvestTime = TimerStartHarvest;
                EventText = ($"Peasant,s Had Collected {Production} Wheat");
                EventDisplay();
            }
            ConsumptionTime -= Time.deltaTime;
            ConsumptionCount.text = Mathf.Round(ConsumptionTime).ToString();
            if (ConsumptionTime <= 0)
            {
                WheatNum = WheatNum - (StartPeasant + StartKnight * 3);
                EventText = ($"Citisens Had Consumed {Consumption} Wheat");
                EventDisplay();

                if (WheatNum < 0)
                {
                    StartKnight = StartKnight + Math.DivRem(WheatNum, 3, out int result);
                    StartPeasant = StartPeasant + result;
                    EventText = ($"Citisens Had Died Because Of Hunger");
                    EventDisplay();
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
            EventDisplay();
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
            EventDisplay();
            UpdateUI();
        }

    }
    void UpdateUI()
    {
        WheatMesh.text = StartWheat.ToString();
        PeasantNum.text = StartPeasant.ToString();
        KnightNum.text = StartKnight.ToString();
        Consumption = (StartPeasant + StartKnight * 3);
        ConsumptionText.text = Consumption.ToString();
        Production = (StartPeasant * 3);
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
            EnemiesNum = StartPeasant / 3 + WaveCount * 2 + WaveCount / 2;
            WaveEnemies.text = EnemiesNum.ToString();
            RewardNum = EnemiesNum * 30 + WaveCount * 2 + WaveCount / 2;
            Reward.text = RewardNum.ToString();
            WavePrepareTime = WavePrepareTime + WaveCount;
            WavePreapare = WavePrepareTime;
            EventText = ("New Wave Is Comming");
            EventDisplay();
            IsWaveActive = true;
            UpdateUI();
        }
    }
    private void UpdateWaveTimer()
    {
        if (IsWaveActive == true)
        {
            WavePreapare -= Time.deltaTime;
            TimeBeforeWave.text = Mathf.Round(WavePreapare).ToString();
            if (WavePreapare <= 0)
            {
                if (StartKnight >= EnemiesNum)
                {
                    StartWheat = StartWheat + RewardNum;
                    EventText = ($"You Had Won {WaveCount} Wave");
                    EventDisplay();
                    WaveCount = WaveCount + 1;
                    WaveNum.text = WaveCount.ToString();
                    IsWaveActive = false;
                }
                else
                {
                    StartWheat = StartWheat - (RewardNum - StartKnight / 15);
                    WheatMesh.text = StartWheat.ToString();
                    StartKnight = StartKnight - RewardNum / 15;
                    if (StartKnight < 0)
                    {
                        StartKnight = 0;
                    }
                    KnightNum.text = StartKnight.ToString();
                    IsWaveActive = false;
                    EventText = ($"You Had Losted {WaveCount} Wave");
                    EventDisplay();
                }
                WavePreapare = WavePrepareTime;
            }
            UpdateUI();
        }
    }
    private void Defeat()
    {
        SceneManager.LoadScene(2);
    }

    private void Victory()
    {
        EventText = ("Victory");
        EventDisplay();
    }
}
