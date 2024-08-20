using System;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class GameSignals : MonoSingleton<GameSignals>
    {
        public Func<bool> OnGetIsSelect = () => false;
        public UnityAction OnSetIsSelectTrue = delegate {  };
        public UnityAction OnSetIsSelectFalse = delegate {  };
        public UnityAction<GameObject> OnAssignSelectBall = delegate {  }; 
        public Func<GameObject> OnGetBall = () => null; 
        public Func<bool> OnGetCanSelect = () => false;
        public UnityAction OnSetCanSelectTrue = delegate {  };
        public UnityAction OnSetCanSelectFalse = delegate {  };
        public UnityAction OnIncreaseCompletedTubes = delegate {  };
        public Func<int> OnGetLevelID = () => 0;
        public Func<bool> OnGetIsLevelFinished = () => false;
        public UnityAction OnNextLevel = delegate {  };
        public UnityAction OnRestartLevel = delegate {  };
    }
}