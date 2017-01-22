using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public GameObject attackText;
    public GameObject speedText;
    public GameObject healthText;
    public GameObject goldText;

    int attack_Power;
    int speed;
    int health;
    int gold;


    // Use this for initialization
    void Start ()
    {

        attack_Power = 5;
        speed = 20;
        health = 100;
        gold = 50;
        //  attackText.GetComponent<Text>().text = "Attack Point: " + attack_Power;
        //speedText.GetComponent<Text>().text = "Speed Point: " + speed;
        //healthText.GetComponent<Text>().text = "Health Point: " + health;
        //goldText.GetComponent<Text>().text = "Total Gold: " + gold;

    }
    public void AddAttackPoint()
    {
        print("test");
        //if (gold <= 0)
        //{
        //    return;
        //}
        //else
        //{
        attack_Power += 2;
        gold -= 15;
        //   }

    }
    // Update is called once per frame
    void Update ()
    {
        Debug.Log(attack_Power);
        attackText.GetComponent<Text>().text = "Attack Point: " + attack_Power;
        //speedText.GetComponent<Text>().text = "Speed Point: " + speed;
        //healthText.GetComponent<Text>().text = "Health Point: " + health;
        //goldText.GetComponent<Text>().text = "Total Gold: " + gold;

    }
}
