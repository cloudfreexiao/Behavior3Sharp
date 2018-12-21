/**
 * The Inverter decorator inverts the result of the child, returning `SUCCESS`
 * for `FAILURE` and `FAILURE` for `SUCCESS`.
 *
 * @module b3
 * @class Inverter
 * @extends Decorator
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class Inverter : Decorator
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);

            this.name = "Inverter";
            this.title = "Inverter ";
        }

        public override B3Status tick(Tick tick)
        {
            if(this.child == null)
            {
                return B3Status.ERROR;
            }

            var status = this.child._execute(tick);
            if(status == B3Status.SUCCESS)
            {
                status = B3Status.FAILURE;
            } else if (status == B3Status.FAILURE)
            {
                status = B3Status.SUCCESS;
            }

            return status;
        }

    }

}