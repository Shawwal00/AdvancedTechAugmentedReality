using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;


public class ShowingFamousPerson : MonoBehaviour
{
    private bool hasSelected = false;
    private enum currentSelected {WinstonChurchill, None}

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

   private currentSelected thisCurrentSelected = currentSelected.None;

    [SerializeField] public GameObject winstonChurchillButton;
    [SerializeField] public GameObject winstonChurchill;

    [SerializeField] public GameObject historyGui;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public TMP_Text factText;

    private GameObject audioManager;
    private AudioSource audioSource;

    public List<AudioClip> allAudioClips;

    private Dictionary<int, Dictionary<string, string>> peopleText = new Dictionary<int, Dictionary<string, string>>();

    private int currentText = 1;

    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        Button WinstonChurchillButtonComponent = winstonChurchillButton.GetComponent<Button>();
        WinstonChurchillButtonComponent.onClick.AddListener(ChangedToChurchill);

        audioManager = GameObject.Find("AudioManager");

        audioSource = audioManager.GetComponent<AudioSource>();

        peopleText[1] = new Dictionary<string, string>();
        peopleText[2] = new Dictionary<string, string>();
        peopleText[3] = new Dictionary<string, string>();

        peopleText[1]["Audio"] = "None";
        peopleText[1]["Text"] = "World War 2 ended in 1945.";

        peopleText[2]["Audio"] = "2";
        peopleText[2]["Text"] = "Random Fact 2";

        peopleText[3]["Audio"] = "3";
        peopleText[3]["Text"] = "3rd Random Fact";
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerUp += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerUp -= FingerDown; 
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        if (historyGui.activeSelf == true)
        {
            if (currentText >= peopleText.Count)
            {
                audioSource.Stop();
                factText.SetText(peopleText[1]["Text"]);

                if (peopleText[1]["Audio"] != "None")
                {
                    for (int i = 0; i < allAudioClips.Count; i++)
                    {
                        if (allAudioClips[i].name == peopleText[1]["Audio"])
                        {
                            audioSource.clip = allAudioClips[i];
                            audioSource.Play();
                            break;
                        }
                    }
                }

                currentText = 1;
            }

            else
            {
                audioSource.Stop();
                factText.SetText(peopleText[currentText + 1]["Text"]);

                if (peopleText[currentText + 1]["Audio"] != "None")
                {
                    for (int i = 0; i < allAudioClips.Count; i++)
                    {
                        if (allAudioClips[i].name == peopleText[currentText + 1]["Audio"])
                        {
                            audioSource.clip = allAudioClips[i];
                            audioSource.Play();
                            break;
                        }
                    }
                }

                currentText = currentText + 1;
            }    
        }


        /*if (raycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("Happening");
            foreach (ARRaycastHit hit in hits)
            {
                if (historyGui.activeSelf == false)
                {
                    historyGui.SetActive(true);
                }

                else if (historyGui.activeSelf == true)
                {
                    historyGui.SetActive(false);
                }
            }
        }*/
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            historyGui.SetActive(true);
        }
    }

    public void ChangedToChurchill()
    {
        historyGui.SetActive(true);
        factText.SetText(peopleText[1]["Text"]);
        currentText = 1;

        if (peopleText[1]["Audio"] != "None")
        {
            for (int i = 0; i < allAudioClips.Count; i++)
            {
                if (allAudioClips[i].name == peopleText[1]["Audio"])
                {
                    audioSource.clip = allAudioClips[i];
                    audioSource.Play();
                    break;
                }
            }
        }
    }

    public void SetToFalse()
    {
        historyGui.SetActive(false);
        factText.SetText(peopleText[1]["Text"]);
        currentText = 1;
    }

    public void playVideo()
    {
        videoPlayer.Play();
    }

    public void stopVideo()
    {
        videoPlayer.Stop();
    }
}
