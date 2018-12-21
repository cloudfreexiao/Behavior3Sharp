/**
 * MemSequence is similar to Sequence node, but when a child returns a
 * `RUNNING` state, its index is recorded and in the next tick the
 * MemPriority call the child recorded directly, without calling previous
 * children again.
 *
 * @module b3
 * @class MemSequence
 * @extends Composite
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class MemSequence : Composite
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);

            this.name = "MemSequence";
            this.title = "MemSequence ";
        }

        public override void open(Tick tick)
        {
            tick.blackboard.Set("runningChild", 0, tick.tree.id, this.id);
        }

        public override B3Status tick(Tick tick)
        {
            var child = tick.blackboard.Get<int>("runningChild", tick.tree.id, this.id, 0);
            for (int i = child; i < this.children.Count; i++)
            {
                var status = this.children[i]._execute(tick);
                if (status != B3Status.SUCCESS)
                {
                    if (status == B3Status.RUNNING)
                    {
                        tick.blackboard.Set("runningChild", i, tick.tree.id, this.id);
                    }
                    return status;
                }
            }

            return B3Status.SUCCESS;
        }

    }

}
