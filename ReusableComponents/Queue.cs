using System;
using Ers;

namespace ReusableComponents
{
    internal class QueueBehavior : ScriptBehaviorComponent
    {
        public ulong Capacity = 5;

        public override void OnEntered(ulong newChild)
        {
            Logger.Debug($"Queue received {newChild.GetName()}");

            var channel = ConnectedEntity.GetComponent<Channel>();
            if (ConnectedEntity.GetComponent<RelationComponent>().Value.ChildCount() >= Capacity)
                channel.Value.InputOpen = false;

            if (channel.Value.ToEntity.GetComponent<Channel>().Value.InputOpen)
            {
                MoveNext();
            }
        }

        public override void OnExited(ulong oldChild)
        {
            ConnectedEntity.GetComponent<Channel>().Value.InputOpen = true;
        }

        public void MoveNext()
        {
            var relation = ConnectedEntity.GetComponent<RelationComponent>();
            if (relation.Value.ChildCount() == 0)
                return;

            var channel = ConnectedEntity.GetComponent<Channel>();
            if (channel.Value.ToEntity.GetComponent<Channel>().Value.InputOpen)
            {
                Entity first = relation.Value.First();
                SubModel.GetSubModel().UpdateParentOnEntity(first, channel.Value.ToEntity);
            }
        }
    }
}
