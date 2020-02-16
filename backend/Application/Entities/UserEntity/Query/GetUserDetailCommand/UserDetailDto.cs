using System;
using System.Linq.Expressions;
using Application.Entities.LocationEntity.Query.GetCountries;
using Application.Entities.LocationEntity.Query.GetStates;
using Domain.Entities;

namespace Application.Entities.UserEntity.Query.GetUserDetailCommand
{
    public class UserDetailDto
    {
        public string AboutMe { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public AddressDto Address { get; set; }
        public AddressDto LocalAddress { get; set; }
        public ContactDto Contact { get; set; }

        public static Expression<Func<UserDetail, UserDetailDto>> Projection
        {
            get
            {
                return UserDetail => new UserDetailDto
                {
                    AboutMe = UserDetail.AboutMe,
                    FirstName = UserDetail.FirstName,
                    LastName = UserDetail.LastName,
                    DateOfBirth = UserDetail.DateOfBirth,
                    Gender = UserDetail.Gender,
                    DateCreated = UserDetail.DateCreated,
                    Address = UserDetail.Address == null ? null : AddressDto.Create(UserDetail.Address),
                    LocalAddress = UserDetail.LocalAddress == null ? null : AddressDto.Create(UserDetail.LocalAddress),
                    Contact = UserDetail.Contact == null ? null : ContactDto.Create(UserDetail.Contact)
                };
            }
        }

        public static UserDetailDto Create(UserDetail userDetail) 
        {
            return Projection.Compile().Invoke(userDetail);
        }
    }


    public class AddressDto
    {
        public CountriesDTO Country { get; set; }
        public StateDTO State { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public bool Global { get; set; }

        public static Expression<Func<Address, AddressDto>> Projection
        {
            get
            {
                return Address => new AddressDto
                {
                    Street = Address.Street,
                    PostCode = Address.PostCode,
                    Country = CountriesDTO.Create(Address.Country),
                    State = StateDTO.Create(Address.State),
                    Global = Address.Global
                };
            }
        }

        public static AddressDto Create(Address address)
        {
            return Projection.Compile().Invoke(address);
        }
    }
    public class ContactDto
    {
        public string Phone_1 { get; set; }
        public string Email_1 { get; set; }
        public static Expression<Func<Contact, ContactDto>> Projection
        {
            get
            {
                return Contact => new ContactDto
                {
                    Phone_1 = Contact.Phone_1,
                    Email_1 = Contact.Email_1,
                };
            }
        }
        public static ContactDto Create(Contact contact) 
        {
            return Projection.Compile().Invoke(contact);
        }
    }
}