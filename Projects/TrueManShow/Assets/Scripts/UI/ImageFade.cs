using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFade : MonoBehaviour
{
    // ___________________________________________ Inspector
    public const float MAX_ALPHA_VALUE = 1.0f;

    [Header("Speed")]
    public float FadeInTimeInSeconds;
    public float FadeOutTimeInSeconds;

    // ___________________________________________ Datamembers
    private float mFadeInSpeed;
    private float mFadeOutSpeed;

    private Image mImage;

    private System.Action mFadeUpdate;
    private delegate bool DelEvaluate(ref Color colour);

    // ___________________________________________ Controls
	#region Fading
	
    /// <summary>
    /// Will change the image's alpha value from 0 -> 1, making it completely solid.
    /// When the transition is completed, it will call the callback method.
    /// </summary>
    /// <param name="FadedOutCallback">If this is null, the callback feature will be ignored</param>
    public void FadeOut(System.Action FadedOutCallback = null)
    {
        Fade(0.0f, (ref Color colour) =>
        {
            colour.a += mFadeOutSpeed * Time.deltaTime;
            return colour.a >= 1.0f;
        }, FadedOutCallback);
    }

    /// <summary>
    /// Will change the image's alpha value from 1 -> 0, making it completely transparent.
    /// When the transition is completed, it will call the callback method.
    /// </summary>
    /// <param name="FadedInCallback">If this is null, the callback feature will be ignored</param>
    public void FadeIn(System.Action FadedInCallback = null)
    {
        Fade(1.0f, (ref Color colour) =>
        {
            colour.a -= mFadeInSpeed * Time.deltaTime;
            return colour.a <= 0.0f;
        }, FadedInCallback);
    }

    /// <summary>
    /// Will change the image's alpha value from 0 -> 1, making it completely solid, THEN, change it to 1 -> 0, making it completely transparent.
    /// When each transition is completed, it will call its respective callback method.
    /// </summary>
    /// <param name="FadedOutCallback">If this is null, this particular callback feature will be ignored</param>
    /// <param name="FadedInCallback">If this is null, this particular callback feature will be ignored</param>
    public void FadeOutFadeIn(System.Action FadedOutCallback, System.Action FadedInCallback)
    {
        FadeOut(() =>
        {
            TryCallCallback(FadedOutCallback);
            FadeIn(FadedInCallback);
        });
    }


    /// <summary>
    /// Will change the image's alpha value from 1 -> 0, making it completely transparent, THEN, change it to 0 -> 1, making it completely solid.
    /// When each transition is completed, it will call its respective callback method.
    /// </summary>
    /// <param name="FadedInCallback">If this is null, this particular callback feature will be ignored</param>
    /// <param name="FadedOutCallback">If this is null, this particular callback feature will be ignored</param>
    public void FadeInFadeOut(System.Action FadedInCallback, System.Action FadedOutCallback)
    {
        FadeIn(() =>
        {
            TryCallCallback(FadedInCallback);
            FadeOut(FadedOutCallback);
        });
    }
	#endregion

    // ___________________________________________ Methods
    private void Fade(float startValue, DelEvaluate Evaluation, System.Action Callback)
    {
		// set the start alpha value
        Color colour = mImage.color;
        colour.a = startValue;

		// enable the update (allowing for the mFadeUpdate to run)
        enabled = true;
        mFadeUpdate = () =>
        {
			// calculate the change and evalaute the result
            if (Evaluation(ref colour))
            {
				// if the desired result was given, then disable the update and try to call the callback				
                enabled = false;
                TryCallCallback(Callback);
            }
			// always set the alpha regardless
            mImage.color = colour;
        };
    }

    private static void TryCallCallback(System.Action callback)
    {
        if (callback != null)
            callback();
    }

    private void Awake()
    {
		// calculate the speeds before hand as they will always be the same at runtime
        mFadeInSpeed = MAX_ALPHA_VALUE / FadeInTimeInSeconds;
        mFadeOutSpeed = MAX_ALPHA_VALUE / FadeOutTimeInSeconds;

        mImage = GetComponent<Image>();
        enabled = false; // stop the update from running at the start
    }

    private void Update()
    {
        mFadeUpdate();
    }
}
