using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EasyStore.Infrastructure
{
    public static class ItextSharpHelper
    {
        public static PdfPCell Cell(string property, BaseColor color)
        {
            return new PdfPCell(new Paragraph(property,
                    FontFactory.GetFont("Calibri", 14, color)))
                { HorizontalAlignment = Element.ALIGN_CENTER };
        }
        public static PdfPCell LeftCell(string property, BaseColor color)
        {
            return new PdfPCell(new Paragraph(property,
                    FontFactory.GetFont("Calibri", 10, color)))
                { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0};
        }
        public static PdfPCell LeftCell(string property, BaseColor color, int size)
        {
            return new PdfPCell(new Paragraph(property,
                    FontFactory.GetFont("Calibri", size, color)))
                { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 };
        }
        public static PdfPCell RightCell(string property, BaseColor color)
        {
            return new PdfPCell(new Paragraph(property,
                    FontFactory.GetFont("Calibri", 10, color)))
                { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 };
        }
        public static PdfPCell RightCell(string property, BaseColor color, int size)
        {
            return new PdfPCell(new Paragraph(property,
                    FontFactory.GetFont("Calibri", size, color)))
                { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 };
        }
    }
}
