using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    //public GameObject playerprefab;
    //PlayerController pc;
    private void Start()
    {
       
        //pc = playerprefab.GetComponent<PlayerController>();

        if (PlayerPrefs.GetString("bolum").Equals(""))
        {
            PlayerPrefs.SetString("bolum", "Level1");
        }
    }

    public void PlayGame()
    {
        string s = PlayerPrefs.GetString("bolum");
        if (s.Equals(""))
        {
            s = "Level1";
            PlayerPrefs.SetString("bolum", "Level1");
        }


        SceneManager.LoadScene(s);
    }


    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            SceneManager.LoadScene("Level2");
        }

        else if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            SceneManager.LoadScene("Level3");
        }

    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("AnaMenu");
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public AudioMixer am;

    public void SetVolume(float volume)
    {

        am.SetFloat("VolumeParam",volume);
    }


    public void rabbit()
    {
        PlayerPrefs.SetString("evrim", "yayli_ayak");
        //pc.jumpBoost = 10f;
        NextLevel();

    }

   /* public void rabbitboynuz()
    {
        PlayerPrefs.SetString("evrim", "yayli_boynuz");
        //pc.jumpBoost = 2f;
        NextLevel();
    }

    public void rabbityilan()
    {
        PlayerPrefs.SetString("evrim", "yayli_yilan");
        //pc.jumpBoost = 2f;
        NextLevel();
    }*/


    public void predator()
    {

        PlayerPrefs.SetString("evrim", "yirtici");
        NextLevel();
    }

    public void boynuz()
    {
        if (PlayerPrefs.GetString("evrim").Equals("yirtici"))
        {

            PlayerPrefs.SetString("evrim", "yirtici_boynuz");
        }

        else if (PlayerPrefs.GetString("evrim").Equals("yayli_ayak"))
        {

            PlayerPrefs.SetString("evrim", "yayli_boynuz");
        }
        
        NextLevel();
    }

    public void yilan()
    {
        if (PlayerPrefs.GetString("evrim").Equals("yirtici"))
        {

            PlayerPrefs.SetString("evrim", "yirtici_yilan");
        }

        else if (PlayerPrefs.GetString("evrim").Equals("yayli_ayak"))
        {

            PlayerPrefs.SetString("evrim", "yayli_yilan");
        }

        NextLevel();
    }
    /*
   

    public void predatoryilan()
    {

        PlayerPrefs.SetString("evrim", "yirtici_yilan");
        NextLevel();
    }*/

    public void deletePref()
    {

        PlayerPrefs.DeleteKey("evrim");
    }

    public void deleteLevel()
    {

        PlayerPrefs.DeleteKey("bolum");
    }
}
