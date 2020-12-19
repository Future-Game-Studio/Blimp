﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceIsle : UIController
{
    private ResourcesIsle _isle;

    [SerializeField] private GameObject _contentList;
    [SerializeField] private ResourceInfo _resourceinfo;
    private List<ResourceButton> _buttons;
    [SerializeField] private ScrollRect _scroll;

    private int _buttonNum;
    private int _level;
    private int _maxLevel;

    private void Start()
    {
        _buttonNum = 0;
    }

    private void Update()
    {
        if (_isle != null)
            UpdateDynamicInfo();
    }

    public override void UpdateAll()
    {
        _isle = UIManager._instance.LastActiveIsle as ResourcesIsle;
        if (_isle == null)
            Debug.LogError("Isle type error");

        //rebuild scroll

        if (_buttons == null)
            _buttons = _contentList.GetComponentsInChildren<ResourceButton>().ToList();


        UpdateStaticInfo();
        UpdateStaticInfo();
        UpdateResourceInfo();
    }

    private void UpdateStaticInfo()
    {
        _buttons.ForEach(b =>
        {
            b.gameObject.SetActive(true);
            b.interactable = true;
        });

        _level = _isle.Level;
        _maxLevel = _isle.Logic.Info.Length;

        for (int i = _level; i < _maxLevel; i++)
        {
            _buttons[i].interactable = false;
            _buttons[i].ClearCount();
            _buttons[i].ChangeProgress(0);
        }

        for (int i = _maxLevel; i < _buttons.Count; i++)
        {
            _buttons[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < _maxLevel; i++)
        {
            
            _buttons[i].ChangeName(_isle.Logic.Info[i].Item.ItemName);
        }

    }

    //rewrite
    private void UpdateDynamicInfo()
    {
        for (int i = 0; i < _level; i++)
        {
            int count = _isle.Items.Container[i].Amount;
            int maxCount = _isle.Logic.Info[i].MaxAmount;
            _buttons[i].ChangeCount(count, maxCount);
            _buttons[i].ChangeProgress(_isle.RefreshedItems[_isle.Logic.Info[i].Item]);
        }

        //rewrite
        _resourceinfo.ChangeCount(_isle.Items.Container[_buttonNum].Amount, _isle.Logic.Info[_buttonNum].MaxAmount);
    }

    private void UpdateResourceInfo()
    {
        Item item = _isle.Logic.Info[_buttonNum].Item;
        _resourceinfo.ChangeInfo(item.ItemName, item.Description, item.Icon);
    }

}