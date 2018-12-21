/**
 * Repeater is a decorator that repeats the tick signal until the child node
 * return `RUNNING` or `ERROR`. Optionally, a maximum number of repetitions
 * can be defined.
 *
 * @module b3
 * @class Repeater
 * @extends Decorator
 **/


namespace XIL.AI.Behavior3Sharp
{
    public class Repeater : Decorator
    {
        private int maxLoop;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.maxLoop = cfg.GetInt32("maxLoop", -1);
            //this.maxLoop = cfg.GetValue<int>("maxLoop", -1);
            this.name = "Repeater";
            this.title = "Repeat <maxLoop>x";
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
            var status = B3Status.SUCCESS;

            while(this.maxLoop < 0 || i< this.maxLoop)
            {
                status = this.child._execute(tick);
                if(status == B3Status.SUCCESS || status == B3Status.FAILURE)
                {
                    i++;
                } 
                else
                {
                    break;
                }
            }

            tick.blackboard.Set("i", i, tick.tree.id, this.id);
            return status;
        }

    }

}