using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class UIBuyShip : MonoBehaviour
{
    public GameObject buyShip;
    public GameObject playerMenu;
    public Text shipLevel;
    public GameObject content;
    public GameObject shipish;
    public List<List<GameObject>> shipInfo = new List<List<GameObject>>();
    public GameObject Panel;
    public Text PanelInfo;
    public Text PanelName;
    public void loadTerritory()
    {
        loadShipList();


        buyShip.SetActive(true);

    }
    public void loadShipList()
    {
        if (shipInfo.Count == 0)
        {
            Debug.Log(Fleet.construction.Count);

            for (int i = 0; i < Fleet.construction.Count; i++)
            {
                List<GameObject> shipy = new List<GameObject>();
                GameObject b = Instantiate(Resources.Load("BuyShip") as GameObject);
                b.transform.SetParent(content.transform);
                b.transform.localScale = Vector3.one;
                b.transform.rotation = Quaternion.Euler(Vector3.zero);
                int cal = i;
                UnityEngine.Events.UnityAction act = new UnityEngine.Events.UnityAction(delegate { purchaseIndex(cal); });

                b.GetComponent<Button>().onClick.AddListener(act);
                shipy.Add(b);


                GameObject a = Instantiate(Resources.Load("TerritoryConst") as GameObject);
                a.transform.SetParent(content.transform);
                a.transform.localScale = Vector3.one;
                a.transform.rotation = Quaternion.Euler(Vector3.zero);
                shipy.Add(a);

                a = Instantiate(Resources.Load("TerritoryConst") as GameObject);
                a.transform.SetParent(content.transform);
                a.transform.localScale = Vector3.one;
                a.transform.rotation = Quaternion.Euler(Vector3.zero);
                shipy.Add(a);
                a = Instantiate(Resources.Load("TerritoryConst") as GameObject);
                a.transform.SetParent(content.transform);
                a.transform.localScale = Vector3.one;
                a.transform.rotation = Quaternion.Euler(Vector3.zero);
                shipy.Add(a);
                a = Instantiate(Resources.Load("TerritoryConst") as GameObject);
                a.transform.SetParent(content.transform);
                a.transform.localScale = Vector3.one;
                a.transform.rotation = Quaternion.Euler(Vector3.zero);
                shipy.Add(a);
                a = Instantiate(Resources.Load("TerritoryConst") as GameObject);
                a.transform.SetParent(content.transform);
                a.transform.localScale = Vector3.one;
                a.transform.rotation = Quaternion.Euler(Vector3.zero);
                shipy.Add(a);
                a = Instantiate(Resources.Load("TerritoryConst") as GameObject);
                a.transform.SetParent(content.transform);
                a.transform.localScale = Vector3.one;
                a.transform.rotation = Quaternion.Euler(Vector3.zero);
                EventTrigger.Entry act2 = new EventTrigger.Entry();
                act2.eventID = EventTriggerType.PointerEnter;
                int holder = i;
                GameObject g = a;
                act2.callback.AddListener((PointerEventData) => { showQueue(holder, g); });
                EventTrigger.Entry act3 = new EventTrigger.Entry();
                act3.eventID = EventTriggerType.PointerExit;
                act3.callback.AddListener((PointerEventData) => { hideQueue(); });
                a.AddComponent<EventTrigger>();
                a.GetComponent<EventTrigger>().triggers.Add(act2);
                a.GetComponent<EventTrigger>().triggers.Add(act3);

                shipy.Add(a);
                shipInfo.Add(shipy);

            }

            setBuyingOptions();





        }
        playerMenu.SetActive(false);
        shipish.SetActive(false);
        buyShip.SetActive(true);

    }

    public void showQueue(int i, GameObject g)
    {
        Navy n = GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().getNavy();
        Planet e = GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet();
        if (e.getEmpire().emprieFleet.getUnderConstruction(i + 1, GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire()) > 0)
        {
            PanelName.text = "The expected construction time of a " + GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().fleet.getByID(i + 1).name +
    " is " + GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().fleet.getByID(i + 1).daysToBuild +
    " Days." + " The next " + GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().fleet.getByID(i + 1).name + " will be avaliable in " + e.getEmpire().emprieFleet.getUnderConstructionNext(i + 1, GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire()) + " Days.";

        }
        else
        {
            PanelName.text = "The expected construction time of a " + GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().fleet.getByID(i + 1).name +
                " is " + GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().fleet.getByID(i + 1).daysToBuild +
                " Days.";
        }

        Panel.transform.localPosition = g.transform.localPosition + new Vector3(-500, 150, 0);

        Panel.SetActive(true);
    }
    public void hideQueue()
    {
        Panel.SetActive(false);
    }
    public void purchaseIndex(int i)
    {
        Navy e = GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet().getNavy();

        GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().emprieFleet.BuyShip(i + 1, GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire());

    }
    public void Update()
    {
        setBuyingOptions();
    }
    public void setBuyingOptions()
    {
        Planet e = GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getHomeworldPlanet();
        int milLevel = e.inf.getMilLevel();
        
        for (int i = 0; i < Fleet.construction.Count; i++)
        {
            shipInfo[i][1].GetComponentInChildren<Text>().text = e.fleet.getByID(i + 1).name;
            shipInfo[i][2].GetComponentInChildren<Text>().text = e.fleet.getByID(i + 1).horsepower.ToString();
            shipInfo[i][3].GetComponentInChildren<Text>().text = (e.fleet.getByID(i + 1).firepower /** (milLevel + 1)*/).ToString();
            shipInfo[i][4].GetComponentInChildren<Text>().text = "₡" + NumberParser.parseNumber((e.fleet.getByID(i + 1).maintenance /** (1 + milLevel)*/).ToString());
            shipInfo[i][5].GetComponentInChildren<Text>().text = "₡" + NumberParser.parseNumber((e.fleet.getByID(i + 1).cost/* * (1 + milLevel)*/).ToString());
            shipInfo[i][6].GetComponentInChildren<Text>().text = e.getEmpire().emprieFleet.getUnderConstruction(i + 1, GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire()).ToString();
        }

    }

}

