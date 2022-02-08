using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MessageController : MonoBehaviour
{
    [Header("Message")]
    [SerializeField] List<GameObject> messages;
    [SerializeField] float messageDelay = 1f;
    [SerializeField] Animator animWoman;  
    
    private void Awake()
    {
        DeActiveMessages();
    }
    private void DeActiveMessages()
    {
        foreach (GameObject message in messages)
        {
            message.SetActive(false);
            message.transform.DOScale(Vector3.zero, 0.1f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onMessaging += OnMessageStart;
        GameEvents.current.onAnimationEnded += OnMessageAnimation;
    }
    private void OnMessageStart()
    {
        animWoman.SetTrigger("OnMessaging");
        StartCoroutine(MessageStart());        
    }
    private IEnumerator MessageStart()
    {
        foreach (GameObject message in messages)
        {
            message.SetActive(true);
            message.gameObject.transform.DOScale(Vector3.one, 0.5f);
            yield return new WaitForSeconds(messageDelay);
        }
    }
    private void HideMessages()
    {
        foreach (GameObject message in messages)
        {
            message.SetActive(false);
        }
    }
    private void OnMessageAnimation()
    {
        if (animWoman.GetCurrentAnimatorStateInfo(0).IsName("UsingPhone") &&
            animWoman.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            //Finish MessageGameplay
            animWoman.SetBool("isUsingPhone", false);
            HideMessages();
            GameEvents.current.onAnimationEnded -= OnMessageAnimation;

            //Start other events
            GameEvents.current.onTapGame += TapCollider.current.CheckWrongTapAttempt;
            GameEvents.current.onTapGame += TapCollider.current.TapBarActive;
            
            StartCoroutine(ArrowTapGame.current.ArrowSpawn());
        }
    }
    
}
