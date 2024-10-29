using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class ShowingFamousPerson : MonoBehaviour
{
    private bool hasSelected = false;
    private enum currentSelected {WinstonChurchill}

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

   private currentSelected thisCurrentSelected;

    [SerializeField] public GameObject winstonChurchillButton;
    [SerializeField] public GameObject winstonChurchill;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        Button WinstonChurchillButtonComponent = winstonChurchillButton.GetComponent<Button>();
        WinstonChurchillButtonComponent.onClick.AddListener(ChangedToChurchill);
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown; 
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;
        if (raycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                Pose pose = hit.pose;
                GameObject obj = Instantiate(winstonChurchill, pose.position, pose.rotation);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangedToChurchill()
    {
        thisCurrentSelected = currentSelected.WinstonChurchill;
    }
}
