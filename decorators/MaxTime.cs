/**
 * The MaxTime decorator limits the maximum time the node child can execute.
 * Notice that it does not interrupt the execution itself (i.e., the child
 * must be non-preemptive), it only interrupts the node after a `RUNNING`
 * status.
 *
 * @module b3
 * @class MaxTime
 * @extends Decorator
 **/

using System;

namespace XIL.AI.Behavior3Sharp
{
    public class MaxTime : Decorator
    {
        private int maxTime;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.maxTime = cfg.GetInt32("maxTime", 0);
            this.name = "MaxTime";
            this.title = "Max <maxTime>ms";
        }

        public override void open(Tick tick)
        {
            var startTime = DateTime.Now.Millisecond;
            tick.blackboard.Set("startTime", startTime, tick.tree.id, this.id);
        }

        public override B3Status tick(Tick tick)
        {
            if (this.child == null)
            {
                return B3Status.ERROR;
            }

            var currTime = DateTime.Now.Millisecond;
            var startTime = tick.blackboard.Get<int>("startTime", tick.tree.id, this.id, 0);
            var status = this.child._execute(tick);
            if (currTime - startTime > this.maxTime)
            {
                return B3Status.FAILURE;
            }

            return status;
        }

    }

}