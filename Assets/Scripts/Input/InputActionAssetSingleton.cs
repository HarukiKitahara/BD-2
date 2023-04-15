namespace MyProject.Input
{
    public partial class InputActionAssetSingleton
    {
        public static InputActionAssetSingleton Instance
        {
            get
            {
                if (_instance == null) _instance = new InputActionAssetSingleton();
                return _instance;
            }
        }
        private static InputActionAssetSingleton _instance;
        //private InputActionAssetSingleton() { }        // 淦，AutoGen出来的代码有public new方法，没法做这一层保护了。
    }
}

