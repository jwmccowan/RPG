﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LayoutAnchor))]
public class Panel : MonoBehaviour
{
    // Used because we cannot serialize a Dictionary in Unity :sigh:
    [SerializeField] List<Position> positionList;
    public Position CurrentPosition { get; private set; }
    public Tweener Transition { get; private set; }
    public bool InTransition { get { return Transition != null; } }

    public Position this[string name]
    {
        get
        {
            if (positionMap.ContainsKey(name))
            {
                return positionMap[name];
            }
            return null;
        }
    }

    Dictionary<string, Position> positionMap;
    LayoutAnchor anchor;

    void Awake()
    {
        anchor = GetComponent<LayoutAnchor>();
        positionMap = new Dictionary<string, Position>(positionList.Count);
        for (int i = 0; i < positionList.Count; i++)
        {
            AddPosition(positionList[i]);
        }
    }

    void Start()
    {
        if (CurrentPosition == null && positionList.Count > 0)
        {
            SetPosition(positionList[0], false);
        }
    }

    public Tweener SetPosition(string positionName, bool animated)
    {
        return SetPosition(this[positionName], animated);
    }

    public Tweener SetPosition(Position p, bool animated)
    {
        CurrentPosition = p;
        if (CurrentPosition == null)
        {
            return null;
        }

        if (InTransition)
        {
            Transition.Stop();
        }

        if (animated)
        {
            Transition = anchor.MoveToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return Transition;
        }
        else
        {
            anchor.SnapToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return null;
        }
    }

    public void AddPosition(Position p)
    {
        positionMap[p.name] = p;
    }

    public void RemovePosition(Position p)
    {
        if (positionMap.ContainsKey(p.name))
        {
            positionMap.Remove(p.name);
        }
    }

    [Serializable]
    public class Position
    {
        public string name;
        public TextAnchor myAnchor;
        public TextAnchor parentAnchor;
        public Vector2 offset;

        public Position(string name)
        {
            this.name = name;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor) : this(name)
        {
            this.myAnchor = myAnchor;
            this.parentAnchor = parentAnchor;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name, myAnchor, parentAnchor)
        {
            this.offset = offset;
        }
    }
}
