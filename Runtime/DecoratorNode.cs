using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    [System.Serializable]
    public abstract class DecoratorNode : Node {

        [SerializeReference]
        [HideInInspector] 
        public Node child;

        protected virtual override void OnStop()
        {
            child?.Stop();
        }
    }
}
