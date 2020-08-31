using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomShaker : MonoBehaviour
{
    Bloom bloom;

    void Start()
    {
        var volume = GetComponent<Volume>();
        if (volume.profile.TryGet<Bloom>(out var b))
        {
            bloom = b;
        }
    }

    void OnEnable()
    {
        BloomFeedback.emitted += OnFeedbackEmitted;
    }

    void OnDisable()
    {
        BloomFeedback.emitted -= OnFeedbackEmitted;
        StopAllCoroutines();
    }

    void OnFeedbackEmitted(AnimationCurve curve)
    {
        StartCoroutine(Shake(curve));
    }

    IEnumerator Shake(AnimationCurve curve)
    {
        float duration = curve[curve.length - 1].time;
        float time = 0;
        do
        {
            var currentIntesity = curve.Evaluate(time);
            bloom.intensity.Override(currentIntesity);
            yield return null;
        } while ((time += Time.deltaTime) < duration);
    }
}
