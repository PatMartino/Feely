using System;
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
    }
}