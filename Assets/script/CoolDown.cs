using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]
    private float redcolorDuration = 1;
    public float CurrentTimeInGame;
    public float LastTimeDamageWasTaken;

    public float timer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (CurrentTimeInGame > LastTimeDamageWasTaken + redcolorDuration)
        {
            if (sr.color != Color.white)
                sr.color = Color.white;
        }
    }



    public void TakeDamage()
    {
        sr.color = Color.red;
        timer = redcolorDuration;
    }

    private void TurnWhite()
    {
        sr.color = Color.white;
    }
}
