using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Globalization;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace ToJson
{
    internal class Program
    {
        private static ElasticSearchHeadler headler = new ElasticSearchHeadler();


        static void Main(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // 设置当前目录
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) // 加载环境配置
            .AddEnvironmentVariables() // 允许环境变量覆盖配置
            .Build();


            headler = config.GetSection("ElasticSearch").Get<ElasticSearchHeadler>() ?? new ElasticSearchHeadler();

            csv();

            excel();
        }

        private static void csv()
        {
            string[] csvFiles = Directory.GetFiles("./", "*.csv");
            List<TransactionRecord>? recordsd = new List<TransactionRecord>();
            foreach (string s in csvFiles)
            {
                List<AccountRecord> records = new List<AccountRecord>();
                using (StreamReader sr = new StreamReader(s))
                {
                    string csvData = sr.ReadToEnd();
                    using (var reader = new StringReader(csvData))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        records = csv.GetRecords<AccountRecord>().ToList(); // 解析为动态对象
                    }
                }

                string json = JsonConvert.SerializeObject(records);
                recordsd = JsonConvert.DeserializeObject<List<TransactionRecord>>(json);
               
            }

            if (recordsd == null) return;
            string index = "account_records";
            var settings = new ConnectionSettings(new Uri(headler.URL))
                           .BasicAuthentication(headler.User, headler.Password)
                           .DefaultIndex(index) // 设置默认索引
                           .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
            var client = new ElasticClient(settings);

            // 2. 创建索引（如果不存在）
            if (!client.Indices.Exists(index).Exists)
            {
                var createIndexResponse = client.Indices.Create(index, c => c
                    .Map<AccountRecord>(m => m.AutoMap()) // 自动映射字段
                );
            }

            foreach (var record in recordsd)
            {
                var indexResponse = client.IndexDocument(record);

                if (indexResponse.IsValid)
                {
                    Console.WriteLine("插入成功: " + indexResponse.Id);
                }
                else
                {
                    Console.WriteLine("插入失败: " + indexResponse.DebugInformation);
                }
            }
        }

        private static void excel()
        {
            string filePath = @"随手记标准账本20250319.xlsx"; // Excel 文件路径
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 允许免费使用 EPPlus

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                for (int i = 0; i < 2; i++) 
                {
                    var worksheet = package.Workbook.Worksheets[i]; // 读取第一个 Sheet
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;

                    List<Dictionary<string, object>> excelData = new List<Dictionary<string, object>>();

                    // 读取表头
                    string[] headers = new string[colCount];
                    for (int col = 1; col <= colCount; col++)
                    {
                        headers[col - 1] = worksheet.Cells[1, col].Text; // 第一行作为字段名
                    }

                    // 读取数据行
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var rowData = new Dictionary<string, object>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            rowData[headers[col - 1]] = worksheet.Cells[row, col].Text;
                        }
                        excelData.Add(rowData);
                    }

                    // 转换为 JSON
                    string json = JsonConvert.SerializeObject(excelData, Newtonsoft.Json.Formatting.Indented);
                    List<TransactionRecord>? records = JsonConvert.DeserializeObject<List<TransactionRecord>>(json);
                    Console.WriteLine(JsonConvert.SerializeObject(records));
                    if (records == null) { return; }

                    string index = "account_records_new";
                    var settings = new ConnectionSettings(new Uri("https://localhost:9200"))
                                   .BasicAuthentication("elastic", "w19941205B")
                                   .DefaultIndex(index) // 设置默认索引
                                   .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                    var client = new ElasticClient(settings);

                    // 2. 创建索引（如果不存在）
                    if (!client.Indices.Exists(index).Exists)
                    {
                        var createIndexResponse = client.Indices.Create(index, c => c
                            .Map<TransactionRecord>(m => m.AutoMap()) // 自动映射字段
                        );
                    }

                    foreach (var record in records)
                    {
                        var indexResponse = client.IndexDocument(record);

                        if (indexResponse.IsValid)
                        {
                            Console.WriteLine("插入成功: " + indexResponse.Id);
                        }
                        else
                        {
                            Console.WriteLine("插入失败: " + indexResponse.DebugInformation);
                        }
                    }
                }
            }
        }
    }


    public class AccountRecord
    {


        [Name("类型")]
        public string? Type { get; set; } // 类型 (收入/支出)

        [Name("账目名称")]
        public string? AccountName { get; set; } // 账目名称

        [Name("金额")]
        public decimal? Amount { get; set; } // 金额

        [Name("时间")]
        public DateTime? Timestamp { get; set; } // 交易时间

        [Name("备注")]
        public string? Notes { get; set; } // 备注

        [Name("相关图片")]
        public string? RelatedImage { get; set; } // 相关图片
    }

    public class TransactionRecord
    {
        private string accountId = string.Empty;

        [JsonProperty("账目编号")]
        public string AccountId
        {
            get
            {
                return accountId;
            }
            set
            {
                if (value != null)
                {
                    accountId = value.Replace("\t", "");
                }
            }
        } // 账目编号

        [JsonProperty("相关图片")]
        public string? RelatedImage { get; set; } // 相关图片

        [JsonProperty("交易类型")]
        public string? Type { get; set; }  // 支出/收入

        [JsonProperty("日期")]
        public DateTime Timestamp { get; set; }  // 交易时间

        [JsonProperty("分类")]
        public string? Category { get; set; }  // 主要分类

        [JsonProperty("子分类")]
        public string? AccountName { get; set; }  // 细分分类

        [JsonProperty("账户1")]
        public string? AccountPrimary { get; set; }  // 主要账户

        [JsonProperty("账户2")]
        public string? AccountSecondary { get; set; }  // 备用账户

        [JsonProperty("账户币种")]
        public string? Currency { get; set; }  // 币种

        [JsonProperty("金额")]
        public decimal Amount { get; set; }  // 交易金额

        [JsonProperty("成员")]
        public string? Member { get; set; }  // 交易相关成员

        [JsonProperty("商家")]
        public string? Merchant { get; set; }  // 商家信息

        [JsonProperty("项目分类")]
        public string? ProjectCategory { get; set; }  // 项目大类

        [JsonProperty("项目")]
        public string? Project { get; set; }  // 具体项目

        [JsonProperty("记账人")]
        public string? Bookkeeper { get; set; }  // 记账人

        [JsonProperty("备注")]
        public string? Notes { get; set; }  // 备注信息
    }

    public class ElasticSearchHeadler
    {
        public string URL {  get; set; }
        public string User {  get; set; }
        public string Password {  get; set; }

        public ElasticSearchHeadler() 
        {
            this.URL = string.Empty;
            this.User = string.Empty;
            this.Password = string.Empty;
        }
    }
}
