using static MagicVilla_Utility.SD;
namespace MagicVilla_Utility;

public class APIRequest
{
    public ApiType ApiType { get; set; }
    public string Url { get; set; }
    public object Data { get; set; }
}