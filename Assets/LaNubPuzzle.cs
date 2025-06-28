using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaNubPuzzle : MonoBehaviour
{
    public static LaNubPuzzle Instance { get; private set; }
    public GameObject canvasLaNab;
    public GameObject FadeOut;

    public void CloseLaNabPuzzleUI()
    {
        canvasLaNab.SetActive(false);
    }

    public void CorrectOrder()
    {
        Debug.Log("Correct!");
        FadeOut.SetActive(true);
        DialougeController.Instance.EnableMouseMove();
        DialougeController.Instance.EnableInput();
        CloseLaNabPuzzleUI();
        SceneManager.LoadScene(2);
    }

    public void WrongOrder()
    {
        Debug.Log("Wrong!");
    }

}
