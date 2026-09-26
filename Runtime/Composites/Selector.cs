using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    
    [System.Serializable]
    public class Selector : CompositeNode
    {
        private int lasSuccessfulIndex;

        protected override void OnStart() { }

        protected override void OnStop()
        {
            foreach (var child in children)
            {
                if (child.started) child.OnStop();
            }
        }

        protected override State OnUpdate() {
            for (int i = 0; i < children.Count; ++i)
            { 
                var childStatus = children[i].Update();

                if (childStatus != State.Failure && i < lasSuccessfulIndex)
                {
                    var previousChild = children[lasSuccessfulIndex];
                    if (previousChild.started) previousChild.OnStop();

                    lasSuccessfulIndex = i;
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