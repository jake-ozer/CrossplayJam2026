using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHolding : MonoBehaviour
{
    public GameObject objectPlayerIsHolding;
    public GameObject holdingLabel;

    private void Update()
    {
        if (objectPlayerIsHolding != null)
        {
            holdingLabel.GetComponent<TextMeshProUGUI>().text = 
                "You are holding: "+objectPlayerIsHolding.GetComponent<BaseHoldable>().holdableName+". Press f to return it back to its spot.";
            holdingLabel.SetActive(true);
        }
        else
        {
            holdingLabel.SetActive(false);
        }

        //drop object
        if (objectPlayerIsHolding != null && GetComponent<PlayerInput>().actions["Drop"].triggered)
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        objectPlayerIsHolding.GetComponent<MeshCollider>().enabled = true;
        objectPlayerIsHolding.transform.GetChild(0).gameObject.SetActive(true);
        objectPlayerIsHolding = null;
    }
}
