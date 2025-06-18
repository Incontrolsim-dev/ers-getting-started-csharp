using System;
using Ers;

namespace ReusableComponents
{
    internal class SinkBehavior : ScriptBehaviorComponent
    {
        public ulong Received = 0;

        public override void OnEntered(Entity newChild)
        {
            Received++;
            SubModel.GetSubModel().DestroyEntity(newChild);
        }
    }
}
