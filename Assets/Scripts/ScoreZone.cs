using UnityEngine;
using TMPro;


public class ScoreZone : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    private void Awake()
    {
        score = GameObject.FindGameObjectsWithTag("Target").Length;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = score.ToString();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Target"))
            return;

        score--;
        UpdateText();

        Destroy(other.gameObject);
    }
}
