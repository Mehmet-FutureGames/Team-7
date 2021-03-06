using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NoteObject : MonoBehaviour
{
   
    private NotePublisher publisher;
    public bool canBePressed;
    public bool perfectHit;
    public bool deActivated;

    NoteManager noteManager;
    float scaleValue;
    // Start is called before the first frame update
    private void Awake()
    {
        noteManager = FindObjectOfType<NoteManager>();
        publisher = FindObjectOfType<NotePublisher>();
        GetNoteList.NoteList.Add(this);
    }
    void Start()
    {
        scaleValue = 0;
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(SizePop());
    }
    IEnumerator SizePop()
    {
        while(scaleValue < 0.6f)
        {
            scaleValue += 0.20f;
            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            yield return new WaitForFixedUpdate();
            
        }
        StartCoroutine(SizeNormalize());
        yield return null;
    }
    IEnumerator SizeNormalize()
    {
        while (scaleValue < 1f)
        {
            scaleValue += 0.004f;
            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            
            yield return new WaitForFixedUpdate();
            
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }
    private void OnEnable()
    {
        deActivated = false;
        if (NoteManager.IsInShop)
        {
            canBePressed = true;
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("Shop").buildIndex || level == SceneManager.GetSceneByName("CoinShop").buildIndex)
        {
            canBePressed = true;
        }
        gameObject.SetActive(false);
    }
#if UNITY_STANDALONE
    void Update()
    {
        if (canBePressed && deActivated == false)
        {
            DesktopInput();
        }
    }
#endif
#if UNITY_ANDROID
    void Update()
    {
        if (canBePressed && deActivated == false)
        {
            AndroidInput();
        }
    }
#endif
    private void AndroidInput()
    {
        if (OverGUICheck.Instance.IsPointerOverUIObject())
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                publisher.NoteHit();
                ButtonPressed();
            }
        }
    }

    public void ButtonAttack()
    {
        publisher.NoteHitAttack();
        ButtonPressed();
    }
    public void ButtonBlock()
    {
        publisher.NoteHitBlock();
        ButtonPressed();
    }
    private void DesktopInput()
    {
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            publisher.NoteHit();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
        */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (OverGUICheck.Instance.IsPointerOverUIObject())
            {

            }
            else
            {
                publisher.NoteHit();
                ButtonPressed();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            publisher.NoteHitBlock();
            ButtonPressed();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            publisher.NoteHitAttack();
            ButtonPressed();
        }
    }

    private void ButtonPressed()
    {
        if (!NoteManager.IsInShop)
        {
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }
    private void OnDisable()
    {
        canBePressed = false;
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Activator"))
        {
            publisher.NoteNotHit();
            canBePressed = false;
            gameObject.SetActive(false);
        }
    }

    
}
