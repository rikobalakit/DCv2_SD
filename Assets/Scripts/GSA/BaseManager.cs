using UnityEngine;

namespace GSA
{
    public abstract class BaseManager : MonoBehaviour
    {
        protected NewGameManager _gameManager;
        
        public virtual void Initialize(NewGameManager gameManager)
        {
            _gameManager = gameManager;
        }
    }
}
