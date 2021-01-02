using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private int xOffset;
    
    private Queue<GameObject> pointQueue;
    private int currentPositionX;
    private bool isStart;

    public void OnStartButtonClick()
    {
        isStart = true;
    }

    public void OnStopButtonClick()
    {
        isStart = false;
    }

    void Start()
    {
        isStart = false;        
        pointQueue = new Queue<GameObject>();
        StartCoroutine(UpdateGraph());
    }
    
    private IEnumerator UpdateGraph()
    {
        while(true)
        {            
            if (pointQueue.Count >= 20)
            {
                GameObject point = CreatePoint(true);
                pointQueue.Enqueue(point);

                //이때는 큐에서 하나 빼고 전체를 한칸 뒤로 옮겨야 함
                GameObject deletePoint = pointQueue.Dequeue();
                Destroy(deletePoint);
                
                foreach (var item in pointQueue)
                {
                    GoBackPoint(item);
                }
            }
            else
            {
                GameObject point = CreatePoint();
                pointQueue.Enqueue(point);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private GameObject CreatePoint(bool endOfGraphX = false)
    {
        GameObject point = Instantiate(pointPrefab);
        point.name = $"Point : {pointQueue.Count}";
        point.transform.SetParent(transform);
        point.transform.position = GetRandomPosition();
        if(!endOfGraphX)
        {
            currentPositionX += xOffset;
        }
            
        return point;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 pos;
        pos.x = currentPositionX;

        if(isStart)
        {
            pos.y = Random.Range(-10.0f, 10.0f);
        }
        else
        {
            pos.y = 0;
        }

        pos.z = 0;

        return pos;
    }

    private void GoBackPoint(GameObject point)
    {
        Vector3 pos = point.transform.position;
        pos.x -= xOffset;
        point.transform.position = pos;
    }
}
