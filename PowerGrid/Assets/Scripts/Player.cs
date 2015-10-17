// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class Player  : MonoBehaviour, IComparable
{
	public int cash = 50;
	private int starterCash = 50;
	[HideInInspector]
	public ArrayList powerPlants = new ArrayList();
	[HideInInspector]
	public ArrayList cities = new ArrayList();
	public ArrayList cityPurchases = new ArrayList();

	public Color color;

	public GameObject scorePiece;
	public TextMesh scoreText;
	public TextMesh cashText;
	
	public struct CityPurchase {
		public City city;
		public int purchaseCost;
		public GameObject obj;
		public bool isTentative;
		public void Commit() {
			isTentative = false;
		}
	}

	void Start () {
		starterCash = cash;
		scorePiece.GetComponent<Renderer> ().material.color = color;
		Reset ();
	}
	public int GetOrderScore() {
		int maxPowerPlantValue = 0;
		foreach (PowerPlant pp in powerPlants) {
			if(maxPowerPlantValue > pp.baseCost)
				maxPowerPlantValue = pp.baseCost;
		}

		return 1000000 * cities.Count + 1000*maxPowerPlantValue + cash;
	}

	public int CompareTo( object p) {
		return GetOrderScore().CompareTo (((Player)p).GetOrderScore());
	}

	public int GetCityCount() {
		return cities.Count;
	}

	public void Reset() {
		powerPlants.Clear ();
		cash = starterCash;
		cities.Clear ();
		foreach (CityPurchase purchase in cityPurchases) {
			DestroyImmediate (purchase.obj);
		}
		cityPurchases.Clear ();
	}

	public void Update() {
		scoreText.text = cities.Count.ToString();
		cashText.text = "$" + cash.ToString();

		foreach (CityPurchase purchase in cityPurchases) {

			Vector3 pos = purchase.obj.transform.position;
			Quaternion rot = purchase.obj.transform.rotation;
			if(purchase.isTentative) {
				pos.y = 0.0f + 0.1f*(1.0f + Mathf.Sin (5*UnityEngine.Time.realtimeSinceStartup));
				rot = Quaternion.Euler (0, 120 * UnityEngine.Time.realtimeSinceStartup, 0);
			} else {
				pos.y = 0.0f;
			}
//			purchase.obj.transform.position = pos;
//			purchase.obj.transform.rotation = rot;
		}

	}

	public void CommitPurchases() {
		print ("commiting purchases");
		foreach (CityPurchase cp in cityPurchases)
			cp.Commit ();
	}

	public override string ToString() {
		string info = name + " cash:" + cash + " [";

		foreach (PowerPlant pp in powerPlants)
			info += pp.ToString ();
		info += "] " + cities.Count;
		return info;
	}
}

