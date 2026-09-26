using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    
    [System.Serializable]
    public class Selector : CompositeNode
    {
        private int _activeChildIndex = -1;

        protected override void OnStart() { }

        protected override void OnStop()
        {
            _activeChildIndex = -1;
            foreach (var child in children)
            {
                child.Stop();
            }
        }

        protected override State OnUpdate() {
            for (int i = 0; i < children.Count; ++i)
            { 
                var childStatus = children[i].Update();

                if (childStatus != State.Failure && i < _activeChildIndex)
                {
                    var previousChild = children[_activeChildIndex];
                    previousChild.Stop();

                    _activeChildIndex = i;
                }

                if (childStatus == State.Running) 
                    return State.Running;
                else if (childStatus == State.Success) 
                    return State.Success;
            }

            return State.Failure;
        }
    }
}