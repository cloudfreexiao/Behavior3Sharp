
namespace XIL.AI.Behavior3Sharp
{
    public class Log : Action
    {
        private string info;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.info = cfg.GetString("info", "log-action");
            this.name = "LogAction";
            this.title = "LogAction";
        }

        public override B3Status tick(Tick tick) {
            //Debug.Log("++tick++" + this.info + " name:" + this.name + "title:" + this.title);
            return B3Status.SUCCESS;
        }
    }
}
