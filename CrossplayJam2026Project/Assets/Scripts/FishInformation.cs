using UnityEngine;
using UnityEngine.SceneManagement;

public class FishInformation : MonoBehaviour
{
    
    public string fishName = "Fishy";

    public void GrabFromInputField(string input)
    {
        fishName = input;
    }


    public void MakeFishBlue()
    {
        PlayerPrefs.SetInt("FishType", 1);
        PlayerPrefs.SetString("FishName", fishName);
        LoadScene();
    }

    public void MakeFishRed()
    {
        PlayerPrefs.SetInt("FishType", 2);
        PlayerPrefs.SetString("FishName", fishName);
        LoadScene();
    }

    public void MakeFishGold()
    {
        PlayerPrefs.SetInt("FishType",3);
        PlayerPrefs.SetString("FishName", fishName);
        LoadScene();
    }



    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
