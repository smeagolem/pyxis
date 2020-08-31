using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensDistortionPendulumShaker : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxIntesity = 0.75f;

    public Pendulum pendulum;

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
        var pendulumDirection = Mathf.Sign(pendulum.velocity);
        var impulseForce = impulse * pendulumDirection;
        pendulum.addTorque(impulseForce);
    }

    void FixedUpdate()
    {
        pendulum.FixedUpdate();
    }

    void Update()
    {
        var intensity = Mathf.Sin(pendulum.angle);
        var quadraticIntensity = Mathf.Sign(intensity) * Mathf.Sqrt(Mathf.Abs(intensity));
        lensDistortion.intensity.Override(quadraticIntensity * maxIntesity);
    }

    void OnDrawGizmosSelected()
    {
        if (enabled) pendulum.DrawGizmos(gameObject.transform.position);
    }
}
