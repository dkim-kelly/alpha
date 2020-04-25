﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smudge : MonoBehaviour
{
    public enum SmudgeType
    {
        SmudgeJ,
        SmudgeK,
        SmudgeL,
        SmudgeNone,
    }

    public SmudgeType type;
    public Color normalColorOff;
    public Color normalColorOn;
    public Color neutralizedColorOff;
    public Color neutralizedColorOn;
    public float percentNeutralized = 0; // 0 means not neutralized, 100 means neutralized, but it's a gradient
    public bool neutralized = false;
    public bool selected = false;

    private new SpriteRenderer renderer;

    private float startScale = 0.7f;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        string letter;
        switch (type)
        {
            case SmudgeType.SmudgeJ:
                letter = "J";
                break;
            case SmudgeType.SmudgeK:
                letter = "K";
                break;
            case SmudgeType.SmudgeL:
                letter = "L";
                break;
            default:
                letter = "None";
                break;
        }

        string filename = "Sprites/Smudge" + letter + "/Sprite" + letter + "" + PersistentManagerScript.Instance.levelIndex;

        print(filename);
        renderer.sprite = Resources.Load<Sprite>(filename);

        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    void Update()
    {
        // green smudges grow slowly
        if (type == SmudgeType.SmudgeL && !neutralized && FloorManager.currentFloor.smudgeManager.allSmudges.Contains(this))
        {
            percentNeutralized -= 10f * Time.deltaTime;
        }


        // change size of smudge based on percentNeutralized
        float scalePercent = 100 - percentNeutralized;
        if (scalePercent <= 40) scalePercent = 50;
        transform.localScale = new Vector3(startScale * scalePercent / 100, startScale * scalePercent / 100, startScale * scalePercent / 100);
    }

    public void Select()
    {
        selected = true;
        renderer.color = neutralized ? neutralizedColorOn : normalColorOn;
    }

    public void Deselect()
    {
        selected = false;
        renderer.color = neutralized ? neutralizedColorOff : normalColorOff;
    }

    public void Neutralize(int percent = 100)
    {
        percentNeutralized += percent;
        if (percentNeutralized >= 100f)
        {
            percentNeutralized = 100f;
            neutralized = true;
            renderer.color = selected ? neutralizedColorOn : neutralizedColorOff;
        }
    }

    public void Clean()
    {
        Destroy(gameObject);
    }
}