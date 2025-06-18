using System;
using Ers;

namespace ExpandingTheModel
{
    internal class QueueBehavior : ScriptBehaviorComponent
    {
        public Entity Target;
        public ulong Capacity = 5;
        public bool InputOpen = true;

        public override void OnEntered(ulong newChild)
        {
            Logger.Debug($"Queue received {newChild.GetName()}");

            if (ConnectedEntity.GetComponent<RelationComponent>().Value.ChildCount() >= Capacity)
                InputOpen = false;

            if (Target.GetComponent<ServerBehavior>().InputOpen)
            {
                MoveNext();
            }
        }

        public override void OnExited(ulong oldChild)
        {
            InputOpen = true;
        }

        public void MoveNext()
        {
            var relation = ConnectedEntity.GetComponent<RelationComponent>();
            if (relation.Value.ChildCount() == 0)
                return;

            if (Target.GetComponent<ServerBehavior>().InputOpen)
            {
                Entity first = relation.Value.First();
                SubModel.GetSubModel().UpdateParentOnEntity(first, Target);
            }
        }
    }
}
