using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using LibraryApi.Models;

namespace LibraryApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryContext>>()))
            {
                
                if (context.LibraryItems.Any())
                {
                    return;   // DB has been seeded
                }

                context.LibraryItems.AddRange(
                    new LibraryItem
                    {
                        Title = "Ostatnich gryzą psy.",
                        Author = "Naval",
                        Edition = "Pierwsze wydanie, 2017",
                        CheckedOutByWhom = "Jan Kowalski",
                        Condition = 2,
                        Category = "Militaria",
                        Description = "Prawdziwe wspomnienia GROM-owca o szczęściu, jakie trzeba w życiu mieć, bo nie zawsze chęci i umiejętności pozwalają, by osiągnąć obrany cel.",
                    },
                    new LibraryItem
                    {
                        Title = "Moja Siła",
                        Author = "Łukasz Grass",
                        Edition = "Pierwsze wydanie, 2021",
                        CheckedOutByWhom = "Bartłomiej Latko",
                        Condition = 1,
                        Category = "Biografie i wspomnienia",
                        Description = "Czy istnieją granice ludzkiej wytrzymałości? Robert Karaś udowadnia, że nie! Ta historia jest tak niewiarygodna, że z powodzeniem mogłaby posłużyć za scenariusz filmu science-fiction. ",
                    },
                    new LibraryItem
                    {
                        Title = "Elektrotechnika",
                        Author = "Stanisław Bolkowski",
                        Edition = "Wydanie 4, 2020",
                        CheckedOutByWhom = "Mateusz Kazak",
                        Condition = 4,
                        Category = "Podręczniki",
                        Description = "Treści podręcznika są zgodne z obowiązującą podstawą programową oraz programem nauczania dla zawodu technik elektryk – przedmiot podstawy elektrotechniki i elektroniki.",
                    },
                    new LibraryItem
                    {
                        Title = "Podręcznik Matematyka 1",
                        Author = "Praca zbiorowa Nowa Era",
                        Edition = "Wydanie 2, 2015",
                        CheckedOutByWhom = "Mateusz Wojciechowski",
                        Condition = 5,
                        Category = "Podręczniki",
                        Description = "Podręcznik dla szkół ponadpodstawowych “MATeMAtyka 1” do zakresu podstawowego i rozszerzonego opracowany przez wydawnictwo Nowa Era.",
                    },
                    new LibraryItem
                    {
                        Title = "Podręcznik Matematyka 2",
                        Author = "Praca zbiorowa Nowa Era",
                        Edition = "Wydanie 3, 2016",
                        CheckedOutByWhom = "Kuba Tomczak",
                        Condition = 2,
                        Category = "Podręczniki",
                        Description = "Podręcznik dla szkół ponadpodstawowych “MATeMAtyka 2” do zakresu podstawowego i rozszerzonego opracowany przez wydawnictwo Nowa Era.",
                    },
                     new LibraryItem
                     {
                         Title = "Podręcznik Matematyka 3",
                         Author = "Praca zbiorowa Nowa Era",
                         Edition = "Wydanie 4, 2018",
                         CheckedOutByWhom = "Wojciech Janicki",
                         Condition = 1,
                         Category = "Podręczniki",
                         Description = "Podręcznik dla szkół ponadpodstawowych “MATeMAtyka 3” do zakresu podstawowego i rozszerzonego opracowany przez wydawnictwo Nowa Era.",
                     }

                );
                context.SaveChanges();
            }
        }
    }
}
