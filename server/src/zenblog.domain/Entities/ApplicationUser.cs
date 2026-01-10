using Microsoft.AspNetCore.Identity;

namespace zenBlog.domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName{get;set;}=default!;
    public string LastName{get;set;}= default!;
    public virtual  ICollection<Blog> Blogs {get;private set;} = [];
    public void AddBlog(Blog blog) => Blogs.Add(blog);
    public string? ImageUrl { get; set; } = default!;
    public Address Address { get; set; } = new();
    public DateTime CreatedOn { get; set; } = default!;
    public DateTime LastModifiedOn { get ; set ; }
    public string? RefreshToken {get;set;}= default!;
    public DateTime RefreshTokenExpiresTime{get;set;} = default!;
    public override string? Email
    {
        get => base.Email!;

        set
        {
            base.Email = value;
            base.UserName = value;
        }
    }

    public ApplicationUser(){}
    public ApplicationUser(string firstname,string lastname,string phonenumber,string email, Address address=null!)
    {
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Email = email;
        Address = address?? new();
        CreatedOn= DateTime.UtcNow;
    }

}

public class Address
{
    public string? Country { get; set; } = default!;
    public string? City { get; set; } = default!;
    public string? Street { get; set; } = default!;
    public string? PostalCode { get; set; } = default!;


    public Address() { }
    public Address(string country, string city, string street, string postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }
}