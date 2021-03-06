﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Mode { Rock, Scissors, Paper };

public enum Result { Win, Lose, Same };

public class RSPGame : MonoBehaviour
{
    public float waitingTme = 5f;
    public Mode MyRsp;
    private Result res;
    public Text countText;
    public Text resultText;

    private void Awake()
    {
        //배열로 찾기
        Text[] texts = GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            if (t.gameObject.name == "CountText")
                countText = t;
            else if (t.gameObject.name == "ResultText")
                resultText = t;
        }

        //리스트로 찾기
        List<Text> list = new List<Text>();
        GetComponentsInChildren<Text>(list);
        countText = list.Find(t => t.gameObject.name == "CountText");
        resultText = list.Find(t => t.gameObject.name == "ResultText");
        //GetComponent<Text>();
        //resultText = GetComponent<Text>();
    }

    private IEnumerator Start()
    {
        //1. 카운트다운 5초
        yield return StartCoroutine(CountDown(waitingTme));
        //1. NPC의 패 선택 (랜덤으로)
        Mode comSrp = GetComSRP();
        //1.5 이와중에 내 패는 직접 Inspector 창에서 선택해야함
        //2. 승패판단
        Result result = PlayGame(comSrp, MyRsp);
        //3. 승패 출력
        showResult(result, comSrp, MyRsp);
        //4. 반복
        //포문 추가
    }

    public Mode GetComSRP()
    {
        Mode ComSelet = (Mode)Random.Range(0, 3); //0이상 3미만 0, 1, 2

        Debug.Log("컴퓨터 : " + ComSelet);
        return ComSelet; //컴퓨터가 낸 패를 리턴
    }

    public Result PlayGame(Mode com, Mode me)
    {
        Debug.Log("*****RSP Game Start*****");
        if (com == me)
        {
            return Result.Same;
        }
        switch (MyRsp)
        {
            case Mode.Rock: //내가 바위
                if (Mode.Paper == com)
                {
                    return Result.Lose;
                }
                else if (Mode.Scissors == com)
                {
                    return Result.Win;
                }
                break;

            case Mode.Scissors: //내가 가위
                if (Mode.Rock == com)
                {
                    return Result.Lose;
                }
                else if (Mode.Paper == com)
                {
                    return Result.Win;
                }
                break;

            case Mode.Paper: //내가 보
                if (Mode.Scissors == com)
                {
                    return Result.Lose;
                }
                else if (Mode.Rock == com)
                {
                    return Result.Win;
                }
                break;
        }
        return Result.Same; //코드가 이쪽으로 빠지면 의미없지만 스위치문 완성위해 넣어줌
    }

    public void showResult(Result result, Mode com, Mode me)
    {
        Debug.Log("당신은 " + result + "");
        Debug.Log("나: " + me + " 컴: " + com);
    }

    private IEnumerator CountDown(float wTime)
    {
        var beginTime = Time.time;
        var time = 0f;

        for (; ; )
        {
            time = waitingTme - (Time.time - beginTime);
            if (time >= 0)
            {
                Debug.Log(time.ToString("0.00"));
                countText.text = time.ToString("0.00");
            }
            else
            {
                Debug.Log("0");
                break;
            }

            yield return null;
        }
        Debug.Log("시작!!");
    }
}