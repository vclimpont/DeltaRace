using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private PlayerController player = null;
    [SerializeField] private AIController[] AIs = null;
    [SerializeField] private Transform endRaceTransform = null;

    private float raceLength;

    public int CurrentLevel { get; private set; }
    public int Progression { get; private set; }
    public int Speed { get; private set; }
    public int RacePosition { get; private set; }
    public int Score { get; private set; }
    public int RacersSize { get; private set; }

    void Awake()
    {
        Instance = this;

        CurrentLevel = 1; // get from save file
        Progression = 0;
        Speed = 0;
        RacePosition = AIs.Length + 1;
        Score = 0;
        RacersSize = RacePosition;
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
        Speed = (int)player.GetComponent<Rigidbody>().velocity.magnitude;
    }

    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("DeltaRace");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
