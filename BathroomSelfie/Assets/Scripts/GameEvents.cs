using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        StartCoroutine(LateStart(0.8f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Message();
    }
    private void Update()
    {
        AnimationEnded();
        ArrowMovement();
        TapGame();
        EndGame();
        WrongTapAttempt();
    }
    public event Action onMessaging;     
    public void Message()
    {
        if(onMessaging != null)
        {
            onMessaging();
        }
    }
    public event Action onAnimationEnded;
    public void AnimationEnded()
    {
        if(onAnimationEnded != null)
        {
            onAnimationEnded();
        }
    }
    public event Action onWrongTapAttempt;
    public void WrongTapAttempt()
    {
        if(onWrongTapAttempt != null)
        {
            onWrongTapAttempt();
        }
    }
    public event Action onArrowMovement;
    public void ArrowMovement()
    {
        if(onArrowMovement != null)
        {
            onArrowMovement();
        }
    }
    public event Action onTapGame;
    public void TapGame()
    {
        if(onTapGame != null)
        {
            onTapGame();
        }
    }
    public event Action OnEndGame;
    public void EndGame()
    {
        if(OnEndGame != null)
        {
            OnEndGame();
        }
    }

}
