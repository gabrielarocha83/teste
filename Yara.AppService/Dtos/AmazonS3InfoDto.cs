using System;

namespace Yara.AppService.Dtos
{
    public class AmazonS3InfoDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Size { get; set; }
    }

    public class AmazonS3SearchDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
