using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour {

    List<City> allCities = new List<City>();

    void Start() {
		//get all cities. Cities might be randomly generated in the future
		for (int i = 0; i < transform.childCount; i++) {
			allCities.Add(transform.GetChild(i).GetComponent<City>());
		}

		//set one city randomly as player city and one city as enemy city
		int index1 = Random.Range(0, allCities.Count);
		int index2 = Random.Range(0, allCities.Count);
		//make sure index1 != index2 and in bounds
		index2 = index1 == index2 ? (index2 > 0 ? index2 - 1 : index2 + 1) : index2;

		allCities[index1].SetupCity(PlayerEnum.Player1);
		allCities[index2].SetupCity(PlayerEnum.Player2);
	}

}
