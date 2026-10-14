using UnityEngine;
using UnityEngine.InputSystem;

// Citeste valorile analogice de grip si trigger de pe controller
// si le trimite catre Animator (parametrii float Grip si Trigger).
public class HandAnimatorDriver : MonoBehaviour
{
    [Header("Input (din XRI Default Input Actions)")]
    public InputActionProperty gripAction;      // Select Value
    public InputActionProperty triggerAction;   // Activate Value

    [Header("Animatie")]
    public Animator animator;
    public float smoothing = 15f;
    public bool logValues = false;   // bifati pentru test

    static readonly int GripId = Animator.StringToHash("Grip");
    static readonly int TriggerId = Animator.StringToHash("Trigger");

    float grip, trigger;

    void Reset()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float g = gripAction.action != null ? gripAction.action.ReadValue<float>() : 0f;
        float t = triggerAction.action != null ? triggerAction.action.ReadValue<float>() : 0f;

        grip = Mathf.Lerp(grip, g, Time.deltaTime * smoothing);
        trigger = Mathf.Lerp(trigger, t, Time.deltaTime * smoothing);

        if (logValues) Debug.Log($"{name}: grip={grip:F2} trigger={trigger:F2}");

        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.SetFloat(GripId, grip);
            animator.SetFloat(TriggerId, trigger);
        }
    }
}
