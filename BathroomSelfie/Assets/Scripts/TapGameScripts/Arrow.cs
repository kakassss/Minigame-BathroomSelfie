using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool canMove = true;
    [SerializeField] float speed;

    private void OnEnable()
    {
        GameEvents.current.onArrowMovement += Movement;
    }
    private void Movement()
    {
        if (canMove)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            GameEvents.current.onArrowMovement -= Movement;
        }
    }
}
