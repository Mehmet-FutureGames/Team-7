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

    public void ÁnimationDone()
    {
        GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        anim.StopPlayback();
        anim.enabled = false;
    }

}
