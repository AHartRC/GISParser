using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using GISParser.Models.Base;

public class PUMA : BaseModel
{
	public short? STATEFP10 { get; set; }
	public int? PUMACE10 { get; set; }
	public int? GEOID10 { get; set; }
	[MaxLength(90, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string NAMELSAD10 { get; set; }
	[MaxLength(5, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string MTFCC10 { get; set; }
	[MaxLength(1, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string FUNCSTAT10 { get; set; }
	public long? ALAND10 { get; set; }
	public long? AWATER10 { get; set; }
	public float INTPTLAT10 { get; set; }
	public float INTPTLON10 { get; set; }
	public DbGeography GEOG { get; set; }
}