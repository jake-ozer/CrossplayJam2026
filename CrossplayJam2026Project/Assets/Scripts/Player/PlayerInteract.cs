using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask interactLayer;
    public GameObject interactLabel;

    private Camera cam;
    public GameObject currentHit;
    public bool hasInteracted = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            //highlight
            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                if (hitObject.GetComponent<Outline>() != null && hitObject.GetComponent<BaseHoldable>() != null &&
                    !hasInteracted)
                {
                    hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                    currentHit = hitInfo.collider.gameObject;
                    interactLabel.SetActive(true);
                    interactLabel.GetComponent<TextMeshProUGUI>().text = currentHit.gameObject.transform.root.GetComponent<BaseHoldable>().name;
                }
            }

            //disable highlight on previous hit
            if (currentHit != null && hitObject.GetComponent<BaseHoldable>() == null)
            {
                currentHit.GetComponent<Outline>().enabled = false;
                currentHit = null;
                hasInteracted = false;
                interactLabel.SetActive(false);
            }

            //click to interact
            if (GetComponent<PlayerInput>().actions["Interact"].triggered && hitObject.GetComponent<BaseHoldable>() != null)
            {
                Debug.Log("You interacted with: " + hitObject.name);
                
                hitInfo.collider.gameObject.GetComponent<Outline>().enabled = false;
                hasInteracted = true;
                interactLabel.SetActive(false);
            }
        }
    }
}