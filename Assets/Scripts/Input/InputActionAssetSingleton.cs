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
        //private InputActionAssetSingleton() { }        // �ƣ�AutoGen�����Ĵ�����public new������û������һ�㱣���ˡ�
    }
}

