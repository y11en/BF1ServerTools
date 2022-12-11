namespace BF1ServerTools.Configs;

public class AuthConfig
{
    public int SelectedIndex { get; set; }
    public bool IsUseMode1 { get; set; }
    public string SessionId1 { get; set; }
    public List<AuthInfo> AuthInfos { get; set; }
    public class AuthInfo
    {
        public string Avatar { get; set; }
        public string DisplayName { get; set; }
        public long PersonaId { get; set; }
        public string Remid { get; set; }
        public string Sid { get; set; }
        public string AccessToken { get; set; }
        public string SessionId2 { get; set; }
    }
}
