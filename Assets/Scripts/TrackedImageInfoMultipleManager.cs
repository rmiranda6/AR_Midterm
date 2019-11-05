using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    public GameObject arObjectsToPlace1;
    public GameObject arObjectsToPlace2;
    public GameObject arObjectsToPlace3;


    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        arObjectsToPlace1 = GetComponent<GameObject>();
        arObjectsToPlace2 = GetComponent<GameObject>();
        arObjectsToPlace3 = GetComponent<GameObject>();


        // setup all game objects in dictionary
        foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            arObjects.Add(arObject.name, newARObject);
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Dismiss() => welcomePanel.SetActive(false);

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display the name of the tracked image in the canvas
        imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

        Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if(arObjectsToPlace != null)
        {
            GameObject goARObject = arObjects[name];
            if(imageTrackedText.text == "24th_Street_Road")
            {
                arObjectsToPlace1.SetActive(true);
                arObjectsToPlace2.SetActive(false);
                arObjectsToPlace3.SetActive(false);
                foreach (GameObject go in arObjects.Values)
                {
                    Debug.Log($"Go in arObjects.Values: {go.name}");
                    if (go.name != name)
                    {
                        go.SetActive(false);
                    }
                }

            }
            if (imageTrackedText.text == "Storm_Cellar")
            {
                arObjectsToPlace2.SetActive(true);
                arObjectsToPlace1.SetActive(false);
                arObjectsToPlace3.SetActive(false);
                foreach (GameObject go in arObjects.Values)
                {
                    Debug.Log($"Go in arObjects.Values: {go.name}");
                    if (go.name != name)
                    {
                        go.SetActive(false);
                    }
                }

            }
            if (imageTrackedText.text == "Evidence_NO2")
            {
                arObjectsToPlace3.SetActive(true);
                arObjectsToPlace1.SetActive(false);
                arObjectsToPlace2.SetActive(false);
                foreach (GameObject go in arObjects.Values)
                {
                    Debug.Log($"Go in arObjects.Values: {go.name}");
                    if (go.name != name)
                    {
                        go.SetActive(false);
                    }
                }

            }
           
        }
    }
}
