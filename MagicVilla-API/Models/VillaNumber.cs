using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models;

public class VillaNumber
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VillaNo { get; set; }
    public string SpecialDetAILS { get; set; }
    [ForeignKey("Villa")]
    public int VillaID { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}