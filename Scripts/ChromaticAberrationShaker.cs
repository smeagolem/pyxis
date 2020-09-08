using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Smeagolem.Pyxis
{

    public class ChromaticAberrationShaker : MonoBehaviour
    {
        ChromaticAberration chromaticAberration;

        void Start()
        {
            var volume = GetComponent<Volume>();
            if (volume.profile.TryGet<ChromaticAberration>(out var ca))
            {
                chromaticAberration = ca;
            }
        }

        void OnEnable()
        {
            ChromaticAberrationFeedback.emitted += OnFeedbackEmitted;
        }

        void OnDisable()
        {
            ChromaticAberrationFeedback.emitted -= OnFeedbackEmitted;
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
                chromaticAberration.intensity.Override(currentIntesity);
                yield return null;
            } while ((time += Time.deltaTime) < duration);
        }
    }

}
