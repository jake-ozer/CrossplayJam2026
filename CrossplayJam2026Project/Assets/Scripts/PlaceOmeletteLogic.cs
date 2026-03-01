using UnityEngine;

public class PlaceOmeletteLogic : MonoBehaviour
{
    [SerializeField] private GameObject omelette;

    [SerializeField] private Animator grandmaAnimator;



    public bool OmelettePlace(BaseHoldable inHandOmelette)
    {
        //Debug.Log("Placed Omelette");

        omelette.SetActive(true);
        if(inHandOmelette.correctOmelette)
        {
            //Play some finishing stuff for end game
            grandmaAnimator.SetBool("isHappy", true);
        }
        else
        {
            //prompt user to go to work
            grandmaAnimator.SetBool("isAngry", true);
        }

        return false;
    } 
}
