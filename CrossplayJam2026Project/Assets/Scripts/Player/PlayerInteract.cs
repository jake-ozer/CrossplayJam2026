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
            bool isHoldingSomething = GetComponent<PlayerHolding>().objectPlayerIsHolding != null;

            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                if (hitObject.GetComponent<Outline>() != null && hitObject.GetComponent<BaseHoldable>() != null && !hasInteracted && !isHoldingSomething)
                {
                    hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                    currentHit = hitInfo.collider.gameObject;
                    interactLabel.SetActive(true);
                    interactLabel.GetComponent<TextMeshProUGUI>().text = currentHit.gameObject.GetComponentInParent<BaseHoldable>().holdableName;
                }

                if (hitObject.GetComponent<Outline>() != null && hitObject.GetComponent<PotLogic>() != null && !hasInteracted && isHoldingSomething && hitObject.GetComponent<PotLogic>().canAddToPot)
                {
                    hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                    currentHit = hitInfo.collider.gameObject;
                    interactLabel.SetActive(true);
                    interactLabel.GetComponent<TextMeshProUGUI>().text = "Left Click to put on pan";
                }

                //If looking at pot and not holding something, display ingredients 
                if(hitObject.GetComponent<Outline>() != null && hitObject.GetComponent<PotLogic>() != null && !hasInteracted && !isHoldingSomething)
                {
                    hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                    currentHit = hitInfo.collider.gameObject;
                    interactLabel.SetActive(true);
                    interactLabel.GetComponent<TextMeshProUGUI>().text = currentHit.GetComponent<PotLogic>().currentIngredients;
                }

                //Set outline for the "cook button"
                if(hitObject.GetComponent<Outline>() != null && hitObject.GetComponent<CookLogic>() != null && !hasInteracted && !isHoldingSomething)
                {
                    hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                    currentHit = hitInfo.collider.gameObject;
                    interactLabel.SetActive(true);
                    interactLabel.GetComponent<TextMeshProUGUI>().text = "Left Click to Cook Omelette";
                }
            }

            //Add to this if statement whenever we need to check for a new interactable is added
            if (currentHit != null && hitObject.GetComponent<BaseHoldable>() == null && hitObject.GetComponent<PotLogic>() == null && hitObject.GetComponent<CookLogic>() == null)
            {
                currentHit.GetComponent<Outline>().enabled = false;
                currentHit = null;
                hasInteracted = false;
                interactLabel.SetActive(false);
            }

            if (currentHit != null && hitObject.GetComponent<PotLogic>() != null && !isHoldingSomething && hasInteracted)
            {
                currentHit.GetComponent<Outline>().enabled = false;
                currentHit = null;
                hasInteracted = false;
                interactLabel.SetActive(false);
            }

            //Unset outline for "cook button"
            if(currentHit != null && hitObject.GetComponent<CookLogic>() != null && !isHoldingSomething && hasInteracted)
            {
                currentHit.GetComponent<Outline>().enabled = false;
                currentHit = null;
                hasInteracted = false;
                interactLabel.SetActive(false);
            }

            if (GetComponent<PlayerInput>().actions["Interact"].triggered && hitObject.GetComponent<BaseHoldable>() != null && !isHoldingSomething)
            {
                Debug.Log("You interacted with: " + hitObject.name);

                hitInfo.collider.gameObject.GetComponent<Outline>().enabled = false;
                hasInteracted = true;
                interactLabel.SetActive(false);

                hitInfo.collider.gameObject.GetComponent<MeshCollider>().enabled = false;
                hitInfo.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<PlayerHolding>().objectPlayerIsHolding = hitInfo.collider.gameObject;
            }

            if (GetComponent<PlayerInput>().actions["Interact"].triggered && hitObject.GetComponent<PotLogic>() != null && isHoldingSomething)
            {
                Debug.Log("You interacted with: " + hitObject.name);

                hitInfo.collider.gameObject.GetComponent<Outline>().enabled = false;
                hasInteracted = true;
                interactLabel.SetActive(false);

                hitInfo.collider.gameObject.GetComponent<PotLogic>().AddToPotFromHoldable(GetComponent<PlayerHolding>().objectPlayerIsHolding.GetComponent<BaseHoldable>());
                GetComponent<PlayerHolding>().DropItem();
            }

            if(GetComponent<PlayerInput>().actions["Interact"].triggered && hitObject.GetComponent<CookLogic>() != null && !isHoldingSomething)
            {
                Debug.Log("You interacted with: " + hitObject.name);

                hitInfo.collider.gameObject.GetComponent<Outline>().enabled = false;
                hasInteracted = true;
                interactLabel.SetActive(false);

                hitInfo.collider.gameObject.GetComponent<CookLogic>().CookTheOmelette();
            }
        }
    }
}