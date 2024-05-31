using System.Data;
using System.Runtime;
using CsvHelper;
using ExcelDataReader;

namespace PortfolioEye.Infrastructure.Services;

public record BondEmissionInfo(
    string Kind,
    string Series,
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

public class BondInformationProvider
{
    public FileStream GetCurrentBondInformation()
    {
        var file = new FileInfo("Dane_dotyczace_obligacji_detalicznych.xls");
        return  File.Open(file.FullName, FileMode.Open, FileAccess.Read);
    }
}

public class BondInformationsReader
{
    public IEnumerable<BondEmissionInfo> ReadInformation(Stream stream)
    {
        List<BondEmissionInfo> result = new List<BondEmissionInfo>();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using var reader = ExcelReaderFactory.CreateBinaryReader(stream);
        var dataset = reader.AsDataSet().Tables["EDO"];
        result.AddRange(ParseEdo(dataset!.Rows));
        dataset = reader.AsDataSet().Tables["ROD"];
        result.AddRange(ParseRod(dataset!.Rows));

        return result;
    }

    private List<BondEmissionInfo> ParseEdo(DataRowCollection rows)
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
            
            var info = new BondEmissionInfo("EDO",serie, isin, redemDate, saleBegin, saleEnd, price, convertPrice,
                interestInPln, margin, years);
            result.Add(info);
        }

        return result;
    }
    
    private List<BondEmissionInfo> ParseRod(DataRowCollection rows)
    {
        List<BondEmissionInfo> result = new List<BondEmissionInfo>();

        foreach (DataRow row in rows)
        {
            var series = Convert.ToString(row[0]);
            if (string.IsNullOrEmpty(series) || !series.Contains("ROD"))
                continue;
            var isin = Convert.ToString(row[1]);
            var redeemDate = Convert.ToString(row[2]);
            var saleBegin = DateOnly.FromDateTime(Convert.ToDateTime(row[3]));
            var saleEnd = DateOnly.FromDateTime(Convert.ToDateTime(row[4]));
            var price = Convert.ToDecimal(row[5]);
            decimal? convertPrice = row[6] == DBNull.Value || "-".Equals(row[6]) ? null : Convert.ToDecimal(row[6]);

            var years = new List<BondEmissionInterestYear>();
            for (var i = 0; i < 12; i++)
                years.Add(new BondEmissionInterestYear(i + 1,  row[9 + i] == DBNull.Value ? null : Convert.ToDecimal(row[9 + i])));

            decimal? interestInPln = row[21] == DBNull.Value ? null : Convert.ToDecimal(row[21]);
            var margin = Convert.ToDecimal(row[22]);
            
            var info = new BondEmissionInfo("ROD",series, isin, redeemDate, saleBegin, saleEnd, price, convertPrice,
                interestInPln, margin, years);
            result.Add(info);
        }

        return result;
    }
}