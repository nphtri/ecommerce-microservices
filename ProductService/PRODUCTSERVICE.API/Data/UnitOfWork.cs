using System;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Data
{
  public interface IUnitOfWork : IDisposable
  {
    IGenericRepo<Art> ArtRepo { get; }
    IGenericRepo<Artist> ArtistRepo { get; }
    IGenericRepo<Product> ProductRepo { get; }
    IGenericRepo<ArtCollection> ArtCollectionRepo { get; }
    ILookupRepo LookupRepo { get; }
    IGenericRepo<LookupType> LookupTypeRepo { get; }
    IGenericRepo<Banner> BannerRepo { get; }
    int Complete();
  }

  public class UnitOfWork : IUnitOfWork
  {
    private readonly AppDbContext _context;
    private bool disposed = false;
    private IGenericRepo<Art> _artRepo;
    private IGenericRepo<Artist> _artistRepo;
    private IGenericRepo<ArtCollection> _artCollectionRepo;
    private ILookupRepo _lookupRepo;
    private IGenericRepo<LookupType> _lookupTypeRepo;
    private IGenericRepo<Product> _productRepo;
    private IGenericRepo<Banner> _bannerRepo;

    public UnitOfWork(AppDbContext context)
    {
      _context = context;
    }
    public IGenericRepo<Art> ArtRepo
    {
      get { return _artRepo ??= new GenericRepo<Art>(_context); }
    }

    public IGenericRepo<Artist> ArtistRepo
    {
      get { return _artistRepo ??= new GenericRepo<Artist>(_context); }
    }

    public IGenericRepo<Product> ProductRepo
    {
      get { return _productRepo ??= new GenericRepo<Product>(_context); }
    }

    public IGenericRepo<ArtCollection> ArtCollectionRepo
    {
      get { return _artCollectionRepo ??= new GenericRepo<ArtCollection>(_context); }
    }

    public ILookupRepo LookupRepo
    {
      get { return _lookupRepo ??= new LookupRepo(_context); }
    }

    public IGenericRepo<LookupType> LookupTypeRepo
    {
      get { return _lookupTypeRepo ??= new GenericRepo<LookupType>(_context); }
    }

    public IGenericRepo<Banner> BannerRepo
    {
      get { return _bannerRepo ??= new GenericRepo<Banner>(_context); }
    }

    public int Complete()
    {
      return _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _context.Dispose();
        }
      }
      this.disposed = true;
    }
    public void Dispose()
    {
      Dispose(true);
      System.GC.SuppressFinalize(this);
    }
  }
}