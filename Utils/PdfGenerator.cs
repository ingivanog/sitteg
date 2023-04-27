using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using iText = iTextSharp.text;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GuanajuatoAdminUsuarios.Utils
{
    public class PdfGenerator<T> : IPdfGenerator<T> where T : class
    {
        //private readonly List<T> _enum;

        //public PdfGenerator(List<T> enumerator)
        //{
        //    _enum = enumerator;
        //}

        public (MemoryStream, string) CreatePdf(List<T> ModelData, string NamePdf, int SizeColumns, string title, string[] ColumnsNames)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;

            string strPDFFileName = string.Format(NamePdf + dTime.ToString("yyyyMMdd") + "-" + ".pdf");

            iText.Document doc = new iText.Document();
            doc.SetMargins(0, 0, 0, 0);

            PdfPTable tableLayout = new PdfPTable(SizeColumns);
            doc.SetMargins(10, 10, 10, 0);
            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontInvoice = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.NORMAL);
            iText.Paragraph paragraph = new iText.Paragraph(title, fontInvoice);
            paragraph.Alignment = iText.Element.ALIGN_CENTER;
            doc.Add(paragraph);
            iText.Paragraph p3 = new iText.Paragraph();
            p3.SpacingAfter = 6;
            doc.Add(p3);

            doc.Add(Add_Content_To_PDF(tableLayout, ModelData, ColumnsNames));

            doc.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return (workStream, strPDFFileName);
            //return File(workStream, "application/pdf", strPDFFileName);
        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, List<T> ModelData, string[] ColumnsNames)
        {
            float[] headers = { 50, 24, 45, 35 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            var count = 1;
            //Add header  
            AddCellToHeader(tableLayout, "CustomerName");
            AddCellToHeader(tableLayout, "Address");
            AddCellToHeader(tableLayout, "Email");
            AddCellToHeader(tableLayout, "ZipCode");

            foreach (var objectItem in ModelData)
            {
                if (count >= 1)
                {
                    AddCellToBody(tableLayout, objectItem.ToString(), count);
                    AddCellToBody(tableLayout, objectItem.ToString(), count);
                    AddCellToBody(tableLayout, objectItem.ToString(), count);
                    AddCellToBody(tableLayout, objectItem.ToString(), count);
                    count++;
                }
            }


            //foreach (var cust in customerData())
            //{
            //    if (count >= 1)
            //    {
            //        //Add body  
            //        AddCellToBody(tableLayout, cust.CustomerName.ToString(), count);
            //        AddCellToBody(tableLayout, cust.Address.ToString(), count);
            //        AddCellToBody(tableLayout, cust.Email.ToString(), count);
            //        AddCellToBody(tableLayout, cust.ZipCode.ToString(), count);
            //        count++;
            //    }
            //}
            return tableLayout;
        }

        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new iText.Phrase(cellText, new iText.Font(iText.Font.FontFamily.HELVETICA, 8, 1, iText.BaseColor.BLACK)))
            {
                HorizontalAlignment = iText.Element.ALIGN_LEFT,
                Padding = 8,
                BackgroundColor = new iText.BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText, int count)
        {
            if (count % 2 == 0)
            {
                tableLayout.AddCell(new PdfPCell(new iText.Phrase(cellText, new iText.Font(iText.Font.FontFamily.HELVETICA, 8, 1, iText.BaseColor.BLACK)))
                {
                    HorizontalAlignment = iText.Element.ALIGN_LEFT,
                    Padding = 8,
                    BackgroundColor = new iText.BaseColor(255, 255, 255)
                });
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new iText.Phrase(cellText, new iText.Font(iText.Font.FontFamily.HELVETICA, 8, 1, iText.BaseColor.BLACK)))
                {
                    HorizontalAlignment = iText.Element.ALIGN_LEFT,
                    Padding = 8,
                    BackgroundColor = new iText.BaseColor(211, 211, 211)
                });
            }
        }


    }
}
