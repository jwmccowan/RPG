using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneState : BattleState
{
    ConversationController conversationController;
    ConversationData data;

    protected override void Awake()
    {
        base.Awake();
        conversationController = owner.GetComponentInChildren<ConversationController>();
        data = Resources.Load<ConversationData>("Conversations/IntroConversation");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (data)
        {
            Resources.UnloadAsset(data);
        }
    }

    public override void Enter()
    {
        Debug.Log("CutSceneState Enter");
        base.Enter();
        conversationController.Show(data);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        this.AddListener(OnConversationComplete, ConversationController.ConversationComplete);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        this.RemoveListener(OnConversationComplete, ConversationController.ConversationComplete);
    }

    protected override void OnFire(object sender, object e)
    {
        conversationController.Next();
    }

    void OnConversationComplete(object sender, object e)
    {
        owner.ChangeState<SelectUnitState>();
    }
}
