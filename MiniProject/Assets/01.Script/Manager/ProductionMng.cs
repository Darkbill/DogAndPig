using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GlobalDefine;
using UnityEngine.UI;
using DG.Tweening;


public class ProductionMng : MonoBehaviour
{
    [Range(0, 10)] public float speed;//time
    [Range(0, 10)] public int size; //changesiz
    [Range(0, 10)] public float startSize;

    [Header("InOption")]

    [Range(-DefineClass.WidthSiz, DefineClass.WidthSiz)] public float inStartPositionX;
    [Range(-DefineClass.HeightSiz, DefineClass.HeightSiz)] public float inStartPositionY;
    [Range(-DefineClass.WidthSiz, DefineClass.WidthSiz)] public float inEndPositionX;
    [Range(-DefineClass.HeightSiz, DefineClass.HeightSiz)] public float inEndPositionY;

    [ColorUsage(true, true)] public Color inColor;   

    [Header("OutOption")]

    [Range(-DefineClass.WidthSiz, DefineClass.WidthSiz)] public float outEndPositionX;
    [Range(-DefineClass.HeightSiz, DefineClass.HeightSiz)] public float outEndPositionY;
    [ColorUsage(true, true)] public Color outColor;   


    public void OnProductionUpdate()
    {
        StartSetting();

        SizeUpdate(speed);
        PositionUpdate(new Vector3(inEndPositionX, inEndPositionY), speed);
        ColorUpdate(inColor, speed);
    }

    public void OffProductionUpdate()
    {
        SizeUpdate(speed);
        PositionUpdate(new Vector3(outEndPositionX, outEndPositionY), speed);
        ColorUpdate(outColor, speed);
    }

    public void DoubleProductionUpdate()
    {
        gameObject.transform.DOScale(speed, speed).OnComplete(() => 
        {
            gameObject.transform.DOScale(speed, speed);
        });
        gameObject.transform.DOLocalMove(new Vector3(inEndPositionX, inEndPositionY), speed).OnComplete(() =>
        {
            gameObject.transform.DOLocalMove(new Vector3(outEndPositionX, outEndPositionY), speed);
        });
        gameObject.GetComponent<Image>().DOColor(inColor, speed).OnComplete(() =>
        {
            gameObject.GetComponent<Image>().DOColor(outColor, speed);
        });
    }

    private void StartSetting()
    {
        gameObject.transform.localPosition = new Vector3(inStartPositionX, inStartPositionY);
    }

    private void SizeUpdate(float time)
    {
        gameObject.transform.DOScale(time, time);
    }

    private void PositionUpdate(Vector3 movepos, float time)
    {
        gameObject.transform.DOLocalMove(movepos, time, true);
    }

    private void ColorUpdate(Color col, float time)
    {
        gameObject.GetComponent<Image>().DOColor(col, time);
    }
}
