using System.ComponentModel;

namespace BlogMVC.Enums
{
    public enum ModerationType
    {
        [Description("Political speech")]
        Political,
        [Description("Offensive language")]
        Language,
        [Description("Narcotics references")]
        Drugs,
        [Description("Threatening speech")]
        Threatening,
        [Description("Sexual content")]
        Sexual,
        [Description("Hate speech")]
        HateSpeech,
        [Description("Targeted shaming")]
        Shaming
    }
}
