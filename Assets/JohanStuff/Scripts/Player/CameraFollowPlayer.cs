using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraFollowPlayer : MonoBehaviour
{
    Transform player;

    float camFollowSpeed;
    Vector3 cameraOffset;
    Animator anim;
    private CameraManager cameraManager;

    float distance;
    public static CameraFollowPlayer Instance;

    [SerializeField]Vector3 cameraShakeMaxOffset;
    Vector3 cameraShakeOffset;
    [SerializeField, Range(0,1f)] float cameraShakeTime;
    float cameraShakeCounter;
    bool shakeCamera;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        player = FindObjectOfType<Player>().transform;
        camFollowSpeed = cameraManager.camFollowSpeed;
        cameraOffset = cameraManager.cameraOffset;
    }
    void LateUpdate()
    {
        if (player != null)
        {
            if (shakeCamera)
            {
                distance = Vector3.Distance(transform.position, player.position + cameraOffset);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + cameraOffset + cameraShakeOffset, distance * camFollowSpeed * 20 * Time.deltaTime);
            }
            else
            {
                distance = Vector3.Distance(transform.position, player.position + cameraOffset);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + cameraOffset, distance * camFollowSpeed * Time.deltaTime);
            }
            
        }
    }
    public void CameraShake()
    {
        StartCoroutine(ActivateCameraShake());
    }
    IEnumerator ActivateCameraShake()
    {
        shakeCamera = true;
        cameraShakeCounter = 0;
        while(cameraShakeTime >= cameraShakeCounter)
        {
            cameraShakeOffset = new Vector3(Random.Range(0f, cameraShakeMaxOffset.x), Random.Range(0f, cameraShakeMaxOffset.y), Random.Range(0f, cameraShakeMaxOffset.z));
            yield return new WaitForSeconds(0.05f);
            cameraShakeCounter += 0.05f;
            Debug.Log("!");
        }
        shakeCamera = false;
    }
    IEnumerator CheckCameraAnim()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("CameraStartAnim"))
            {
                //Time.timeScale = 1;
                break;
            }
        }
        
    }
    public void ÁnimationDone()
    {
        Time.timeScale = 1;
        anim.enabled = false;
    }
    IEnumerator WaitForTimeScale()
    {
        
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 0;
    }
    private void OnLevelWasLoaded(int level)
    {
        anim = GetComponent<Animator>();
        if (level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            anim.enabled = true;
            Time.timeScale = 0;
            //StartCoroutine(WaitForTimeScale());
        }
        else if (level == SceneManager.GetSceneByName("Level_2").buildIndex)
        {
            StartCoroutine(WaitForTimeScale());
        }
        else
        {
            anim.enabled = false;
        }
    }
}
