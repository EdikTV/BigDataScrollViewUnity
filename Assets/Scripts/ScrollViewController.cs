using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enums;
using LitJson;
using PesonData;
using Resources.JsonData;
using Services.Abstracts;
using Services.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject content;
    [SerializeField] private int showCount;
    [SerializeField] private int objectsLimit;

    [Inject] private IInfoService _infoService;
    
    private float currentScrollbarValue;
    private int currentIndex;
    private bool isGoingDown;
    
    private List<GameObject> spawnedObjects;
    
    void Start()
    {
        spawnedObjects = new List<GameObject>();
        
        scrollRect.onValueChanged.AddListener(OnScrollbarChanged);

        _infoService.ConfigureData(JsonForAndroid.Json);
        
        currentIndex += showCount;
        SpawnUserInfos(showCount, currentIndex);
    }
    
    public void OnScrollbarChanged(Vector2 vector)
    {
        var newValue = vector.y;

        isGoingDown = !(currentScrollbarValue > newValue);

        currentScrollbarValue = newValue;
    }

    // Вызывается в событии Begin Drag в объекте People Scroll View
    public void Spawn()
    {
        if (isGoingDown)
        {
            if (currentIndex - showCount > 0)
            {
                currentIndex -= showCount;
            }
        }
        else
        {
            currentIndex += showCount;
        }
        
        Debug.Log(currentIndex);
        
        SpawnUserInfos(showCount, currentIndex);
    }
    
    private void SpawnUserInfos(int take, int skip)
    {
        if (spawnedObjects.Count() > objectsLimit)
        {
            foreach (var spawnedObject in spawnedObjects)
            {
                Destroy(spawnedObject);
            }
            
            spawnedObjects.Clear();
        }
        
        var userInfos = _infoService.GetPagedUserInfo(new PagginationModel
        {
            TakeCount = take,
            SkipCount = skip
        });

        foreach (var userInfo in userInfos)
        {
            var infoUi = Instantiate((GameObject)UnityEngine.Resources.Load(nameof(Prefabs.PersonData)));
                infoUi.transform.SetParent(content.transform);
                infoUi.transform.position = content.transform.position;
                infoUi.GetComponent<PersonData>().SetName(userInfo.Name);
                
            spawnedObjects.Add(infoUi);
        }
    }

    private void OnDisable()
    {
        scrollRect.onValueChanged.RemoveListener(OnScrollbarChanged);
        
    }
}
