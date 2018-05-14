using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class InfoServices
    {
        private RestClient<Info> _restClient = new RestClient<Info>();

        public async Task<List<Info>> GetInfoForMonthAsync(DateTime now)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/info";

            var listOfInfo = await _restClient.GetAsync();

            return listOfInfo;
            //await Task.Delay(1000);
            //return new List<Info>()
            //{
            //    new Info
            //    {
            //        Id = 1,
            //        Creator = new Player
            //        {
            //            Id = 3,
            //            Name = "Milan Mino Lietava"
            //        },
            //        CreationDateTime = new DateTime(2018, 2, 27),
            //        Categories = new List<Category>()
            //        {
            //            new Category
            //            {
            //                Id = 1,
            //                Name = "Muži"
            //            },
            //            new Category
            //            {
            //                Id=  2,
            //                Name = "Juniori"
            //            },
            //            new Category
            //            {
            //                Id = 3,
            //                Name = "Dorast"
            //            }
            //        },
            //        Content =
            //            "Ahojte, po dnesnom zapase JEX mi pisal Andrej Vrabel, ci sme sa (s Venom po zapase) o nom nerozpravali pokial ide o jeho hernu cinnost a oblasti, v ktorych by sa mohol zlepsit.\n" +
            //            "Priviedlo ma to k myslienke, ci by nestalo za to sa s hracmi, ktori o to stoja porozpravat \"medzi 4 ocami\" prave na podobne temy - co zlepsit, co robia dobre a podobne - dat im spatnu vazbu. Verim, ze niektorych by to motivovalo a nieco by si z rozhovoru odniesli.\n" +
            //            "Moja predstava - napisat mail, ze kto z hracov ma zaujem, nech sa oslovi svojho trenera a poziada o podobny rozhovor. Pred alebo po niektorom treningu cca 5-10 minut.\n" +
            //            "Co to moze priniest:\n" +
            //            "1. trener zisti kto ma zaujem\n" +
            //            "2. hrac ziska spatnu vazbu a ocakavam, ze u hraca v danej oblasti aj nastanu zmeny\n" +
            //            "3. z rozhovoru sa trener mozno dozvie co by sa inak nedozvedel\n" +

            //            "Konkretne Andrej si spatnu vazbu na svoju hru uz niekolko krat v minulosti pytal. Po jeho domacej premiere v MI som mu opat nieco povedal - zakazdym uprimne podakuje hoci je to len detail. A pyta si znova. Vidiet na nom, ze sa chce zlepsovat a jeho zaujem nie je kratkodoby.\n" +
            //            "Andrej u mna v tomto smere vycnieva. Mozno mate podobne skusenosti s inymi hracmi, ktorych trenujete.\n" +
            //            "Roman uz v minulosti mal podobnu iniciativu na fungovanie jednotlivcov. Galo tusim tiez, uz neviem na co to bolo zamerane. Toto je nieco podobne.\n" +
            //            "Ak mate potrebu komentovat, nech sa paci. Len som nechcel, aby to ostalo u mna v sufliku, mozno viete vyuzite hociktoru informaciu z majlu po svojom.\n" +
            //            "zdravi,\n" +
            //            "milan",
            //        IsGridDeleteEditInfoVisible = true
            //    },
            //    new Info
            //    {
            //        Id = 2,
            //        Creator = new Player
            //        {
            //            Id = 3,
            //            Name = "Milan Mino Lietava"
            //        },
            //        CreationDateTime = new DateTime(2018, 2, 26),
            //        Categories = new List<Category>()
            //        {
            //            new Category
            //            {
            //                Id = 2,
            //                Name = "Juniori"
            //            }
            //        },
            //        Content =
            //            "cavte v sobotu na POJEX:\n" +
            //            "zraz UTV 9:30\n" +
            //            "Samuel Hazucha\n" +
            //            "Jakub Šuška\n" +
            //            "Ondrej Kubička\n" +
            //            "Andrej Vrábel\n" +
            //            "Michal Tulák\n" +
            //            "Ladislav Mirt\n" +
            //            "Sebastian Ftorek\n" +
            //            "Erik Gavlák\n" +
            //            "Jakub Malík\n" +
            //            "Richard Cvacho\n" +
            //            "Jakub Kelbel\n" +
            //            "Marek Hrobar\n" +
            //            "Matej Michalcik\n" +
            //            "Matúš Zelník\n" +
            //            "Adam Syptak\n" +
            //            "Jakub Matejička",
            //        IsGridDeleteEditInfoVisible = true
            //    }
            //};
        }

        public async Task<bool> PushInfoAsync(Info selectedInfo)
        {
            Player creator = new Player();
            creator.Id = Settings.IdPlayer;
            selectedInfo.Creator = creator;

            _restClient.Resource = "info";

            var success = await _restClient.PostAsync(selectedInfo);

            return success;
        }

        public async Task<bool> DeleteInfoAsync(int id)
        {
            _restClient.Resource = "info/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }

        public async Task<bool> PutInfoAsync(int id, Info selectedInfo)
        {
            _restClient.Resource = "info";
            var success = await _restClient.PutAsync(selectedInfo);
            return success;
        }
    }
}
