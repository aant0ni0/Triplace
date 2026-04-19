using Triplace.Application.Commands;
using Triplace.Application.Services;
using Triplace.Domain.Enums;
using Triplace.Domain.Ids;
using Triplace.Domain.ValueObjects;

namespace Triplace.Api.Seeding;

public static class OperaKrakowskaSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var attractionService = services.GetRequiredService<AttractionService>();
        var catalogService = services.GetRequiredService<SeasonalCatalogService>();
        var relationService = services.GetRequiredService<RelationService>();
        var routeService = services.GetRequiredService<RouteService>();

        // 1. Atrakcja główna
        var operaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Opera Krakowska",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.AudioGuide, AttractionAmenity.GuideAvailable, AttractionAmenity.WheelchairAccess, AttractionAmenity.GiftShop },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków")]));

        // 2. Sub-atrakcje
        var spektaklOperaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Spektakl Operowy",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.AudioGuide, AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Wieczorny spektakl na głównej scenie — opera włoska i polska")]));

        var spektaklBaletId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Spektakl Baletowy",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Spektakl Baletu Opery Krakowskiej — klasyka i choreografia współczesna")]));

        var spektaklOperetkaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Spektakl Operetkowy",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.FamilyFriendly, AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Lekki repertuar operetkowy — idealny dla rodzin i pierwszych widzów")]));

        var backstageId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Zwiedzanie za Kulisami",
            AttractionCategory.Museum,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.GuideAvailable, AttractionAmenity.FamilyFriendly },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Pracownia kostiumów, maszyneria sceniczna, sala prób i główna scena — grupy do 15 osób")]));

        var warsztatyId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Warsztaty Wokalne z Solistą",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Short,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.GuideAvailable },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Godzinne warsztaty prowadzone przez solistę Opery — maks. 10 uczestników")]));

        var foyerId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Bar w Foyer",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Short,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Szampan, wino i przekąski w eleganckim foyer Opery — czynny podczas przerw spektakli")]));

        var kolacjaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Kolacja Przedspektaklowa",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.WheelchairAccess },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków"), new MetadataEntry("opis", "Trzydaniowa kolacja w restauracji partnerskiej (17:00–19:00) — tylko w dniach spektakli")]));

        var operaNaDziedzincuId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Opera na Dziedzińcu",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Summer },
            VisitDuration.Long,
            IsOutdoor: true,
            IsFree: false,
            new HashSet<AttractionAmenity> { AttractionAmenity.FamilyFriendly, AttractionAmenity.ParkingNearby },
            [new MetadataEntry("adres", "ul. Lubicz 48, 31-512 Kraków (dziedziniec)"), new MetadataEntry("sezon", "lipiec – sierpień")]));

        // 3. Publish
        foreach (var id in new[] { operaId, spektaklOperaId, spektaklBaletId, spektaklOperetkaId,
                     backstageId, warsztatyId, foyerId, kolacjaId, operaNaDziedzincuId })
            await attractionService.PublishAsync(id);

        // 4. Hierarchy: sub-atrakcje pod Opera Krakowska
        await attractionService.AddChildAsync(operaId, spektaklOperaId);
        await attractionService.AddChildAsync(operaId, spektaklBaletId);
        await attractionService.AddChildAsync(operaId, spektaklOperetkaId);
        await attractionService.AddChildAsync(operaId, backstageId);
        await attractionService.AddChildAsync(operaId, warsztatyId);
        await attractionService.AddChildAsync(operaId, foyerId);
        await attractionService.AddChildAsync(operaId, kolacjaId);
        await attractionService.AddChildAsync(operaId, operaNaDziedzincuId);

        // 5. Seasonal catalog
        var catalogId = await catalogService.CreateAsync(new CreateSeasonalCatalogCommand(
            "Opera Krakowska — Sezon 2025/2026",
            Season.Autumn,
            new DateOnly(2025, 9, 1),
            new DateOnly(2026, 6, 30),
            "Kraków",
            "Sezon artystyczny Opery Krakowskiej 2025/2026",
            700));

        foreach (var id in new[] { operaId, spektaklOperaId, spektaklBaletId, spektaklOperetkaId, backstageId })
            await catalogService.AddAttractionAsync(catalogId, id);

        var summerCatalogId = await catalogService.CreateAsync(new CreateSeasonalCatalogCommand(
            "Opera na Dziedzińcu — Lato 2025",
            Season.Summer,
            new DateOnly(2025, 7, 1),
            new DateOnly(2025, 8, 31),
            "Kraków",
            "Letnie spektakle plenerowe w dziedzińcu Opery Krakowskiej",
            400));

        await catalogService.AddAttractionAsync(summerCatalogId, operaNaDziedzincuId);

        // 6. Relations
        var operaAttrId = new AttractionId((await attractionService.GetByIdAsync(operaId))!.Id.Value);
        var operaMainId = new AttractionId((await attractionService.GetByIdAsync(spektaklOperaId))!.Id.Value);
        var baletId2 = new AttractionId((await attractionService.GetByIdAsync(spektaklBaletId))!.Id.Value);
        var operetkaId2 = new AttractionId((await attractionService.GetByIdAsync(spektaklOperetkaId))!.Id.Value);
        var foyerAttrId = new AttractionId((await attractionService.GetByIdAsync(foyerId))!.Id.Value);
        var kolacjaAttrId = new AttractionId((await attractionService.GetByIdAsync(kolacjaId))!.Id.Value);
        var backstageAttrId = new AttractionId((await attractionService.GetByIdAsync(backstageId))!.Id.Value);

        // Spektakle wykluczają się wzajemnie (jedna scena, jeden wieczór)
        await relationService.AddExclusionAsync(operaMainId, baletId2);
        await relationService.AddExclusionAsync(operaMainId, operetkaId2);
        await relationService.AddExclusionAsync(baletId2, operetkaId2);

        // Rekomendacje: po spektaklu bar w foyer, do spektaklu kolacja
        await relationService.AddRecommendationAsync(operaMainId, foyerAttrId);
        await relationService.AddRecommendationAsync(baletId2, foyerAttrId);
        await relationService.AddRecommendationAsync(operaMainId, kolacjaAttrId);

        // Backstage dobrze łączy się ze spektaklem tego samego dnia
        await relationService.AddRecommendationAsync(backstageAttrId, operaMainId);

        // 7. Route
        var routeItems = new List<RouteItemCommand>
        {
            new(operaMainId, Priority.Must),
            new(foyerAttrId, Priority.Must),
            new(kolacjaAttrId, Priority.Optional),
            new(backstageAttrId, Priority.Optional),
        };

        await routeService.CreateAsync(new CreateRouteCommand(
            "Wieczór w Operze Krakowskiej",
            "Kompletny wieczór operowy — spektakl, bar w foyer i opcjonalnie kolacja lub kulisy",
            Season.Autumn,
            null,
            routeItems));

        var routeItemsDay = new List<RouteItemCommand>
        {
            new(backstageAttrId, Priority.Must),
            new(new AttractionId((await attractionService.GetByIdAsync(warsztatyId))!.Id.Value), Priority.Must),
            new(operaMainId, Priority.Must),
        };

        await routeService.CreateAsync(new CreateRouteCommand(
            "Dzień z Operą Krakowską",
            "Warsztaty wokalne i zwiedzanie kulis rano, wieczorny spektakl — pełne zanurzenie w świecie opery",
            Season.Autumn,
            null,
            routeItemsDay));
    }
}