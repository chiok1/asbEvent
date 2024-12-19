using QRCoder;
using ZXing;
using SkiaSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing.Windows.Compatibility;

namespace asbEvent.Helpers;

public class QRCoder {

    public static byte[] GenerateQRCode(string data) {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
        return qrCodeAsPngByteArr;
    }

    public static string DecodeQRCode(byte[] qrCodeAsPngByteArr) {
        using var ms = new MemoryStream(qrCodeAsPngByteArr);
        using var skBitmap = SKBitmap.Decode(ms);
        var reader = new ZXing.SkiaSharp.BarcodeReader();
        var result = reader.Decode(skBitmap);
        Console.WriteLine(result?.Text ?? string.Empty);
        return result?.Text ?? string.Empty;
    }

    // public static string GenerateQRCode(string data) {
    //     QRCodeGenerator qrGenerator = new QRCodeGenerator();
    //     QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
    //     PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
    //     byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
    //     return Convert.ToBase64String(qrCodeAsPngByteArr);
    // }

    // public static string GenerateQRCodeImage(string data) {
    //     // QRCodeGenerator qrGenerator = new QRCodeGenerator();
    //     // QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
    //     // PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
    //     // byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
    //     // return Convert.ToBase64String(qrCodeAsPngByteArr);
    //     // byte[] qrCodeAsPngByteArr = Convert.FromBase64String(data);
    //     // PngByteQRCode qrCode = new PngByteQRCode(qrCodeAsPngByteArr);
    //     // BitmapByteQRCode qrCodeGraphic = qrCode.GetGraphic(20);
    //     // return qrCodeGraphic;

    // public static Bitmap GenerateQRCodeImage(string base64EncodedString) {
    //     byte[] qrCodeAsPngByteArr = Convert.FromBase64String(base64EncodedString);
    //     using (MemoryStream ms = new MemoryStream(qrCodeAsPngByteArr)) {
    //         Bitmap qrCodeImage = new Bitmap(ms);
    //         return qrCodeImage;
    //     }
    // }
    // }
}