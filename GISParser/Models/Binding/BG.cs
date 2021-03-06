using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using GISParser.Models.Base;

public class BG : BaseModel
{
	public short? STATEFP { get; set; }
	public short? COUNTYFP { get; set; }
	public int? TRACTCE { get; set; }
	public short? BLKGRPCE { get; set; }
	public long? GEOID { get; set; }
	[MaxLength(13, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string NAMELSAD { get; set; }
	[MaxLength(5, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string MTFCC { get; set; }
	[MaxLength(1, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string FUNCSTAT { get; set; }
	public long? ALAND { get; set; }
	public long? AWATER { get; set; }
	public float INTPTLAT { get; set; }
	public float INTPTLON { get; set; }
	public DbGeography GEOG { get; set; }
}