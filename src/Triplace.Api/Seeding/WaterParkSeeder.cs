using Triplace.Application.Commands;
using Triplace.Application.Services;
using Triplace.Domain.Enums;
using Triplace.Domain.Ids;
using Triplace.Domain.ValueObjects;

namespace Triplace.Api.Seeding;

public static class WaterParkSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var attractionService = services.GetRequiredService<AttractionService>();
        var catalogService = services.GetRequiredService<SeasonalCatalogService>();
        var relationService = services.GetRequiredService<RelationService>();
        var routeService = services.GetRequiredService<RouteService>();

        // 1. Create attractions (Drafts)
        
        // KOMPLEKS BASENOWY
        var waterParkId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Water Park - Kompleks Basenowy",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.FamilyFriendly, AttractionAmenity.ParkingNearby, AttractionAmenity.Cafe, AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Wodna 1, 00-001 Kraków"), new MetadataEntry("miasto", "Kraków")]));

        var poolId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Hala basenowa",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.WheelchairAccess, AttractionAmenity.FamilyFriendly },
            [new MetadataEntry("opis", "Główny basen sportowy i rekreacyjny")]));

        var saunaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Strefa saun",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.BlindFriendly },
            [new MetadataEntry("opis", "Relaks w saunach suchych i parowych")]));

        var outdoorId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Strefa parkowa",
            AttractionCategory.Park,
            new HashSet<Season> { Season.Summer },
            VisitDuration.Medium,
            IsOutdoor: true,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.FamilyFriendly, AttractionAmenity.ParkingNearby },
            [new MetadataEntry("sezon", "Czerwiec - Sierpień")]));

        // FITPARK
        var fitparkId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Fitpark",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.ParkingNearby, AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Sprawna 5, 00-001 Kraków")]));

        var gymId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Siłownia",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.AudioGuide },
            [new MetadataEntry("opis", "Sprzęt cargo i siłowy")]));

        var classesId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Zajęcia grupowe",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Short,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.FamilyFriendly },
            [new MetadataEntry("typ", "Zumba, Joga, Aerobik")]));

        // HOTEL
        var hotelId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Hotel Wellness",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.WheelchairAccess, AttractionAmenity.ParkingNearby, AttractionAmenity.Cafe, AttractionAmenity.GiftShop },
            [new MetadataEntry("adres", "ul. Wodna 1, 00-001 Kraków")]));

        var accommodationId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Nocleg w hotelu",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.BlindFriendly },
            [new MetadataEntry("typ", "Pokój 2-osobowy Standard")]));

        var breakfastId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Śniadanie hotelowe",
            AttractionCategory.Restaurant,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Short,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.FamilyFriendly },
            [new MetadataEntry("typ", "Bufet szwedzki")]));

        // 2. Publish all
        var allIds = new[] { waterParkId, poolId, saunaId, outdoorId, fitparkId, gymId, classesId, hotelId, accommodationId, breakfastId };
        foreach (var id in allIds)
            await attractionService.PublishAsync(id);

        // 3. Set Hierarchy
        await attractionService.AddChildAsync(waterParkId, poolId);
        await attractionService.AddChildAsync(waterParkId, saunaId);
        await attractionService.AddChildAsync(waterParkId, outdoorId);
        
        await attractionService.AddChildAsync(fitparkId, gymId);
        await attractionService.AddChildAsync(fitparkId, classesId);
        
        await attractionService.AddChildAsync(hotelId, accommodationId);
        await attractionService.AddChildAsync(hotelId, breakfastId);

        // 4. Create Seasonal Catalog
        var catalogId = await catalogService.CreateAsync(new CreateSeasonalCatalogCommand(
            "Kraków - Lato w Mieście 2025",
            Season.Summer,
            new DateOnly(2025, 6, 1),
            new DateOnly(2025, 8, 31),
            "Mazowsze",
            "Katalog letnich atrakcji wodnych i sportowych w Krakowie",
            1000));

        foreach (var id in new[] { waterParkId, fitparkId, hotelId })
            await catalogService.AddAttractionAsync(catalogId, id);

        // 5. Relations
        await relationService.AddRecommendationAsync(
            new AttractionId((await attractionService.GetByIdAsync(waterParkId))!.Id.Value),
            new AttractionId((await attractionService.GetByIdAsync(fitparkId))!.Id.Value));

        await relationService.AddRecommendationAsync(
            new AttractionId((await attractionService.GetByIdAsync(hotelId))!.Id.Value),
            new AttractionId((await attractionService.GetByIdAsync(waterParkId))!.Id.Value));

        // Active Day Route
        await routeService.CreateAsync(new CreateRouteCommand(
            "Dzień Aktywny (Water Park + Fitpark)",
            "Kompletny dzień dla osób aktywnych - basen i siłownia",
            Season.Summer,
            null,
            new List<RouteItemCommand> {
                new(new AttractionId(waterParkId.Value), Priority.Must),
                new(new AttractionId(fitparkId.Value), Priority.Must)
            }));

        var premiumRouteItems = new List<RouteItemCommand> {
            new(new AttractionId(hotelId.Value), Priority.Must),
            new(new AttractionId(waterParkId.Value), Priority.Must),
            new(new AttractionId(fitparkId.Value), Priority.Optional)
        };

        foreach (var season in new[] { Season.Spring, Season.Summer, Season.Autumn, Season.Winter })
        {
            await routeService.CreateAsync(new CreateRouteCommand(
                $"Weekend Premium ({season})",
                "Luksusowy weekend w Krakowie: Hotel, Baseny i Wellness",
                season,
                null,
                premiumRouteItems));
        }
    }
}
