using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
public class BallRespawn : MonoBehaviour
{
    public float respawnDelay = 8f;   // secunde dupa ce a fost lasata din mana
    public float fallLimitY = -2f;    // daca pica sub podea

    Vector3 startPos;
    Quaternion startRot;
    Rigidbody rb;
    XRGrabInteractable grab;
    float releasedTimer = -1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        releasedTimer = -1f;
        gameObject.tag = "Throwable";   // poate puncta din nou dupa ce e luata in mana
    }

    void OnRelease(SelectExitEventArgs args) { releasedTimer = respawnDelay; }

    void Update()
    {
        if (releasedTimer > 0f)
        {
            releasedTimer -= Time.deltaTime;
            if (releasedTimer <= 0f) ResetBall();
        }
        if (transform.position.y < fallLimitY) ResetBall();
    }

    public void ResetBall()
    {
        releasedTimer = -1f;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(startPos, startRot);
        gameObject.tag = "Throwable";   // redevine valida pentru scor
    }
}