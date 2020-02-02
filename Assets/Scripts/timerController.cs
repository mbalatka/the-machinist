﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class timerController : MonoBehaviour
{
	public float timer;
	public Text timerText;
    public GameObject victoryMenu;
    public GameController gameController;
    private Dictionary<int, GameObject> incidents;

    public LevelInterface levelConfig;

    void Start() {
        Time.timeScale = 1f;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        this.levelConfig = this.GetComponent<LevelInterface>();
        this.timer = levelConfig.getLevelTime();
        this.incidents = levelConfig.getIncidents();
    }

    void Update() {
        if(timer <= 0.0f && !victoryMenu.activeSelf){
            timer = 0.0f;
            Time.timeScale = 0f;
            gameController.UpdateBestUnlockStage();
            victoryMenu.SetActive(true);
        } else {
            timer -= Time.deltaTime;
            int second = Mathf.FloorToInt(timer);
            if(incidents.ContainsKey(second)) {
                GameObject machine = incidents[second];
                machine.GetComponent<Machine>().stop();
                incidents.Remove(second);
            }
        }

        timerText.text = "Time: " + String.Format("{0:#,###.#}", timer);

    }
}
