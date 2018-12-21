
/**
 * The BaseNode class is used as super class to all nodes in BehaviorJS. It
 * comprises all common variables and methods that a node must have to
 * execute.
 *
 * **IMPORTANT:** Do not inherit from this class, use `Composite`,
 * `Decorator`, `Action` or `Condition`, instead.
 *
 * The attributes are specially designed to serialization of the node in a
 * JSON format. In special, the `parameters` attribute can be set into the
 * visual editor (thus, in the JSON file), and it will be used as parameter
 * on the node initialization at `BehaviorTree.load`.
 *
 * BaseNode also provide 5 callback methods, which the node implementations
 * can override. They are `enter`, `open`, `tick`, `close` and `exit`. See
 * their documentation to know more. These callbacks are called inside the
 * `_execute` method, which is called in the tree traversal.
 *
 * @module b3
 * @class BaseNode
 **/

using System.Collections.Generic;



namespace XIL.AI.Behavior3Sharp
{
    public abstract class BaseNode
    {
        protected string id;
        protected string name = "";
        protected string category = "";
        protected string title;
        protected string description = "";
        protected Dictionary<string, string> parameters;
        protected Dictionary<string, string> properties;

        public virtual void Initialize(Behavior3NodeCfg cfg)
        {
            this.id = cfg.id;
            this.name = cfg.name;
            this.description = cfg.description;
            this.title = cfg.title;

            this.parameters = new Dictionary<string, string>();
            this.properties = new Dictionary<string, string>();

            this.parameters = cfg.parameters;
            this.properties = cfg.properties;
        }

        public string GetID()
        {
            return this.id;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetTitle()
        {
            return this.title;
        }

        public string GetCategory()
        {
            return this.category;
        }

        /**
     * This is the main method to propagate the tick signal to this node. This
     * method calls all callbacks: `enter`, `open`, `tick`, `close`, and
     * `exit`. It only opens a node if it is not already open. In the same
     * way, this method only close a node if the node  returned a status
     * different of `RUNNING`.
     *
     * @method _execute
     * @param {Tick} tick A tick instance.
     * @return {Constant} The tick state.
     * @protected
     **/
        public B3Status _execute(Tick tick)
        {
            // ENTER
            this._enter(tick);

            // OPEN
            bool isopen = tick.blackboard.Get<bool>("isOpen", tick.tree.id, this.id, false);
            if (!isopen)
            {
                this._open(tick);
            }

            // TICK
            var status = this._tick(tick);

            // CLOSE
            if (status != B3Status.RUNNING)
            {
                this._close(tick);
            }

            // EXIT
            this._exit(tick);

            return status;
        }

        /**
         * Wrapper for enter method.
         * @method _enter
         * @param {Tick} tick A tick instance.
         * @protected
         **/
        public void _enter(Tick tick)
        {
            //tick._enterNode(this);
            this.enter(tick);
        }

        /**
        * Wrapper for open method.
        * @method _open
        * @param {Tick} tick A tick instance.
        * @protected
        **/
        public void _open(Tick tick)
        {
            //tick._openNode(this);
            //tick.blackboard.set('isOpen', true, tick.tree.id, this.id);
            this.open(tick);
        }

        /**
         * Wrapper for tick method.
         * @method _tick
         * @param {Tick} tick A tick instance.
         * @return {Constant} A state constant.
         * @protected
         **/
        public B3Status _tick(Tick tick)
        {
            //tick._tickNode(this);
            return this.tick(tick);
        }

        /**
         * Wrapper for close method.
         * @method _close
         * @param {Tick} tick A tick instance.
         * @protected
         **/
        public void _close(Tick tick)
        {
            tick._closeNode(this);
            tick.blackboard.Set("isOpen", false, tick.tree.id, this.id);
            this.close(tick);
        }

        /**
         * Wrapper for exit method.
         * @method _exit
         * @param {Tick} tick A tick instance.
         * @protected
         **/
        public void _exit(Tick tick)
        {
            tick._exitNode(this);
            this.exit(tick);
        }

        /**
         * Enter method, override this to use. It is called every time a node is
         * asked to execute, before the tick itself.
         *
         * @method enter
         * @param {Tick} tick A tick instance.
         **/
        public virtual void enter(Tick tick) { }

        /**
     * Open method, override this to use. It is called only before the tick
     * callback and only if the not isn't closed.
     *
     * Note: a node will be closed if it returned `RUNNING` in the tick.
     *
     * @method open
     * @param {Tick} tick A tick instance.
     **/
        public virtual void open(Tick tick) { }
        /**
        * Tick method, override this to use. This method must contain the real
        * execution of node (perform a task, call children, etc.). It is called
        * every time a node is asked to execute.
        *
        * @method tick
        * @param {Tick} tick A tick instance.
        **/
        public virtual B3Status tick(Tick tick) { return B3Status.ERROR;  }

        /**
         * Close method, override this to use. This method is called after the tick
         * callback, and only if the tick return a state different from
         * `RUNNING`.
         *
         * @method close
         * @param {Tick} tick A tick instance.
         **/
        public virtual void close(Tick tick) { }

        /**
         * Exit method, override this to use. Called every time in the end of the
         * execution.
         *
         * @method exit
         * @param {Tick} tick A tick instance.
         **/
        public virtual void exit(Tick tick) { }

    }
}
