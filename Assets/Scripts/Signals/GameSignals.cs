using System;
using System.Collections.Generic;
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
        public UnityAction<GameObject, List<GameObject>,List<GameObject>> OnAssignSelectBall = delegate {  }; 
        public Func<GameObject> OnGetBall = () => null; 
        public UnityAction OnIncreaseCompletedTubes = delegate {  };
        public Func<int> OnGetLevelID = () => 0;
        public Func<bool> OnGetIsLevelFinished = () => false;
        public UnityAction OnNextLevel = delegate {  };
        public UnityAction OnRestartLevel = delegate {  };
        public Func<List<GameObject>> OnGetPreviousTubeList = () => null;
        public Func<List<GameObject>> OnGetPreviousBallPlaces = () => null;
        
    }
}