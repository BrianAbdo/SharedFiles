using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
public class Date: MonoBehaviour
	{
	public int year;
	public int month;
	public int day;
	int count;
	string date;
    //EventManager manger = new EventManager();
		public Date ()
		{
		day = 1;
		count = 0;
		year = 0;
		month = 1;
		}
    //public EventManager getEventManager()
    //{
    //    return manger;
    //}
	public void setDate(){
		date = day.ToString () + "." + month.ToString () + "." + year.ToString ();

	}
    public void loadDate(int d, int m, int y)
    {
        if (d == 0)
        {
            d = 1;
        }
        day = d;
        month = m;
        year = y;
        Debug.Log(day + "" + month + "" + year);
    }
	public string getDate(){
		return date;

	}
	public string getCompletionDate(int i){
		int newdays = day;
		int newmonts = month;
		int newyears = year;
		for (int xx = 0; xx < i; xx++) {
			newdays += 1;
			if (newdays == 26) {
				newmonts += 1;
				newdays = 1;
				if (newmonts == 11) {
					newyears += 1;
					newmonts = 1;
				}
		}
	}
		string newDate = newdays.ToString () + "." + newmonts.ToString () + "." + newyears.ToString ();
		return newDate;
	}
	public void Update(){
        if (!GameObject.Find("Galaxy").GetComponent<CustomBuild>().paused)
        {

            int maroon = 40 / (GameObject.Find("Galaxy").GetComponent<CustomBuild>().getConstraint());

            count += 1;

            if (count >= maroon)
            {
                day += 1;
                //manger.pickEvent();

                for (int i = 0; i < GameObject.Find("Galaxy").GetComponent<CustomBuild>().players.Count; i++)
                {
                    GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[i].Update();
                }
                for (int i = 0; i < GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar.Count; i++)
                {
                    GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].Update();
                    for (int j = 0; j < GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet.Count; j++)
                    {
                        //Debug.Log(GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet[j].getName());
                        if (GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet[j].getTerraformed())
                        {
                            GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet[j].getGameObj().GetComponent<Defence>().updateDefence();

                        }
                        if (GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet[j].getTerraformingNeeed())
                        {
                            GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet[j].incrementTerraformingDays();
                        }
                        Planet planet = GameObject.Find("Galaxy").GetComponent<CustomBuild>().solar[i].planet[j];
                        foreach (InstantiateConstruction.CreateConstruction c in planet.underConstruction)
                        {
                            c.buildDays--;
                            if (c.buildDays <= 0)
                            {
                                planet.construction.Add(c);

                                if (c.id == 1)
                                {
                                    planet.inf.setMilLevel(10);
                                }

                                if (c.id == 21)
                                {
                                    planet.inf.setEcoLevel(3);
                                }

                                if (c.id == 22)
                                {
                                    planet.ariable += Random.Range(Construction.getConstruction(22).attrValue, Construction.getConstruction(22).attrValue2);
                                }

                                if (c.id == 46)
                                {
                                    planet.getEconomy().GPP += (planet.getEconomy().GPP * 0.01f);
                                }

                                if (c.id == 23)
                                {
                                    float chance = Random.Range(0f, 1f);

                                    if (chance < 60)
                                        planet.setDrillSite(true);
                                }

                                if (c.id == 24)
                                {
                                    GameObject.Find("TopMenu").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(18).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                                    GameObject.Find("TopMenu").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(18).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                                }

                                if (c.id == 9)
                                {
                                    planet.NavalYardConstruct();
                                }
                            }
                        }
                        foreach (InstantiateConstruction.CreateConstruction c in planet.construction)
                        {
                            planet.checkUnderConstruction(c.id);
                        }


                        if (planet.getConstruction(9))
                        {
                            if (planet.getEmpire().underConstruction.Count > 0)
                            {
                                List<int> ints = new List<int>();

                                foreach (InstantiateFleet.CreateFleet c in Fleet.construction)
                                {
                                    InstantiateEmpireFleet.UnderConstruction f = planet.getEmpire().emprieFleet.getFirstOfType(c.id, planet.getEmpire());
                                    if (f != null)
                                    {
                                        f.days--;
                                        if (f.days == 0)
                                        {
                                            planet.getEmpire().emprieFleet.BuildShip(f, planet.getEmpire());
                                            ints.Add(f.id);
                                        }

                                    }
    
                                    
                                }

                                foreach (int f in ints)
                                {
                                    planet.getEmpire().emprieFleet.checkUnderConstructionDays(f, planet.getEmpire());

                                }
                                ints.Clear();
                            }
                 
                        }
                    }
                }
                if (day == 26)
                {


                    for (int i = 0; i < GameObject.Find("Galaxy").GetComponent<CustomBuild>().players.Count; i++)
                    {
                        GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[i].getEmpire().Update();
                    }


                    //double corruptionholder = GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getCorruption();
                    //double educationholder = GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getEducation();

                    month += 1;


                    //GameObject two = new GameObject();
                    //Text corruption = two.AddComponent<Text>();
                    //corruption.text = "Monthly Corruption Change: " + (GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getCorruption() - corruptionholder).ToString();
                    //corruption.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                    //corruption.fontSize = 10;
                    //manger.internalList.Add(corruption);

                    //GameObject three = new GameObject();
                    //Text educ = three.AddComponent<Text>();
                    //educ.text = "Monthly Corruption Change: " + (GameObject.Find("Galaxy").GetComponent<CustomBuild>().players[0].getEmpire().getEducation() - educationholder).ToString();
                    //educ.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                    //educ.fontSize = 10;
                    //manger.internalList.Add(educ);


                    day = 1;
                    if (month == 11)
                    {
                        year += 1;
                        month = 1;
                        Debug.Log(year);
                        if (PlayerPrefs.GetInt("AutoSave") == 1)
                        {
                            InGameSaveLoad.Save();
                            Debug.Log("SAVING");
                        }
                        if (PlayerPrefs.GetInt("AutoSave") == 2)
                        {
                            if (year % 2 == 0)
                            {
                                InGameSaveLoad.Save();

                                Debug.Log("SAVING");
                            }
                        }
                        if (PlayerPrefs.GetInt("AutoSave") == 3)
                        {
                            if (year % 5 == 0)
                            {
                                InGameSaveLoad.Save();

                                Debug.Log("SAVING");
                            }
                        }
                        if (PlayerPrefs.GetInt("AutoSave") == 4)
                        {
                            if (year % 10 == 0)
                            {
                                InGameSaveLoad.Save();

                                Debug.Log("SAVING");
                            }
                        }

                    }

                }
                count = 0;
            }
            setDate();



        }
    }
      
	}


