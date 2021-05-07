using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        StartCoroutine(SizePop());
    }
    IEnumerator SizePop()
    {
        while(scaleValue < 0.5f)
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
            scaleValue += 0.01f;
            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            
            yield return new WaitForFixedUpdate();
            
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }
    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        deActivated = false;
        //gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
    }
    private void OnLevelWasLoaded(int level)
    {
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
                gameObject.SetActive(false);
                canBePressed = false;
                NoteMiss.Instance.TriggerCountZero();
            }
        }
    }

    public void ButtonAttack()
    {
        publisher.NoteHitAttack();
        gameObject.SetActive(false);
        canBePressed = false;
        NoteMiss.Instance.TriggerCountZero();
    }
    public void ButtonBlock()
    {
        publisher.NoteHitBlock();
        gameObject.SetActive(false);
        canBePressed = false;
        NoteMiss.Instance.TriggerCountZero();
    }
    private void DesktopInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            publisher.NoteHit();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (OverGUICheck.Instance.IsPointerOverUIObject())
            {

            }
            else
            {
                publisher.NoteHit();
                gameObject.SetActive(false);
                canBePressed = false;
                NoteMiss.Instance.TriggerCountZero();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            publisher.NoteHitBlock();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            publisher.NoteHitAttack();
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
