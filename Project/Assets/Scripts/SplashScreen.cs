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
        if (Input.GetMouseButton(0) || Input.anyKey)
        {
            this.LoadMenu();
        }

        switch (this.state)
        {
            case State.FadeIn:
                {
                    float ratio = this.duration > 0 ? (Time.time - this.startTime) / this.fadeDuration : 1f;
                    this.logo.color = Color.Lerp(this.logoTransparentColor, this.logoColor, ratio);
                    if (Time.time > this.startTime + this.fadeDuration)
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
                    float ratio = this.duration > 0 ? (Time.time - this.startTime) / this.fadeDuration : 1f;
                    this.logo.color = Color.Lerp(this.logoColor, this.logoTransparentColor, ratio);
                    if (Time.time > this.startTime + this.fadeDuration)
                    {
                        this.LoadMenu();
                    }
                }

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadSceneAsync(this.sceneToLoad, LoadSceneMode.Single);
    }
}
