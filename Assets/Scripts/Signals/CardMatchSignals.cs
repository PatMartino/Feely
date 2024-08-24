using System;
using System.Collections.Generic;
using Extensions;
using Games.CardMatch;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CardMatchSignals : MonoSingleton<CardMatchSignals>
    {
        public Func<List<CardTypes>> OnGetLevelCards = () => null;
        public UnityAction OnInstantiateCards = delegate {  };
        public UnityAction OnSetIsSelectTrue = delegate {  };
        public UnityAction OnSetIsSelectFalse = delegate {  };
        public Func<bool> OnGetISelect = () => false;
        public UnityAction<CardTypes> OnSetSelectedCardType = delegate {  };
        public Func<CardTypes> OnGetSelectedCardType = () => CardTypes.Red;
        public UnityAction<GameObject> OnSelectCard = delegate {  };
        public UnityAction OnWrongSelection = delegate {  };
        public UnityAction OnTrueSelection = delegate {  };
        public UnityAction OnIncreaseMatchAmount = delegate {  };
        public UnityAction OnSetCanSelectTrue = delegate {  };
        public UnityAction OnSetCanSelectFalse = delegate {  };
        public Func<bool> OnGetCanSelect = () => false;
        public UnityAction OnDestroyAllCards = delegate {  };
    }
}