namespace XIL.AI.Behavior3Sharp
{

    public class TestB3
    {

        public static void Test()
        {
            var tree = Behavior3Factory.singleton.BuildBehavior3TreeFromConfig(@"Assets/Plugins/AI/Behavior3Sharp/etc/test_tree.json");
            tree.inspect();
            var blackboard = new Blackboard();
            for(int i=0; i< 1000; i++)
            {
                tree.Tick(i, blackboard);
            }
        }

    }


}