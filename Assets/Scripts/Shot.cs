using UnityEngine;

public class Shot : MonoBehaviour
{
    public float timeToLive = 5f;

    private float expireTimer = 0f;

    public void Update()
    {
        expireTimer += Time.deltaTime;
        if (expireTimer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
