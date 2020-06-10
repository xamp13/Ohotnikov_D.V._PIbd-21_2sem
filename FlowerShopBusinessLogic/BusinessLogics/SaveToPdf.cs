﻿using FlowerShopBusinessLogic.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.BusinessLogics
{
    class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "6cm", "6cm", "6cm" };

            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            if (info.BouquetFlowers != null)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> { "Цветок", "Букет", "Количество" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                foreach (var bf in info.BouquetFlowers)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = table,
                        Texts = new List<string>
                    {
                        bf.FlowerName,
                        bf.BouquetName,
                        bf.Count.ToString()
                    },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
            }
            else if (info.StorageFlowers != null)
            {
                int sum = 0;

                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> { "Цветок", "Склад", "Количество" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });

                foreach (var wc in info.StorageFlowers)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = table,
                        Texts = new List<string>
                    {
                        wc.FlowerName,
                        wc.StorageName,
                        wc.Count.ToString()
                    },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });

                    sum += wc.Count;
                }

                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string>
                    {
                        "Всего",
                        "",
                        sum.ToString()
                    },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}