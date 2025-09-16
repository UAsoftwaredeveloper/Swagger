
using System.Text.Json;

public interface ICaptureService
{
    void CaptureIfValid(string json);
}

public class CaptureService : ICaptureService
{
    private readonly IBlockchainWriter _blockchainWriter;

    public CaptureService(IBlockchainWriter blockchainWriter)
    {
        _blockchainWriter = blockchainWriter;
    }

    public void CaptureIfValid(string json)
    {
        try
        {
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var captured = new CapturedInquiry();

            string[] possibleKeys = { "inquiry", "inquery", "enquery" };

            bool matchFound = false;

            foreach (var key in possibleKeys)
            {
                if (root.TryGetProperty(key, out var value))
                {
                    captured.EnquiryData = value.GetRawText();
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
            {
                captured.EnquiryData = root.GetRawText();
            }

            _blockchainWriter.Write(captured);
        }
        catch
        {
            // Ignore invalid or non-matching payloads
        }
    }
}
