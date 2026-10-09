using System.Collections;
using UnityEngine;
using TMPro;
public class Hoop : MonoBehaviour
{
    [Header("Referinte")]
    public TextMeshProUGUI scoreText;
    public ParticleSystem scoreEffect;
    public AudioSource scoreSound;
    public Renderer flashRenderer; // de ex. Backboard
    public Transform player; // lasati gol: se ia Main Camera
    [Header("Setari")]
    public float threePointDistance = 4f;
    public string throwableTag = "Throwable";
    int totalScore;
    void Start()
    {
        if (player == null && Camera.main != null)
            player = Camera.main.transform;
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(throwableTag)) return;
        // doar daca mingea cade de sus in jos
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || rb.linearVelocity.y > 0f) return;
        // distanta orizontala jucator -> cos
        Vector3 p = player != null ? player.position : Vector3.zero;
        Vector3 h = transform.position;
        p.y = 0f;
        h.y = 0f;
        float distance = Vector3.Distance(p, h);
        int points = distance >= threePointDistance ? 3 : 2;
        totalScore += points;

        if (scoreText != null)
            scoreText.text = points == 3
            ? $"3 PUNCTE! Total: {totalScore}"
            : $"2 puncte Total: {totalScore}";
        if (scoreEffect != null)
        {
            scoreEffect.transform.position = transform.position;
            scoreEffect.Play();
        }
        if (scoreSound != null) scoreSound.Play();
        if (flashRenderer != null) StartCoroutine(Flash());
        // evita punctarea repetata a aceleiasi mingi
        other.gameObject.tag = "Untagged";
    }
    IEnumerator Flash()
    {
        Color original = flashRenderer.material.color;
        flashRenderer.material.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        flashRenderer.material.color = original;
    }
}