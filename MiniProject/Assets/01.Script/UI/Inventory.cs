using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct Itemtype
{
    public int ItemCode;
    public string ItemLink;

    public Itemtype(int code, string link)
    {
        ItemCode = code;
        ItemLink = link;
        
    }
}

public class Inventory : MonoBehaviour
{
    List<Itemtype> ItemList = new List<Itemtype>();

    public int SlotSiz;

    private void Awake()
    {
        Itemtype item = new Itemtype(1, "coin");
        ItemList.Add(item);
        item = new Itemtype(2, "example");
        ItemList.Add(item);
        
    }

    // Update is called once per frame
    void Update()
    {
        FindInventoryList();
    }
    void FindInventoryList()
    {
        Transform but = UIMng.Ins.ItemInventory.transform.GetChild(0);

        RectTransform rowRectTransform = but.GetComponent<RectTransform>();
        RectTransform cotainerRectTransform = gameObject.GetComponent<RectTransform>();

        float width = cotainerRectTransform.rect.width;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height;
        float ScroolHeight = height * SlotSiz / 3;

        //cotainerRectTransform.offsetMin = new Vector2(cotainerRectTransform.offsetMin.x, -ScroolHeight / 2);
        //cotainerRectTransform.offsetMax = new Vector2(cotainerRectTransform.offsetMax.x, ScroolHeight / 2);


        for (int i = 0;i< SlotSiz; ++i)
        {
            if (ItemList.Count > i)
            {
                but = UIMng.Ins.ItemInventory.transform.GetChild(i);
                but.GetComponent<Image>().sprite = Resources.Load<Sprite>(ItemList[i].ItemLink);
            }
            else
            {
                but = UIMng.Ins.ItemInventory.transform.GetChild(i);
                but.GetComponent<Image>().sprite = Resources.Load<Sprite>("pig");
            }
        }
    }
}
