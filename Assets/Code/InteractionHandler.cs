using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class InteractionHandler : MonoBehaviour
{
    int maxDistance = 10;
    public Image reticle;
    public LayerMask interactableLayer;

    public GameObject trashUI;
    public GameObject keyUI;
    public GameObject doorUI;

    public GameObject door;
    public GameObject floor;
    public bool hasKey;
    public bool hasButton;

    public GameObject particles;
    
    Transform camTrans;

    void Start()
    {
        camTrans = Camera.main.transform;
    }
    
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(camTrans.position, camTrans.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name + hit.collider.gameObject.tag);
                // Change this to destroy the object
                //Destroy(hit.collider.gameObject);
                if (hit.collider.CompareTag("Trash"))
                {
                    Debug.Log("destroying trash");
                    Destroy(hit.collider.gameObject);
                    trashUI.SetActive(true);
                    StartCoroutine(HideUIAfterDelay(trashUI, 3f));
                }

                if (hit.collider.CompareTag("Key"))
                {
                    Destroy(hit.collider.gameObject);
                    hasKey = true;
                    trashUI.SetActive(false);
                    keyUI.SetActive(true);
                    StartCoroutine(HideUIAfterDelay(keyUI, 3f));
                }

                if (hit.collider.CompareTag("Door") && hasKey)
                {
                    door.SetActive(false);

                }
                
                if (hit.collider.CompareTag("Door") && hasKey==false)
                {
                    doorUI.SetActive(true);
                    StartCoroutine(HideUIAfterDelay(doorUI, 3f));
                }

                if (hit.collider.CompareTag("Button"))
                {
                    hasButton = true;
                    floor.SetActive(true);
                }

            }
        }
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(camTrans.position, camTrans.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            reticle.color = Color.red;
        }
        else
        {
            reticle.color = Color.white;
        }

        if (hasButton)
        {
            particles.SetActive(true);
        }
    }

    IEnumerator HideUIAfterDelay(GameObject uiElement, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        uiElement.SetActive(false); // Deactivate the UI element
    }
}
