using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph : MonoBehaviour
{
    [SerializeField] private int xOffset;
    [SerializeField] private float updateSpeed;

    private LineRenderer line;
    private bool isStart;
    private int currentCount;

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
        currentCount = 1;
        line = GetComponent<LineRenderer>();
        StartCoroutine(UpdateGrpah());
    }
 
    private IEnumerator UpdateGrpah()
    {
        while(true)
        {
            if(line.positionCount >= 20)
            {
                ShiftLine();
                line.SetPosition(line.positionCount - 1, GetRandomPos());
            }
            else
            {
                line.positionCount = currentCount;
                line.SetPosition(currentCount - 1, GetRandomPos());
                currentCount += 1;                
            }

            yield return new WaitForSeconds(updateSpeed);
        }
    }

    private void ShiftLine()
    {
        for(int i = 0; i < line.positionCount - 1; i++)
        {
            Vector3 pos = line.GetPosition(i + 1);
            pos.x -= xOffset;
            line.SetPosition(i, pos);
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector3 pos = Vector3.zero;
        pos.x = currentCount * xOffset;

        if(isStart)
        {
            pos.y = Random.Range(-10.0f, 10.0f);
        }
        else
        {
            pos.y = 0;
        }

        return pos;
    }
}
