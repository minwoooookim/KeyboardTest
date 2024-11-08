using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Countdown : MonoBehaviour
{
    public int countdownTime = 3;
    public TextMeshProUGUI countdownDisplay;

    public void Start_NK_OnClick()
    {
        StartCoroutine(CountdownToStartNK());
    }

    public void Start_WK_OnClick()
    {
        StartCoroutine(CountdownToStartWK());
    }

    IEnumerator CountdownToStartNK()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Test 1");
    }

    IEnumerator CountdownToStartWK()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Test 2");
    }
}
