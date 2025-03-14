/// <summary>
/// 캐릭터 관리 클래스(싱글톤)
/// </summary>
public class CharacterManager : Singleton<CharacterManager>
{
    public Player Player { get; set; }
}
