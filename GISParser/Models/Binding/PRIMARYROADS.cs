using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using GISParser.Models.Base;

public class PRIMARYROADS : BaseModel
{
	public long? LINEARID { get; set; }
	[MaxLength(49, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string FULLNAME { get; set; }
	[MaxLength(1, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string RTTYP { get; set; }
	[MaxLength(5, ErrorMessage = "{0}'s length must be {1} characters or less")]
	public string MTFCC { get; set; }
	public DbGeography GEOG { get; set; }
}