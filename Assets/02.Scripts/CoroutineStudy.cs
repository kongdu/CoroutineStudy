using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//프로그램의 흐름을 중단하고, 유니티에 제어권을 돌려주고 다음에 다시 중지한곳부터 실행을 계속할 수 있는 기능
//특정 작업을 단계적으로 실행하거나, 시간이 흐름에 따라 발생하는 코드를 작성하거나, 다른 연산이 완료될 때까지 기다리는 등의 코드를 작성할 수 있음

public class CoroutineStudy : MonoBehaviour
{
    public float duration = 5.0f;
    public Vector3 beginPos;
    public Vector3 endPos;
    public Vector3 distance;

    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    void Awake()
    {
        distance = endPos - beginPos;
        //StartCoroutine(ShowTime());
        //StartCoroutine(AnimalSound());
        StartCoroutine(CountDown(duration));
    }

    //카운트다운기능
    IEnumerator CountDown(float duration)
    {
        var beginTime = Time.time;
        var percent = 0f;
        for (; ; )
        {

            percent = (Time.time - beginTime)/duration;
            if (percent > 1f)

            {
                break;
            }
                //Debug.Log(time);
                //transform.position = beginPos + distance * percent;
                transform.position = beginPos + distance * curve.Evaluate(percent);
             
            //} else
            //{
            //    Debug.Log("0.00");
            //    break;
            //}

            yield return null;
        }
        transform.position = endPos;
        Debug.Log("시작!!!");
    }

    IEnumerator ShowTime()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));

    }


    IEnumerator AnimalSound()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("야옹");
        yield return new WaitForSeconds(3f);
        Debug.Log("어흥");
        for (; ; )
        {
            yield return new WaitForSeconds(.5f);
            Debug.Log("yap");
        }
    }

}
