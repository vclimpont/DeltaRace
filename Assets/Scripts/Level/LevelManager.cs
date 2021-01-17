using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private PlayerController player = null;
    [SerializeField] private AIController[] AIs = null;
    [SerializeField] private Transform endRaceTransform = null;

    private float raceLength;
    private SaveManager sm;

    public enum LevelState { InRace, EndRace }

    public bool LowSettings { get; private set; }
    public int CurrentLevel { get; private set; }
    public int Progression { get; private set; }
    public int Speed { get; private set; }
    public int RacePosition { get; private set; }
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public int RacersSize { get; private set; }
    public LevelState CurrentState { get; private set; } 

    public bool NewRecord { get; private set; }

    void Awake()
    {
        Instance = this;
        sm = SaveManager.Instance;
        CurrentState = LevelState.InRace;

        CurrentLevel = sm.state.MaxLevel;
        HighScore = sm.state.HighScore;
        LowSettings = sm.state.LowSettings;
        Progression = 0;
        Speed = 0;
        RacePosition = AIs.Length + 1;
        Score = 0;
        RacersSize = RacePosition;
        NewRecord = false;
        raceLength = endRaceTransform.position.z - player.transform.position.z;
    }

    void Update()
    {
        SetRacePosition();
        SetProgression();
        SetSpeed();
    }

    void SetRacePosition()
    {
        int dtPosition = 1;
        float playerPosZ = player.transform.position.z;

        for (int i = 0; i < AIs.Length; i++)
        {
            if(AIs[i].transform.position.z > playerPosZ)
            {
                dtPosition++;
            }
        }

        RacePosition = dtPosition;
    }

    void SetProgression()
    {
        Progression = (int)(((raceLength - (endRaceTransform.position.z - player.transform.position.z)) / raceLength) * 100);
    }

    void SetSpeed()
    {
        Speed = (int)player.GetComponent<Rigidbody>().velocity.magnitude * 2;
    }

    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
    }

    public int GetFinalScore()
    {
        return (int)(Score * CurrentLevel * 1.0f / RacePosition);
    }

    public void EndRace()
    {
        if(CurrentState == LevelState.EndRace)
        {
            return;
        }

        CurrentState = LevelState.EndRace;
        

        if(GetFinalScore() > sm.state.HighScore)
        {
            NewRecord = true;
            sm.state.HighScore = GetFinalScore();
            HighScore = GetFinalScore();
            sm.Save();
        }
        
        if(RacePosition == 1)
        {
            sm.state.MaxLevel++;
            sm.Save();
        }
    }
}
