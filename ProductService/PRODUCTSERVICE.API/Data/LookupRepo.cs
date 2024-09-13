using System.Collections.Generic;
using System.Linq;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Data
{
  public interface ILookupRepo : IGenericRepo<Lookup>
  {
    IEnumerable<Lookup> GetArtStyles();
  }

  public class LookupRepo : GenericRepo<Lookup>, ILookupRepo
  {
    public LookupRepo(AppDbContext context) : base(context)
    {
    }
    public IEnumerable<Lookup> GetArtStyles()
    {
      var lookupType = _context.LookupTypes.FirstOrDefault(_ => _.Value.Equals(LookupTypeConstant.ART_STYLE));
      if (lookupType != null)
      {
        return _context.Lookups.Where(_ => _.LookupTypeId == lookupType.Id).AsEnumerable();
      }
      return new List<Lookup>();
    }
  }

}