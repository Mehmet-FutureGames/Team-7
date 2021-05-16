using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [TextArea(3, 3)]
    [SerializeField] string enemyDescription;
    [SerializeField] GameObject textToShow;
    Player player;
    float playerDistance;
    bool hasSpawnedATextBox;
    GameObject text;
    Vector3 offset = new Vector3(0, 15, 0);
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateDistance();
        if (CalculateDistance())
        {
            TextToShow();
        }
        else
        {
            DestroyText();
        }
    }

    private bool CalculateDistance()
    {
        playerDistance = (player.transform.position - transform.position).magnitude;
        if(playerDistance < 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void TextToShow()
    {
        if (!hasSpawnedATextBox)
        {
            text = Instantiate(textToShow, transform.position, Quaternion.identity, transform);
            text.transform.position = new Vector3(text.transform.position.x, offset.y, text.transform.position.z);
            text.GetComponent<TextMeshProUGUI>().text = enemyDescription;
            hasSpawnedATextBox = true;
        }
    }
    private void DestroyText()
    {
        Destroy(text);
        hasSpawnedATextBox = false;
    }
}
