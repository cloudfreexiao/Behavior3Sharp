/**
 * This decorator limit the number of times its child can be called. After a
 * certain number of times, the Limiter decorator returns `FAILURE` without
 * executing the child.
 *
 * @module b3
 * @class Limiter
 * @extends Decorator
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class Limiter : Decorator
    {
        private int maxLoop;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.maxLoop = cfg.GetInt32("maxLoop", 1);
            this.name = "Limiter";
            this.title = "Limit <maxLoop> Activations";
        }

        public override void open(Tick tick)
        {
            tick.blackboard.Set("i", 0, tick.tree.id, this.id);
        }

        public override B3Status tick(Tick tick)
        {
            if (this.child == null)
            {
                return B3Status.ERROR;
            }

            var i = tick.blackboard.Get<int>("i", tick.tree.id, this.id, 0);

            if (i < this.maxLoop)
            {
                var status = this.child._execute(tick);
                if (status == B3Status.SUCCESS || status == B3Status.FAILURE)
                {
                    tick.blackboard.Set("i", i + 1, tick.tree.id, this.id);
                }
                return status;
            }

            return B3Status.FAILURE;
        }

    }

}