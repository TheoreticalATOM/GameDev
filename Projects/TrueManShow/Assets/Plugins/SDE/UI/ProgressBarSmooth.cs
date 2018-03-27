using System.Collections;
using UnityEngine;

namespace SDE.UI
{
    public class ProgressBarSmooth : Progress
    {
        public const float NEAR_ENOUGH_FUDGE_FACTOR = 0.4f;
        [System.Flags]
        public enum EBarDir { Horizontal = 1, Vertical = 2 }

        public RectTransform Bar;
        public EBarDir Direction = EBarDir.Horizontal;
        public float LerpSpeed;
        
        private Vector2 mOrigSize;
        private Coroutine mRoutine;

        // ________________________________________________________ Controls
        /// <summary>
        /// Sets the bar size to be the percentage scale of its original size
        /// example: RectTransform: 200, value: 5, maxValue: 10 | size = RT * (value/maxValue) = 100
        /// </summary>
        protected override void OnUpdateProgress(float percentage)
        {
            Vector2 targetSize = mOrigSize;

            SetBarRect(ref targetSize, percentage);

            // Run a coroutine that updates the bar over time
            if (mRoutine != null) StopCoroutine(mRoutine);
            mRoutine = StartCoroutine(BarRoutine(targetSize));
        }

        // ________________________________________________________ Methods
        private void Awake()
        {
            Vector2 size = Bar.sizeDelta;
            mOrigSize = size;

            // start by making the bar progress to be 0
            SetBarRect(ref size, 0.0f);
            Bar.sizeDelta = size;
        }

        private void SetBarRect(ref Vector2 size, float value)
        {
            // update the appropriate size, depending on the binary x/y flags
            if ((Direction & EBarDir.Horizontal) == EBarDir.Horizontal) size.x *= value;
            if ((Direction & EBarDir.Vertical) == EBarDir.Vertical) size.y *= value;
        }

        IEnumerator BarRoutine(Vector2 targetSize)
        {
            Vector2 currPos = Vector2.zero;
            do
            {
                // constantly move towards the targeted size, until the size is close enough 
                currPos = Bar.sizeDelta;
                Bar.sizeDelta = Vector2.Lerp(currPos, targetSize, LerpSpeed * Time.deltaTime);
                yield return null;
                // NOTE: using a near enough value, rather than the exact value due to floating point inaccuracy
            } while ((targetSize - currPos).magnitude > NEAR_ENOUGH_FUDGE_FACTOR);
        }
    }
}