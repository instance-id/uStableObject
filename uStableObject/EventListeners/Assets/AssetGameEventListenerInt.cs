﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using uStableObject.Utilities;

namespace                                       uStableObject
{
    [CreateAssetMenu(menuName = "uStableObject/AssetListener/Int")]
    public class                                AssetGameEventListenerInt : AssetGameEventListenerBase<int>
    {
        public GameEventInt                     _event;
        public UnityEventTypes.Int              _response;

        public override GameEventBase<int>      Event { get { return (this._event); } }
        public override UnityEvent<int>         Response { get { return (this._response); } }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/Create/uStableObject/AssetListener/As Child - Int")]
        public static void                      AddTypeAsChild()
        {
            ScriptableUtils.AddAsChild<AssetGameEventListenerInt>("EventListener - ");
        }

        [UnityEditor.MenuItem("Assets/Create/uStableObject/AssetListener/As Child - Int", true)]
        public static bool                      AddTypeAsChildValidation()
        {
            return (AddAsChildValidation());
        }

        [ContextMenu("Match event name")]
        public void                             MatchEventName()
        {
            base.MatchPrefixAndEventName();
        }
#endif
    }
}
