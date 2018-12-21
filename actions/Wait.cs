/**
 * Wait a few seconds.
 *
 * @module b3
 * @class Wait
 * @extends Action
 **/

using System;

namespace XIL.AI.Behavior3Sharp
{
    public class Wait: Action
    {
        private int endTime;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.endTime = cfg.GetInt32("milliseconds", 0);
            this.name = "Wait";
            this.title = "Wait <milliseconds>ms ";
        }

        public override void open(Tick tick)
        {
            var startTime = DateTime.Now.Millisecond;
            tick.blackboard.Set("startTime", startTime, tick.tree.id, this.id);
        }

        public override B3Status tick(Tick tick)
        {
            var currTime = DateTime.Now.Millisecond;
            var startTime = tick.blackboard.Get<int>("startTime", tick.tree.id, this.id, 0);
            if(currTime - startTime > this.endTime)
            {
                return B3Status.SUCCESS;
            }
            return B3Status.RUNNING;
        }

    }
}
