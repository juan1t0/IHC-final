// To use this example, attach this script to an empty GameObject.
// Create three buttons (Create>UI>Button). Next, select your
// empty GameObject in the Hierarchy and click and drag each of your
// Buttons from the Hierarchy to the Your First Button, Your Second Button
// and Your Third Button fields in the Inspector.
// Click each Button in Play Mode to output their message to the console.
// Note that click means press down and then release.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{

    public void ButtonServer() {
        Debug.Log("server mode");
        PlayerPrefs.SetInt("type", 0);
        SceneManager.LoadScene(1);
    }
    public void ButtonPlayer1() {
        Debug.Log("client 1");
        PlayerPrefs.SetInt("type", 1);
        SceneManager.LoadScene(1);
    }
    public void ButtonPlayer2() {
        Debug.Log("client 2");
        PlayerPrefs.SetInt("type", 2);
        SceneManager.LoadScene(1);
    }
}