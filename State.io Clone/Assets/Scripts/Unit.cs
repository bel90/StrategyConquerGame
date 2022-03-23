using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    City goalCity;
    PlayerEnum unitFromPlayer;

    float movementSpeed = 1.5f;

    public void SetupUnit(City goal, PlayerEnum player) {
        goalCity = goal;
        unitFromPlayer = player;
        gameObject.GetComponent<Renderer>().material.color = StaticGameData.playerColors[(int)player];

    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, goalCity.transform.position, movementSpeed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other) {
        City tmpCity = other.gameObject.GetComponent<City>();
        if (tmpCity == goalCity) {
            goalCity.GetAttacked(unitFromPlayer);
            Destroy(gameObject);
        }
    }
}
