using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Smeagolem.Pyxis
{

    public class LensDistortionCurveShaker : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float maxIntesity = 0.75f;
        public bool replace = true;

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
            LensDistortionCurveFeedback.emitted += OnFeedbackEmitted;
        }

        void OnDisable()
        {
            LensDistortionCurveFeedback.emitted -= OnFeedbackEmitted;
            StopAllCoroutines();
        }

        void OnFeedbackEmitted(AnimationCurve curve)
        {
            if (replace && shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = null;
            }

            if (shakeCoroutine == null)
            {
                shakeCoroutine = StartCoroutine(Shake(curve));
            }
        }

        IEnumerator Shake(AnimationCurve curve)
        {
            float duration = curve[curve.length - 1].time;
            float time = 0;
            do
            {
                var intensity = curve.Evaluate(time);
                lensDistortion.intensity.Override(intensity);
                yield return null;
            } while ((time += Time.deltaTime) < duration);
            shakeCoroutine = null;
        }
    }

}
