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
    [SerializeField] private TextMeshProUGUI CostPeasant;
    [SerializeField] private TextMeshProUGUI CostKnight;
    //Economy
    [SerializeField] private TextMeshProUGUI HarvestCount;
    [SerializeField] private float TimerStartHarvest;
    [SerializeField] private TextMeshProUGUI ConsumptionCount;
    [SerializeField] private float TimerStartConsumption;
    [SerializeField] private TextMeshProUGUI ConsumptionText;
    [SerializeField] private TextMeshProUGUI ProductionText;
    [SerializeField] private TextMeshProUGUI PopulationText;
    //Gold
    [SerializeField] public int StartGold;
    [SerializeField] private TextMeshProUGUI GoldText;
    [SerializeField] int StartMiners;
    [SerializeField] private TextMeshProUGUI MinersText;
    [SerializeField] private Button BuyMiner;
    [SerializeField] private int MinerCost = 30;
    [SerializeField] private Button SellWheat;
    [SerializeField] private Button SellGold;
    [SerializeField] int GoldPrice = 100;
    //Upgrades
    [SerializeField] private TextMeshProUGUI PeasantsUpgrade;
    [SerializeField] private TextMeshProUGUI KnightsUpgrade;
    [SerializeField] private Button BuyPeasantUpgrade;
    [SerializeField] private Button BuyKnightUpgrade;
    [SerializeField] private int PeasantUpCost;
    [SerializeField] private int KnightUpCost;
    [SerializeField] private int PeasantLvl;
    [SerializeField] private int KnightLvl;
    [SerializeField] private bool AfterWave10;
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
    [SerializeField] private TextMeshProUGUI DefenceText;
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
        BuyMiner.onClick.AddListener(Miner);
        StartNewWave.onClick.AddListener(Wave);
        SellWheat.onClick.AddListener(BuyGold);
        SellGold.onClick.AddListener(BuyWheat);
        HarvestTime = TimerStartHarvest;
        ConsumptionTime = TimerStartConsumption;
        IsPopZ = false;
        IsHundredWavesWin = false;
        BuyPeasantUpgrade.onClick.AddListener(PeasantUpgrade);
        BuyKnightUpgrade.onClick.AddListener(KnightUpgrade);

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
                WheatNum = WheatNum + StartPeasant * (1 + 1*PeasantLvl);
                StartWheat = WheatNum;
                HarvestTime = TimerStartHarvest;
                EventText = ($"Peasant,s Had Collected {Production} Wheat");
                EventDisplay();
            }
            ConsumptionTime -= Time.deltaTime;
            ConsumptionCount.text = Mathf.Round(ConsumptionTime).ToString();
            if (ConsumptionTime <= 0)
            {
                WheatNum = WheatNum - (StartPeasant + StartKnight * 3 + StartMiners*5);
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
    private void Miner()
    {
        if (StartGold >= MinerCost)
        {
            StartMiners = StartMiners + 1;
            StartGold = StartGold - MinerCost;
            EventText = ("New Miner Was Hiered");
            EventDisplay();
            UpdateUI();
        }
    }
    private void BuyGold()
    {
        StartGold++;
        StartWheat = StartWheat - GoldPrice;
        EventText = ("You Had Buyed Gold");
        EventDisplay();
        UpdateUI();
    }
    private void BuyWheat()
    {
        if (StartGold > 0)
        {
            StartGold--;
            StartWheat = StartWheat + GoldPrice;
            EventText = ("You Had Buyed Wheat");
            EventDisplay();
            UpdateUI();
        }
        else
        {
            EventText = ("You Don,t Have Enough Gold!");
            EventDisplay();
        }
    }
    private void PeasantUpgrade()
    {
        if (AfterWave10 == true && PeasantUpCost < StartGold)
        {
            StartGold = StartGold - PeasantUpCost;
            PeasantLvl++;
            PeasantCost++;
            UpdateUI();
        }
    }

    private void KnightUpgrade()
    {
        if (AfterWave10 == true && KnightUpCost < StartGold)
        {
            StartGold = StartGold - KnightUpCost;
            KnightLvl++;
            KnightCost++;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        WheatMesh.text = StartWheat.ToString();
        GoldText.text = StartGold.ToString();
        PeasantNum.text = StartPeasant.ToString();
        KnightNum.text = StartKnight.ToString();
        MinersText.text = StartMiners.ToString();
        CostPeasant.text = PeasantCost.ToString();
        CostKnight.text = KnightCost.ToString();
        Consumption = (StartPeasant + StartKnight * 3);
        ConsumptionText.text = Consumption.ToString();
        Production = (StartPeasant * (1 + 1*PeasantLvl));
        ProductionText.text = Production.ToString();
        Population = (StartPeasant + StartKnight);
        PopulationText.text = Population.ToString();
        PeasantsUpgrade.text = PeasantLvl.ToString();
        KnightsUpgrade.text = KnightLvl.ToString();
        DefenceText.text = (StartKnight * KnightLvl).ToString();
        if (Population <= 0)
        {
            IsPopZ = true;
        }
    }
    private void Wave()
    {
        if (IsWaveActive == false)
        {
            EnemiesNum = StartPeasant / 3 * 2*PeasantLvl + WaveCount * 2 + WaveCount / 2;
            if (EnemiesNum < 0)
            {
                EnemiesNum = EnemiesNum * EnemiesNum;
            }
            WaveEnemies.text = EnemiesNum.ToString();
            RewardNum = EnemiesNum * (PeasantCost+KnightCost) + WaveCount * 2 + WaveCount / 2;
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
                if (StartKnight*KnightLvl >= EnemiesNum)
                {
                    StartWheat = StartWheat + RewardNum;
                    EventText = ($"You Had Won {WaveCount} Wave");
                    EventDisplay();
                    WaveCount = WaveCount + 1;
                    if (WaveCount == 10)
                    {
                        AfterWave10 = true;
                    }
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
