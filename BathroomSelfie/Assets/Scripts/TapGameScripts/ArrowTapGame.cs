using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTapGame : MonoBehaviour
{
    public static ArrowTapGame current;
    public int amountArrow = 10;
    [HideInInspector] public int spawnCounter = 0;

    [SerializeField] List<GameObject> prefabArrows;
    [SerializeField] List<GameObject> currentArrows;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelayTime = 1.1f;

    [SerializeField] EndGame endGame;
    [SerializeField] TapCollider tapCollider;

    private int arrowCounter = 0;
    private void Awake()
    {
        current = this;
    }
    public IEnumerator ArrowSpawn()
    {
        while (spawnCounter < amountArrow)
        {
            GameObject newArrow = Instantiate(prefabArrows[arrowCounter]);
            newArrow.transform.position = spawnPoint.position;
            newArrow.transform.SetParent(transform);

            currentArrows.Add(newArrow);
            yield return new WaitForSeconds(spawnDelayTime);
            spawnCounter++;

            //to kept 0-4
            arrowCounter++;
            if (arrowCounter == prefabArrows.Count)
            {
                arrowCounter = 0;
            }            
        }
        yield return new WaitForSeconds(spawnDelayTime);
        GameEvents.current.OnEndGame += endGame.CheckIfTapGameEnded;
        GameEvents.current.onTapGame -= tapCollider.CheckWrongTapAttempt;
        
    }
    
}
