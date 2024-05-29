using System.Data;
using System.Runtime;
using CsvHelper;
using ExcelDataReader;

namespace PortfolioEye.Infrastructure.Services;

public record BondEmissionInfo(
    string Serie,
    string Isin,
    string RedeemDate,
    DateOnly SaleStart,
    DateOnly SaleEnd,
    decimal Price,
    decimal? ConvertPrice,
    decimal? InterestPln,
    decimal Margin,
    List<BondEmissionInterestYear> Years);

public record BondEmissionInterestYear(int YearNo, decimal? InterestRate);

public class BondInformationsReader
{
    public void ReadInformation()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var file = new FileInfo("Dane_dotyczace_obligacji_detalicznych.xls");
        using var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateBinaryReader(stream);
        var dataset = reader.AsDataSet().Tables["EDO"];
        var rows = dataset.Rows;

        var edo = parseEdo(rows);
    }

    private List<BondEmissionInfo> parseEdo(DataRowCollection rows)
    {
        List<BondEmissionInfo> result = new List<BondEmissionInfo>();

        foreach (DataRow row in rows)
        {
            var serie = Convert.ToString(row[0]);
            if (string.IsNullOrEmpty(serie) || !serie.Contains("EDO"))
                continue;
            var isin = Convert.ToString(row[1]);
            var redemDate = Convert.ToString(row[2]);
            var saleBegin = DateOnly.FromDateTime(Convert.ToDateTime(row[3]));
            var saleEnd = DateOnly.FromDateTime(Convert.ToDateTime(row[4]));
            var price = Convert.ToDecimal(row[5]);
            decimal? convertPrice = row[6] == DBNull.Value ? null : Convert.ToDecimal(row[6]);

            var years = new List<BondEmissionInterestYear>();
            for (var i = 0; i < 10; i++)
                years.Add(new BondEmissionInterestYear(i + 1,  row[9 + i] == DBNull.Value ? null : Convert.ToDecimal(row[9 + i])));

            decimal? interestInPln = row[19] == DBNull.Value ? null : Convert.ToDecimal(row[19]);
            var margin = Convert.ToDecimal(row[20]);
            
            var info = new BondEmissionInfo(serie, isin, redemDate, saleBegin, saleEnd, price, convertPrice,
                interestInPln, margin, years);
            result.Add(info);
        }

        return result;
    }
}