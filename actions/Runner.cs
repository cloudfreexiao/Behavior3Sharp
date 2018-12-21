
/**
 * This action node returns RUNNING always.
 *
 * @module b3
 * @class Runner
 * @extends Action
 **/


namespace XIL.AI.Behavior3Sharp
{
    public class Runner: Action
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.name = "Runner";
            this.title = "Runner";
        }

        public override B3Status tick(Tick tick) { return B3Status.RUNNING; }
    }
}
