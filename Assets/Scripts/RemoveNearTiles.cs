using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNearTiles : MonoBehaviour
{
    [SerializeField] private float radius = 50;
    [SerializeField] private float waitingTime = 5;
    [SerializeField] private Transform tilesParent;
    [SerializeField] private Transform removePivot;

    [SerializeField] private float loopTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(removeTiles());
    }


    private IEnumerator removeTiles()
    {
        yield return new WaitForSeconds(waitingTime);

        while (true)
        {
            yield return new WaitForSeconds(loopTime);
            int tilesNumber = tilesParent.transform.childCount;
            for (int i = 0; i < tilesNumber; i++)
            {
                Transform child = tilesParent.GetChild(i);
                if (!child.gameObject.activeSelf)
                {
                    continue;
                }

                if (Vector3.Distance(removePivot.position , child.position) < radius)
                {
                    tilesParent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}