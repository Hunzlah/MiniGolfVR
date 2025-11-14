using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MiniGolfManager : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballSpawnPosition;

    [SerializeField] private TextMeshProUGUI HitCountLabel;
    [SerializeField] private TextMeshProUGUI MissCountLabel;

    private int hitCount, missCount;


    private void OnEnable ()
    {
        Ball.OnBallDestroyed += OnBallDestroyed;
        Ball.OnBallHitByPlayer += OnBallHitByPlayer;
    }
    private void OnDisable ()
    {
        Ball.OnBallDestroyed -= OnBallDestroyed;
        Ball.OnBallHitByPlayer -= OnBallHitByPlayer;
    }

    private void OnBallHitByPlayer ()
    {
        hitCount++;
        HitCountLabel.text = hitCount.ToString();
    }

    private async void OnBallDestroyed (bool isHitTarget)
    {
        missCount++;
        MissCountLabel.text = missCount.ToString();

        await Task.Delay (300);
        SpawnNewBall();
    }
    private void SpawnNewBall ()
    {
        Ball temp = Instantiate(ballPrefab, null);
        temp.transform.position = ballSpawnPosition.position;
    }
}
