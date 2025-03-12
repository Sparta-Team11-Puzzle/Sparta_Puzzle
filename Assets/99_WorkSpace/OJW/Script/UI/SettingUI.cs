using DataDeclaration;

public class SettingUI : BaseUI
{
    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Setting);
    }
}
