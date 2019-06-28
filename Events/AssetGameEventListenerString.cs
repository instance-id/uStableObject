﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using uStableObject.Data;
using uStableObject.Utilities;

namespace                                       uStableObject
{
    [CreateAssetMenu(menuName = "uStableObject/GameEvent/AssetListener/String")]
    public class                                AssetGameEventListenerString : AssetGameEventListenerBase<string>
    {
        public GameEventString                  _event;
        public UnityEventTypes.String           _response;

        public override GameEventBase<string>   Event { get { return (this._event); } }
        public override UnityEvent<string>      Response { get { return (this._response); } }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/Create/uStableObject/GameEvent/AssetListener/As Child - String")]
        public static void                      AddTypeAsChild()
        {
            ScriptableUtils.AddAsChild(typeof(AssetGameEventListenerString), "EventListener - ");
        }

        [UnityEditor.MenuItem("Assets/Create/uStableObject/GameEvent/AssetListener/As Child - String", true)]
        public static bool                      AddTypeAsChildValidation()
        {
            return (AddAsChildValidation());
        }

        [ContextMenu("Match event name")]
        public override void                    MatchEventName()
        {
            base.MatchEventName();
        }
#endif
    }
}
