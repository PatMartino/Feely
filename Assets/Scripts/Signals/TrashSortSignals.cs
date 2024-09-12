using System;
using Enums;
using Extensions;
using Games.TrashSort;
using UnityEngine.Events;

namespace Signals
{
    public class TrashSortSignals : MonoSingleton<TrashSortSignals>
    {
        public Func<int> OnGetLevelID = () => 0;
        public UnityAction OnTrashGeneration = delegate {  };
        public UnityAction OnRemoveTrash = delegate {  };
        public UnityAction<ButtonType> OnThrowTrashToBin = delegate {  };
        public UnityAction OnSwapBinsUI = delegate {  };
        public UnityAction OnAssignBins = delegate {  };
        public UnityAction OnPauseGame = delegate {  };
        public UnityAction OnContinueGame = delegate {  };
        public Func<TrashSortGameStates> OnGetGameState = () => TrashSortGameStates.Play;
        public UnityAction OnIncreaseScore = delegate {  };
        public UnityAction OnDecreaseScore = delegate {  };
        public Func<int> OnGetScore = () => 0;
        public UnityAction OnUpdateScore = delegate {  };
        public UnityAction<float> OnStartTimer = delegate {  };
        public UnityAction OnStopTimer = delegate {  };
        public UnityAction OnContinuousTimer = delegate {  };
        public Func<float> OnGetTimeRemaining = () => 0;
        public UnityAction OnUpdateTime = delegate {  };
        public UnityAction OnLevelFinish = delegate {  };
        public Func<LevelStatus> OnGetLevelStatus = () => LevelStatus.Complete;
        public Func<int> OnGetAccurateAmount = () => 0;
        public Func<int> OnGetWrongAmount = () => 0;
        public Func<int> OnGetAccuracy = () => 0;
        public UnityAction OnGameUI = delegate {  };
        public UnityAction OnEndGameUI = delegate {  };
        public UnityAction OnResetScore = delegate {  };
        public UnityAction OnNextLevel = delegate {  };
        public UnityAction OnPlayAgain = delegate {  };
        public UnityAction OnActivateBins = delegate {  };
        public UnityAction OnStartLevel = delegate {  };
    }
}