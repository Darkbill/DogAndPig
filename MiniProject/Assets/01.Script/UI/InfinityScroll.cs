using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfinityScroll : MonoBehaviour
{
	public ContentItem itemExam;
	public GameObject scrollView;
	public int upPadding;
	public int leftPadding;

	private List<ContentItem> contentList = new List<ContentItem>();
	private List<PlayerSkillData> itemList = new List<PlayerSkillData>();

	int verCount;
	int horCount;
	int showCount;

	private Rect itemSize;
	Rect scrolViewSize;
	Vector3 changePos;
	int headContent;
	int tailContent;
	int firstItem;
	int lastItem;


	private void Start()
	{
		Setting();
	}
	private void Setting()
	{
		itemList = JsonMng.Ins.playerSkillDataTable.ToList();
		itemSize = itemExam.GetComponent<RectTransform>().rect;
		scrolViewSize = scrollView.GetComponent<RectTransform>().rect;
		//스크롤뷰의 크기를 기준으로 화면에 보여줄 content의 갯수를 지정
		horCount = (int)Mathf.Floor(scrolViewSize.width / (itemSize.width + leftPadding));
		verCount = (int)Mathf.Floor(scrolViewSize.height / (itemSize.height + upPadding));
		showCount = horCount * verCount;
		//스크롤뷰 전체 크기 설정
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (itemSize.height + upPadding) * Mathf.CeilToInt(((float)itemList.Count / (float)horCount)));
		float c = 0;
		if (horCount != 0)
		{
			c = leftPadding + itemSize.width;
		}
		changePos = new Vector3(c, itemSize.height + upPadding, 0);
		gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

		if (showCount >= itemList.Count) SetBasicScroll(itemList.Count);
		else SetInfinityScroll(itemList.Count);
		itemExam.gameObject.SetActive(false);
		SetItemList();
	}

	private void Update()
	{
		//TODO : horCount를 한번에 갱신!
		float cPos = gameObject.transform.localPosition.y;
		float topValue = Mathf.Ceil(cPos / (itemSize.size.y + upPadding));
		if (topValue > horCount/firstItem)
		{
			firstItem++;
			lastItem++;
			Vector3 nextStartPos = contentList[tailContent].gameObject.transform.localPosition;
			nextStartPos.x = contentList[headContent].transform.localPosition.x;
			for (int i = 0; i < horCount; ++i)
			{
				contentList[headContent].gameObject.transform.localPosition =
					nextStartPos + new Vector3(changePos.x * i,-changePos.y,0);
				if (lastItem < itemList.Count-1)
				{
					contentList[headContent].Setting(itemList[lastItem].skillID);
				}
				else
				{
					contentList[headContent].gameObject.SetActive(false);
				}
				tailContent = headContent;
				headContent++;
			}
		}
		//else if (topValue < Mathf.FloorToInt((float)firstItem / (float)horCount))
		//{
		//	Debug.Log(Mathf.FloorToInt((float)horCount / (float)firstItem));
		//	if (firstItem == 0) return;
		//	firstItem--;
		//	lastItem--;
		//	Vector3 nextStartPos = contentList[headContent].gameObject.transform.localPosition;
		//	nextStartPos.x = contentList[headContent].transform.localPosition.x;
		//	//int saveHead = headContent;
		//	int saveTail = tailContent;
		//	tailContent -= horCount - 1;
		//	//lastItem--;
		//	for (int i = 0; i < horCount; ++i)
		//	{
		//		headContent--;
		//		contentList[tailContent].gameObject.transform.localPosition =
		//			nextStartPos + new Vector3(changePos.x * i, +changePos.y, 0);

		//		if (lastItem >= 0)
		//		{
		//			contentList[tailContent].Setting(itemList[firstItem].skillID);
		//		}
		//		else
		//		{
		//			contentList[tailContent].gameObject.SetActive(false);
		//		}

		//		tailContent++;
				
		//	}
		//	tailContent = saveTail - horCount;
		//	if (tailContent == -1) tailContent = contentList.Count-1;
		//}
	}

	private void SetBasicScroll(int itemCount)
	{
		for (int i = 0; i < verCount; ++i)
		{
			for (int j = 0; j < horCount; ++j)
			{
				GameObject o = Instantiate(itemExam.gameObject, gameObject.transform);
				contentList.Add(o.GetComponent<ContentItem>());
				Vector2 pos = new Vector2(-(scrolViewSize.width / 2) + itemSize.width * j + (leftPadding * (j + 1)) + (itemSize.width / 2),-itemSize.height * i - (upPadding * (i + 1)) - (itemSize.height / 2));
				o.GetComponent<RectTransform>().localPosition = pos;
				itemCount--;
				if (itemCount == 0) return;
			}
		}
	}
	private void SetInfinityScroll(int itemCount)
	{
		CreateUpSpace();
		SetBasicScroll(itemCount);
	}

	private void CreateUpSpace()
	{
		//크기에 맞는 content를 미리 생성
		for (int i = 0; i < horCount; ++i)
		{
			GameObject o = Instantiate(itemExam.gameObject, gameObject.transform);
			contentList.Add(o.GetComponent<ContentItem>());
			Rect r = o.GetComponent<RectTransform>().rect;
			Vector2 pos = new Vector2(-(scrolViewSize.width / 2) + r.width * i + (leftPadding * (i + 1)) + (r.width / 2),
									  r.height / 2);
			o.GetComponent<RectTransform>().localPosition = pos;
			o.gameObject.SetActive(false);
		}
	}

	private void SetItemList()
	{
		if (itemList.Count < showCount)
		{
			for (int i = 0; i < itemList.Count; ++i)
			{
				contentList[i].Setting(itemList[i].skillID);
			}
			for (int i = itemList.Count; i < showCount; ++i)
			{
				contentList[i].gameObject.SetActive(false);
			}
		}
		else
		{
			for (int i = horCount; i < contentList.Count; ++i)
			{
				contentList[i].Setting(itemList[i].skillID);
			}
		}
		headContent = 0;
		tailContent = contentList.Count - 1;
		firstItem = 1;
		lastItem = contentList.Count - 1 - horCount;
	}
}