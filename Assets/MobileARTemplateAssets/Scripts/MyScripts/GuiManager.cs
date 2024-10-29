using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    //Gui
    [SerializeField] public GameObject startButton;
    [SerializeField] public GameObject createButton;
    [SerializeField] public GameObject introGui;
    [SerializeField] public GameObject createGui;
    [SerializeField] public GameObject cancelButton;

    // Start is called before the first frame update
    void Start()
    {
        Button startButtonComponent = startButton.GetComponent<Button>();
        startButtonComponent.onClick.AddListener(StartClicked);

        Button createButtonComponent = createButton.GetComponent<Button>();
        createButtonComponent.onClick.AddListener(CreateClicked);

        Button cancelButtonComponent = cancelButton.GetComponent<Button>();
        cancelButtonComponent.onClick.AddListener(CancelClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void StartClicked() 
    {
       introGui.SetActive(false);
        createButton.SetActive(true);
    }

    void CreateClicked()
    {
        createButton.SetActive(false);
        createGui.SetActive(true);
    }

    void CancelClicked()
    {
        createButton.SetActive(true);
        createGui.SetActive(false);
    }
}
