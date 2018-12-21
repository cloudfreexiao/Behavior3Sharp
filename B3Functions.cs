using System;


namespace XIL.AI.Behavior3Sharp
{
    public class B3Functions
    {
        public static string CreateUUID()
        {
            return Guid.NewGuid().ToString();
        }

        //public static BaseNode CreateBehavior3Instance<T>(string classname)
        //{
        //    return CreateInstance<BaseNode>("Behavior3Sharp", "XIL.AI.Behavior3Sharp", classname);
        //}

        //private static T CreateInstance<T>(string asmname, string spacename, string classname)
        //{
        //    try
        //    {
        //        string fullname = spacename + "." + classname;
        //        string path = fullname + "," + asmname;
        //        Type o = Type.GetType(fullname);
        //        System.Object obj = Activator.CreateInstance(o, true);
        //        return (T)obj;
        //    }
        //    catch(Exception e)
        //    {
        //        Debug.Log("CreateInstance Error:" + e.Message.ToString());
        //        return default(T);
        //    }
        //}


    }

}
