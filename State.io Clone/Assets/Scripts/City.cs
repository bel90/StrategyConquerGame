using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour {

    private PlayerEnum ownedBy = PlayerEnum.Neutral;
    public TMP_Text unitsNumber;
    public GameObject unitPrefab;
    private Renderer thisRenderer;

    int maxUnits = 50;
    int currentUnits = 10;
    float newUnitInSecs = .5f;

    //only recruit if you didn't get attacked and you didn't attack for a while
    DateTime lastAttack = DateTime.Now.AddDays(-1);
    DateTime lastSending = DateTime.Now.AddDays(-1);
    float timeUntilRecruit = 3f;

    // Start is called before the first frame update
    void Start() {
        thisRenderer = gameObject.GetComponent<Renderer>();
        SetTextAndColor();
        StartCoroutine(RecruitUnits());
    }

    public void SetupCity(PlayerEnum player, int _maxUnits = 50, int _currentUnits = 10) {
        ownedBy = player;
        maxUnits = _maxUnits;
        currentUnits = _currentUnits;

        SetTextAndColor();
    }

    private void SetTextAndColor() {
        unitsNumber.text = $"{currentUnits}";
        if (thisRenderer == null) thisRenderer = gameObject.GetComponent<Renderer>();
        thisRenderer.material.color = StaticGameData.playerColors[(int)ownedBy];
    }

    public IEnumerator RecruitUnits() {

        yield return new WaitForSeconds(newUnitInSecs);
        DateTime timeThatNeedsToBePassed = DateTime.Now.AddSeconds(-timeUntilRecruit);

        if (ownedBy != PlayerEnum.Neutral) {
            if (lastAttack.CompareTo(timeThatNeedsToBePassed) < 0 && lastSending.CompareTo(timeThatNeedsToBePassed) < 0) {
                if (currentUnits < maxUnits) {
                    currentUnits++;
                    SetTextAndColor();
                }
            }
        }

        StartCoroutine(RecruitUnits());
	}

    public void GetAttacked(PlayerEnum attacker) {
        lastAttack = DateTime.Now;

        if (ownedBy != PlayerEnum.Neutral && attacker != ownedBy) {
            currentUnits -= 1;
            if (currentUnits <= 0) {
                ownedBy = PlayerEnum.Neutral;
			}
		} else if (ownedBy == PlayerEnum.Neutral) {
            currentUnits -= 1;
            if (currentUnits <= 0) {
                ownedBy = attacker;
            }
		} else if (attacker == ownedBy) {
            currentUnits++;
		}

        SetTextAndColor();

    }

    public void AttackCity(City goalCity) {
        Debug.Log($"Attack!");
        lastSending = DateTime.Now;

        StartCoroutine(SendAllUnits(goalCity));
	}

    private IEnumerator SendAllUnits(City goalCity) {
        while (currentUnits > 0 && ownedBy != PlayerEnum.Neutral) {
            currentUnits--;
            SetTextAndColor();
            GameObject newUnit = Instantiate(unitPrefab, transform.position, Quaternion.identity);
            newUnit.GetComponent<Unit>().SetupUnit(goalCity, ownedBy);

            yield return new WaitForSeconds(.5f);
            lastSending = DateTime.Now;
        }
    }
}
