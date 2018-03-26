using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using SDE.GamePool;

[RequireComponent(typeof(BoxCollider2D))]
public class ToaderTriggerDisabler : MonoBehaviour, IPoolable
{
    [System.Serializable]
    public class DisableStates
    {
        public Sprite Sprite;
        public float MinDuration;
        public float MaxDuration;
    }

    public DisableStates[] States;

    private Image[] mImages;
    private BoxCollider2D mCollider;

    public void OnCreated()
    {
        mCollider = GetComponent<BoxCollider2D>();
        mImages = GetComponentsInChildren<Image>(true);
    }

    public void OnSpawned()
    {
        StartCoroutine(TriggerDisablerRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator TriggerDisablerRoutine()
    {
        while (enabled)
        {
            int i = 0;
            mCollider.enabled = true;
            for (; i < States.Length - 1; i++)
                yield return new WaitForSeconds(SetSpriteAndGetRandomTime(States[i]));

            mCollider.enabled = false;
            yield return new WaitForSeconds(SetSpriteAndGetRandomTime(States[i]));
            mCollider.enabled = true;

            for (; i >= 0; i--)
                yield return new WaitForSeconds(SetSpriteAndGetRandomTime(States[i]));
        }
    }

    private void SetImages(Sprite sprite)
    {
        foreach (Image image in mImages)
            image.sprite = sprite;
    }

    private float SetSpriteAndGetRandomTime(DisableStates state)
    {
        SetImages(state.Sprite);
        return Random.Range(state.MinDuration, state.MaxDuration);
    }
}
