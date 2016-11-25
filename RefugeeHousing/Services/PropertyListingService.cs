﻿using RefugeeHousing.Models;
using RefugeeHousing.Translations;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Services
{
    public class PropertyListingService
    {
        public static void AddListingToDatabase(ListingViewModel listingViewModel, string currentUserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var locationId = listingViewModel.PlaceId;
                var location = LocationRepository.GetOrCreateLocation(db, locationId);
                var currentUser = UserIdentityService.GetUser(db, currentUserId);

                var listing = new Listing
                {
                    Appliances = listingViewModel.Appliances,
                    Elevator = listingViewModel.Elevator,
                    Furnished = listingViewModel.Furnished,
                    Price = listingViewModel.Price,
                    LanguagesSpoken = listingViewModel.LanguagesSpoken,
                    Location = location,
                    LocationId = locationId,
                    NumberOfBedrooms = listingViewModel.NumberOfBedrooms,
                    Owner = currentUser,
                    OwnerId = currentUserId
                };

                db.Listings.Add(listing);
                db.SaveChanges();
            }
        }

        public ListingDetailsViewModel GetListing(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var requestedListing = db.Listings.Find(id);
                if (requestedListing == null)
                {
                    return null;
                }
                var location = db.Locations.Find(requestedListing.LocationId);
                var listingDetailsViewModel = new ListingDetailsViewModel
                {
                    Appliances = requestedListing.Appliances,
                    Elevator = requestedListing.Elevator,
                    Furnished = requestedListing.Furnished,
                    Id = requestedListing.Id,
                    Location = location,
                    NumberOfBedrooms = requestedListing.NumberOfBedrooms,
                    Price = requestedListing.Price
                };

                return listingDetailsViewModel;
            }
        }
    }
}