using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class Boot : MonoBehaviour
{
    [SerializeField]
    private Image _splashFirstItem;

    [SerializeField]
    private Image _splashSecondItem;

    [SerializeField]
    private string mainScene;

    [SerializeField]
    private float _intervalSeconds = 1.5f;

    [SerializeField]
    private float _fadeSeconds = 1.5f;

    private Queue<Action> _splashQueue = new Queue<Action>();
    private GameObject _currentObject;
    private Tween _currentTween;
    private bool firstLaunch;
    private void Awake()
    {
        LogWithTime("Boot.Awake");

        //we cannot check save file here, so the work around using player prefs
        if (PlayerPrefs.GetInt("FirstLaunch", 0) == 0)
        {
            // Code to execute if it's the first launch
            Debug.Log("First launch!");

            // Set a flag to indicate that the game has been launched at least once
            PlayerPrefs.SetInt("FirstLaunch", 1);
            PlayerPrefs.Save();
        }
        else
        {
            // Code to execute if it's not the first launch
            Debug.Log("Not the first launch.");
            firstLaunch = false;
        }

        FixScreenResolution(force: Environment.CommandLine.Contains("reset_resolution"));
    }

    private void Start()
    {
        StartAnimation();
    }

    private void Update()
    {
        if (UnityEngine.Input.anyKeyDown && !firstLaunch)
        {
            SkipSplashScreen();
        }
    }

    private void StartAnimation()
    {
        // Configure and play a Queue of animations

        // Add steps to the action queue
        // Step 1 - Apple Aracde video
        // Step 2 - Fade in developer logo
        //_splashQueue.Enqueue(() => FadeIn(_splashFirstItem, _fadeSeconds));
        //// Step 3 - Hold for x seconds.
        //_splashQueue.Enqueue(() => StartCoroutine(WaitForSecondsSplash(_intervalSeconds)));
        //// Step 4 - Fade out developer logo
        //_splashQueue.Enqueue(() => FadeOut(_splashFirstItem, _fadeSeconds));
        //// Step 5 - Hold for x seconds.
        //_splashQueue.Enqueue(() => StartCoroutine(WaitForSecondsSplash(_intervalSeconds)));
        //// Step 6 - Fade in partner logo
        //_splashQueue.Enqueue(() => FadeIn(_splashSecondItem, _fadeSeconds));
        //// Step 7 - Hold for x seconds.
        //_splashQueue.Enqueue(() => StartCoroutine(WaitForSecondsSplash(_intervalSeconds)));
        //// Step 8 - Fade out partner logo
        //_splashQueue.Enqueue(() => FadeOut(_splashSecondItem, _fadeSeconds));
        // Step 9 - Show loading throbber
        _splashQueue.Enqueue(() => StartCoroutine(LoadMain()));
        // Start executing actions
        ExecuteNextAction();

    }

    void FadeIn(Image image, float duration)
    {
        _currentObject = image.gameObject;
        Color tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;

        _currentTween = image.DOFade(1f, duration).OnComplete(ExecuteNextAction);
    }

    void FadeOut(Image image, float duration)
    {
        _currentObject = image.gameObject;
        Color tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;

        _currentTween = image.DOFade(0f, duration).OnComplete(ExecuteNextAction);
    }

    public void ExecuteNextAction()
    {
        if (_splashQueue.Count > 0)
        {
            Action nextAction = _splashQueue.Dequeue();
            nextAction.Invoke();
        }
        else
        {
            Debug.Log("Sequence complete.");
        }
    }

    IEnumerator WaitForSecondsSplash(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ExecuteNextAction();
    }

    private void SkipSplashScreen()
    {
        if (_currentObject != null)
        {
            if (_currentTween != null)
            {
                _currentTween.Complete();
            }
            else
            {
                _currentObject.SetActive(false);
                ExecuteNextAction();
            }
        }
        else
        {
            ExecuteNextAction();
        }
    }

    static public void LogWithTime(string msg)
    {
        if (Application.isEditor)
            Debug.Log(msg); //unity editor already prints the timestamp
        else
            Debug.Log($"[{DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss:fff")}] {msg}");
    }

    public IEnumerator LoadMain()
    {
        //NOTE: WaitForEndOfFrame isn't supported in batch mode, for more info see https://docs.unity3d.com/Manual/CLIBatchmodeCoroutines.html
        //      This fixes an issue when trying to run PlayMode tests in Unity where it would be stuck indefinitely until the end of frame, which would never occur.
        LogWithTime("wait 1 frame");
        yield return Application.isBatchMode ? null : new WaitForEndOfFrame();
        LogWithTime("wait 1 frame done");

        // Load the main scene async but do not start it
        LogWithTime("loading main scene");
        SceneManager.LoadSceneAsync(mainScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    
    private void FixScreenResolution(bool force = false)
    {
#if UNITY_STANDALONE_WIN
        // MDL-35949: This workaround work only in fullscreen. It's called twice to give user some time to press ALT-ENTER.
        if (force || Screen.currentResolution.width < 200 || Screen.currentResolution.height < 200)
        {
            LogWithTime($"ScreenResolution will reset to 1920x1080 {FullScreenMode.ExclusiveFullScreen} (was {Screen.currentResolution.width}x{Screen.currentResolution.height} {Screen.fullScreenMode})");
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen); // The default value set in Unity
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
#endif
    }

}
