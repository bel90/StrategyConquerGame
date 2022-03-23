using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public LineRenderer line;
    City startCity;
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                City tmpCity = hit.collider.GetComponent<City>();

                if (tmpCity != null) {
                    startCity = tmpCity;

                    line.gameObject.SetActive(true);
                    line.SetPosition(0, startCity.gameObject.transform.position);
                    line.SetPosition(1, startCity.gameObject.transform.position);
                } 
			}
		}

        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                City tmpCity = hit.collider.GetComponent<City>();

                if (tmpCity != null) {
                    startCity.AttackCity(tmpCity);
                }
                startCity = null;
                line.gameObject.SetActive(false);
            }
        }

        //draw line
        if (startCity != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                City tmpCity = hit.collider.GetComponent<City>();
                if (tmpCity != null) {
                    line.SetPosition(1, hit.transform.position);
                } else {
                    line.SetPosition(1, hit.point);
                }
            }
        }
    }
}
