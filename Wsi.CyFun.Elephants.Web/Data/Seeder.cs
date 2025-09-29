using Microsoft.EntityFrameworkCore;
using Wsi.CyFun.Elephants.Core.Entities;

namespace Wsi.CyFun.Elephants.Web.Data;

public class Seeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // MATURITIES
        var maturities = new Maturity[]
        {
            new Maturity
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Threshold = 1,
                Description = "Initial"
            },
            new Maturity
            {
                Id = Guid.Parse("4fa76e53-6826-5673-c4ed-3d874e77b0b7"),
                Threshold = 2,
                Description = "Repeatable"
            },
            new Maturity
            {
                Id = Guid.Parse("5fb67d42-7935-6784-d5fa-4e985d88c1c8"),
                Threshold = 3,
                Description = "Defined"
            }
        };

        // MUNICIPALITIES
        var municipalities = new Municipality[]
        {
            new Municipality
            {
                Id = Guid.Parse("6ec58c31-8a44-7895-e60b-5f096e99d2d9"),
                Name = "Brugge"
            },
            new Municipality
            {
                Id = Guid.Parse("7fd49b20-9b55-89a6-f71c-601a7faad3ea"),
                Name = "Knokke"
            },
            new Municipality
            {
                Id = Guid.Parse("8ae30a09-ac66-9ab7-082d-612bc0bbe4fb"),
                Name = "Gent"
            }
        };

        // ASSESSMENTS
        var assessments = new Assessment[]
        {
            //new Assessment
            //{
            //    Id = Guid.Parse("9bf219fa-cd77-8abc-193e-723cdfbb650c"),
            //    AssessorId = Guid.Parse("a93e1d4b-2c58-4f76-87a1-ccf5a9d1b3e4"), // Peter
            //    MaturityId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), // Initial
            //    ApplicationUserId = Guid.Parse("6f2c8a91-3d47-4f1b-ae9c-71b3d5c4e8f2"), // Bart
            //    MunicipalityId = Guid.Parse("6ec58c31-8a44-7895-e60b-5f096e99d2d9"), // Amsterdam
            //    Created = DateTime.Now
            //},
            //new Assessment
            //{
            //    Id = Guid.Parse("af1e2d0b-de88-9bcd-2a4f-834de1cc761d"),
            //    AssessorId = Guid.Parse("a93e1d4b-2c58-4f76-87a1-ccf5a9d1b3e4"), // Peter
            //    MaturityId = Guid.Parse("4fa76e53-6826-5673-c4ed-3d874e77b0b7"), // Repeatable
            //    ApplicationUserId = Guid.Parse("b7f4c2d1-98ea-4a65-9e20-17d6b3a8f0c9"), // Milleto
            //    MunicipalityId = Guid.Parse("7fd49b20-9b55-89a6-f71c-601a7faad3ea"), // Rotterdam
            //    Created = DateTime.Now
            //}
        };
        var functions = new Function[]
        {
            new Function
            {
                Name = "Identify",
                Description = "",
                Created = DateTime.Now,
                Code = "ID",
                Id = Guid.Parse("8F7D6C5E-B4A3-48D2-9F1E-0C8B7A6D5E4F")
            },
             new Function
             {
                 Name = "Protect",
                 Description = "",
                 Created = DateTime.Now,
                 Code = "PR",
                 Id = Guid.Parse("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6") // fixed
             },
             new Function
             {
                 Name = "Detect",
                 Description = "",
                 Created = DateTime.Now,
                 Code = "DE",
                 Id = Guid.Parse("7e6d5c4b-3a2f-41e0-d9c8-b7a6f5e4d3c2")
             },
             new Function
             {
                 Name = "Respond",
                 Description = "",
                 Created = DateTime.Now,
                 Code = "RS",
                 Id = Guid.Parse("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5") // fixed
             },
             new Function
             {
                 Name = "Recover",
                 Description = "",
                 Created = DateTime.Now,
                 Code = "RC",
                 Id = Guid.Parse("2a3b4c5d-6e7f-48a9-b0c1-d2e3f4a5b6c7") // fixed
             },
        };

        var categories = new Category[]
        {
     new Category
     {
         Name = "Asset Management",
         Description = "",
         Code = "ID.AM",
         Id = Guid.Parse("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), // fixed
         FunctionId = functions[0].Id
     },
     new Category
     {
         Name = "Business Environment ",
         Description = "",
         Code = "ID.BE",
         Id = Guid.Parse("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), // fixed
         FunctionId = functions[0].Id
     },
     new Category
     {
         Name = "Governance",
         Description = "",
         Code = "ID.GV",
         Id = Guid.Parse("6a7b8c9d-0e1f-42a3-b4c5-d6e7f8a9b0c1"), // fixed
         FunctionId = functions[0].Id
     },
     new Category
     {
         Name = "Risk Assessment",
         Description = "",
         Code = "ID.RA",
         Id = Guid.Parse("1a2b3c4d-5e6f-47a8-b9c0-d1e2f3a4b5c6"), // fixed
         FunctionId = functions[0].Id
     },
     new Category
     {
         Name = "Risk Management Strategy",
         Description = "",
         Code = "ID.RM",
         Id = Guid.Parse("4a5b6c7d-8e9f-40a1-b2c3-d4e5f6a7b8c9"), // fixed
         FunctionId = functions[0].Id
     },
     new Category
     {
         Name = "Supply Chain Risk Management",
         Description = "",
         Code = "ID.SC",
         Id = Guid.Parse("b9c8d7e6-f5a4-43b3-a2c1-b0a9c8d7e6f5"), // fixed
         FunctionId = functions[0].Id
     },
     new Category
     {
         Name = "Identity Management, Authentication and Access Control ",
         Description = "",
         Code = "PR.AC",
         Id = Guid.Parse("a4b3c2d1-e0f9-48a8-b7c6-d5e4f3a2b1c0"), // fixed
         FunctionId = functions[1].Id
     },
     new Category
     {
         Name = "Awareness and Training",
         Description = "",
         Code = "PR.AT",
         Id = Guid.Parse("b7c6d5e4-f3a2-49d5-e4f3-a2b1c0d9e8f7"), // fixed
         FunctionId = functions[1].Id
     },
     new Category
     {
         Name = "Data Security",
         Description = "",
         Code = "PR.DS",
         Id = Guid.Parse("c6d5e4f3-a2b1-44a0-b9c8-d7e6f5a4b3c2"), // fixed
         FunctionId = functions[1].Id
     },
     new Category
     {
         Name = "Information Protection Processes and Procedures",
         Description = "",
         Code = "PR.IP",
         Id = Guid.Parse("d5e4f3a2-b1c0-47a1-b0c9-d8e7f6a5b4c3"), // fixed
         FunctionId = functions[1].Id
     },
     new Category
     {
         Name = "Maintenance",
         Description = "",
         Code = "PR.MA",
         Id = Guid.Parse("e4f3a2b1-c0d9-45a7-b6c5-d4e3f2a1b0c9"), // fixed
         FunctionId = functions[1].Id
     },
     new Category
     {
         Name = "Protective Technology",
         Description = "",
         Code = "PR.PT",
         Id = Guid.Parse("f3a2b1c0-d9e8-40a1-b2c3-d4e5f6a7b8c9"), // fixed
         FunctionId = functions[1].Id
     },
     new Category
     {
         Name = "Anomalies and Events",
         Description = "",
         Code = "DE.AE",
         Id = Guid.Parse("a3b2c1d0-e9f8-48a8-b7c6-d5e4f3a2b1c0"), // fixed
         FunctionId = functions[2].Id
     },
     new Category
     {
         Name = "Security Continuous Monitoring",
         Description = "",
         Code = "DE.CM",
         Id = Guid.Parse("b2c1d0e9-f8a7-37a6-b5c4-d3e2f1a0b9c8"), // fixed
         FunctionId = functions[2].Id
     },
     new Category
     {
         Name = "Detection Processes",
         Description = "",
         Code = "DE.DP",
         Id = Guid.Parse("c1d0e9f8-a7b6-78a9-b0c1-d2e3f4a5b6c7"), // fixed
         FunctionId = functions[2].Id
     },
     new Category
     {
         Name = "Detection Processes",
         Description = "",
         Code = "RS.RP",
         Id = Guid.Parse("d0e9f8a7-b6c5-26a3-b2c1-d0e9f8a7b6c5"), // fixed
         FunctionId = functions[3].Id
     },
     new Category
     {
         Name = "Communications",
         Description = "",
         Code = "RS.CO",
         Id = Guid.Parse("e9f8a7b6-c5d4-59a7-b6c5-d4e3f2a1b0c9"), // fixed
         FunctionId = functions[3].Id
     },
     new Category
     {
         Name = "Analysis",
         Description = "",
         Code = "RS.AN",
         Id = Guid.Parse("f8a7b6c5-d4e3-71a8-b9c0-d1e2f3a4b5c6"), // fixed
         FunctionId = functions[3].Id
     },
     new Category
     {
         Name = "Mitigation",
         Description = "",
         Code = "RS.MI",
         Id = Guid.Parse("a7b6c5d4-e3f2-83a2-b1c0-d9e8f7a6b5c4"), // fixed
         FunctionId = functions[3].Id
     },
     new Category
     {
         Name = "Improvements",
         Description = "",
         Code = "RS.IM",
         Id = Guid.Parse("b6c5d4e3-f2a1-94a1-b2c3-d4e5f6a7b8c9"), // fixed
         FunctionId = functions[3].Id
     },
     new Category
     {
         Name = "Recovery Planning",
         Description = "",
         Code = "RC.RP",
         Id = Guid.Parse("c5d4e3f2-a1b0-10a2-b1c9-d8e7f6a5b4c3"), // fixed
         FunctionId = functions[4].Id
     },
     new Category
     {
         Name = "Improvements",
         Description = "",
         Code = "RC.IM",
         Id = Guid.Parse("d4e3f2a1-b0c9-21a4-b3c2-d1e0f9e8d7c6"), // fixed
         FunctionId = functions[4].Id
     },
     new Category
     {
         Name = "Communications",
         Description = "",
         Code = "RC.CO",
         Id = Guid.Parse("e3f2a1b0-c9d8-32a8-b9c0-d1e2f3a4b5c6"), // fixed
         FunctionId = functions[4].Id
     },
        };
        var subCategories = new SubCategory[]
                {
                     //SUBCATEGORIES FOR Asset Management
                     new SubCategory
                     {
                         Id = Guid.Parse("e6f5c4d3-a2b1-44c0-b9d8-e7f6a5b4c3d2"),
                         Description =
                             "Fysieke apparaten en systemen die binnen de organisatie worden gebruikt, worden geïnventariseerd",
                         Code = "ID.AM-1",
                         Created = DateTime.Now,
                         Name = "",
                         Order = 1,
                         CategoryId = categories[0].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("ff81c45e-f75e-4c45-a47c-b5c5055c2161"),
                         Description =
                             "Softwareplatformen, en applicaties die binnen de organisatie worden gebruikt, worden geïnventariseerd",
                         Code = "ID.AM-2",
                         Created = DateTime.Now,
                         Order = 2,
                         Name = "",
                         CategoryId = categories[0].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("d2c19e8a-4b76-4d3f-9e3c-840a123b5d22"),
                         Description = "Organisatiecommunicatie en gegevensstromen worden in kaart gebracht\n",
                         Code = "ID.AM-3",
                         Created = DateTime.Now,
                         Order = 3,
                         Name = "",
                         CategoryId = categories[0].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("3a1b9c5d-7e8f-4a2b-9c3d-5e6f7a8b9c0d"),
                         Description = "Externe informatiesystemen worden gecatalogiseerd",
                         Code = "ID.AM-4",
                         Created = DateTime.Now,
                         Order = 4,
                         Name = "",
                         CategoryId = categories[0].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("9b8c7d6e-5f4e-4d3c-b2a1-0f9e8d7c6b5a"),
                         Description =
                             "Hulpbronnen (bijv. hardware, apparaten, gegevens, tijd, personeel en software) worden geprioriteerd op basis van hun classificatie, kriticiteit en bedrijfswaarde.",
                         Code = "ID.AM-5",
                         Created = DateTime.Now,
                         Order = 5,
                         Name = "",
                         CategoryId = categories[0].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("3f2504e0-4f89-41d3-9a0c-0305e82c3301"),
                         Description =
                             "Cyberbeveiligingsrollen, -verantwoordelijkheden en -bevoegdheden voor het voltallige personeel en externe belanghebbenden zijn vastgesteld",
                         Code = "ID.AM-6",
                         Created = DateTime.Now,
                         Order = 6,
                         Name = "",
                         CategoryId = categories[0].Id
                     },
        
        
        
                     //SUBCATEGORIES FOR Business Enviroment
                     new SubCategory
                     {
                         Id = Guid.Parse("12345678-90ab-cdef-1234-567890abcdef"),
                         Description =
                             "De rol van de organisatie in de toeleveringsketen wordt geïdentificeerd en gecommuniceerd",
                         Code = "ID.BE-1",
                         Created = DateTime.Now,
                         Order = 1,
                         Name = "",
                         CategoryId = categories[1].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("abcdef12-3456-7890-abcd-ef1234567890"),
                         Description =
                             "De plaats van de organisatie in kritieke infrastructuur en haar bedrijfstak wordt geïdentificeerd en gecommuniceerd",
                         Code = "ID.BE-2",
                         Created = DateTime.Now,
                         Order = 2,
                         Name = "",
                         CategoryId = categories[1].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("98765432-10fe-dcba-9876-543210fedcba"),
                         Description =
                             "Prioriteiten voor de missie, doelstellingen en activiteiten van de organisatie worden vastgesteld en gecommuniceerd",
                         Code = "ID.BE-3",
                         Created = DateTime.Now,
                         Order = 3,
                         Name = "",
                         CategoryId = categories[1].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("fedcba98-7654-3210-fedc-ba9876543210"),
                         Description =
                             "Afhankelijkheden en kritieke functies voor de levering van kritieke diensten zijn vastgesteld",
                         Code = "ID.BE-4",
                         Created = DateTime.Now,
                         Order = 4,
                         Name = "",
                         CategoryId = categories[1].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("13579bdf-2468-ace0-1357-9bdf2468ace0"),
                         Description =
                             "De veerkrachtvereisten ter ondersteuning van de levering van kritieke diensten zijn vastgesteld voor alle operationele toestanden (bijv. onder dwang/aanval, tijdens herstel, normale activiteiten)",
                         Code = "ID.BE-5",
                         Created = DateTime.Now,
                         Order = 5,
                         Name = "",
                         CategoryId = categories[1].Id
                     },
        
        
        
                     //SUBCATEGORIES FOR Governance
                     new SubCategory
                     {
                         Id = Guid.Parse("2468ace0-1357-9bdf-2468-ace01357bdf2"),
                         Description = "Het cyberbeveiligingsbeleid van de organisatie wordt vastgesteld en gecommuniceerd",
                         Code = "ID.GV-1",
                         Created = DateTime.Now,
                         Order = 1,
                         Name = "",
                         CategoryId = categories[2].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"),
                         Description =
                             "Wettelijke en regelgevende vereisten met betrekking tot cyberbeveiliging, inclusief verplichtingen op het gebied van privacy en burgerlijke vrijheden, worden begrepen en beheerd.",
                         Code = "ID.GV-3",
                         Created = DateTime.Now,
                         Order = 2,
                         Name = "",
                         CategoryId = categories[2].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("b2c3d4e5-f6a1-b2c3-d4e5-f6a1b2c3d4e5"),
                         Description = "Governance- en risicobeheerprocessen richten zich op cyberbeveiligingsrisico's",
                         Code = "ID.GV-4",
                         Created = DateTime.Now,
                         Order = 3,
                         Name = "",
                         CategoryId = categories[2].Id
                     },
        
        
                     //SUBCATEGORIES FOR Risk Assessment
                     new SubCategory
                     {
                         Id = Guid.Parse("c3d4e5f6-a1b2-c3d4-e5f6-a1b2c3d4e5f6"),
                         Description = "Kwetsbaarheden van bedrijfsmiddelen worden geïdentificeerd en gedocumenteerd",
                         Code = "ID.RA-1",
                         Created = DateTime.Now,
                         Order = 1,
                         Name = "",
                         CategoryId = categories[3].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("d4e5f6a1-b2c3-d4e5-f6a1-b2c3d4e5f6a1"),
                         Description =
                             "Informatie over cyberdreigingen wordt ontvangen van forums en bronnen waar informatie wordt gedeeld",
                         Code = "ID.RA-2",
                         Created = DateTime.Now,
                         Order = 2,
                         Name = "",
                         CategoryId = categories[3].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("e5f6a1b2-c3d4-e5f6-a1b2-c3d4e5f6a1b2"),
                         Description =
                             "Bedreigingen, kwetsbaarheden, waarschijnlijkheden en gevolgen worden gebruikt om risico's te bepalen.",
                         Code = "ID.RA-5",
                         Created = DateTime.Now,
                         Order = 3,
                         Name = "",
                         CategoryId = categories[3].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("f6a1b2c3-d4e5-f6a1-b2c3-d4e5f6a1b2c3"),
                         Description = "Risicomaatregelen worden geïdentificeerd en geprioriteerd",
                         Code = "ID.RA-6",
                         Created = DateTime.Now,
                         Order = 4,
                         Name = "",
                         CategoryId = categories[3].Id
                     },
        
        
                     //SUBCATEGORIES FOR Risk Management Strategy
                     new SubCategory
                     {
                         Id = Guid.Parse("a1b2c3d4-e5f6-a1b2-c3d4-e5f6a1b2c3d4"),
                         Description = "Risicomaatregelen worden geïdentificeerd en geprioriteerd",
                         Code = "ID.RM-1",
                         Created = DateTime.Now,
                         Order = 1,
                         Name = "",
                         CategoryId = categories[4].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("b2c3d4e5-f6a1-b2c3-d4e5-f67890123456"),
                         Description = "Organisatorische risicotolerantie wordt bepaald en duidelijk uitgedrukt",
                         Code = "ID.RM-2",
                         Created = DateTime.Now,
                         Order = 2,
                         Name = "",
                         CategoryId = categories[4].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("c3d4e5f6-a1b2-c3d4-e5f6-789012345678"),
                         Description =
                             "De organisatie bepaalt haar risicotolerantie op basis van haar rol in kritieke infrastructuur en sectorspecifieke risicoanalyse.",
                         Code = "ID.RM-3",
                         Created = DateTime.Now,
                         Order = 3,
                         Name = "",
                         CategoryId = categories[4].Id
                     },
        
        
                     //SUBCATEGORIES FOR Supply Chain Risk Management
        
                     new SubCategory
                     {
                         Id = Guid.Parse("4d7f2b98-1e6c-4a3d-90e1-7b2a5fc31d49"),
                         Description =
                             "Leveranciers en externe partners van informatiesystemen, onderdelen en diensten worden geïdentificeerd, geprioriteerd en beoordeeld met behulp van een proces voor de beoordeling van cyberrisico's in de toeleveringsketen.",
                         Code = "ID.SC-2",
                         Created = DateTime.Now,
                         Order = 1,
                         Name = "",
                         CategoryId = categories[5].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("9c3e7a2f-6d41-4b19-ae5f-83f1c728d2b0"),
                         Description =
                             "Contracten met leveranciers en externe partners worden gebruikt om passende maatregelen te implementeren die zijn ontworpen om te voldoen aan de doelstellingen van het cyberbeveiligingsprogramma en het risicobeheerplan voor de cyberketen van een organisatie.",
                         Code = "ID.SC-3",
                         Created = DateTime.Now,
                         Order = 2,
                         Name = "",
                         CategoryId = categories[5].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("a2d4c7f9-8b3e-45a0-b1c2-73f0e4d19f6e"),
                         Description =
                             "Leveranciers en externe partners worden routinematig beoordeeld met behulp van audits, testresultaten of andere vormen van evaluaties om te bevestigen dat ze aan hun contractuele verplichtingen voldoen.",
                         Code = "ID.SC-4",
                         Created = DateTime.Now,
                         Order = 3,
                         Name = "",
                         CategoryId = categories[5].Id
                     },
                     new SubCategory
                     {
                         Id = Guid.Parse("f5b1a8c3-2e4d-4c76-9d9f-2a6e1c9b8a71"),
                         Description =
                             "Reactie- en herstelplanning en tests worden uitgevoerd met leveranciers en externe leveranciers",
                         Code = "ID.SC-5",
                         Created = DateTime.Now,
                         Order = 4,
                         Name = "",
                         CategoryId = categories[5].Id
                     },
                };
        //
        //         //REQUIREMENTS 
        //
        var requirements = new Requirement[]
         {
        //     //REQUIREMENTS FOR ID.AM-1
             new Requirement
             {
                 Id = Guid.Parse("5a8e1d9c-7f4b-4c6f-b2e3-1d9a7f6e4c8b"),
                 Description = "Een inventarisatie van bedrijfsmiddelen die verband houden met informatie en informatieverwerkingsfaciliteiten binnen de organisatie moet worden gedocumenteerd, herzien en bijgewerkt wanneer zich wijzigingen voordoen.",
                 Code = "ID.AM-1.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 1,
                 SubCategoryId = subCategories[0].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("7b6e2a0c-6f42-4e2c-9a2a-0a1b2c3d4e5f"),
                 Description = "De inventaris van bedrijfsmiddelen die verband houden met informatie en informatieverwerkingsfaciliteiten moet veranderingen in de  context van de organisatie weerspiegelen  en alle informatie bevatten die nodig is voor effectieve verantwoording.",
                 Code = "ID.AM-1.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 2,
                 SubCategoryId = subCategories[0].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("c4f3e2d1-b0a9-4876-9c8b-1a2b3c4d5e6f"),
                 Description = "Wanneer ongeautoriseerde hardware wordt gedetecteerd, wordt deze in quarantaine geplaatst voor mogelijke uitzonderingsbehandeling, verwijderd of vervangen en wordt de inventaris bijgewerkt.",
                 Code = "ID.AM-1.3",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 3,
                 SubCategoryId = subCategories[0].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5d4c3b2a"),
                 Description = "Een inventaris die weergeeft welke softwareplatforms en applicaties in de organisatie worden gebruikt, moet worden gedocumenteerd, herzien en bijgewerkt wanneer zich wijzigingen voordoen.",
                 Code = "ID.AM-2.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 4,
                 SubCategoryId = subCategories[0].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("2e3f4d5c-6b7a-8c9d-0e1f-2d3e4f5a6b7c"),
                 Description = "De inventarisatie van softwareplatforms en applicaties die verband houden met informatie en informatieverwerking moet veranderingen in de  context van de organisatie weerspiegelen  en alle informatie bevatten die nodig is voor effectieve verantwoording.",
                 Code = "ID.AM-2.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 5,
                 SubCategoryId = subCategories[1].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("f1e2d3c4-b5a6-7987-6543-210fedcba987"),
                 Description = "De personen die verantwoordelijk en aansprakelijk zijn voor het beheer van softwareplatforms en applicaties binnen de organisatie moeten worden geïdentificeerd.",
                 Code = "ID.AM-2.3",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 6,
                 SubCategoryId = subCategories[1].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                 Description = "Wanneer niet-geautoriseerde software wordt gedetecteerd, wordt deze in quarantaine geplaatst voor mogelijke uitzonderingsbehandeling, verwijderd of vervangen en wordt de inventaris dienovereenkomstig bijgewerkt.",
                 Code = "ID.AM-2.4",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 7,
                 SubCategoryId = subCategories[1].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("a9b8c7d6-e5f4-3a2b-1c0d-9e8f7a6b5c4d"),
                 Description = "Informatie die de organisatie opslaat en gebruikt, moet worden geïdentificeerd.",
                 Code = "ID.AM-3.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 8,
                 SubCategoryId = subCategories[2].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("5e4d3c2b-1a9f-8e7d-6c5b-4a3f2e1d0c9b"),
                 Description = "Alle verbindingen binnen de ICT/OT-omgeving van de organisatie en met andere interne platforms van de organisatie moeten in kaart worden gebracht, gedocumenteerd, goedgekeurd en waar nodig bijgewerkt.",
                 Code = "ID.AM-3.3",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 9,
                 SubCategoryId = subCategories[2].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("2a3b4c5d-6e7f-8a9b-0c1d-2e3f4a5b6c7d"),
                 Description = "De organisatie moet alle externe services en de verbindingen die ermee zijn gemaakt in kaart brengen, documenteren, autoriseren en bij wijzigingen bijwerken.",
                 Code = "ID.AM-4.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 10,
                 SubCategoryId = subCategories[3].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("8f9e0d1c-2b3a-4d5e-6f7a-8b9c0d1e2f3a"),
                 Description = "De middelen van de organisatie (hardware, apparaten, gegevens, tijd, personeel, informatie en software) moeten worden geprioriteerd op basis van hun classificatie, kriticiteit en bedrijfswaarde.",
                 Code = "ID.AM-5.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 11,
                 SubCategoryId = subCategories[4].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e"),
                 Description = "De rollen, verantwoordelijkheden en bevoegdheden op het gebied van informatiebeveiliging en cyberbeveiliging binnen de organisatie moeten worden gedocumenteerd.",
                 Code = "ID.AM-6.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = true,
                 Order = 12,
                 SubCategoryId = subCategories[5].Id,

             },



             new Requirement
             {
                 Id = Guid.Parse("c1d2e3f4-a5b6-c7d8-e9f0-a1b2c3d4e5f6"),
                 Description = "De rol van de organisatie in de toeleveringsketen moet worden vastgesteld, gedocumenteerd en gecommuniceerd.",
                 Code = "ID.BE-1.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 13,
                 SubCategoryId = subCategories[6].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d"),
                 Description = "De plaats van de organisatie in kritieke infrastructuur en haar bedrijfstak moet worden vastgesteld en gecommuniceerd.",
                 Code = "ID.BE-2.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 14,
                 SubCategoryId = subCategories[7].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("3e4f5a6b-7c8d-9e0f-1a2b-3c4d5e6f7a8b"),
                 Description = "Prioriteiten voor de missie, doelstellingen en activiteiten van de organisatie worden vastgesteld en gecommuniceerd.",
                 Code = "ID.BE-3.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 15,
                 SubCategoryId = subCategories[8].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("9c0b1a2d-3e4f-5c6b-7a8d-9e0f1a2b3c4d"),
                 Description = "Afhankelijkheden en missiekritische functies voor de levering van kritieke diensten worden geïdentificeerd, gedocumenteerd en geprioriteerd op basis van hun kriticiteit als onderdeel van het risicobeoordelingsproces.",
                 Code = "ID.BE-4.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 16,
                 SubCategoryId = subCategories[9].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("5f6e7d8c-9b0a-1d2e-3f4c-5d6e7f8a9b0c"),
                 Description = "Om de cyberweerbaarheid te ondersteunen en de levering van kritieke diensten te beveiligen, worden de nodige vereisten geïdentificeerd, gedocumenteerd en hun implementatie getest en goedgekeurd.",
                 Code = "ID.BE-5.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 17,
                 SubCategoryId = subCategories[10].Id,
             },



             new Requirement
             {
                 Id = Guid.Parse("1d2c3b4a-5f6e-7d8c-9b0a-1d2e3f4c5d6e"),
                 Description = "Beleid en procedures voor informatiebeveiliging en cyberveiligheid worden opgesteld, gedocumenteerd, beoordeeld, goedgekeurd en bijgewerkt wanneer zich wijzigingen voordoen.",
                 Code = "ID.GV-1.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 18,
                 SubCategoryId = subCategories[11].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("7f8e9d0c-1b2a-3d4c-5e6f-7a8b9c0d1e2f"),
                 Description = "Er moet een informatiebeveiligings- en cyberbeveiligingsbeleid voor de hele organisatie worden opgesteld, gedocumenteerd, bijgewerkt wanneer zich wijzigingen voordoen, verspreid en goedgekeurd door het senior management.",
                 Code = "ID.GV-1.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 19,
                 SubCategoryId = subCategories[11].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("3a4b5c6d-7e8f-9a0b-1c2d-3e4f5a6b7c8d"),
                 Description = "Wettelijke en regelgevende vereisten met betrekking tot informatie-/cyberbeveiliging, waaronder privacyverplichtingen, worden begrepen en geïmplementeerd.",
                 Code = "ID.GV-3.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 20,
                 SubCategoryId = subCategories[12].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("9e0f1a2b-3c4d-5e6f-7a8b-9c0d1e2f3a4b"),
                 Description = "Wettelijke en regelgevende vereisten met betrekking tot informatie-/cyberbeveiliging, waaronder privacyverplichtingen, worden beheerd.",
                 Code = "ID.GV-3.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 21,
                 SubCategoryId = subCategories[12].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("e84b7f21-3c9a-4d5e-91af-2d7c3b8a4e90 "),
                 Description = "Als onderdeel van het algehele risicobeheer van het bedrijf moet een alomvattende strategie voor het beheer van informatiebeveiliging en cyberbeveiligingsrisico's worden ontwikkeld en bijgewerkt wanneer zich veranderingen voordoen.",
                 Code = "ID.GV-4.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 22,
                 SubCategoryId = subCategories[13].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("1a7f6c3d-b452-44f9-8e01-9c2a17d3fcb4"),
                 Description = "Informatiebeveiligings- en cyberbeveiligingsrisico's worden gedocumenteerd, formeel goedgekeurd en bijgewerkt wanneer zich wijzigingen voordoen.",
                 Code = "ID.GV-4.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 23,
                 SubCategoryId = subCategories[13].Id,

             },




             new Requirement
             {
                 Id = Guid.Parse("d3a5f6c2-8b71-4e4f-9c3a-1f25b7e2a6d8"),
                 Description = "Bedreigingen en kwetsbaarheden moeten worden geïdentificeerd.",
                 Code = "ID.RA-1.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 24,
                 SubCategoryId = subCategories[14].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("7c2f4b91-5de3-470a-bc12-e8f3d6a9c014"),
                 Description = "Er moet een proces worden vastgesteld om kwetsbaarheden van de bedrijfskritische systemen van de organisatie continu te bewaken, te identificeren en te documenteren.",
                 Code = "ID.RA-1.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 25,
                 SubCategoryId = subCategories[14].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("b19e3a74-2f5c-495e-a4c1-03ea47b6e2d3"),
                 Description = "Er moet een bewustwordingsprogramma voor bedreigingen en kwetsbaarheden worden geïmplementeerd dat de mogelijkheid omvat om informatie uit te wisselen tussen organisaties.",
                 Code = "ID.RA-2.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 26,
                 SubCategoryId = subCategories[15].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("f1c8d2e4-3a9b-4d62-95b0-2f7a8c1d6e53"),
                 Description = "De organisatie moet risicobeoordelingen uitvoeren waarbij het risico wordt bepaald door bedreigingen, kwetsbaarheden en de impact op bedrijfsprocessen en bedrijfsmiddelen.",
                 Code = "ID.RA-5.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 27,
                 SubCategoryId = subCategories[16].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("ae42b790-1fd3-4e88-b7c6-8c4e2d1f9a35"),
                 Description = "De organisatie moet risicobeoordelingen uitvoeren en documenteren waarin het risico wordt bepaald door bedreigingen, kwetsbaarheden, de impact op bedrijfsprocessen en bedrijfsmiddelen en de waarschijnlijkheid dat deze zich voordoen.",
                 Code = "ID.RA-5.2",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 28,
                 SubCategoryId = subCategories[16].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("6d3f9b12-4c7e-42a3-9fd2-b5a0e61c2784"),
                 Description = "Er moet een allesomvattende strategie worden ontwikkeld en ge誰mplementeerd om de risico's voor de kritieke systemen van de organisatie te beheren, inclusief de identificatie en prioritering van risicomaatregelen.",
                 Code = "ID.RA-6.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 29,
                 SubCategoryId = subCategories[17].Id,

             },






             new Requirement
             {
                 Id = Guid.Parse("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"),
                 Description = "Een cyberrisicobeheerproces dat de belangrijkste interne en externe belanghebbenden identificeert en het aanpakken van risicogerelateerde kwesties en informatie vergemakkelijkt, moet worden gecreëerd, gedocumenteerd, herzien, goedgekeurd en bijgewerkt wanneer zich wijzigingen voordoen.",
                 Code = "ID.RM-1.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 30,
                 SubCategoryId = subCategories[18].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("9a0b1c2d-3e4f-5a6b-7c8d-9e0f1a2b3c4d"),
                 Description = "De organisatie moet duidelijk haar risicobereidheid bepalen.",
                 Code = "ID.RM-2.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 31,
                 SubCategoryId = subCategories[19].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("5e6f7a8b-9c0d-1e2f-3a4b-5c6d7e8f9a0b"),
                 Description = "De rol van de organisatie in kritieke infrastructuur en de sector bepalen de risicobereidheid van de organisatie.",
                 Code = "ID.RM-3.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 32,
                 SubCategoryId = subCategories[20].Id,

             },




             new Requirement
             {
                 Id = Guid.Parse("3e5a9c84-7b1f-4d0c-8fa1-92c4b7e3d2a9"),
                 Description = "De organisatie moet minstens een keer per jaar risicobeoordelingen van de cybertoeleveringsketen uitvoeren of wanneer zich een wijziging voordoet in de kritieke systemen, de operationele omgeving of de toeleveringsketen van de organisatie; deze beoordelingen moeten worden gedocumenteerd en de resultaten moeten worden verspreid onder relevante belanghebbenden, waaronder degenen die verantwoordelijk zijn voor ICT/OT-systemen.",
                 Code = "ID.SC-2.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 33,
                 SubCategoryId = subCategories[21].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("9fd2713a-bd4e-4f16-a802-3c7f14e0d6c1"),
                 Description = "Op basis van de resultaten van de risicobeoordeling van de cybertoeleveringsketen wordt een contractueel kader voor leveranciers en externe partners opgesteld om het delen van gevoelige informatie en gedistribueerde en onderling verbonden ICT/OT-producten en -diensten aan te pakken.",
                 Code = "ID.SC-3.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 34,
                 SubCategoryId = subCategories[22].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("12a4c7d9-8f30-462e-981a-6a5bd2e93f5e"),
                 Description = "De organisatie controleert beoordelingen van de naleving van contractuele verplichtingen door leveranciers en externe partners door routinematig audits, testresultaten en andere evaluaties te controleren.",
                 Code = "ID.SC-4.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 35,
                 SubCategoryId = subCategories[23].Id,

             },
             new Requirement
             {
                 Id = Guid.Parse("9d8c7b6a-5f4e-3d2c-1b0a-9f8e7d6c5b4a"),
                 Description = "De organisatie moet de belangrijkste medewerkers van leveranciers en externe partners identificeren en documenteren om hen als belanghebbenden te betrekken bij de reactie- en herstelplanningsactiviteiten.",
                 Code = "ID.SC-5.1",
                 Created = DateTime.Now,
                 IsKeyMeasurment = false,
                 Order = 36,
                 SubCategoryId = subCategories[24].Id,

             },

         };

        // GUIDANCES

        var guidances = new Guidance[]
        {
                      new Guidance
                      {
                          Id = Guid.Parse("c1a8e3f0-2b3c-4d95-8f66-9a7b12345678 "),
                          Description = "Deze inventaris omvat vaste en draagbare computers, tablets, mobiele telefoons, PLC's (Programmable Logic Controllers), sensoren, actuatoren, robots, bewerkingsmachines, firmware, netwerkschakelaars, routers, voedingen en andere netwerkcomponenten of -apparaten.",
                          Created = DateTime.Now,
                          Order = 1,
                          RequirementId = requirements[0].Id,
                          IsTitle = false
                      },
                      new Guidance
                      {
                          Id = Guid.Parse("7f9e1d24-5a3f-4e6a-9b2d-0f1e2c3a4b5c"),
                          Description = "Deze inventaris moet alle bedrijfsmiddelen bevatten, ongeacht of ze zijn aangesloten op het netwerk van de organisatie.",
                          Created = DateTime.Now,
                          Order = 2,
                          RequirementId = requirements[0].Id,
                          IsTitle = false
                      },
                      new Guidance
                      {
                          Id = Guid.Parse("d7b91c8f-b5a2-4b42-bfeb-395bbaaa89f0"),
                          Description = "Het gebruik van een IT asset management tool kan worden overwogen.",
                          Created = DateTime.Now,
                          Order = 3,
                          RequirementId = requirements[0].Id,
                          IsTitle = false
                      },


                  };

        //APPLICATIONUSERS

        var applicationUsers = new ApplicationUser[]
        {
            new ApplicationUser
            {
                Id = Guid.Parse("6f2c8a91-3d47-4f1b-ae9c-71b3d5c4e8f2"),
                Created = DateTime.Now,
                IsAssessor = false,
                IsAdmin = false,
                Username = "Bart"
            },
            new ApplicationUser
            {
                Id = Guid.Parse("a93e1d4b-2c58-4f76-87a1-ccf5a9d1b3e4"),
                Created = DateTime.Now,
                IsAssessor = true,
                IsAdmin = false,
                Username = "Peter"
            },
            new ApplicationUser
            {
                Id = Guid.Parse("b7f4c2d1-98ea-4a65-9e20-17d6b3a8f0c9"),
                Created = DateTime.Now,
                IsAssessor = false,
                IsAdmin = true,
                Username = "Milleto"
            }

        };



        modelBuilder.Entity<Function>().HasData(functions);
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<SubCategory>().HasData(subCategories);
        modelBuilder.Entity<Requirement>().HasData(requirements);
        modelBuilder.Entity<Guidance>().HasData(guidances);
        modelBuilder.Entity<ApplicationUser>().HasData(applicationUsers);
        modelBuilder.Entity<Maturity>().HasData(maturities);
        modelBuilder.Entity<Municipality>().HasData(municipalities);
        modelBuilder.Entity<Assessment>().HasData(assessments);
    }
}
