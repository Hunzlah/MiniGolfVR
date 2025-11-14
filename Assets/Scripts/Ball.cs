using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private bool isHitByPlayer;
    private bool isHitTarget;

    public static event Action OnBallHitByPlayer;
    public static event Action<bool> OnBallDestroyed;

    private void OnTriggerEnter (Collider other)
    {

        if (other.GetComponent<PitHole>())
        {
            Destroy(gameObject);
        }
        if (other.GetComponent<TargetHole>())
        {
            isHitTarget = true;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter (Collision collision)
    {
        if (!isHitByPlayer && collision.gameObject.GetComponent<Club>())
        {
            isHitByPlayer = true;
            OnBallHitByPlayer?.Invoke();
        }
    }
    private void Update ()
    {
        if (isHitByPlayer && rb.linearVelocity == Vector3.zero)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy ()
    {
        OnBallDestroyed?.Invoke(isHitTarget);
    }
}
