using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float fadeDuration;

    private State state;
    private float startTime;

    private Image logo;
    private Color logoColor;
    private Color logoTransparentColor;

    private enum State
    {
        FadeIn,
        Displayed,
        FadeOut,
    }

    private void Start()
    {
        this.startTime = Time.time;
        GameObject logoObject = GameObject.Find("Logo");
        this.logo = logoObject.GetComponent<Image>();
        Debug.Assert(this.logo != null, "Can't retrieve logo object.");
        this.logoColor = this.logo.color;
        this.logoTransparentColor = new Color(1f, 1f, 1f, 0f);
    }
    
    private void Update()
    {
        switch (this.state)
        {
            case State.FadeIn:
                {
                    float ratio = this.duration > 0 ? (Time.time - this.startTime) / this.duration : 1f;
                    this.logo.color = Color.Lerp(this.logoTransparentColor, this.logoColor, ratio);
                    if (Time.time > this.startTime + this.duration)
                    {
                        this.startTime = Time.time;
                        this.state = State.Displayed;
                    }
                }

                break;

            case State.Displayed:
                {
                    if (Time.time > this.startTime + this.duration)
                    {
                        this.startTime = Time.time;
                        this.state = State.FadeOut;
                    }
                }

                break;

            case State.FadeOut:
                {
                    float ratio = this.duration > 0 ? (Time.time - this.startTime) / this.duration : 1f;
                    this.logo.color = Color.Lerp(this.logoColor, this.logoTransparentColor, ratio);
                    if (Time.time > this.startTime + this.duration)
                    {
                        SceneManager.LoadSceneAsync(this.sceneToLoad, LoadSceneMode.Single);
                    }
                }

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
