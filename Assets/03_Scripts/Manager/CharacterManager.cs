/// <summary>
/// 캐릭터 관리 클래스(싱글톤)
/// </summary>
public class CharacterManager : Singleton<CharacterManager>
{
    //Player에게 전역에서 접근할 수 있도록 함
    public Player Player { get; set; }
}
