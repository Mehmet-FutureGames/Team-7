using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankYou : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ThankYouAndGoodByeYousuck());
    }
    IEnumerator ThankYouAndGoodByeYousuck()
    {
        yield return new WaitForSeconds(25f);
        SceneManager.LoadScene("MainMenu");
    }


}
