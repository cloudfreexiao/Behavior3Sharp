/**
 * This action node returns `FAILURE` always.
 *
 * @module b3
 * @class Failer
 * @extends Action
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class Failer : Action
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.name = "Failer";
            this.title = "Failer";
        }

        public override B3Status tick(Tick tick) { return B3Status.FAILURE; }
    }
}
