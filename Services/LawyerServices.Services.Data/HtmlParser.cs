using AngleSharp;
using AngleSharp.Dom;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data
{
    public class HtmlParser : IHtmlParser
    {
        private readonly IDeletableEntityRepository<Court> courtRepository;
        public HtmlParser(IDeletableEntityRepository<Court> courtRepository)
        {
            this.courtRepository = courtRepository;
        }
        public async Task<List<string>> AngleSharpParseAsync(string start, string end, string caseNumber, string year, string courtId)
        {
            List<string> cases = new List<string>();
            var courtUrl = await this.courtRepository.All().FirstOrDefaultAsync(x=>x.Id == courtId);
            var url = String.Format(courtUrl.CourtUrl, start, end, caseNumber, year);
            string html = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        html = await content.ReadAsStringAsync();
                    }
                }
            }
            using var context = BrowsingContext.New(Configuration.Default);
            using var doc = await context.OpenAsync(req => req.Content(html));
            var els = doc.QuerySelectorAll("td");
            if (els.Length > 0)
            {
                foreach (var e in els)
                {
                    cases.Add(e.Text());
                    Console.WriteLine(e.Text());
                }
                //var adas = new Case();
                //adas.NumberOfCase = cases[2];
                //adas.Type = cases[1];
                //adas.Judge = cases[5];
                //adas.Date = DateTime.Parse(cases[7]);
            }

            return cases;
        }
    }
}
