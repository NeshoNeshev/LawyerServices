namespace LawyerServices.Services.Data
{
    public interface IHtmlParser
    {
        public Task<List<string>> AngleSharpParseAsync(string start, string end, string caseNumber, string year, string courtId);
    }
}
