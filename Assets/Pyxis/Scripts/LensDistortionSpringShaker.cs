using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensDistortionSpringShaker : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxIntesity = 0.75f;

    public Spring spring;

    LensDistortion lensDistortion;
    Coroutine shakeCoroutine;

    void Start()
    {
        var volume = GetComponent<Volume>();
        if (volume.profile.TryGet<LensDistortion>(out var ld))
        {
            lensDistortion = ld;
        }
    }

    void OnEnable()
    {
        LensDistortionImpulseFeedback.emitted += OnFeedbackEmitted;
    }

    void OnDisable()
    {
        LensDistortionImpulseFeedback.emitted -= OnFeedbackEmitted;
    }

    void OnFeedbackEmitted(float impulse)
    {
        var springAccelerationDirection = spring.acceleration.normalized;
        if (springAccelerationDirection == Vector2.zero)
        {
            springAccelerationDirection = Vector2.right;
        }
        var impulseForce = impulse * springAccelerationDirection;
        spring.addForce(impulseForce);
    }

    void FixedUpdate()
    {
        spring.FixedUpdate();
    }

    void Update()
    {
        var intensity = Mathf.Clamp(spring.position.x, -1f, 1f);
        var quadraticIntensity = Mathf.Sign(intensity) * Mathf.Sqrt(Mathf.Abs(intensity));
        lensDistortion.intensity.Override(quadraticIntensity * maxIntesity);
    }

    void OnDrawGizmosSelected()
    {
        if (enabled) spring.DrawGizmos(gameObject.transform.position);
    }
}
