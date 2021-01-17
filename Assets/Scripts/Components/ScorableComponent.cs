using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorableComponent : MonoBehaviour
{
    [SerializeField] private int[] scores = null;

    public int Score { get; set; }

    void Awake()
    {
        int i = Random.Range(0, scores.Length);
        Score = scores[i];
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            LevelManager.Instance.AddScore(Score);
            AudioManager.Instance.Play("Ring");
            UILevelManager.Instance.PlayScoreAnimation();
        }
    }
}
