using System.Collections.Specialized;
using UnityEngine;

public class InitializeFish : MonoBehaviour
{

    private string fishName;
    private int fishType;

    [SerializeField] BaseHoldable redFish;
    [SerializeField] BaseHoldable blueFish;
    [SerializeField] BaseHoldable goldFish;


    void Awake()
    {
        fishName = PlayerPrefs.GetString("FishName");
        fishType = PlayerPrefs.GetInt("FishType");
    }

    void Start()
    {
        if(fishName.Equals(""))
        {
            fishName = "Fishy";
        }
        switch(PlayerPrefs.GetInt("FishType"))
        {
            case 1:
                blueFish.gameObject.SetActive(true);
                blueFish.holdableName = fishName;
                break;
            case 2:
                redFish.gameObject.SetActive(true);
                redFish.holdableName = fishName;
                break;
            case 3:
                goldFish.gameObject.SetActive(true);
                goldFish.holdableName = fishName;
                break;
            default:
                break;
        }
    }
}
